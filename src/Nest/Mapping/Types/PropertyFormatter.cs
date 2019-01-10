using System;
using Utf8Json;
using Utf8Json.Internal;
using Utf8Json.Resolvers;

namespace Nest
{
	internal class PropertyFormatter : IJsonFormatter<IProperty>
	{
		private static readonly AutomataDictionary AutomataDictionary = new AutomataDictionary
		{
			{ "type", 0 },
			{ "properties", 1 }
		};

		public IProperty Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var arraySegment = reader.ReadNextBlockSegment();

			var segmentReader = new JsonReader(arraySegment.Array, arraySegment.Offset);
			var count = 0;
			string typeString = null;
			var type = FieldType.None;

			while (segmentReader.ReadIsInObject(ref count))
			{
				var propertyName = segmentReader.ReadPropertyNameSegmentRaw();
				if (AutomataDictionary.TryGetValue(propertyName, out var value))
				{
					switch (value)
					{
						case 0:
							typeString = segmentReader.ReadString();
							type = typeString.ToEnum<FieldType>().GetValueOrDefault(type);
							break;
						case 1:
							type = FieldType.Object;
							break;
					}

					break;
				}
				else
					segmentReader.ReadNextBlock();
			}

			segmentReader = new JsonReader(arraySegment.Array, arraySegment.Offset);

			switch (type)
			{
				case FieldType.Text: return Deserialize<TextProperty>(ref segmentReader, formatterResolver);
				case FieldType.Keyword: return Deserialize<KeywordProperty>(ref segmentReader, formatterResolver);
				case FieldType.Float:
				case FieldType.Double:
				case FieldType.Byte:
				case FieldType.Short:
				case FieldType.Integer:
				case FieldType.Long:
				case FieldType.ScaledFloat:
				case FieldType.HalfFloat:
					var numberProperty = Deserialize<NumberProperty>(ref segmentReader, formatterResolver);
					((IProperty)numberProperty).Type = typeString;
					return numberProperty;
				case FieldType.Date: return Deserialize<DateProperty>(ref segmentReader, formatterResolver);
				case FieldType.Boolean: return Deserialize<BooleanProperty>(ref segmentReader, formatterResolver);
				case FieldType.Binary: return Deserialize<BinaryProperty>(ref segmentReader, formatterResolver);
				case FieldType.Object: return Deserialize<ObjectProperty>(ref segmentReader, formatterResolver);
				case FieldType.Nested: return Deserialize<NestedProperty>(ref segmentReader, formatterResolver);
				case FieldType.Ip: return Deserialize<IpProperty>(ref segmentReader, formatterResolver);
				case FieldType.GeoPoint: return Deserialize<GeoPointProperty>(ref segmentReader, formatterResolver);
				case FieldType.GeoShape: return Deserialize<GeoShapeProperty>(ref segmentReader, formatterResolver);
				case FieldType.Completion: return Deserialize<CompletionProperty>(ref segmentReader, formatterResolver);
				case FieldType.TokenCount: return Deserialize<TokenCountProperty>(ref segmentReader, formatterResolver);
				case FieldType.Murmur3Hash: return Deserialize<Murmur3HashProperty>(ref segmentReader, formatterResolver);
				case FieldType.Percolator: return Deserialize<PercolatorProperty>(ref segmentReader, formatterResolver);
				case FieldType.DateRange: return Deserialize<DateRangeProperty>(ref segmentReader, formatterResolver);
				case FieldType.DoubleRange: return Deserialize<DoubleRangeProperty>(ref segmentReader, formatterResolver);
				case FieldType.FloatRange: return Deserialize<FloatRangeProperty>(ref segmentReader, formatterResolver);
				case FieldType.IntegerRange: return Deserialize<IntegerRangeProperty>(ref segmentReader, formatterResolver);
				case FieldType.LongRange: return Deserialize<LongRangeProperty>(ref segmentReader, formatterResolver);
				case FieldType.IpRange: return Deserialize<IpRangeProperty>(ref segmentReader, formatterResolver);
				case FieldType.Join: return Deserialize<JoinProperty>(ref segmentReader, formatterResolver);
				case FieldType.Alias: return Deserialize<FieldAliasProperty>(ref segmentReader, formatterResolver);
				case FieldType.None:
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, "mapping property converter does not know this value");
			}

			return null;
		}

		public void Serialize(ref JsonWriter writer, IProperty value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			switch (value.Type)
			{
				case "text":
					Serialize<ITextProperty>(ref writer, value, formatterResolver);
					break;
				case "keyword":
					Serialize<IKeywordProperty>(ref writer, value, formatterResolver);
					break;
				case "float":
				case "double":
				case "byte":
				case "short":
				case "integer":
				case "long":
				case "scaled_float":
				case "half_float":
					Serialize<INumberProperty>(ref writer, value, formatterResolver);
					break;
				case "date":
					Serialize<IDateProperty>(ref writer, value, formatterResolver);
					break;
				case "boolean":
					Serialize<IBooleanProperty>(ref writer, value, formatterResolver);
					break;
				case "binary":
					Serialize<IBinaryProperty>(ref writer, value, formatterResolver);
					break;
				case "object":
					Serialize<IObjectProperty>(ref writer, value, formatterResolver);
					break;
				case "nested":
					Serialize<INestedProperty>(ref writer, value, formatterResolver);
					break;
				case "ip":
					Serialize<IIpProperty>(ref writer, value, formatterResolver);
					break;
				case "geo_point":
					Serialize<IGeoPointProperty>(ref writer, value, formatterResolver);
					break;
				case "geo_shape":
					Serialize<IGeoShapeProperty>(ref writer, value, formatterResolver);
					break;
				case "completion":
					Serialize<ICompletionProperty>(ref writer, value, formatterResolver);
					break;
				case "token_count":
					Serialize<ITokenCountProperty>(ref writer, value, formatterResolver);
					break;
				case "murmur3":
					Serialize<IMurmur3HashProperty>(ref writer, value, formatterResolver);
					break;
				case "percolator":
					Serialize<IPercolatorProperty>(ref writer, value, formatterResolver);
					break;
				case "date_range":
					Serialize<IDateRangeProperty>(ref writer, value, formatterResolver);
					break;
				case "double_range":
					Serialize<IDoubleRangeProperty>(ref writer, value, formatterResolver);
					break;
				case "float_range":
					Serialize<IFloatRangeProperty>(ref writer, value, formatterResolver);
					break;
				case "integer_range":
					Serialize<IIntegerRangeProperty>(ref writer, value, formatterResolver);
					break;
				case "long_range":
					Serialize<ILongRangeProperty>(ref writer, value, formatterResolver);
					break;
				case "ip_range":
					Serialize<IIpRangeProperty>(ref writer, value, formatterResolver);
					break;
				case "join":
					Serialize<IJoinProperty>(ref writer, value, formatterResolver);
					break;
				case "alias":
					Serialize<IFieldAliasProperty>(ref writer, value, formatterResolver);
					break;
				default:
					var formatter = DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<IProperty>();
					formatter.Serialize(ref writer, value, formatterResolver);
					break;
			}
		}

		private static void Serialize<TProperty>(ref JsonWriter writer, IProperty value, IJsonFormatterResolver formatterResolver)
			where TProperty : class, IProperty
		{
			var formatter = formatterResolver.GetFormatter<TProperty>();
			formatter.Serialize(ref writer, value as TProperty, formatterResolver);
		}

		private static TProperty Deserialize<TProperty>(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
			where TProperty : IProperty
		{
			var formatter = formatterResolver.GetFormatter<TProperty>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}
	}
}
