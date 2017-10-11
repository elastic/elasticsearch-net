using System;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class PropertyJsonConverter : JsonConverter
	{
		public override bool CanWrite => false;

		public override bool CanConvert(Type objectType) => objectType == typeof(IProperty);

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var jObject = JObject.Load(reader);
			JToken typeToken;
			JToken propertiesToken;

			var type = FieldType.None;
			if (jObject.TryGetValue("type", out typeToken))
				type = typeToken.Value<string>().ToEnum<FieldType>().GetValueOrDefault(type);
			else if (jObject.TryGetValue("properties", out propertiesToken))
				type = FieldType.Object;

			switch (type)
			{
				case FieldType.Text:
					return jObject.ToObject<TextProperty>();
				case FieldType.Keyword:
					return jObject.ToObject<KeywordProperty>();
				case FieldType.String:
#pragma warning disable 618
					return jObject.ToObject<StringProperty>();
#pragma warning restore 618
				case FieldType.Float:
				case FieldType.Double:
				case FieldType.Byte:
				case FieldType.Short:
				case FieldType.Integer:
				case FieldType.Long:
				case FieldType.ScaledFloat:
				case FieldType.HalfFloat:
					var num = jObject.ToObject<NumberProperty>();
					num.Type = type.GetStringValue();
					return num;
				case FieldType.Date:
					return jObject.ToObject<DateProperty>();
				case FieldType.Boolean:
					return jObject.ToObject<BooleanProperty>();
				case FieldType.Binary:
					return jObject.ToObject<BinaryProperty>();
				case FieldType.Object:
					return jObject.ToObject<ObjectProperty>();
				case FieldType.Nested:
					return jObject.ToObject<NestedProperty>();
				case FieldType.Ip:
					return jObject.ToObject<IpProperty>();
				case FieldType.GeoPoint:
					return jObject.ToObject<GeoPointProperty>();
				case FieldType.GeoShape:
					return jObject.ToObject<GeoShapeProperty>();
				case FieldType.Completion:
					return jObject.ToObject<CompletionProperty>();
				case FieldType.TokenCount:
					return jObject.ToObject<TokenCountProperty>();
				case FieldType.Murmur3Hash:
					return jObject.ToObject<Murmur3HashProperty>();
				case FieldType.Percolator:
					return jObject.ToObject<PercolatorProperty>();
				case FieldType.DateRange:
					return jObject.ToObject<DateRangeProperty>();
				case FieldType.DoubleRange:
					return jObject.ToObject<DoubleRangeProperty>();
				case FieldType.FloatRange:
					return jObject.ToObject<FloatRangeProperty>();
				case FieldType.IntegerRange:
					return jObject.ToObject<IntegerRangeProperty>();
				case FieldType.LongRange:
					return jObject.ToObject<LongRangeProperty>();
				case FieldType.Join:
					return jObject.ToObject<JoinProperty>();
				case FieldType.None:
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, "mapping property converter does not know this value");
			}

			return null;
		}
	}
}
