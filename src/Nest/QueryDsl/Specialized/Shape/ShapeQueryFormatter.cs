// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Internal;

namespace Nest
{
	internal class ShapeQueryFieldNameFormatter : IJsonFormatter<IShapeQuery>
	{
		public IShapeQuery Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();

		public void Serialize(ref JsonWriter writer, IShapeQuery value, IJsonFormatterResolver formatterResolver)
		{
			var fieldName = value.Field;
			if (fieldName == null)
			{
				writer.WriteNull();
				return;
			}

			var settings = formatterResolver.GetConnectionSettings();
			var field = settings.Inferrer.Field(fieldName);

			if (field.IsNullOrEmpty())
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBeginObject();
			var name = value.Name;
			var boost = value.Boost;
			var ignoreUnmapped = value.IgnoreUnmapped;

			if (!name.IsNullOrEmpty())
			{
				writer.WritePropertyName("_name");
				writer.WriteString(name);
				writer.WriteValueSeparator();
			}
			if (boost != null)
			{
				writer.WritePropertyName("boost");
				writer.WriteDouble(boost.Value);
				writer.WriteValueSeparator();
			}
			if (ignoreUnmapped != null)
			{
				writer.WritePropertyName("ignore_unmapped");
				writer.WriteBoolean(ignoreUnmapped.Value);
				writer.WriteValueSeparator();
			}

			writer.WritePropertyName(field);

			writer.WriteBeginObject();

			var written = false;

			if (value.Shape != null)
			{
				writer.WritePropertyName("shape");
				var shapeFormatter = formatterResolver.GetFormatter<IGeoShape>();
				shapeFormatter.Serialize(ref writer, value.Shape, formatterResolver);
				written = true;
			}
			else if (value.IndexedShape != null)
			{
				writer.WritePropertyName("indexed_shape");
				var fieldLookupFormatter = formatterResolver.GetFormatter<IFieldLookup>();
				fieldLookupFormatter.Serialize(ref writer, value.IndexedShape, formatterResolver);
				written = true;
			}

			if (value.Relation.HasValue)
			{
				if (written)
					writer.WriteValueSeparator();

				writer.WritePropertyName("relation");
				formatterResolver.GetFormatter<ShapeRelation>()
					.Serialize(ref writer, value.Relation.Value, formatterResolver);
			}

			writer.WriteEndObject();
			writer.WriteEndObject();
		}
	}

	internal class ShapeQueryFormatter : IJsonFormatter<IShapeQuery>
	{
		private static readonly AutomataDictionary Fields = new AutomataDictionary
		{
			{ "boost", 0 },
			{ "_name", 1 },
			{ "ignore_unmapped", 2 }
		};

		private static readonly AutomataDictionary ShapeFields = new AutomataDictionary
		{
			{ "shape", 0 },
			{ "indexed_shape", 1 },
			{ "relation", 2 }
		};

		public IShapeQuery Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull())
				return null;

			var count = 0;
			string field = null;
			double? boost = null;
			string name = null;
			bool? ignoreUnmapped = null;
			IShapeQuery query = null;
			ShapeRelation? relation = null;

			while (reader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyNameSegmentRaw();
				if (Fields.TryGetValue(propertyName, out var value))
				{
					switch (value)
					{
						case 0:
							boost = reader.ReadDouble();
							break;
						case 1:
							name = reader.ReadString();
							break;
						case 2:
							ignoreUnmapped = reader.ReadBoolean();
							break;
					}
				}
				else
				{
					field = propertyName.Utf8String();
					var shapeCount = 0;
					while (reader.ReadIsInObject(ref shapeCount))
					{
						var shapeProperty = reader.ReadPropertyNameSegmentRaw();
						if (ShapeFields.TryGetValue(shapeProperty, out var shapeValue))
						{
							switch (shapeValue)
							{
								case 0:
									var shapeFormatter = formatterResolver.GetFormatter<IGeoShape>();
									query = new ShapeQuery
									{
										Shape = shapeFormatter.Deserialize(ref reader, formatterResolver)
									};
									break;
								case 1:
									var fieldLookupFormatter = formatterResolver.GetFormatter<FieldLookup>();
									query = new ShapeQuery
									{
										IndexedShape = fieldLookupFormatter.Deserialize(ref reader, formatterResolver)
									};
									break;
								case 2:
									relation = formatterResolver.GetFormatter<ShapeRelation>()
										.Deserialize(ref reader, formatterResolver);
									break;
							}
						}
					}
				}
			}

			if (query == null)
				return null;

			query.Boost = boost;
			query.Name = name;
			query.Field = field;
			query.Relation = relation;
			query.IgnoreUnmapped = ignoreUnmapped;
			return query;
		}

		public void Serialize(ref JsonWriter writer, IShapeQuery value, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();
	}
}
