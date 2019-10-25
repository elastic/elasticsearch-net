using System.Collections.Generic;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Internal;
using Elasticsearch.Net.Utf8Json.Resolvers;


namespace Nest
{
	internal class SortFormatter : IJsonFormatter<ISort>
	{
		private static readonly AutomataDictionary SortFields = new AutomataDictionary
		{
			{ "_geo_distance", 0 },
			{ "_script", 1 }
		};

		public ISort Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			ISort sort = null;

			var count = 0;
			while (reader.ReadIsInObject(ref count))
			{
				var sortProperty = reader.ReadPropertyNameSegmentRaw();
				if (SortFields.TryGetValue(sortProperty, out var value))
				{
					switch (value)
					{
						case 0:
							var propCount = 0;
							string field = null;
							var geoDistanceSegment = reader.ReadNextBlockSegment();
							var geoDistanceReader = new JsonReader(geoDistanceSegment.Array, geoDistanceSegment.Offset);
							IEnumerable<GeoLocation> points = null;
							while (geoDistanceReader.ReadIsInObject(ref propCount))
							{
								var nameSegment = geoDistanceReader.ReadPropertyNameSegmentRaw();
								if (geoDistanceReader.GetCurrentJsonToken() == JsonToken.BeginArray)
								{
									field = nameSegment.Utf8String();
									points = formatterResolver.GetFormatter<IEnumerable<GeoLocation>>()
										.Deserialize(ref geoDistanceReader, formatterResolver);
									break;
								}

								// skip value if not array
								geoDistanceReader.ReadNextBlock();
							}
							geoDistanceReader = new JsonReader(geoDistanceSegment.Array, geoDistanceSegment.Offset);
							var geoDistanceSort = formatterResolver.GetFormatter<GeoDistanceSort>()
								.Deserialize(ref geoDistanceReader, formatterResolver);
							geoDistanceSort.Field = field;
							geoDistanceSort.Points = points;
							sort = geoDistanceSort;
							break;
						case 1:
							sort = formatterResolver.GetFormatter<ScriptSort>()
								.Deserialize(ref reader, formatterResolver);
							break;
					}
				}
				else
				{
					var field = sortProperty.Utf8String();
					var sortField = formatterResolver.GetFormatter<FieldSort>()
						.Deserialize(ref reader, formatterResolver);
					sortField.Field = field;
					sort = sortField;
				}
			}

			return sort;
		}

		public void Serialize(ref JsonWriter writer, ISort value, IJsonFormatterResolver formatterResolver)
		{
			if (value?.SortKey == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBeginObject();
			var settings = formatterResolver.GetConnectionSettings();
			switch (value.SortKey.Name ?? string.Empty)
			{
				case "_script":
					writer.WritePropertyName("_script");
					var scriptSort = (IScriptSort)value;
					var scriptSortFormatter = formatterResolver.GetFormatter<IScriptSort>();
					scriptSortFormatter.Serialize(ref writer, scriptSort, formatterResolver);
					break;
				case "_geo_distance":
					var geo = value as IGeoDistanceSort;
					writer.WritePropertyName(geo.SortKey.Name);

					var innerWriter = new JsonWriter();
					var formatter = DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<IGeoDistanceSort>();
					formatter.Serialize(ref innerWriter, geo, formatterResolver);

					var buffer = innerWriter.GetBuffer();
					// get all the written bytes except the closing }
					for (var i = buffer.Offset; i < buffer.Count - 1; i++)
						writer.WriteRawUnsafe(buffer.Array[i]);

					// does the IGeoDistanceSort have other properties set i.e. is it more than simply {} ?
					if (buffer.Count > 2)
						writer.WriteValueSeparator();

					writer.WritePropertyName(settings.Inferrer.Field(geo.Field));
					var geoFormatter = formatterResolver.GetFormatter<IEnumerable<GeoLocation>>();
					geoFormatter.Serialize(ref writer, geo.Points, formatterResolver);
					writer.WriteEndObject();
					break;
				default:
					writer.WritePropertyName(settings.Inferrer.Field(value.SortKey));
					var sortFormatter = DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<IFieldSort>();
					sortFormatter.Serialize(ref writer, value as IFieldSort, formatterResolver);
					break;
			}
			writer.WriteEndObject();
		}
	}
}
