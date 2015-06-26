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
	public interface IGeoShapeLineStringQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		ILineStringGeoShape Shape { get; set; }
	}

	public class GeoShapeLineStringQuery : PlainQuery, IGeoShapeLineStringQuery
	{
		public string Name { get; set; }
		bool IQuery.IsConditionless { get { return false; } }
		public PropertyPathMarker Field { get; set; }
		public ILineStringGeoShape Shape { get; set; }

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

	public class GeoShapeLineStringQueryDescriptor<T> : IGeoShapeLineStringQuery where T : class
	{
		private IGeoShapeLineStringQuery Self { get { return this;}}
		string IQuery.Name { get; set; }
		bool IQuery.IsConditionless
		{
			get
			{
				return ((IGeoShapeQuery)this).Field.IsConditionless() || Self.Shape == null || !Self.Shape.Coordinates.HasAny();
			}
		}
		PropertyPathMarker IGeoShapeQuery.Field { get; set; }
		ILineStringGeoShape IGeoShapeLineStringQuery.Shape { get; set; }
		
		public GeoShapeLineStringQueryDescriptor<T> OnField(string field)
		{
			Self.Field = field;
			return this;
		}

		public GeoShapeLineStringQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			Self.Field = objectPath;
			return this;
		}

		public GeoShapeLineStringQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public GeoShapeLineStringQueryDescriptor<T> Coordinates(IEnumerable<IEnumerable<double>> coordinates)
		{
			if (Self.Shape == null)
				Self.Shape = new LineStringGeoShape();
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
