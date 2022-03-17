// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;

namespace Elastic.Clients.Elasticsearch;

public sealed class GeoDistanceSort : SortBase
{
	public GeoDistanceType? DistanceType { get; set; }

	public Field Field { get; set; }

	public GeoPoints GeoPoints { get; set; }

	public DistanceUnit? Unit { get; set; }

	public bool? IgnoreUnmapped { get; set; }
}

public sealed class GeoDistanceSortDescriptor : DescriptorBase<GeoDistanceSortDescriptor>
{
	private Field _field;
	private GeoDistanceType? _geoDistanceType;
	private SortMode? _sortMode;
	private SortOrder? _order;
	private DistanceUnit? _distanceUnit;
	private GeoPoints _points;
	private bool? _ignoreUnmappedFields;

	public GeoDistanceSortDescriptor DistanceType(GeoDistanceType distanceType) => Assign(distanceType, (a, v) => a._geoDistanceType = v);

	public GeoDistanceSortDescriptor Field(Field field) => Assign(field, (a, v) => a._field = v);

	public GeoDistanceSortDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> objectPath) => Assign(objectPath, (a, v) => a._field = v);

	public GeoDistanceSortDescriptor Field<TDocument>(Expression<Func<TDocument, object>> objectPath) => Assign(objectPath, (a, v) => a._field = v);

	public GeoDistanceSortDescriptor GeoPoints(params GeoPoint[] geoPoints) => Assign(geoPoints, (a, v) => a._points = v);

	public GeoDistanceSortDescriptor GeoPoints(IEnumerable<GeoPoint> geoPoints) => Assign(geoPoints, (a, v) => a._points = new GeoPoints(v));

	public GeoDistanceSortDescriptor IgnoreUnmappedFields(bool? ignore = true) => Assign(ignore, (a, v) => a._ignoreUnmappedFields = v);

	public GeoDistanceSortDescriptor Mode(SortMode mode) => Assign(mode, (a, v) => a._sortMode = v);

	public GeoDistanceSortDescriptor Order(SortOrder order) => Assign(order, (a, v) => a._order = v);

	public GeoDistanceSortDescriptor Unit(DistanceUnit mode) => Assign(mode, (a, v) => a._distanceUnit = v);

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("_geo_distance");
		writer.WriteStartObject();

		if (_geoDistanceType.HasValue)
		{
			writer.WritePropertyName("distance_type");
			JsonSerializer.Serialize(writer, _geoDistanceType.Value, options);
		}

		if (_distanceUnit.HasValue)
		{
			writer.WritePropertyName("unit");
			JsonSerializer.Serialize(writer, _distanceUnit.Value, options);
		}

		if (_ignoreUnmappedFields.HasValue)
		{
			writer.WritePropertyName("ignore_unmapped");
			JsonSerializer.Serialize(writer, _ignoreUnmappedFields.Value, options);
		}

		if (_order.HasValue)
		{
			writer.WritePropertyName("order");
			JsonSerializer.Serialize(writer, _order.Value, options);
		}

		if (_sortMode.HasValue)
		{
			writer.WritePropertyName("mode");
			JsonSerializer.Serialize(writer, _sortMode.Value, options);
		}

		if (_field is not null && _points is not null)
		{
			writer.WritePropertyName(settings.Inferrer.Field(_field));
			JsonSerializer.Serialize(writer, _points, options);
		}

		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}

public sealed class GeoDistanceSortDescriptor<TDocument> : DescriptorBase<GeoDistanceSortDescriptor<TDocument>>
{
	private Field _field;
	private GeoDistanceType? _geoDistanceType;
	private SortMode? _sortMode;
	private SortOrder? _order;
	private DistanceUnit? _distanceUnit;
	private GeoPoints _points;
	private bool? _ignoreUnmappedFields;

	public GeoDistanceSortDescriptor<TDocument> DistanceType(GeoDistanceType distanceType) => Assign(distanceType, (a, v) => a._geoDistanceType = v);

	public GeoDistanceSortDescriptor<TDocument> Field(Field field) => Assign(field, (a, v) => a._field = v);

	public GeoDistanceSortDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> objectPath) => Assign(objectPath, (a, v) => a._field = v);

	public GeoDistanceSortDescriptor<TDocument> Field<T, TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a._field = v);

	public GeoDistanceSortDescriptor<TDocument> GeoPoints(params GeoPoint[] geoPoints) => Assign(geoPoints, (a, v) => a._points = v);

	public GeoDistanceSortDescriptor<TDocument> GeoPoints(IEnumerable<GeoPoint> geoPoints) => Assign(geoPoints, (a, v) => a._points = new GeoPoints(v));

	public GeoDistanceSortDescriptor<TDocument> IgnoreUnmappedFields(bool? ignore = true) => Assign(ignore, (a, v) => a._ignoreUnmappedFields = v);

	public GeoDistanceSortDescriptor<TDocument> Mode(SortMode mode) => Assign(mode, (a, v) => a._sortMode = v);

	public GeoDistanceSortDescriptor<TDocument> Order(SortOrder order) => Assign(order, (a, v) => a._order = v);

	public GeoDistanceSortDescriptor<TDocument> Unit(DistanceUnit mode) => Assign(mode, (a, v) => a._distanceUnit = v);

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("_geo_distance");
		writer.WriteStartObject();

		if (_geoDistanceType.HasValue)
		{
			writer.WritePropertyName("distance_type");
			JsonSerializer.Serialize(writer, _geoDistanceType.Value, options);
		}

		if (_distanceUnit.HasValue)
		{
			writer.WritePropertyName("unit");
			JsonSerializer.Serialize(writer, _distanceUnit.Value, options);
		}

		if (_ignoreUnmappedFields.HasValue)
		{
			writer.WritePropertyName("ignore_unmapped");
			JsonSerializer.Serialize(writer, _ignoreUnmappedFields.Value, options);
		}

		if (_order.HasValue)
		{
			writer.WritePropertyName("order");
			JsonSerializer.Serialize(writer, _order.Value, options);
		}

		if (_sortMode.HasValue)
		{
			writer.WritePropertyName("mode");
			JsonSerializer.Serialize(writer, _sortMode.Value, options);
		}

		if (_field is not null && _points is not null)
		{
			writer.WritePropertyName(settings.Inferrer.Field(_field));
			JsonSerializer.Serialize(writer, _points, options);
		}

		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}

