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

		public IMultiPointGeoShape Shape { get; set; }
	}

	public class GeoShapeMultiPointQueryDescriptor<T> : IGeoShapeMultiPointQuery where T : class
	{
		private IGeoShapeMultiPointQuery Self { get { return this; }}

		PropertyPathMarker IGeoShapeQuery.Field { get; set; }
		
		IMultiPointGeoShape IGeoShapeMultiPointQuery.Shape { get; set; }
			
		string IQuery.Name { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IGeoShapeQuery)this).Field.IsConditionless() || Self.Shape == null || !Self.Shape.Coordinates.HasAny();
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

		public GeoShapeMultiPointQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
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
			if (Self.Shape == null)
				Self.Shape = new MultiPointGeoShape();
			Self.Shape.Coordinates = coordinates;
			return this;
		}
	}
}
