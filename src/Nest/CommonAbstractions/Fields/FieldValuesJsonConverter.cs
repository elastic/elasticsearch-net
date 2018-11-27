using System;
using System.Linq;
using System.Runtime.Serialization;
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
			var fields = o.Properties().ToDictionary(p => p.Name, p => (LazyDocument)null); //new LazyDocument(p.Value, s));
			var inferrer = serializer.GetConnectionSettings().Inferrer;
			var fieldValues = new FieldValues(inferrer, fields);
			return fieldValues;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotSupportedException();
	}
}
