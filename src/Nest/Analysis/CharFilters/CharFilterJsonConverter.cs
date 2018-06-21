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
			var o = JObject.Load(reader);

			var typeProperty = o.Property("type");
			if (typeProperty == null) return null;

			var typePropertyValue = typeProperty.Value.ToString();
			switch(typePropertyValue)
			{
				case "html_strip": return o.ToObject<HtmlStripCharFilter>(ElasticContractResolver.Empty);
				case "mapping": return o.ToObject<MappingCharFilter>(ElasticContractResolver.Empty);
				case "pattern_replace": return o.ToObject<PatternReplaceCharFilter>(ElasticContractResolver.Empty);
				case "kuromoji_iteration_mark": return o.ToObject<KuromojiIterationMarkCharFilter>(ElasticContractResolver.Empty);
				case "icu_normalizer": return o.ToObject<IcuNormalizationCharFilter>(ElasticContractResolver.Empty);
			}
			return null;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}
	}
}
