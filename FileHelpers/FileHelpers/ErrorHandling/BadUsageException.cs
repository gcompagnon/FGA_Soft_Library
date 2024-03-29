#region "  � Copyright 2005-07 to Marcos Meli - http://www.devoo.net"

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;

namespace FileHelpers
{
	/// <summary>Indicates the wrong usage of the library.</summary>
    [Serializable]
    public class BadUsageException : FileHelpersException
	{
		/// <summary>Creates an instance of an BadUsageException.</summary>
		/// <param name="message">The exception Message</param>
		protected internal BadUsageException(string message) : base(message)
		{
		}

		/// <summary>Creates an instance of an BadUsageException.</summary>
		/// <param name="message">The exception Message</param>
		/// <param name="line">The line number where the problem was found</param>
		/// <param name="column">The column number where the problem was found</param>
		protected internal BadUsageException(int line, int column, string message) 
			: base(line, column, message)
		{
		}

		internal BadUsageException(LineInfo line, string message) 
			: this(line.mReader.LineNumber, line.mCurrentPos, message)
		{
		}

//		/// <summary>Creates an instance of an BadUsageException.</summary>
//		/// <param name="message">The exception Message</param>
//		/// <param name="innerEx">The inner exception.</param>
//		protected internal BadUsageException(string message, Exception innerEx) : base(message, innerEx)
//		{
//		}

	}
}