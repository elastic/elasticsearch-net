using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class CharFilterJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;
		public override bool CanWrite => false;
		public override bool CanRead => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JObject o = JObject.Load(reader);

			JProperty typeProperty = o.Property("type");
			if (typeProperty == null) return null;

			var typePropertyValue = typeProperty.Value.ToString();
			switch(typePropertyValue)
			{
				case "html_strip": return o.ToObject<HtmlStripCharFilter>(ElasticContractResolver.Empty);
				case "mapping": return o.ToObject<MappingCharFilter>(ElasticContractResolver.Empty);
				case "pattern_replace": return o.ToObject<PatternReplaceCharFilter>(ElasticContractResolver.Empty);
			}
			return null;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
