using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.Resolvers.Converters.Queries
{
	public class TermQueryJsonReader : JsonConverter
	{
		public override bool CanRead { get { return true; } }
		public override bool CanWrite { get { return false; } }

		public override bool CanConvert(Type objectType)
		{
			return true; //only to be used with attribute or contract registration.
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var j = JObject.Load(reader);
			if (j == null || !j.HasValues) return null;
			ITermQuery query = new TermQueryDescriptor<object>();
			var field = j.Properties().First().Name;
			var o = j[field] as JObject;
			query.Field = field;
			if (o == null) return query;

			foreach (var jv in o)
			{
				switch (jv.Key)
				{
					case "boost":
						query.Boost = jv.Value.Value<double>();
						break;
					case "value":
						query.Value = jv.Value.Value<string>();
						break;
				}
			}

			return query;

		}
	}
	
}
