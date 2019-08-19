using System;
using System.Data;
using System.IO;
using FileHelpers;
using FileHelpers.RunTime;
using NUnit.Framework;

namespace FileHelpersTests
{
	[TestFixture]
	public class RunTimeClassesFiles
	{
		//FileHelperEngine engine;

        [Test]
		public void LoadFromXML()
		{
            ClassBuilder cb = ClassBuilder.LoadFromXml(Common.TestPath(@"RunTime\VendorImport.xml"));
            Type t = cb.CreateRecordClass(); // this line generates an error in the FH library 

            using (FileHelperAsyncEngine engine = new FileHelperAsyncEngine(t))
            {
                engine.BeginReadString("");

                while (engine.ReadNext() != null)
                {
                }
            } 
		}

	}
}