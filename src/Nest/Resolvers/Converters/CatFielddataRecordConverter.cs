using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.Resolvers.Converters
{

	public class CatFielddataRecordConverter : JsonConverter
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
				var prop = reader.Value as JProperty;
				switch (prop.Name)
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
						o.FieldSizes[prop.Name] = reader.ReadAsString();
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