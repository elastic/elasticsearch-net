using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using ElasticSearch.Client.DSL;
using Newtonsoft.Json.Linq;

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

	public class FacetsMetaDataConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return typeof(FacetsMetaData).IsAssignableFrom(objectType);
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var target = new FacetsMetaData() { };

			// Load JObject from stream
            JObject jObject = JObject.Load(reader);
			foreach (var facet in jObject.Properties())
			{
				//var facet = serializer.Deserialize<Facet>(facetName.CreateReader());
			}

			serializer.Populate(jObject.CreateReader(), target);

			return target;
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			
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
