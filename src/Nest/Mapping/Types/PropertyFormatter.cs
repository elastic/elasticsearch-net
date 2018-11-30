using System;
using Utf8Json;
using Utf8Json.Internal;

namespace Nest
{
	internal class PropertyFormatter : IJsonFormatter<IProperty>
	{
		private static readonly AutomataDictionary _automataDictionary = new AutomataDictionary
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
				if (_automataDictionary.TryGetValue(propertyName, out var value))
				{
					switch (value)
					{
						case 0:
							typeString = reader.ReadString();
							type = typeString.ToEnum<FieldType>().GetValueOrDefault(type);
							break;
						case 1:
							type = FieldType.Object;
							break;
					}
				}
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

		public void Serialize(ref JsonWriter writer, IProperty value, IJsonFormatterResolver formatterResolver) => throw new NotSupportedException();

		private static TProperty Deserialize<TProperty>(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
			where TProperty : IProperty
		{
			var formatter = formatterResolver.GetFormatter<TProperty>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}
	}
}
