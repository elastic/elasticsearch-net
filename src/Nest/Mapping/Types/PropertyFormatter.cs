// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Internal;


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
							if (type == FieldType.None)
								type = FieldType.Object;

							segmentReader.ReadNextBlock();
							break;
					}

					// explicit type has been set
					if (value == 0)
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
				case FieldType.SearchAsYouType: return Deserialize<SearchAsYouTypeProperty>(ref segmentReader, formatterResolver);
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
				case FieldType.DateNanos: return Deserialize<DateNanosProperty>(ref segmentReader, formatterResolver);
				case FieldType.Boolean: return Deserialize<BooleanProperty>(ref segmentReader, formatterResolver);
				case FieldType.Binary: return Deserialize<BinaryProperty>(ref segmentReader, formatterResolver);
				case FieldType.Object: return Deserialize<ObjectProperty>(ref segmentReader, formatterResolver);
				case FieldType.Nested: return Deserialize<NestedProperty>(ref segmentReader, formatterResolver);
				case FieldType.Ip: return Deserialize<IpProperty>(ref segmentReader, formatterResolver);
				case FieldType.GeoPoint: return Deserialize<GeoPointProperty>(ref segmentReader, formatterResolver);
				case FieldType.GeoShape: return Deserialize<GeoShapeProperty>(ref segmentReader, formatterResolver);
				case FieldType.Shape: return Deserialize<ShapeProperty>(ref segmentReader, formatterResolver);
				case FieldType.Point: return Deserialize<PointProperty>(ref segmentReader, formatterResolver);
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
				case FieldType.RankFeature: return Deserialize<RankFeatureProperty>(ref segmentReader, formatterResolver);
				case FieldType.RankFeatures: return Deserialize<RankFeaturesProperty>(ref segmentReader, formatterResolver);
				case FieldType.Flattened: return Deserialize<FlattenedProperty>(ref segmentReader, formatterResolver);
				case FieldType.Histogram: return Deserialize<HistogramProperty>(ref segmentReader, formatterResolver);
				case FieldType.ConstantKeyword: return Deserialize<ConstantKeywordProperty>(ref segmentReader, formatterResolver);
				case FieldType.Wildcard: return Deserialize<WildcardProperty>(ref segmentReader, formatterResolver);
				case FieldType.None:
					// no "type" field in the property mapping, or FieldType enum could not be parsed from typeString
					return Deserialize<ObjectProperty>(ref segmentReader, formatterResolver);
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, "mapping property converter does not know this value");
			}
		}

		public void Serialize(ref JsonWriter writer, IProperty value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			switch (value)
			{
				case ITextProperty textProperty:
					Serialize(ref writer, textProperty, formatterResolver);
					break;
				case IKeywordProperty keywordProperty:
					Serialize(ref writer, keywordProperty, formatterResolver);
					break;
				case INumberProperty numberProperty:
					Serialize(ref writer, numberProperty, formatterResolver);
					break;
				case IDateProperty dateProperty:
					Serialize(ref writer, dateProperty, formatterResolver);
					break;
				case IBooleanProperty booleanProperty:
					Serialize(ref writer, booleanProperty, formatterResolver);
					break;
				case INestedProperty nestedProperty:
					Serialize(ref writer, nestedProperty, formatterResolver);
					break;
				case IObjectProperty objectProperty:
					Serialize(ref writer, objectProperty, formatterResolver);
					break;
				case ISearchAsYouTypeProperty searchAsYouTypeProperty:
					Serialize(ref writer, searchAsYouTypeProperty, formatterResolver);
					break;
				case IDateNanosProperty dateNanosProperty:
					Serialize(ref writer, dateNanosProperty, formatterResolver);
					break;
				case IBinaryProperty binaryProperty:
					Serialize(ref writer, binaryProperty, formatterResolver);
					break;
				case IIpProperty ipProperty:
					Serialize(ref writer, ipProperty, formatterResolver);
					break;
				case IGeoPointProperty geoPointProperty:
					Serialize(ref writer, geoPointProperty, formatterResolver);
					break;
				case IGeoShapeProperty geoShapeProperty:
					Serialize(ref writer, geoShapeProperty, formatterResolver);
					break;
				case IShapeProperty shapeProperty:
					Serialize(ref writer, shapeProperty, formatterResolver);
					break;
				case IPointProperty pointProperty:
					Serialize(ref writer, pointProperty, formatterResolver);
					break;
				case ICompletionProperty completionProperty:
					Serialize(ref writer, completionProperty, formatterResolver);
					break;
				case ITokenCountProperty tokenCountProperty:
					Serialize(ref writer, tokenCountProperty, formatterResolver);
					break;
				case IMurmur3HashProperty murmur3HashProperty:
					Serialize(ref writer, murmur3HashProperty, formatterResolver);
					break;
				case IPercolatorProperty percolatorProperty:
					Serialize(ref writer, percolatorProperty, formatterResolver);
					break;
				case IDateRangeProperty dateRangeProperty:
					Serialize(ref writer, dateRangeProperty, formatterResolver);
					break;
				case IDoubleRangeProperty doubleRangeProperty:
					Serialize(ref writer, doubleRangeProperty, formatterResolver);
					break;
				case IFloatRangeProperty floatRangeProperty:
					Serialize(ref writer, floatRangeProperty, formatterResolver);
					break;
				case IIntegerRangeProperty integerRangeProperty:
					Serialize(ref writer, integerRangeProperty, formatterResolver);
					break;
				case ILongRangeProperty longRangeProperty:
					Serialize(ref writer, longRangeProperty, formatterResolver);
					break;
				case IIpRangeProperty ipRangeProperty:
					Serialize(ref writer, ipRangeProperty, formatterResolver);
					break;
				case IJoinProperty joinProperty:
					Serialize(ref writer, joinProperty, formatterResolver);
					break;
				case IFieldAliasProperty fieldAliasProperty:
					Serialize(ref writer, fieldAliasProperty, formatterResolver);
					break;
				case IRankFeatureProperty rankFeatureProperty:
					Serialize(ref writer, rankFeatureProperty, formatterResolver);
					break;
				case IRankFeaturesProperty rankFeaturesProperty:
					Serialize(ref writer, rankFeaturesProperty, formatterResolver);
					break;
				case IFlattenedProperty flattenedProperty:
					Serialize(ref writer, flattenedProperty, formatterResolver);
					break;
				case IHistogramProperty histogramProperty:
					Serialize(ref writer, histogramProperty, formatterResolver);
					break;
				case IConstantKeywordProperty constantKeywordProperty:
					Serialize(ref writer, constantKeywordProperty, formatterResolver);
					break;
				case IWildcardProperty wildcardProperty:
					Serialize(ref writer, wildcardProperty, formatterResolver);
					break;
				case IGenericProperty genericProperty:
					Serialize(ref writer, genericProperty, formatterResolver);
					break;
				default:
					var formatter = formatterResolver.GetFormatter<object>();
					formatter.Serialize(ref writer, value, formatterResolver);
					break;
			}
		}

		private static void Serialize<TProperty>(ref JsonWriter writer, TProperty value, IJsonFormatterResolver formatterResolver)
			where TProperty : class, IProperty
		{
			var formatter = formatterResolver.GetFormatter<TProperty>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}

		private static TProperty Deserialize<TProperty>(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
			where TProperty : IProperty
		{
			var formatter = formatterResolver.GetFormatter<TProperty>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}
	}
}
