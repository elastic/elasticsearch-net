using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	internal class CatFielddataRecordJsonConverter : JsonConverter
	{
		public override bool CanWrite
		{
			get { return false; }
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}


		public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
										JsonSerializer serializer)
		{
			var o = new CatFielddataRecord() { FieldSizes = new Dictionary<string, string>() };
			while (reader.Read())
			{
				var prop = reader.Value as string;
				if (prop == null) return o;

				switch (prop)
				{
					case "id":
						o.Id = reader.ReadAsString();
						continue;
					case "node":
					case "n":
						o.Node = reader.ReadAsString();
						continue;
					case "host":
						o.Host = reader.ReadAsString();
						continue;
					case "ip":
						o.Ip = reader.ReadAsString();
						continue;
					case "total":
						o.Total = reader.ReadAsString();
						continue;
					default:
						var value = reader.ReadAsString();
						o.FieldSizes[prop] = value;
						continue;
				}
			}
			return o;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(CatFielddataRecord);
		}
	}
}