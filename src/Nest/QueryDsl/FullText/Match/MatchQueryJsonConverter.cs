using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class MatchQueryJsonConverter: JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;

		public override bool CanRead => true;

		public override bool CanWrite => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var j = JObject.Load(reader);
			if (!j.HasValues) return null;

			var firstProp = j.Properties().FirstOrDefault();
			if (firstProp == null) return null;

			var field = firstProp.Name;
			var jo = firstProp.Value.Value<JObject>();
			if (jo == null) return null;

			JToken v;
			string type = null;
			if (jo.TryGetValue("type", out v)) type = v.Value<string>();

			IMatchQuery fq;
			if (type.IsNullOrEmpty()) fq = new MatchQueryDescriptor<object>();
			else if (type == "phrase") fq = new MatchPhraseQueryDescriptor<object>();
			else if (type == "phrase_prefix") fq = new MatchPhrasePrefixQueryDescriptor<object>();
			else return null;

			fq.Field = field;
			fq.Boost = GetPropValue<double?>(jo, "boost");
			fq.Analyzer = GetPropValue<string>(jo, "analyzer");
			fq.CutoffFrequency = GetPropValue<double?>(jo, "cutoff_frequency");
			fq.Fuzziness = GetFuzziness(jo);
			fq.FuzzyTranspositions = GetPropValue<bool?>(jo, "fuzzy_transpositions");
			fq.Lenient = GetPropValue<bool?>(jo, "lenient");
			fq.MaxExpansions = GetPropValue<int?>(jo, "max_expansions");
			fq.PrefixLength = GetPropValue<int?>(jo, "prefix_length");
			fq.Query = GetPropValue<string>(jo, "query");
			fq.Slop = GetPropValue<int?>(jo, "slop");
			
			var rewriteString = GetPropValue<string>(jo, "rewrite");
			if (!rewriteString.IsNullOrEmpty())
				fq.Rewrite = rewriteString.ToEnum<RewriteMultiTerm>();
			
			var operatorString = GetPropValue<string>(jo, "operator");
			if (!rewriteString.IsNullOrEmpty())
				fq.Operator = operatorString.ToEnum<Operator>();

			return fq;
		}

		private static IFuzziness GetFuzziness(IDictionary<string, JToken> jObject)
		{
			JToken jToken;
			if (!jObject.TryGetValue("fuzziness", out jToken)) return null;

			var tmp = jToken.ToObject<IFuzziness>();
			return tmp;
		}

		public TReturn GetPropValue<TReturn>(JObject jObject, string FieldName)
		{
			JToken jToken = null;
			return !jObject.TryGetValue(FieldName, out jToken) 
				? default(TReturn) 
				: jToken.Value<TReturn>();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = value as IFieldNameQuery;
			if (v == null)
				return;

			var fieldName = v.Field;
			if (fieldName == null)
				return;

			var contract = serializer.ContractResolver as SettingsContractResolver;
			if (contract == null)
				return;

			var field = contract.Infer.FieldName(fieldName);
			if (field.IsNullOrEmpty())
				return;
			
			writer.WriteStartObject();
			{
				writer.WritePropertyName(field);
				serializer.Serialize(writer, value);
			}
			writer.WriteEndObject();
		}
	}
}