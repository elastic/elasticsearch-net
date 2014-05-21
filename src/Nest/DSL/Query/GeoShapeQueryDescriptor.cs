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
	public interface IGeoShapeQuery : IFieldNameQuery
	{
		PropertyPathMarker Field { get; set; }

		[JsonProperty("shape")]
		GeoShapeVector Shape { get; set; }
	}

	public class GeoShapeQuery : PlainQuery, IGeoShapeQuery
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
		public GeoShapeVector Shape { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class GeoShapeQueryDescriptor<T> : IGeoShapeQuery where T : class
	{
		PropertyPathMarker IGeoShapeQuery.Field { get; set; }
		
		GeoShapeVector IGeoShapeQuery.Shape { get; set; }
		
		bool IQuery.IsConditionless
		{
			get
			{
				return ((IGeoShapeQuery)this).Field.IsConditionless() || (((IGeoShapeQuery)this).Shape == null || !((IGeoShapeQuery)this).Shape.Coordinates.HasAny());
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
		
		public GeoShapeQueryDescriptor<T> OnField(string field)
		{
			((IGeoShapeQuery)this).Field = field;
			return this;
		}
		public GeoShapeQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IGeoShapeQuery)this).Field = objectPath;
			return this;
		}
		

		public GeoShapeQueryDescriptor<T> Type(string type)
		{
			if (((IGeoShapeQuery)this).Shape == null)
				((IGeoShapeQuery)this).Shape = new GeoShapeVector();
			((IGeoShapeQuery)this).Shape.Type = type;
			return this;
		}

		public GeoShapeQueryDescriptor<T> Coordinates(IEnumerable<IEnumerable<double>> coordinates)
		{
			if (((IGeoShapeQuery)this).Shape == null)
				((IGeoShapeQuery)this).Shape = new GeoShapeVector();
			((IGeoShapeQuery)this).Shape.Coordinates = coordinates;
			return this;
		}

	}

}
