using System;
using System.Collections;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using Elasticsearch.Net;
using Nest.DSL.Query.Behaviour;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public class SpanTermQueryConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return true;
		}

		public override bool CanRead { get { return true; } }

		public override bool CanWrite { get { return true; } }

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var j = JObject.Load(reader);
			if (!j.HasValues) return null;

			var firstProp = j.Properties().FirstOrDefault();
			if (firstProp == null) return null;

			var field = firstProp.Name;
			var jo = firstProp.Value.Value<JObject>();
			if (jo == null) return null;

			ISpanTermQuery fq = new SpanTermQueryDescriptor<object>();
			fq.Field = field;
			fq.Boost = GetPropValue<double?>(jo, "boost");
			fq.Value = GetPropValue<string>(jo, "value");

			return fq;
		}

		public TReturn GetPropValue<TReturn>(JObject jObject, string propertyName)
		{
			JToken jToken = null;
			return !jObject.TryGetValue(propertyName, out jToken) 
				? default(TReturn) 
				: jToken.Value<TReturn>();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = value as IFieldNameQuery;
			if (v == null) return;

			var fieldName = v.GetFieldName();
			if (fieldName == null) return;

			var sq = value as ISpanTermQuery;
			if (sq == null) return;

			var contract = serializer.ContractResolver as ElasticContractResolver;
			if (contract == null)
				return;

			var field = contract.Infer.PropertyPath(fieldName);
			if (field.IsNullOrEmpty())
				return;
			
			writer.WriteStartObject();
			{
				writer.WritePropertyName(field);
				writer.WriteStartObject();
				if (sq.Value != null)
				{
					writer.WritePropertyName("value");
					writer.WriteValue(sq.Value);
				}
				if (sq.Boost.HasValue)
				{
					writer.WritePropertyName("boost");
					writer.WriteValue(sq.Boost.Value);
				}
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
		}
	}
}