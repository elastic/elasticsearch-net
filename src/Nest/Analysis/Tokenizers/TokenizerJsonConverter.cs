using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class TokenizerJsonConverter : JsonConverter
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
			switch(typePropertyValue.ToLowerInvariant())
			{
				case "edgengram":
				case "edge_ngram": return o.ToObject<EdgeNGramTokenizer>(ElasticContractResolver.Empty);
				case "ngram": return o.ToObject<NGramTokenizer>(ElasticContractResolver.Empty);
				case "path_hierarchy": return o.ToObject<PathHierarchyTokenizer>(ElasticContractResolver.Empty);
				case "pattern": return o.ToObject<PatternTokenizer>(ElasticContractResolver.Empty);
				case "standard": return o.ToObject<StandardTokenizer>(ElasticContractResolver.Empty);
				case "uax_url_email": return o.ToObject<UaxEmailUrlTokenizer>(ElasticContractResolver.Empty);
				case "whitespace": return o.ToObject<WhitespaceTokenizer>(ElasticContractResolver.Empty);
			}
			return null;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
