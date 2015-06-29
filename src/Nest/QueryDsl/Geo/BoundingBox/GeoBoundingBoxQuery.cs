using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoBoundingBoxQuery : IFieldNameQuery
	{
		[JsonProperty("top_left")]
		string TopLeft { get; set; }

		[JsonProperty("bottom_right")]
		string BottomRight { get; set; }
		
		[JsonProperty("type")]
		GeoExecution? Type { get; set; }
	}
	
	public class GeoBoundingBoxQuery : FieldNameQuery, IGeoBoundingBoxQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public string TopLeft { get; set; }
		public string BottomRight { get; set; }
		public GeoExecution? Type { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.GeoBoundingBox = this;

		internal static bool IsConditionless(IGeoBoundingBoxQuery q)
		{
			return q.Field.IsConditionless() 
				|| q.TopLeft.IsNullOrEmpty() 
				|| q.BottomRight.IsNullOrEmpty();
		}
	}

	public class GeoBoundingBoxQueryDescriptor<T> : IGeoBoundingBoxQuery where T : class
	{
		private IGeoBoundingBoxQuery Self => this;
		string IQuery.Name { get; set; }
		bool IQuery.Conditionless => GeoBoundingBoxQuery.IsConditionless(this);
		PropertyPathMarker IFieldNameQuery.Field { get; set; }
		string IGeoBoundingBoxQuery.TopLeft { get; set; }
		string IGeoBoundingBoxQuery.BottomRight { get; set; }
		GeoExecution? IGeoBoundingBoxQuery.Type { get; set; }

		public GeoBoundingBoxQueryDescriptor<T> Name (string name)
		{
			Self.Name = name;
			return this;
		}

		public GeoBoundingBoxQueryDescriptor<T> Field(string field)
		{
			Self.Field = field;
			return this;
		}

		public GeoBoundingBoxQueryDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			Self.Field = field;
			return this;
		}

		public GeoBoundingBoxQueryDescriptor<T> TopLeft(string topLeft)
		{
			Self.TopLeft = topLeft;
			return this;
		}

		public GeoBoundingBoxQueryDescriptor<T> BottomRight(string bottomRight)
		{
			Self.BottomRight = bottomRight;
			return this;
		}

		public GeoBoundingBoxQueryDescriptor<T> Type(GeoExecution type)
		{
			Self.Type = type;
			return this;
		}
    }
}
