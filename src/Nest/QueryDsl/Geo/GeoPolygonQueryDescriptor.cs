using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

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

		public string Name { get; set; }
		bool IQuery.IsConditionless { get { return QueryCondition.IsConditionless(this); } }
		public PropertyPathMarker Field { get; set; }
		public IEnumerable<string> Points { get; set; }
	}

	// TODO : Finish implementing
	public class GeoPolygonQueryDescriptor : IGeoPolygonQuery
	{
		private IGeoPolygonQuery _ { get { return this; } }

		bool IQuery.IsConditionless { get { return QueryCondition.IsConditionless(this); } }
		PropertyPathMarker IGeoPolygonQuery.Field { get; set; }
		IEnumerable<string> IGeoPolygonQuery.Points { get; set; }
		string IQuery.Name { get; set; }

		public PropertyPathMarker GetFieldName()
		{
			return _.Field;
		}

		public void SetFieldName(string fieldName)
		{
			_.Field = fieldName;
		}
	}
}
