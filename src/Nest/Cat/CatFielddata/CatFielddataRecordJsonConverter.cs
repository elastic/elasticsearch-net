using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	internal class CatFielddataRecordJsonConverter : JsonConverter
	{
		public override bool CanWrite => false;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
										JsonSerializer serializer)
		{
			var record = new CatFielddataRecord();
			while (reader.Read())
			{
				var prop = reader.Value as string;
				if (prop == null) return record;

				switch (prop)
				{
					case "id":
						record.Id = reader.ReadAsString();
						continue;
					case "node":
					case "n":
						record.Node = reader.ReadAsString();
						continue;
					case "host":
						record.Host = reader.ReadAsString();
						continue;
					case "ip":
						record.Ip = reader.ReadAsString();
						continue;
					case "field":
						record.Field = reader.ReadAsString();
						continue;
					case "size":
						record.Size = reader.ReadAsString();
						continue;
				}
			}
			return record;
		}

		public override bool CanConvert(Type objectType) => objectType == typeof(CatFielddataRecord);
	}
}
