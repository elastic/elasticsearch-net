using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class RangeQueryJsonConverter: FieldNameQueryJsonConverter<NumericRangeQuery>
	{
		private static readonly string[] _rangeKeys = new[] { "gt", "gte", "lte", "lt" };
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
			

			var isNumeric = !jo.Properties().Any(p=>p.Name == "format" || p.Name == "time_zone")
				&& jo.Properties().Any(p=> _rangeKeys.Contains(p.Name) && (p.Value.Type  == JTokenType.Integer || p.Value.Type == JTokenType.Float));
						

			IRangeQuery fq;
			if (isNumeric)
			{
				fq = FromJson.ReadAs<NumericRangeQuery>(jo.CreateReader(), objectType, existingValue, serializer);
			}
			else 
			{
				fq = FromJson.ReadAs<DateRangeQuery>(jo.CreateReader(), objectType, existingValue, serializer);
			}
			
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