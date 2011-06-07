using System;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using ElasticSearch.Client.DSL;

namespace ElasticSearch.Client
{
	public class DynamicContractResolver : DefaultContractResolver
	{
	    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
	    {
	        var contract = new JsonObjectContract(type);
	        return base.CreateProperties(contract.CreatedType, contract.MemberSerialization);
	    }
	}

	public class QueryJsonConverter : JsonConverter
	{
/*
		private readonly Type[] parameterTypes;
*/
/*
		private readonly Dictionary<string, object> parameterInstances;
*/

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
				if (fq is IValue)
				{
					IValue v = fq as IValue;
					writer.WritePropertyName("value");
					writer.WriteValue(v.Value);
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
