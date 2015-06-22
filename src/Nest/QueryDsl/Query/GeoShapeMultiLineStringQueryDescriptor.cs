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
	public interface IGeoShapeMultiLineStringQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		IMultiLineStringGeoShape Shape { get; set; }
	}

	public class GeoShapeMultiLineStringQuery : PlainQuery, IGeoShapeMultiLineStringQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.GeoShape = this;
		}

		public string Name { get; set; }
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

		public IMultiLineStringGeoShape Shape { get; set; }
	}

	public class GeoShapeMultiLineStringQueryDescriptor<T> : IGeoShapeMultiLineStringQuery where T : class
	{
		private IGeoShapeMultiLineStringQuery Self { get { return this; } }

		PropertyPathMarker IGeoShapeQuery.Field { get; set; }
		
		IMultiLineStringGeoShape IGeoShapeMultiLineStringQuery.Shape { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IGeoShapeQuery)this).Field.IsConditionless() || Self.Shape == null || !Self.Shape.Coordinates.HasAny();
			}
		}

		string IQuery.Name { get; set; }

		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			((IGeoShapeQuery)this).Field = fieldName;
		}
		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return ((IGeoShapeQuery)this).Field;
		}

		public GeoShapeMultiLineStringQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}
		
		public GeoShapeMultiLineStringQueryDescriptor<T> OnField(string field)
		{
			((IGeoShapeQuery)this).Field = field;
			return this;
		}
		public GeoShapeMultiLineStringQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IGeoShapeQuery)this).Field = objectPath;
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
