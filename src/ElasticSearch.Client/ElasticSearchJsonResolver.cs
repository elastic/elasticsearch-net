using System;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using ElasticSearch.Client.DSL;
using Newtonsoft.Json.Linq;
using System.Linq;

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

	public class FacetsMetaDataConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return typeof(Dictionary<string, List<FacetMetaData>>).IsAssignableFrom(objectType);
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JObject jObject = JObject.Load(reader);
			var target = new Dictionary<string,List<FacetMetaData>>();
			var allMetaData = this.ParseAllMetaData(jObject, reader, serializer);
			
			var groupedMetaData =
				from a in allMetaData
				group a by a.Field into b
				select new { Key = b.Key, Value = b.ToList()};
			foreach (var g in groupedMetaData)
				target.Add(g.Key, g.Value);
				
			return target;
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			
		}

		private IEnumerable<FacetMetaData> ParseAllMetaData(JObject jObject, JsonReader reader, JsonSerializer serializer)
		{
			var list = new List<FacetMetaData>();
			// Load JObject from stream
			
			foreach (var facetMetaDataObject in jObject.Properties())
			{
				var facetMetaData = this.ParseMetaData(facetMetaDataObject, reader, serializer);
				list.Add(facetMetaData);
			}
			return list;
		}
		private FacetMetaData ParseMetaData(JProperty facetMetaDataObject, JsonReader reader, JsonSerializer serializer)
		{
			var facetMetaData = new FacetMetaData() { Facets = new List<Facet>() };
			var name = facetMetaDataObject.Name;
			var data = (JObject)facetMetaDataObject.Value;
			var type = data.Value<string>("_type");

			facetMetaData.Field = name;
			facetMetaData.Type = type;
			facetMetaData.Missing = data.Value<int>("missing");
			var facetsArray = (JArray)(data.Property(type).Value);
			foreach (var facet in facetsArray)
			{
				var f = this.ParseFacet(facet, type, reader, serializer);
				if (f != null)
					facetMetaData.Facets.Add(f);
			}
			return facetMetaData;
		}

		private Facet ParseFacet(JToken facet, string type, JsonReader reader, JsonSerializer serializer)
		{
			switch (type)
			{
				case "terms":
					var term = serializer.Deserialize<TermFacet>(facet.CreateReader());
					term.Key = term.Term;
					return term;
			}
			return null;
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
