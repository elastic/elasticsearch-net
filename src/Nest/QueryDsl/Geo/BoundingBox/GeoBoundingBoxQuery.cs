// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Internal;


namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(GeoBoundingBoxQueryFormatter))]
	public interface IGeoBoundingBoxQuery : IFieldNameQuery
	{
		IBoundingBox BoundingBox { get; set; }

		GeoExecution? Type { get; set; }

		GeoValidationMethod? ValidationMethod { get; set; }
	}

	public class GeoBoundingBoxQuery : FieldNameQueryBase, IGeoBoundingBoxQuery
	{
		public IBoundingBox BoundingBox { get; set; }
		public GeoExecution? Type { get; set; }

		public GeoValidationMethod? ValidationMethod { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.GeoBoundingBox = this;

		internal static bool IsConditionless(IGeoBoundingBoxQuery q) =>
			q.Field.IsConditionless() || q.BoundingBox?.BottomRight == null && q.BoundingBox?.TopLeft == null && q.BoundingBox?.WellKnownText == null;
	}

	public class GeoBoundingBoxQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<GeoBoundingBoxQueryDescriptor<T>, IGeoBoundingBoxQuery, T>
			, IGeoBoundingBoxQuery where T : class
	{
		protected override bool Conditionless => GeoBoundingBoxQuery.IsConditionless(this);
		IBoundingBox IGeoBoundingBoxQuery.BoundingBox { get; set; }
		GeoExecution? IGeoBoundingBoxQuery.Type { get; set; }
		GeoValidationMethod? IGeoBoundingBoxQuery.ValidationMethod { get; set; }

		public GeoBoundingBoxQueryDescriptor<T> BoundingBox(double topLeftLat, double topLeftLon, double bottomRightLat, double bottomRightLon) =>
			BoundingBox(f => f.TopLeft(topLeftLat, topLeftLon).BottomRight(bottomRightLat, bottomRightLon));

		public GeoBoundingBoxQueryDescriptor<T> BoundingBox(GeoLocation topLeft, GeoLocation bottomRight) =>
			BoundingBox(f => f.TopLeft(topLeft).BottomRight(bottomRight));

		public GeoBoundingBoxQueryDescriptor<T> BoundingBox(string wkt) =>
			BoundingBox(f => f.WellKnownText(wkt));

		public GeoBoundingBoxQueryDescriptor<T> BoundingBox(Func<BoundingBoxDescriptor, IBoundingBox> boundingBoxSelector) =>
			Assign(boundingBoxSelector, (a, v) => a.BoundingBox = v?.Invoke(new BoundingBoxDescriptor()));

		public GeoBoundingBoxQueryDescriptor<T> Type(GeoExecution? type) => Assign(type, (a, v) => a.Type = v);

		public GeoBoundingBoxQueryDescriptor<T> ValidationMethod(GeoValidationMethod? validation) => Assign(validation, (a, v) => a.ValidationMethod = v);
	}

	internal class GeoBoundingBoxQueryFormatter : IJsonFormatter<IGeoBoundingBoxQuery>
	{
		private static readonly AutomataDictionary Fields = new AutomataDictionary
		{
			{ "_name", 0 },
			{ "boost", 1 },
			{ "validation_method", 2 },
			{ "type", 3 }
		};

		public IGeoBoundingBoxQuery Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
				return null;

			var query = new GeoBoundingBoxQuery();
			var count = 0;
			while (reader.ReadIsInObject(ref count))
			{
				var property = reader.ReadPropertyNameSegmentRaw();
				if (Fields.TryGetValue(property, out var value))
				{
					switch (value)
					{
						case 0:
							query.Name = reader.ReadString();
							break;
						case 1:
							query.Boost = reader.ReadDouble();
							break;
						case 2:
							query.ValidationMethod = formatterResolver.GetFormatter<GeoValidationMethod>()
								.Deserialize(ref reader, formatterResolver);
							break;
						case 3:
							query.Type = formatterResolver.GetFormatter<GeoExecution>()
								.Deserialize(ref reader, formatterResolver);
							break;
					}
				}
				else
				{
					query.Field = property.Utf8String();
					query.BoundingBox = formatterResolver.GetFormatter<IBoundingBox>()
						.Deserialize(ref reader, formatterResolver);
				}
			}

			return query;
		}

		public void Serialize(ref JsonWriter writer, IGeoBoundingBoxQuery value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var written = false;

			writer.WriteBeginObject();

			if (!value.Name.IsNullOrEmpty())
			{
				writer.WritePropertyName("_name");
				writer.WriteString(value.Name);
				written = true;
			}

			if (value.Boost != null)
			{
				if (written)
					writer.WriteValueSeparator();

				writer.WritePropertyName("boost");
				writer.WriteDouble(value.Boost.Value);
				written = true;
			}

			if (value.ValidationMethod != null)
			{
				if (written)
					writer.WriteValueSeparator();

				writer.WritePropertyName("validation_method");
				formatterResolver.GetFormatter<GeoValidationMethod>()
					.Serialize(ref writer, value.ValidationMethod.Value, formatterResolver);
				written = true;
			}

			if (value.Type != null)
			{
				if (written)
					writer.WriteValueSeparator();

				writer.WritePropertyName("type");
				formatterResolver.GetFormatter<GeoExecution>()
					.Serialize(ref writer, value.Type.Value, formatterResolver);
				written = true;
			}

			if (written)
				writer.WriteValueSeparator();

			var settings = formatterResolver.GetConnectionSettings();
			writer.WritePropertyName(settings.Inferrer.Field(value.Field));
			formatterResolver.GetFormatter<IBoundingBox>()
				.Serialize(ref writer, value.BoundingBox, formatterResolver);

			writer.WriteEndObject();
		}
	}
}
