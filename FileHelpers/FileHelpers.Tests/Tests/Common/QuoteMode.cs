using System;
using System.Data;
using System.IO;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
{
	[TestFixture]
	public class QuoteModeTests
	{
		FileHelperEngine engine;
		private const int ExpectedRecords = 6;
		private readonly string[] ExpectedNames = new string[] {"VINET", "TO,SP", "HA\"AR", "VICTE", "S\"U\"P,\"\"", "HA,,,NAR"};


		private void ValidateData(QuoteMode1[] data)
		{
			Assert.AreEqual(ExpectedRecords, data.Length);
			for(int i = 0; i < data.Length; i++)
				Assert.AreEqual(ExpectedNames[i], data[i].CustomerName);
		}

		private void ValidateData(QuoteMode2[] data)
		{
			Assert.AreEqual(ExpectedRecords, data.Length);
			for(int i = 0; i < data.Length; i++)
				Assert.AreEqual(ExpectedNames[i], data[i].CustomerName);
		}

		[Test]
		public void ReadOptionalRead()
		{
			engine = new FileHelperEngine(typeof (QuoteMode1));
			QuoteMode1[] res = Common.ReadTest(engine, @"Good\QuoteMode1.txt") as QuoteMode1[];
			ValidateData(res);
		}

		[Test]
		public void ReadOptionalWrite()
		{
			engine = new FileHelperEngine(typeof (QuoteMode2));
            Assert.Throws<BadUsageException>(() 
                => Common.ReadTest(engine, @"Good\QuoteMode1.txt"));
		}

		[Test]
		public void WriteOptionalRead()
		{
			engine = new FileHelperEngine(typeof (QuoteMode1));
			QuoteMode1[] res = Common.ReadTest(engine, @"Good\QuoteMode1.txt") as QuoteMode1[];

			engine.WriteFile("quotetemp1.txt",res);

			res = engine.ReadFile("quotetemp1.txt") as QuoteMode1[];
			ValidateData(res);

			if (File.Exists("quotetemp1.txt")) File.Delete("quotetemp1.txt");
		}

		
		[DelimitedRecord(",")]
		private class QuoteMode1
		{
			public string CustomerID;
			[FieldQuoted(QuoteMode.OptionalForRead)]
			public string CustomerName;

		}

		[DelimitedRecord(",")]
			private class QuoteMode2
		{
			public string CustomerID;
			[FieldQuoted(QuoteMode.OptionalForWrite)]
			public string CustomerName;

		}

		[DelimitedRecord(",")]
		private class QuoteMode3
		{
			public string CustomerID;
			[FieldQuoted(QuoteMode.OptionalForBoth)]
			public string CustomerName;

		}


		[DelimitedRecord(",")]
		private class QuoteMode4
		{
			public string CustomerID;
			[FieldQuoted(QuoteMode.AlwaysQuoted)]
			public string CustomerName;

		}

        [Test]
        public void AutoRemoveQuotes()
        {
            CsvEngine eng = new CsvEngine(new CsvOptions("YourClass", ',', 2, 0));
            DataTable dt = eng.ReadFileAsDT(Common.TestPath(@"Good\QuoteMode1.txt"));

            Assert.AreEqual("VINET", dt.Rows[0][1]);

        }

	}
}