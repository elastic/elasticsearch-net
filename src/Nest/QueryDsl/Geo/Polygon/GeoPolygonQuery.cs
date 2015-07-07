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
		[JsonProperty("points")]
		IEnumerable<string> Points { get; set; }
	}

	public class GeoPolygonQuery : FieldNameQuery, IGeoPolygonQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public IEnumerable<string> Points { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.GeoPolygon = this;
		internal static bool IsConditionless(IGeoPolygonQuery q) => !q.Points.HasAny() || q.Points.All(p => p.IsNullOrEmpty());
	}

	public class GeoPolygonQueryDescriptor<T> : IGeoPolygonQuery
	{
		private IGeoPolygonQuery Self => this;
		string IQuery.Name { get; set; }
		bool IQuery.Conditionless => GeoPolygonQuery.IsConditionless(this);
		PropertyPath IFieldNameQuery.Field { get; set; }
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
	}
}
