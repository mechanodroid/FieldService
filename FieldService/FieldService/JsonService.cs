using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace FieldService {

	public class JsonService {

		private string _storagePath;
		private List<ItemRecord> _ItemRecords =  new List<ItemRecord>();

		public JsonService(string storagePath) {
			_storagePath = storagePath;

			if (!Directory.Exists(_storagePath))
				Directory.CreateDirectory(_storagePath);

			RefreshCache();
		}

		#region JsonService implementation
		public void SaveItemRecords(List<ItemRecord> item_records)	{
			string RECORDS_TO_BE_STORED = JsonConvert.SerializeObject(item_records);
			File.WriteAllText(Path.Combine(_storagePath, "FIELD_SERVICE_SAVED_RECORDS.json"), RECORDS_TO_BE_STORED);
		}

		public void RefreshCache()	{
			string[] filenames = Directory.GetFiles(_storagePath, "FIELD_SERVICE_SAVED_RECORDS.json");
			//Console.WriteLine("Data Saved.");

			if (filenames.Length > 0) {
				string RECORDS = File.ReadAllText(Path.Combine(_storagePath, "FIELD_SERVICE_SAVED_RECORDS.json"));
				_ItemRecords = JsonConvert.DeserializeObject<List<ItemRecord>>(RECORDS);
			}
		}

		public List<ItemRecord> GetItemRecords	{
			get { return _ItemRecords; }
		}
		#endregion
	}
}

