using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof (CompositeJsonConverter<GeoShapeQueryJsonConverter, GeoShapeQueryFieldNameConverter>))]
	public interface IGeoShapeQuery : IFieldNameQuery
	{
		/// <summary>
		/// Controls the spatial relation operator to use at search time.
		/// </summary>
		[JsonProperty("relation")]
		GeoShapeRelation? Relation { get; set; }

		/// <summary>
		/// Will ignore an unmapped field and will not match any documents for this query.
		/// This can be useful when querying multiple indexes which might have different mappings.
		/// </summary>
		[JsonProperty("ignore_unmapped")]
		bool? IgnoreUnmapped { get; set; }

		/// <summary>
		/// The geo shape to search with
		/// </summary>
		[JsonProperty("shape")]
		IGeoShape Shape { get; set; }

		/// <summary>
		/// Indexed geo shape to search with
		/// </summary>
		[JsonProperty("indexed_shape")]
		IFieldLookup IndexedShape { get; set; }
	}

	public class GeoShapeQuery : FieldNameQueryBase, IGeoShapeQuery
	{
		/// <inheritdoc />
		public GeoShapeRelation? Relation { get; set; }

		/// <inheritdoc />
		public bool? IgnoreUnmapped { get; set; }

		/// <inheritdoc />
		public IGeoShape Shape { get; set; }

		/// <inheritdoc />
		public IFieldLookup IndexedShape { get; set; }

		protected override bool Conditionless => IsConditionless(this);

		internal static bool IsConditionless(IGeoShapeQuery q)
		{
			if (q.Field.IsConditionless())
				return true;

			switch (q.Shape)
			{
				case ICircleGeoShape circleGeoShape:
					return circleGeoShape.Coordinates == null || string.IsNullOrEmpty(circleGeoShape?.Radius);
				case IEnvelopeGeoShape envelopeGeoShape:
					return envelopeGeoShape.Coordinates == null;
				case IGeometryCollection geometryCollection:
					return geometryCollection.Geometries == null;
				case ILineStringGeoShape lineStringGeoShape:
					return lineStringGeoShape.Coordinates == null;
				case IMultiLineStringGeoShape multiLineStringGeoShape:
					return multiLineStringGeoShape.Coordinates == null;
				case IMultiPointGeoShape multiPointGeoShape:
					return multiPointGeoShape.Coordinates == null;
				case IMultiPolygonGeoShape multiPolygonGeoShape:
					return multiPolygonGeoShape.Coordinates == null;
				case IPointGeoShape pointGeoShape:
					return pointGeoShape.Coordinates == null;
				case IPolygonGeoShape polygonGeoShape:
					return polygonGeoShape.Coordinates == null;
				case null:
					return q.IndexedShape.IsConditionless();
				default:
					return true;
			}
		}

		internal override void InternalWrapInContainer(IQueryContainer container) => container.GeoShape = this;
	}

	public class GeoShapeQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<GeoShapeQueryDescriptor<T>, IGeoShapeQuery, T>, IGeoShapeQuery
		where T : class
	{
		GeoShapeRelation? IGeoShapeQuery.Relation { get; set; }
		bool? IGeoShapeQuery.IgnoreUnmapped { get; set; }
		IGeoShape IGeoShapeQuery.Shape { get; set; }
		IFieldLookup IGeoShapeQuery.IndexedShape { get; set; }

		protected override bool Conditionless => GeoShapeQuery.IsConditionless(this);

		/// <inheritdoc cref="IGeoShapeQuery.Relation"/>
		public GeoShapeQueryDescriptor<T> Relation(GeoShapeRelation? relation) => Assign(a => a.Relation = relation);

		/// <inheritdoc cref="IGeoShapeQuery.IgnoreUnmapped"/>
		public GeoShapeQueryDescriptor<T> IgnoreUnmapped(bool? ignoreUnmapped = true) => Assign(a => a.IgnoreUnmapped = ignoreUnmapped);

		/// <inheritdoc cref="IGeoShapeQuery.Shape"/>
		public GeoShapeQueryDescriptor<T> Shape(Func<GeoShapeDescriptor, IGeoShape> selector) =>
			Assign(a => a.Shape = selector?.Invoke(new GeoShapeDescriptor()));

		/// <inheritdoc cref="IGeoShapeQuery.IndexedShape"/>
		public GeoShapeQueryDescriptor<T> IndexedShape(Func<FieldLookupDescriptor<T>, IFieldLookup> selector) =>
			Assign(a => a.IndexedShape = selector?.Invoke(new FieldLookupDescriptor<T>()));
	}

	/// <summary>
	/// Descriptor for building a <see cref="IGeoShape"/>
	/// </summary>
	public class GeoShapeDescriptor : DescriptorBase<GeoShapeDescriptor, IDescriptor>
	{
		public IGeoShape Circle(GeoCoordinate coordinate, string radius) =>
			new CircleGeoShape(coordinate, radius);

		public IGeoShape Envelope(GeoCoordinate topLeftCoordinate, GeoCoordinate bottomRightCoordinate) =>
			new EnvelopeGeoShape(new [] { topLeftCoordinate, bottomRightCoordinate });

		public IGeoShape Envelope(IEnumerable<GeoCoordinate> coordinates) =>
			new EnvelopeGeoShape(coordinates);

		public IGeoShape GeometryCollection(IEnumerable<IGeoShape> geometries) =>
			new GeometryCollection(geometries);

		public IGeoShape GeometryCollection(params IGeoShape[] geometries) =>
			new GeometryCollection(geometries);

		public IGeoShape LineString(IEnumerable<GeoCoordinate> coordinates) =>
			new LineStringGeoShape(coordinates);

		public IGeoShape LineString(params GeoCoordinate[] coordinates) =>
			new LineStringGeoShape(coordinates);

		public IGeoShape MultiLineString(IEnumerable<IEnumerable<GeoCoordinate>> coordinates) =>
			new MultiLineStringGeoShape(coordinates);

		public IGeoShape MultiLineString(params IEnumerable<GeoCoordinate>[] coordinates) =>
			new MultiLineStringGeoShape(coordinates);

		public IGeoShape Point(GeoCoordinate coordinates) => new PointGeoShape(coordinates);

		public IGeoShape MultiPoint(IEnumerable<GeoCoordinate> coordinates) =>
			new MultiPointGeoShape(coordinates);

		public IGeoShape MultiPoint(params GeoCoordinate[] coordinates) =>
			new MultiPointGeoShape(coordinates);

		public IGeoShape Polygon(IEnumerable<IEnumerable<GeoCoordinate>> coordinates) =>
			new PolygonGeoShape(coordinates);

		public IGeoShape Polygon(params IEnumerable<GeoCoordinate>[] coordinates) =>
			new PolygonGeoShape(coordinates);

		public IGeoShape MultiPolygon(IEnumerable<IEnumerable<IEnumerable<GeoCoordinate>>> coordinates) =>
			new MultiPolygonGeoShape(coordinates);

		public IGeoShape MultiPolygon(params IEnumerable<IEnumerable<GeoCoordinate>>[] coordinates) =>
			new MultiPolygonGeoShape(coordinates);
	}
}
