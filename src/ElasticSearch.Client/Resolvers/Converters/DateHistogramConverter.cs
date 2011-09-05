using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq.Expressions;
using System.Reflection;
using Fasterflect;
using ElasticSearch.Client.Mapping;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;

namespace ElasticSearch.Client.DSL
{
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
			return new DateHistogramFacet() { Count = count, Time = d1, Key = d1.ToString() };
		}

	}
}
