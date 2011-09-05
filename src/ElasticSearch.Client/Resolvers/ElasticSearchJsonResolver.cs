using System;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using ElasticSearch.Client.DSL;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Reflection;
using ElasticSearch.Client.Mapping;

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

	public class ElasticResolver : CamelCasePropertyNamesContractResolver
	{
		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			var property = base.CreateProperty(member, memberSerialization);
			var attributes = member.GetCustomAttributes(typeof(ElasticPropertyAttribute), false);
			if (attributes == null || !attributes.Any())
				return property;

			var att = attributes.First() as ElasticPropertyAttribute;
			if (!att.Name.IsNullOrEmpty())
				property.PropertyName = att.Name;
			return property;
		}
		new public string ResolvePropertyName(string propertyName)
		{
			return base.ResolvePropertyName(propertyName);
		}
	}


	


	public class QueryJsonConverter : JsonConverter
	{

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
