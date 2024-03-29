using System;
using System.Data;
using System.IO;
using FileHelpers;
using FileHelpers.RunTime;
using NUnit.Framework;
#if NET_2_0
using System.Collections.Generic;
#endif

namespace FileHelpersTests
{
	[TestFixture]
	public class RunTimeClassesExtra
	{
		FileHelperEngine engine;


		[Test]
		public void ReadAsDataTable1()
		{

			DelimitedClassBuilder cb = new DelimitedClassBuilder("ImportContact", ","); 
			cb.IgnoreEmptyLines = true; 
			cb.GenerateProperties = true; 
			
			cb.AddField("FirstName", typeof(string)); 
			cb.LastField.TrimMode = FileHelpers.TrimMode.Both; 
			cb.LastField.FieldQuoted = false; 

			cb.AddField("LastName", typeof(string)); 
			cb.LastField.TrimMode = TrimMode.Both; 
			cb.LastField.FieldQuoted = false; 
			
			cb.AddField("StreetNumber", typeof(string)); 
			cb.LastField.TrimMode = TrimMode.Both; 
			cb.LastField.FieldQuoted = false; 
			
			cb.AddField("StreetAddress", typeof(string)); 
			cb.LastField.TrimMode = TrimMode.Both; 
			cb.LastField.FieldQuoted = false; 
			
			cb.AddField("Unit", typeof(string)); 
			cb.LastField.TrimMode = TrimMode.Both; 
			cb.LastField.FieldQuoted = false; 
			
			cb.AddField("City", typeof(string)); 
			cb.LastField.TrimMode = TrimMode.Both; 
			cb.LastField.FieldQuoted = false; 
			
			cb.AddField("State", typeof(string)); 
			cb.LastField.TrimMode = TrimMode.Both; 
			cb.LastField.FieldQuoted = false; 
			
			cb.AddField("Zip", typeof(string)); 
			cb.LastField.TrimMode = TrimMode.Both; 
			cb.LastField.FieldQuoted = false; 

			engine = new FileHelperEngine(cb.CreateRecordClass()); 

			string source = "Alex & Jen,Bouquet,1815,Bell Rd,, Batavia,OH,45103" + Environment.NewLine + 
							"Mark & Lisa K ,Arlinghaus,1817,Bell Rd,, Batavia,OH,45103" + Environment.NewLine +
							"Ed & Karen S ,Craycraft,1819,Bell Rd,, Batavia,OH,45103" + Environment.NewLine;

			DataTable contactData = engine.ReadStringAsDT(source);

			Assert.AreEqual(3, contactData.Rows.Count);
			Assert.AreEqual(8, contactData.Columns.Count);
			
			Assert.AreEqual("Alex & Jen", contactData.Rows[0][0].ToString());
			Assert.AreEqual("Mark & Lisa K", contactData.Rows[1][0].ToString());

			// new DelimitedClassBuilder("", ",");
		}



		[Test]
		public void ReadAsDataTable2()
		{

			DelimitedClassBuilder cb = new DelimitedClassBuilder("ImportContact", ","); 
			cb.IgnoreEmptyLines = true; 
			cb.GenerateProperties = true; 
			
			cb.AddField("FirstName", typeof(string)); 
			cb.LastField.TrimMode = FileHelpers.TrimMode.Both; 
			cb.LastField.FieldQuoted = false; 

			cb.AddField("LastName", typeof(string)); 
			cb.LastField.TrimMode = TrimMode.Both; 
			cb.LastField.FieldQuoted = false; 
			
			cb.AddField("StreetNumber", typeof(string)); 
			cb.LastField.TrimMode = TrimMode.Both; 
			cb.LastField.FieldQuoted = false; 
			
			cb.AddField("StreetAddress", typeof(string)); 
			cb.LastField.TrimMode = TrimMode.Both; 
			cb.LastField.FieldQuoted = false; 
			
			cb.AddField("Unit", typeof(string)); 
			cb.LastField.TrimMode = TrimMode.Both; 
			cb.LastField.FieldQuoted = false; 
			
			cb.AddField("City", typeof(string)); 
			cb.LastField.TrimMode = TrimMode.Both; 
			cb.LastField.FieldQuoted = false; 
			
			cb.AddField("State", typeof(string)); 
			cb.LastField.TrimMode = TrimMode.Both; 
			cb.LastField.FieldQuoted = false; 
			
			cb.AddField("Zip", typeof(string)); 
			cb.LastField.TrimMode = TrimMode.Both; 
			cb.LastField.FieldQuoted = false; 

			engine = new FileHelperEngine(cb.CreateRecordClass()); 

			DataTable contactData = engine.ReadFileAsDT(Common.TestPath(@"Good\ReadAsDataTable.txt"));

			Assert.AreEqual(3, contactData.Rows.Count);
			Assert.AreEqual(8, contactData.Columns.Count);
			
			Assert.AreEqual("Alex & Jen", contactData.Rows[0][0].ToString());
			Assert.AreEqual("Mark & Lisa K", contactData.Rows[1][0].ToString());

			// new DelimitedClassBuilder("", ",");
		}

#if NET_2_0


        [Test]
        public void RunTimeNullableFields()
        {
            DelimitedClassBuilder cb = new DelimitedClassBuilder("ImportContact", ",");

            cb.AddField("Field1", "int?");
            cb.AddField("Field2", typeof(int?));
            cb.AddField("Field3", "Nullable<int>");
            cb.AddField("Field4", typeof (Nullable<int>));
        }



        [Test]
        public void RunTimeGenerics()
        {
            DelimitedClassBuilder cb = new DelimitedClassBuilder("ImportContact", ",");

            cb.AddField("Field2", typeof(Dictionary<int, List<string>>));
            cb.AddField("Field1", "List<int>");
            cb.AddField("Field2", typeof(List<int>));
            cb.AddField("Field3", "Nullable<int>");
            cb.AddField("Field4", typeof(Nullable<int>));
        }

#endif
	}
}