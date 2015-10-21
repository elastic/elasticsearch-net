using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	internal class DictionaryToPropertiesJsonConverter<TValue> : JsonConverter
	{
		public override bool CanConvert(Type objectType) => objectType == typeof(IDictionary<string, TValue>);

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var dict = new Dictionary<string, TValue>();
			// TODO
			return dict;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var dict = value as IDictionary<string, TValue>;
			foreach (var kv in dict)
			{
				writer.WritePropertyName(kv.Key);
				writer.WriteValue(kv.Value);
			}
		}
	}
}
