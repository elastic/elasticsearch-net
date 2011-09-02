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
			var propertyName = this.GetFacetPropertyName(type);


			facetMetaData.Field = name;
			facetMetaData.Type = type;
			facetMetaData.Missing = data.Value<int>("missing");
			if (new[] { "statistical" }.Contains(type))
			{
				var f = this.ParseFacet(data, type, reader, serializer);
				if (f != null)
					facetMetaData.Facets.Add(f);
			}
			else 
			{
				var facetsArray = (JArray)(data.Property(propertyName).Value);
				foreach (var facet in facetsArray)
				{
					var f = this.ParseFacet(facet, type, reader, serializer);
					if (f != null)
						facetMetaData.Facets.Add(f);
				}
			}
			return facetMetaData;
			
		}

		private string GetFacetPropertyName(string type)
		{
			switch (type)
			{
				case "terms_stats":
					return "terms";
				case "range":
					return "ranges";
				case "date_histogram":
				case "histogram":
					return "entries";
			}
			return type;
		}

		private Facet ParseFacet(JToken facet, string type, JsonReader reader, JsonSerializer serializer)
		{
			switch (type)
			{
				case "terms_stats":
					var termStatistics = serializer.Deserialize<TermStatsFacet>(facet.CreateReader());
					termStatistics.Key = termStatistics.Term;
					return termStatistics;


				case "statistical":
					var statisticalFacet = serializer.Deserialize<StatisticalFacet>(facet.CreateReader());
					statisticalFacet.Key = "__SingleFacet__";
					return statisticalFacet;

				case "terms":
					var term = serializer.Deserialize<TermFacet>(facet.CreateReader());
					term.Key = term.Term;
					return term;
				case "date_histogram":
					var dateHistogram = serializer.Deserialize<DateHistogramFacet>(facet.CreateReader());
					return dateHistogram;
				case "histogram":
					var histogram = serializer.Deserialize<HistogramFacet>(facet.CreateReader());
					return histogram;
				case "range":
					if (((JObject)facet).Property("from_str") != null ||
						((JObject)facet).Property("to_str") != null
						)
					{
						var dateRange = serializer.Deserialize<DateRangeFacet>(facet.CreateReader());
						dateRange.Key = string.Format("From {0} to {1}", dateRange.From, dateRange.To);
						return dateRange;
					}
					var range = serializer.Deserialize<RangeFacet>(facet.CreateReader());
					range.Key = string.Format("From {0} to {1}", range.From, range.To);
					return range;
			}
			return null;
		}
	}

	public class DateHistogramConverter : JsonConverter
	{

		public override bool CanConvert(Type objectType)
		{
			return typeof(DateHistogramFacet).IsAssignableFrom(objectType);
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JObject jObject = JObject.Load(reader);

			var count = jObject.Property("count").Value.Value<int>();
			var ticks = jObject.Property("time").Value.Value<double>();
			var d1 = ticks.JavaTimeStampToDateTime();
			return new DateHistogramFacet(){ Count = count, Time = d1, Key = d1.ToString()};
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
