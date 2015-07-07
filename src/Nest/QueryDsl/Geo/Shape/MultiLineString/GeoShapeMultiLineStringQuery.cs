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

	public class GeoShapeMultiLineStringQuery : FieldNameQuery, IGeoShapeMultiLineStringQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public IMultiLineStringGeoShape Shape { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.GeoShape = this;
		internal static bool IsConditionless(IGeoShapeMultiLineStringQuery q) => q.Field.IsConditionless() || q.Shape == null || !q.Shape.Coordinates.HasAny();
	}

	public class GeoShapeMultiLineStringQueryDescriptor<T> : IGeoShapeMultiLineStringQuery where T : class
	{
		private IGeoShapeMultiLineStringQuery Self => this;
		string IQuery.Name { get; set; }
		bool IQuery.Conditionless => GeoShapeMultiLineStringQuery.IsConditionless(this);
		PropertyPath IFieldNameQuery.Field { get; set; }
		IMultiLineStringGeoShape IGeoShapeMultiLineStringQuery.Shape { get; set; }

		public GeoShapeMultiLineStringQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}
		
		public GeoShapeMultiLineStringQueryDescriptor<T> OnField(string field)
		{
			Self.Field = field;
			return this;
		}
		public GeoShapeMultiLineStringQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			Self.Field = objectPath;
			return this;
		}

		public GeoShapeMultiLineStringQueryDescriptor<T> Coordinates(IEnumerable<IEnumerable<IEnumerable<double>>> coordinates)
		{
			if (Self.Shape == null)
				Self.Shape = new MultiLineStringGeoShape();
			Self.Shape.Coordinates = coordinates;
			return this;
		}
	}
}
