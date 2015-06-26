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
	public interface IGeoShapeMultiPointQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		IMultiPointGeoShape Shape { get; set; }
	}

	public class GeoShapeMultiPointQuery : PlainQuery, IGeoShapeMultiPointQuery
	{
		public string Name { get; set; }
		bool IQuery.IsConditionless { get { return false; } }
		public PropertyPathMarker Field { get; set; }
		public IMultiPointGeoShape Shape { get; set; }

		protected override void WrapInContainer(IQueryContainer container)
		{
			container.GeoShape = this;
		}

		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return this.Field;
		}

		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			this.Field = fieldName;
		}
	}

	public class GeoShapeMultiPointQueryDescriptor<T> : IGeoShapeMultiPointQuery where T : class
	{
		private IGeoShapeMultiPointQuery Self { get { return this; }}
		string IQuery.Name { get; set; }
		bool IQuery.IsConditionless
		{
			get
			{
				return ((IGeoShapeQuery)this).Field.IsConditionless() || Self.Shape == null || !Self.Shape.Coordinates.HasAny();
			}
		}
		PropertyPathMarker IGeoShapeQuery.Field { get; set; }
		IMultiPointGeoShape IGeoShapeMultiPointQuery.Shape { get; set; }

		public GeoShapeMultiPointQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public GeoShapeMultiPointQueryDescriptor<T> OnField(string field)
		{
			Self.Field = field;
			return this;
		}

		public GeoShapeMultiPointQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			Self.Field = objectPath;
			return this;
		}

		public GeoShapeMultiPointQueryDescriptor<T> Coordinates(IEnumerable<IEnumerable<double>> coordinates)
		{
			if (Self.Shape == null)
				Self.Shape = new MultiPointGeoShape();
			Self.Shape.Coordinates = coordinates;
			return this;
		}

		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			Self.Field = fieldName;
		}

		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return Self.Field;
		}
	}
}
