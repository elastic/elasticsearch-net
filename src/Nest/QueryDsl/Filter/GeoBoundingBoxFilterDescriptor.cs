using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoBoundingBoxFilter : IFilter
	{
		PropertyPathMarker Field { get; set; }
		
		[JsonProperty("top_left")]
		string TopLeft { get; set; }

		[JsonProperty("bottom_right")]
		string BottomRight { get; set; }
		
		[JsonProperty("type")]
		GeoExecution? GeoExecution { get; set; }
	}
	
	public class GeoBoundingBoxFilter : PlainFilter, IGeoBoundingBoxFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.GeoBoundingBox = this;
		}

		public PropertyPathMarker Field { get; set; }
		public string TopLeft { get; set; }
		public string BottomRight { get; set; }
		public GeoExecution? GeoExecution { get; set; }
	}

	public class GeoBoundingBoxFilterDescriptor : FilterBase, IGeoBoundingBoxFilter
	{
		bool IFilter.IsConditionless
		{
			get
			{
				var f = ((IGeoBoundingBoxFilter)this);
				return f.Field.IsConditionless() || f.TopLeft.IsNullOrEmpty() || f.BottomRight.IsNullOrEmpty();
			}
		}

		PropertyPathMarker IGeoBoundingBoxFilter.Field { get; set; }

		string IGeoBoundingBoxFilter.TopLeft { get; set; }

		string IGeoBoundingBoxFilter.BottomRight { get; set; }

		GeoExecution? IGeoBoundingBoxFilter.GeoExecution { get; set; }

	}
}
