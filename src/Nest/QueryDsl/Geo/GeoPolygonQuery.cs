using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoPolygonQuery : IFieldNameQuery
	{
		PropertyPathMarker Field { get; set; }

		[JsonProperty("points")]
		IEnumerable<string> Points { get; set; }
	}

	public class GeoPolygonQuery : PlainQuery, IGeoPolygonQuery
	{
		public string Name { get; set; }
		bool IQuery.IsConditionless { get { return QueryCondition.IsConditionless(this); } }
		public PropertyPathMarker Field { get; set; }
		public IEnumerable<string> Points { get; set; }

		protected override void WrapInContainer(IQueryContainer container)
		{
			container.GeoPolygon = this;
		}

		public PropertyPathMarker GetFieldName()
		{
			return Field;
		}

		public void SetFieldName(string fieldName)
		{
			Field = fieldName;
		}
	}

	public class GeoPolygonQueryDescriptor<T> : IGeoPolygonQuery
	{
		private IGeoPolygonQuery Self { get { return this; } }
		string IQuery.Name { get; set; }
		bool IQuery.IsConditionless { get { return QueryCondition.IsConditionless(this); } }
		PropertyPathMarker IGeoPolygonQuery.Field { get; set; }
		IEnumerable<string> IGeoPolygonQuery.Points { get; set; }

		public GeoPolygonQueryDescriptor<T> Field(string field)
		{
			Self.Field = field;
			return this;
		}

		public GeoPolygonQueryDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			Self.Field = field;
			return this;
		}

		public GeoPolygonQueryDescriptor<T> Points(IEnumerable<string> points)
		{
			Self.Points = points;
			return this;
		}

		public GeoPolygonQueryDescriptor<T> Points(params string[] points)
		{
			Self.Points = points;
			return this;
		}

		public PropertyPathMarker GetFieldName()
		{
			return Self.Field;
		}

		public void SetFieldName(string fieldName)
		{
			Self.Field = fieldName;
		}
	}
}
