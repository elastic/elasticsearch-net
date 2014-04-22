using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	public interface IPrefixFilter : IFilterBase
	{
		PropertyPathMarker Field { get; set; }
		string Prefix { get; set; }
	}
	public class PrefixFilter : FilterBase, IPrefixFilter
	{
		bool IFilterBase.IsConditionless { get { return ((IPrefixFilter)this).Field.IsConditionless() || ((IPrefixFilter)this).Prefix.IsNullOrEmpty(); } }

		PropertyPathMarker IPrefixFilter.Field { get; set; }
		string IPrefixFilter.Prefix { get; set; }
	} 

	public interface IQueryFilter : IFilterBase
	{
		
	}
	public class QueryFilter : FilterBase, IQueryFilter
	{
		
	}



	public interface IGeoBoundingBoxFilter : IFilterBase
	{
		PropertyPathMarker Field { get; set; }
		
		[JsonProperty("top_left")]
		string TopLeft { get; set; }

		[JsonProperty("bottom_right")]
		string BottomRight { get; set; }
		
		[JsonProperty("type")]
		GeoExecution? GeoExecution { get; set; }
	}

	public class GeoBoundingBoxFilter : FilterBase, IGeoBoundingBoxFilter
	{
		bool IFilterBase.IsConditionless
		{
			get
			{
				return ((IGeoBoundingBoxFilter)this).TopLeft.IsNullOrEmpty() || ((IGeoBoundingBoxFilter)this).BottomRight.IsNullOrEmpty();
			}
		}

		PropertyPathMarker IGeoBoundingBoxFilter.Field { get; set; }

		string IGeoBoundingBoxFilter.TopLeft { get; set; }

		string IGeoBoundingBoxFilter.BottomRight { get; set; }

		GeoExecution? IGeoBoundingBoxFilter.GeoExecution { get; set; }

	}
}
