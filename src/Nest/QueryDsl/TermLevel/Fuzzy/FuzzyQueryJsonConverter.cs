using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class FuzzyQueryJsonConverter: FieldNameQueryJsonConverter<FuzzyQuery>
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
			if (!jo.TryGetValue("value", out v)) return null;

			IFuzzyQuery fq;
			if (v.Type == JTokenType.String)
			{
				fq = new FuzzyQuery()
				{
					Value = GetPropValue<string>(jo, "value"),
					Fuzziness = GetPropObject<Fuzziness>(jo, "fuzziness")
				};
			}
			else if (v.Type == JTokenType.Date)
			{
				fq = new FuzzyDateQuery()
				{
					Value = GetPropValue<DateTime?>(jo, "value"),
					Fuzziness = GetPropObject<Time>(jo, "fuzziness")
				};
			}
			else if (v.Type == JTokenType.Integer || v.Type == JTokenType.Float)
			{
				fq = new FuzzyNumericQuery()
				{
					Value = GetPropValue<double?>(jo, "value"),
					Fuzziness = GetPropValue<double?>(jo, "fuzziness")
				};
			}
			else return null; 

			fq.PrefixLength = GetPropValue<int?>(jo, "prefix_length"); 
			fq.MaxExpansions = GetPropValue<int?>(jo, "max_expansions");
			fq.Transpositions = GetPropValue<bool?>(jo, "transpositions");
			var rewriteString = GetPropValue<string>(jo, "rewrite");
			if (!rewriteString.IsNullOrEmpty())
				fq.Rewrite = rewriteString.ToEnum<RewriteMultiTerm>();
			
			fq.Name = GetPropValue<string>(jo, "_name");
			fq.Boost = GetPropValue<double?>(jo, "boost");
			fq.Field = field;

			return fq;
		}

		public TReturn GetPropObject<TReturn>(JObject jObject, string field)
		{
			JToken jToken = null;
			return !jObject.TryGetValue(field, out jToken) 
				? default(TReturn) 
				: jToken.ToObject<TReturn>();
		}
		public TReturn GetPropValue<TReturn>(JObject jObject, string field)
		{
			JToken jToken = null;
			return !jObject.TryGetValue(field, out jToken) 
				? default(TReturn) 
				: jToken.Value<TReturn>();
		}
	}
}