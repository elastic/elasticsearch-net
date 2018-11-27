using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class SimilarityJsonConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => false;

		public override bool CanConvert(Type objectType) =>
			typeof(ISimilarity).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var o = JObject.Load(reader);
			var typeProperty = o.Property("type");

			if (typeProperty == null) return null;

			var typePropertyValue = typeProperty.Value.ToString().ToUpperInvariant();

			switch (typePropertyValue)
			{
				case "BM25":
					return o.ToObject<BM25Similarity>(ElasticContractResolver.Empty);
				case "LMDIRICHLET":
					return o.ToObject<LMDirichletSimilarity>(ElasticContractResolver.Empty);
				case "DFR":
					return o.ToObject<DFRSimilarity>(ElasticContractResolver.Empty);
				case "DFI":
					return o.ToObject<DFISimilarity>(ElasticContractResolver.Empty);
				case "IB":
					return o.ToObject<IBSimilarity>(ElasticContractResolver.Empty);
				case "LMJELINEKMERCER":
					return o.ToObject<LMJelinekMercerSimilarity>(ElasticContractResolver.Empty);
				case "SCRIPTED":
					return o.ToObject<ScriptedSimilarity>(ElasticContractResolver.Empty);
				default:
					var dict = o.ToObject<Dictionary<string, object>>();
					return new CustomSimilarity(dict);
			}
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
			throw new NotSupportedException();
	}
}
