using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Nest.DSL.Query.Behaviour;
using Nest.Resolvers;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoShapePointQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		IPointGeoShape Shape { get; set; }
	}

	public class GeoShapePointQuery : PlainQuery, IGeoShapePointQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.GeoShape = this;
		}

		bool IQuery.IsConditionless { get { return false; } }

		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return this.Field;
		}

		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			this.Field = fieldName;
		}

		public PropertyPathMarker Field { get; set; }

		public IPointGeoShape Shape { get; set; }
	}

	public class GeoShapePointQueryDescriptor<T> : IGeoShapePointQuery where T : class
	{
		PropertyPathMarker IGeoShapeQuery.Field { get; set; }
		
		IPointGeoShape IGeoShapePointQuery.Shape { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IGeoShapeQuery)this).Field.IsConditionless() || ((IGeoShapePointQuery)this).Shape == null || !((IGeoShapePointQuery)this).Shape.Coordinates.HasAny();
			}

		}
		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			((IGeoShapeQuery)this).Field = fieldName;
		}
		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return ((IGeoShapeQuery)this).Field;
		}
		
		public GeoShapePointQueryDescriptor<T> OnField(string field)
		{
			((IGeoShapeQuery)this).Field = field;
			return this;
		}
		public GeoShapePointQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IGeoShapeQuery)this).Field = objectPath;
			return this;
		}

		public GeoShapePointQueryDescriptor<T> Coordinates(IEnumerable<double> coordinates)
		{
			if (((IGeoShapePointQuery)this).Shape == null)
				((IGeoShapePointQuery)this).Shape = new PointGeoShape();
			((IGeoShapePointQuery)this).Shape.Coordinates = coordinates;
			return this;
		}
	}
}
