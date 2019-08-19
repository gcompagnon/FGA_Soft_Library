//----------------------------------------------------------------------------
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
//WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A  PARTICULAR PURPOSE.
//----------------------------------------------------------------------------

using Event = Bloomberglp.Blpapi.Event;
using Element = Bloomberglp.Blpapi.Element;
using InvalidRequestException = Bloomberglp.Blpapi.InvalidRequestException;
using Message = Bloomberglp.Blpapi.Message;
using Name = Bloomberglp.Blpapi.Name;
using Request = Bloomberglp.Blpapi.Request;
using Service = Bloomberglp.Blpapi.Service;
using Session = Bloomberglp.Blpapi.Session;

using ArrayList = System.Collections.ArrayList;

namespace Bloomberglp.Blpapi.Examples
{

	public class RefDataExample
	{
		private static readonly Name SECURITY_DATA = new Name("securityData");
		private static readonly Name SECURITY = new Name("security");
		private static readonly Name FIELD_DATA = new Name("fieldData");
		private static readonly Name RESPONSE_ERROR = new Name("responseError");
		private static readonly Name SECURITY_ERROR = new Name("securityError");
		private static readonly Name FIELD_EXCEPTIONS = new Name("fieldExceptions");
		private static readonly Name FIELD_ID = new Name("fieldId");
		private static readonly Name ERROR_INFO = new Name("errorInfo");
		private static readonly Name CATEGORY = new Name("category");
		private static readonly Name MESSAGE = new Name("message");

		private string     d_host;
		private int        d_port;
		private ArrayList  d_securities;
		private ArrayList  d_fields;

		public static void Main(string[] args)
		{
			System.Console.WriteLine("Reference Data Example");
			RefDataExample example = new RefDataExample();
			example.run(args);

			System.Console.WriteLine("Press ENTER to quit");
			System.Console.Read();
		}

		public RefDataExample()
		{
			d_host = "localhost";
			d_port = 8194;
			d_securities = new ArrayList();
			d_fields = new ArrayList();
		}

		private void run(string[] args)
		{
			if (!parseCommandLine(args)) return;

			SessionOptions sessionOptions = new SessionOptions();
			sessionOptions.ServerHost = d_host;
			sessionOptions.ServerPort = d_port;

			System.Console.WriteLine("Connecting to " + d_host + ":" + d_port);
			Session session = new Session(sessionOptions);
			bool sessionStarted = session.Start();
			if (!sessionStarted) 
			{
				System.Console.Error.WriteLine("Failed to start session.");
				return;
			}
            if (!session.OpenService("//blp/refdata"))
            {
                System.Console.Error.WriteLine("Failed to open //blp/refdata");
                return;
            }

			try 
			{
				sendRefDataRequest(session);
			} 
			catch (InvalidRequestException e) 
			{
				System.Console.WriteLine(e.ToString());				
			}

			// wait for events from session.
			eventLoop(session);

			session.Stop();
		}

		private void eventLoop(Session session)
		{
			bool done = false;
			while (!done) 
			{
				Event eventObj = session.NextEvent();
				if (eventObj.Type == Event.EventType.PARTIAL_RESPONSE) 
				{
					System.Console.WriteLine("Processing Partial Response");
					processResponseEvent(eventObj);
				}
				else if (eventObj.Type == Event.EventType.RESPONSE) 
				{
					System.Console.WriteLine("Processing Response");
					processResponseEvent(eventObj);
					done = true;
				} 
				else 
				{
					foreach (Message msg in eventObj)
					{						
						System.Console.WriteLine(msg.AsElement);
						if (eventObj.Type == Event.EventType.SESSION_STATUS) 
						{
							if (msg.MessageType.Equals("SessionTerminated")) 
							{                           
								done = true;
							}
						}
					}
				}
			}
		}

		// return true if processing is completed, false otherwise
		private void processResponseEvent(Event eventObj)
		{
            foreach (Message msg in eventObj)
			{				
				if (msg.HasElement(RESPONSE_ERROR)) 
				{
					printErrorInfo("REQUEST FAILED: ", msg.GetElement(RESPONSE_ERROR));
					continue;
				}

				Element securities = msg.GetElement(SECURITY_DATA);
				int numSecurities = securities.NumValues;
				System.Console.WriteLine("Processing " + numSecurities + " securities:");
				for (int i = 0; i < numSecurities; ++i) 
				{
					Element security = securities.GetValueAsElement(i);
					string ticker = security.GetElementAsString(SECURITY);
					System.Console.WriteLine("\nTicker: " + ticker);
					if (security.HasElement("securityError")) 
					{
						printErrorInfo("\tSECURITY FAILED: ",
							security.GetElement(SECURITY_ERROR));
						continue;
					}

					Element fields = security.GetElement(FIELD_DATA);
					if (fields.NumElements > 0) 
					{
						System.Console.WriteLine("FIELD\t\tVALUE");
						System.Console.WriteLine("-----\t\t-----");
						int numElements = fields.NumElements;
						for (int j = 0; j < numElements; ++j) 
						{
							Element field = fields.GetElement(j);
							System.Console.WriteLine(field.Name + "\t\t" +
								field.GetValueAsString());
						}
					}
					System.Console.WriteLine("");
					Element fieldExceptions = security.GetElement(FIELD_EXCEPTIONS);
					if (fieldExceptions.NumValues > 0) 
					{
						System.Console.WriteLine("FIELD\t\tEXCEPTION");
						System.Console.WriteLine("-----\t\t---------");
						for (int k = 0; k < fieldExceptions.NumValues; ++k) 
						{
							Element fieldException =
								fieldExceptions.GetValueAsElement(k);
							printErrorInfo(fieldException.GetElementAsString(FIELD_ID) +
								"\t\t", fieldException.GetElement(ERROR_INFO));
						}
					}
				}
			}
		}

		private void sendRefDataRequest(Session session)
		{            
            Service refDataService = session.GetService("//blp/refdata");
			Request request = refDataService.CreateRequest("ReferenceDataRequest");

			// Add securities to request
			Element securities = request.GetElement("securities");

			for (int i = 0; i < d_securities.Count; ++i) 
			{
				securities.AppendValue((string)d_securities[i]);
			}

			// Add fields to request
			Element fields = request.GetElement("fields");
			for (int i = 0; i < d_fields.Count; ++i) 
			{
				fields.AppendValue((string)d_fields[i]);
			}

			System.Console.WriteLine("Sending Request: " + request);
			session.SendRequest(request, null);
		}

		private bool parseCommandLine(string[] args)
		{
			for (int i = 0; i < args.Length; ++i) 
			{
				if (string.Compare(args[i], "-s", true) == 0)
				{
					d_securities.Add(args[i+1]);
				}
				else if (string.Compare(args[i], "-f", true) == 0)
				{            
					d_fields.Add(args[i+1]);
				}
				else if (string.Compare(args[i], "-ip", true) == 0)            
				{
					d_host = args[i+1];
				}
				else if (string.Compare(args[i], "-p", true) == 0)
				{
                    int outPort = 0;
                    if (int.TryParse(args[i + 1], out outPort))
                    {
                        d_port = outPort;
                    }
                }
				else if (string.Compare(args[i], "-h", true) == 0)
				{
					printUsage();
					return false;
				}
			}

			// handle default arguments
			if (d_securities.Count == 0) 
			{
				d_securities.Add("IBM US Equity");
				d_securities.Add("MSFT US Equity");
			}

			if (d_fields.Count == 0) 
			{
				d_fields.Add("PX_LAST");
			}

			return true;
		}

		private void printErrorInfo(string leadingStr, Element errorInfo)
		{
			System.Console.WriteLine(leadingStr + errorInfo.GetElementAsString(CATEGORY) +
				" (" + errorInfo.GetElementAsString(MESSAGE) + ")");
		}

		private void printUsage()
		{
			System.Console.WriteLine("Usage:");
			System.Console.WriteLine("	Retrieve reference data ");
			System.Console.WriteLine("		[-s			<security	= IBM US Equity>");
			System.Console.WriteLine("		[-f			<field		= PX_LAST>");
			System.Console.WriteLine("		[-ip 		<ipAddress	= localhost>");
			System.Console.WriteLine("		[-p 		<tcpPort	= 8194>");
		}
	}
}