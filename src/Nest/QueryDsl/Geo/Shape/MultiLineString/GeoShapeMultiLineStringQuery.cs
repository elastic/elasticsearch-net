using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoShapeMultiLineStringQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		IMultiLineStringGeoShape Shape { get; set; }
	}

	public class GeoShapeMultiLineStringQuery : FieldNameQueryBase, IGeoShapeMultiLineStringQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public IMultiLineStringGeoShape Shape { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.GeoShape = this;
		internal static bool IsConditionless(IGeoShapeMultiLineStringQuery q) => q.Field.IsConditionless() || q.Shape == null || !q.Shape.Coordinates.HasAny();
	}

	public class GeoShapeMultiLineStringQueryDescriptor<T> 
		: FieldNameQueryDescriptorBase<GeoShapeMultiLineStringQueryDescriptor<T>, IGeoShapeMultiLineStringQuery, T>
		, IGeoShapeMultiLineStringQuery where T : class
	{
		bool IQuery.Conditionless => GeoShapeMultiLineStringQuery.IsConditionless(this);
		IMultiLineStringGeoShape IGeoShapeMultiLineStringQuery.Shape { get; set; }

		public GeoShapeMultiLineStringQueryDescriptor<T> Coordinates(IEnumerable<IEnumerable<IEnumerable<double>>> coordinates) => Assign(a =>
		{
			a.Shape = a.Shape ?? new MultiLineStringGeoShape();
			a.Shape.Coordinates = coordinates;
		});
	}
}
