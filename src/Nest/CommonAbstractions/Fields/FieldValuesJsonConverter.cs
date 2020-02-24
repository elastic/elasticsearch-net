using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class FieldValuesJsonConverter : JsonConverter
	{
		public override bool CanWrite => false;

		public override bool CanConvert(Type objectType) => throw new NotSupportedException();

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var o = JObject.Load(reader);
			var s = serializer.GetConnectionSettings().SourceSerializer;
			var r = serializer.GetConnectionSettings().RequestResponseSerializer;
			var fields = o.Properties().ToDictionary(p => p.Name, p => new LazyDocument(p.Value, s, r));
			var inferrer = serializer.GetConnectionSettings().Inferrer;
			var fieldValues = new FieldValues(inferrer, fields);
			return fieldValues;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotSupportedException();
	}
}
