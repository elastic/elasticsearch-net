using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class AnalyzerJsonConverter : JsonConverter
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
				case "stop": return o.ToObject<StopAnalyzer>(ElasticContractResolver.Empty);
				case "standard": return o.ToObject<StandardAnalyzer>(ElasticContractResolver.Empty);
				case "snowball": return o.ToObject<SnowballAnalyzer>(ElasticContractResolver.Empty);
				case "pattern": return o.ToObject<PatternAnalyzer>(ElasticContractResolver.Empty);
				case "keyword": return o.ToObject<KeywordAnalyzer>(ElasticContractResolver.Empty);
				case "whitespace": return o.ToObject<WhitespaceAnalyzer>(ElasticContractResolver.Empty);
				case "simple": return o.ToObject<SimpleAnalyzer>(ElasticContractResolver.Empty);
				default:
					if (o.Property("tokenizer") != null) 
						return o.ToObject<CustomAnalyzer>(ElasticContractResolver.Empty);
					return o.ToObject<LanguageAnalyzer>(ElasticContractResolver.Empty);
			}
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
