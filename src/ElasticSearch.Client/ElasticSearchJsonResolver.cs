using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using ElasticSearch.DSL;

namespace ElasticSearch.Client
{
	public class DynamicContractResolver : DefaultContractResolver
	{
		public DynamicContractResolver()
		{
		}

		protected override IList<JsonProperty> CreateProperties(JsonObjectContract contract)
		{
			IList<JsonProperty> properties = base.CreateProperties(contract);

			return properties;
		}
	}

	public class QueryJsonConverter : JsonConverter
	{
		private readonly Type[] parameterTypes;
		private readonly Dictionary<string, object> parameterInstances;

		public override bool CanConvert(Type objectType)
		{
			return typeof(IFieldQuery).IsAssignableFrom(objectType);
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			IFieldQuery fq = value as IFieldQuery;
			if (fq != null)
			{
				writer.WriteStartObject();
				writer.WritePropertyName(fq.Field);
				writer.WriteStartObject();
				if (fq is Term)
				{
					Term t = fq as Term;
					writer.WritePropertyName("value");
					writer.WriteValue(t.Value);
				}
				if (fq.Boost != 1.0)
				{
					writer.WritePropertyName("boost");
					writer.WriteValue(fq.Boost);
				}
				writer.WriteEndObject();
				writer.WriteEndObject();
			}
			else
				writer.WriteNull();
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return null;
		}

	}

}
