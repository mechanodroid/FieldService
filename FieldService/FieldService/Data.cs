using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Android.App;

namespace FieldService {		

	public class Data {

		public static readonly JsonService Service = new JsonService(
			Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path, "FieldService")
		);

		private static List<ItemRecord> records = new List<ItemRecord>(Service.GetItemRecords);
		private static int pamphletsTotal = 0, pensTotal = 0, cdsTotal= 0, count = 0;

		public static void AddRecord(int pamphletsAmount, int pensAmount, int cdsAmount, string location)	{
			if (!(pamphletsAmount == 0 && pensAmount == 0 && cdsAmount == 0)) {
				records.Add(new ItemRecord(pamphletsAmount, pensAmount, cdsAmount, location));
				Service.SaveItemRecords(records);
			}
		}
			
		public static void SumOfRecords()	{
			for(int i = count; i < records.Count; i++) {
				pamphletsTotal += records.ElementAt(i).Item(0);
				pensTotal+= records.ElementAt(i).Item(1);
				cdsTotal += records.ElementAt(i).Item(2);
			}		
			count = records.Count;
		}

		public static int PamphletsTotal	{
			get	{ return pamphletsTotal; }
		}

		public static int PensTotal	{
			get	{ return pensTotal; }
		}

		public static int CdsTotal	{
			get	{ return cdsTotal; }
		}


		public static string printItemLocation(int i)	{
			string temp = "";
			foreach (ItemRecord record in records) {
				if (record.Item(i) != 0) {
					temp += "Location: " + record.Location;
					temp += "\nAmount: " + record.Item(i) + "\n\n";
				}
			}
			if (temp == "") {
				temp = "No Data Entered Yet.";
			}
			return temp;
		}
	}

	public class ItemRecord {
	
		private int pamphlets, pens, cds;
		private string location = null;

		public ItemRecord(int pamphlets, int pens, int cds, String location)	{
			this.pamphlets = pamphlets;
			this.pens = pens;
			this.cds = cds;
			this.location = location;
		}

		public string Location	{
			get	{ return location; }
		}

		public int Pamphlets	{
			get	{ return pamphlets; }
		}

		public int Pens	{
			get	{ return pens; }
		}

		public int CDs	{
			get	{ return cds; }
		}

		public int Item(int x)	{
			switch (x) {
				case 0:
					return pamphlets;
				case 1:
					return pens;
				case 2:
					return cds;
				default:
					return 0;
			}
		}
	}
}

