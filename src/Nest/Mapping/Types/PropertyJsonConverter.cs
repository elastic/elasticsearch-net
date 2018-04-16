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

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
			throw new NotSupportedException();

		private TProperty ReadProperty<TProperty>(JObject j, JsonSerializer s) where TProperty : IProperty =>
			FromJson.ReadAs<TProperty>(j.CreateReader(), s);

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var jObject = JObject.Load(reader);

			var type = FieldType.None;
			if (jObject.TryGetValue("type", out var typeToken))
				type = typeToken.Value<string>().ToEnum<FieldType>().GetValueOrDefault(type);
			else if (jObject.TryGetValue("properties", out _))
				type = FieldType.Object;

			switch (type)
			{
				case FieldType.Text: return ReadProperty<TextProperty>(jObject, serializer);
				case FieldType.Keyword: return ReadProperty<KeywordProperty>(jObject, serializer);
				case FieldType.Float:
				case FieldType.Double:
				case FieldType.Byte:
				case FieldType.Short:
				case FieldType.Integer:
				case FieldType.Long:
				case FieldType.ScaledFloat:
				case FieldType.HalfFloat:
					var num = ReadProperty<NumberProperty>(jObject, serializer);
					((IProperty)num).Type = type.GetStringValue();
					return num;
				case FieldType.Date: return ReadProperty<DateProperty>(jObject, serializer);
				case FieldType.Boolean: return ReadProperty<BooleanProperty>(jObject, serializer);
				case FieldType.Binary: return ReadProperty<BinaryProperty>(jObject, serializer);
				case FieldType.Object: return ReadProperty<ObjectProperty>(jObject, serializer);
				case FieldType.Nested: return ReadProperty<NestedProperty>(jObject, serializer);
				case FieldType.Ip: return ReadProperty<IpProperty>(jObject, serializer);
				case FieldType.GeoPoint: return ReadProperty<GeoPointProperty>(jObject, serializer);
				case FieldType.GeoShape: return ReadProperty<GeoShapeProperty>(jObject, serializer);
				case FieldType.Completion: return ReadProperty<CompletionProperty>(jObject, serializer);
				case FieldType.TokenCount: return ReadProperty<TokenCountProperty>(jObject, serializer);
				case FieldType.Murmur3Hash: return ReadProperty<Murmur3HashProperty>(jObject, serializer);
				case FieldType.Percolator: return ReadProperty<PercolatorProperty>(jObject, serializer);
				case FieldType.DateRange: return ReadProperty<DateRangeProperty>(jObject, serializer);
				case FieldType.DoubleRange: return ReadProperty<DoubleRangeProperty>(jObject, serializer);
				case FieldType.FloatRange: return ReadProperty<FloatRangeProperty>(jObject, serializer);
				case FieldType.IntegerRange: return ReadProperty<IntegerRangeProperty>(jObject, serializer);
				case FieldType.LongRange: return ReadProperty<LongRangeProperty>(jObject, serializer);
				case FieldType.IpRange: return ReadProperty<IpRangeProperty>(jObject, serializer);
				case FieldType.Join: return ReadProperty<JoinProperty>(jObject, serializer);
				case FieldType.None:
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, "mapping property converter does not know this value");
			}

			return null;
		}
	}
}
