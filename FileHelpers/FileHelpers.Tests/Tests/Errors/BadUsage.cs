using System;
using System.IO;
using System.Reflection;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.Errors
{
	[TestFixture]
	public class BadUsage
	{
		#region  "  DuplicateAttributes  "

		[FixedLengthRecord]
		public class TestDupli
		{
			[FieldFixedLength(10)]
			[FieldDelimiter("|")] 
            public string Field1;
		}

		[Test]
		public void DuplicatedDefinition()
		{
            Assert.Throws<BadUsageException>(() 
                => new FileHelperEngine(typeof (TestDupli)));
		}

		#endregion

		#region  "  SwitchedAttributes "

		[FixedLengthRecord]
		public class SwitchedAttributes1
		{
			[FieldDelimiter("|")] 
            public string Field1;
		}

		[DelimitedRecord("|")]
		public class SwitchedAttributes2
		{
			[FieldFixedLength(12)] 
            public string Field1;
		}

		[Test]
		public void SwitchedAttb1()
		{
            Assert.Throws<BadUsageException>(() 
                => new FileHelperEngine(typeof (SwitchedAttributes1)));
		}

		[Test]
		public void SwitchedAttb2()
		{
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine(typeof(SwitchedAttributes2)));
		}

		#endregion

		#region  "  NullValue "

		[DelimitedRecord("|")]
		public class NullValue1Type
		{
			[FieldNullValue(22)] public string Field1;
		}

		[Test]
		public void NullValue1()
		{
            Assert.Throws<BadUsageException>(() 
                => new FileHelperEngine(typeof (NullValue1Type)));
		}

		#endregion

		#region  "  NoMarkedClass "

		public class NoMarkedClass
		{
			[FieldNullValue(22)] public string Field1;
		}

		[Test]
		public void NoMarked()
		{
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine(typeof(NoMarkedClass)));
		}

		#endregion

		#region  "  FixedWithOutLength "

		[FixedLengthRecord]
		public class FixedWithOutLengthClass
		{
			public string Field1;
		}

		[Test]
		public void FixedWithOutLength()
		{
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine(typeof(FixedWithOutLengthClass)));
		}

		#endregion

		#region  "  NoFields "

		[FixedLengthRecord]
		public class NoFieldsClass
		{
		}

		[Test]
		public void NoFields()
		{
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine(typeof(NoFieldsClass)));
		}

		#endregion

		#region  "  NoFields2"

		[DelimitedRecord(",")]
		public class NoFieldsClass2
		{
			[FieldIgnored]
			public string MyField;
		}

		[Test]
		public void NoFields2()
		{
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine(typeof(NoFieldsClass2)));
		}

		#endregion

		#region  "  NoFields3  "

		[DelimitedRecord(",")]
			public class NoFieldsClass3
		{
			[FieldIgnored]
			public string MyField;

			[FieldIgnored]
			public string MyField2;
		}

		[Test]
		public void NoFields3()
		{
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine(typeof(NoFieldsClass3)));
		}

		#endregion


		#region  "  NoConstructor  "

		[FixedLengthRecord]
		public class NoConstructorClass
		{
			[FieldFixedLength(22)] public string Field1;

			public NoConstructorClass(bool foo)
			{
				foo = true;
			}
		}

		[Test]
		public void NoConstructor()
		{
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine(typeof(NoConstructorClass)));
		}

		#endregion


		#region  "  NoConstructorConverter  "

		private class ConvClass: ConverterBase
		{
			public ConvClass(bool foo)
			{}

			public override object StringToField(string from)
			{
				throw new NotImplementedException();
			}
		}


		[DelimitedRecord(",")]
		public class NoConstructorConvClass
		{
			[FieldConverter(typeof(ConvClass))]
			public string Field1;
		}

		[Test]
		public void NoConstructorConverter()
		{
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine(typeof(NoConstructorConvClass)));
		}


		[DelimitedRecord(",")]
		public class NoConstructorConvClass2
		{
			[FieldConverter(typeof(ConvClass), "hola")]
			public string Field1;
		}

		[Test]
		public void NoConstructorConverter2()
		{
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine(typeof(NoConstructorConvClass2)));
		}


		[DelimitedRecord(",")]
		public class NoConstructorConvClass3
		{
			[FieldConverter(typeof(ConvClass), 123)]
			public string Field1;
		}

		[Test]
		public void NoConstructorConverter3()
		{
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine(typeof(NoConstructorConvClass3)));
		}

		[DelimitedRecord(",")]
		public class NoConstructorConvClass4
		{
			[FieldConverter(typeof(FakeConverter), 123)]
			public string Field1;

			private class FakeConverter
			{}

		}

		[Test]
		public void NoConstructorConverter4()
		{
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine(typeof(NoConstructorConvClass4)));
		}

		#endregion

		#region  "  DateFormat  "

		[DelimitedRecord("|")]
		public class DateFormat1Class
		{
			[FieldConverter(ConverterKind.Date, null)] public DateTime DateField;
		}

		[DelimitedRecord("|")]
		public class DateFormat2Class
		{
			[FieldConverter(ConverterKind.Date, "")] public DateTime DateField;
		}

		[DelimitedRecord("|")]
		public class DateFormat3Class
		{
			[FieldConverter(ConverterKind.Date, "d��#|||??��3&&...dddMMyyyy")] public DateTime DateField;
		}


		[Test]
		public void BadDateFormat1()
		{
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine(typeof(DateFormat1Class)));
		}

		[Test]
		public void BadDateFormat2()
		{
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine(typeof(DateFormat2Class)));
		}

//		[Test]
//		public void BadDateFormat3()
//		{
        //Assert.Throws<BadUsageException>(() 
        //        => new FileHelperEngine(typeof (DateFormat3Class));
//		}

		#endregion

//		#region  "  TrimBad  "
//
//		[DelimitedRecord("|")]
//		public class TrimClass
//		{
//			[Trim(TrimMode.Both)]
//			public int Field1;
//		}
//
//		[Test]
//		public void TrimOtherThanString()
//		{
//			new FileHelperEngine(typeof (TrimClass));
//		}
//
//		#endregion

		#region  "  AlignBad  "

		[DelimitedRecord("|")]
		public class AlignClass
		{
			[FieldAlign(AlignMode.Left)] public int Field1;
		}

		[Test]
		public void AlignError()
		{
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine(typeof(AlignClass)));
		}

		#endregion

		#region  "  NonSystemType  "

		[DelimitedRecord("|")]
		public class NonSystemTypeClass
		{
			// One non system type ex FileInfo
			public FileInfo Field1;
		}

		[Test]
		public void NonSystemType()
		{
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine(typeof(NonSystemTypeClass)));
		}

		#endregion

		#region  "  ValueType  "

		public struct ValueTypeClass
		{
			// One non system type ex FileInfo
			public FileInfo Field1;
		}

		[Test]
		public void ValueType()
		{
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine(typeof(ValueTypeClass)));
		}

		#endregion


		#region  "  BadRecordTypes  "

		[DelimitedRecord("|")]
		public class BadRecordTypeClass
		{
			public string Field1;
		}

		[Test]
		public void BadRecordType()
		{
            FileHelperEngine engine = new FileHelperEngine(typeof(BadRecordTypeClass));
			Assert.Throws<BadUsageException>(() 
                => engine.WriteString(new [] {"hola"}));
            
		}

		[Test]
		public void NullRecordType()
		{
            Assert.Throws<BadUsageException>(()
                => new FileHelperEngine((Type)null));
		}

		#endregion

		[Test]
		public void WriteBadUsage()
		{
			FileHelperEngine engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res = new SampleType[2];

			res[0] = new SampleType();

			res[0].Field1 = DateTime.Now.AddDays(1);
			res[0].Field2 = "je";
			res[0].Field3 = 0;

            Assert.Throws<BadUsageException>(() 
                => engine.WriteString(res));
		}

		[Test]
		public void WriteBadUsage2()
		{
			FileHelperEngine engine = new FileHelperEngine(typeof (SampleType));
            Assert.Throws<ArgumentNullException>(() 
                => engine.WriteString(null));
		}


	}
}