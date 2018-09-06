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

			var fq = GetRangeQuery(serializer, jo);

			fq.Name = GetPropValue<string>(jo, "_name");
			fq.Boost = GetPropValue<double?>(jo, "boost");
			fq.Field = field;

			return fq;
		}

		private static IRangeQuery GetRangeQuery(JsonSerializer serializer, JObject jo)
		{
			var isNumeric = false;
			var isLong = false;

			foreach (var property in jo.Properties())
			{
				if (property.Name == "format" || property.Name == "time_zone")
					return FromJson.ReadAs<DateRangeQuery>(jo.CreateReader(), serializer);
				if (_rangeKeys.Contains(property.Name))
				{
					if (property.Value.Type == JTokenType.Float)
						isNumeric = true;
					else if (property.Value.Type == JTokenType.Integer)
						isLong = true;
				}
			}
			if (isNumeric)
				return FromJson.ReadAs<NumericRangeQuery>(jo.CreateReader(), serializer);
			if (isLong)
				return FromJson.ReadAs<LongRangeQuery>(jo.CreateReader(), serializer);

			return FromJson.ReadAs<DateRangeQuery>(jo.CreateReader(), serializer);
		}

		private static TReturn GetPropValue<TReturn>(JObject jObject, string field)
		{
			JToken jToken = null;
			return !jObject.TryGetValue(field, out jToken)
				? default(TReturn)
				: jToken.Value<TReturn>();
		}
	}
}
