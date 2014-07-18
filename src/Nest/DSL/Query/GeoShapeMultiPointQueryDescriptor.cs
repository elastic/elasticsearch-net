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
	public interface IGeoShapeMultiPointQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		IMultiPointGeoShape Shape { get; set; }
	}

	public class GeoShapeMultiPointQuery : PlainQuery, IGeoShapeMultiPointQuery
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

		public IMultiPointGeoShape Shape { get; set; }
	}

	public class GeoShapeMultiPointQueryDescriptor<T> : IGeoShapeMultiPointQuery where T : class
	{
		PropertyPathMarker IGeoShapeQuery.Field { get; set; }
		
		IMultiPointGeoShape IGeoShapeMultiPointQuery.Shape { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IGeoShapeQuery)this).Field.IsConditionless() || ((IGeoShapeMultiPointQuery)this).Shape == null || !((IGeoShapeMultiPointQuery)this).Shape.Coordinates.HasAny();
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
		
		public GeoShapeMultiPointQueryDescriptor<T> OnField(string field)
		{
			((IGeoShapeQuery)this).Field = field;
			return this;
		}
		public GeoShapeMultiPointQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IGeoShapeQuery)this).Field = objectPath;
			return this;
		}

		public GeoShapeMultiPointQueryDescriptor<T> Coordinates(IEnumerable<IEnumerable<double>> coordinates)
		{
			if (((IGeoShapeMultiPointQuery)this).Shape == null)
				((IGeoShapeMultiPointQuery)this).Shape = new MultiPointGeoShape();
			((IGeoShapeMultiPointQuery)this).Shape.Coordinates = coordinates;
			return this;
		}
	}
}
