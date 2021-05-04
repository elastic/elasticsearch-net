// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(CompositeFormatter<IShapeQuery, ShapeQueryFormatter, ShapeQueryFieldNameFormatter>))]
	public interface IShapeQuery : IFieldNameQuery
	{
		/// <summary>
		/// Will ignore an unmapped field and will not match any documents for this query.
		/// This can be useful when querying multiple indexes which might have different mappings.
		/// </summary>
		[DataMember(Name ="ignore_unmapped")]
		bool? IgnoreUnmapped { get; set; }

		/// <summary>
		/// Indexed geo shape to search with
		/// </summary>
		[DataMember(Name ="indexed_shape")]
		IFieldLookup IndexedShape { get; set; }

		/// <summary>
		/// Controls the spatial relation operator to use at search time.
		/// </summary>
		[DataMember(Name ="relation")]
		ShapeRelation? Relation { get; set; }

		/// <summary>
		/// The geo shape to search with
		/// </summary>
		[DataMember(Name ="shape")]
		IGeoShape Shape { get; set; }
	}

	public class ShapeQuery : FieldNameQueryBase, IShapeQuery
	{
		/// <inheritdoc />
		public bool? IgnoreUnmapped { get; set; }

		/// <inheritdoc />
		public IFieldLookup IndexedShape { get; set; }

		/// <inheritdoc />
		public ShapeRelation? Relation { get; set; }

		/// <inheritdoc />
		public IGeoShape Shape { get; set; }

		protected override bool Conditionless => IsConditionless(this);

		internal static bool IsConditionless(IShapeQuery q)
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

		internal override void InternalWrapInContainer(IQueryContainer container) => container.Shape = this;
	}

	public class ShapeQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<ShapeQueryDescriptor<T>, IShapeQuery, T>, IShapeQuery
		where T : class
	{
		protected override bool Conditionless => ShapeQuery.IsConditionless(this);
		bool? IShapeQuery.IgnoreUnmapped { get; set; }
		IFieldLookup IShapeQuery.IndexedShape { get; set; }
		ShapeRelation? IShapeQuery.Relation { get; set; }
		IGeoShape IShapeQuery.Shape { get; set; }

		/// <inheritdoc cref="IShapeQuery.Relation" />
		public ShapeQueryDescriptor<T> Relation(ShapeRelation? relation) =>
			Assign(relation, (a, v) => a.Relation = v);

		/// <inheritdoc cref="IShapeQuery.IgnoreUnmapped" />
		public ShapeQueryDescriptor<T> IgnoreUnmapped(bool? ignoreUnmapped = true) =>
			Assign(ignoreUnmapped, (a, v) => a.IgnoreUnmapped = v);

		/// <inheritdoc cref="IShapeQuery.Shape" />
		public ShapeQueryDescriptor<T> Shape(Func<GeoShapeDescriptor, IGeoShape> selector) =>
			Assign(selector, (a, v) => a.Shape = v?.Invoke(new GeoShapeDescriptor()));

		/// <inheritdoc cref="IShapeQuery.IndexedShape" />
		public ShapeQueryDescriptor<T> IndexedShape(Func<FieldLookupDescriptor<T>, IFieldLookup> selector) =>
			Assign(selector, (a, v) => a.IndexedShape = v?.Invoke(new FieldLookupDescriptor<T>()));
	}
}
