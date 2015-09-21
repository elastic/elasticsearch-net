using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IScrollRequest 
	{
		TimeUnitExpression Scroll { get; set; }
	}
	
	//TODO complex old route update routine needs to be ported

	//internal static class ScrollPathInfo
	//{
	//	public static void Update(
	//		IScrollRequest request,
	//		IConnectionSettingsValues settings, 
	//		RouteValues pathInfo)
	//	{
	//		// force POST scrollId can be quite big
	//		pathInfo.HttpMethod = HttpMethod.POST;
	//		pathInfo.ScrollId = request.ScrollId;
	//		// force scroll id out of RequestParameters (potentially very large)
	//		request.RequestParameters.RemoveQueryString("scroll_id");
	//		request.RequestParameters.AddQueryString("scroll", request.Scroll);
	//	}
	//}

	//TODO signal to codegen to not generate constructors for this one
	public partial class ScrollRequest 
	{
		public string ScrollId { get; set; }
		public TimeUnitExpression Scroll { get; set; }

		public ScrollRequest(string scrollId, TimeUnitExpression scrollTimeout)
		{
			this.ScrollId = scrollId;
			this.Scroll = scrollTimeout;
		}
	}

	public partial class ScrollDescriptor<T> where T : class
	{
		TimeUnitExpression IScrollRequest.Scroll { get; set; }

		///<summary>Specify how long a consistent view of the index should be maintained for scrolled search</summary>
		public ScrollDescriptor<T> Scroll(TimeUnitExpression scroll) => Assign(a => a.Scroll = scroll);

		///<summary>The scroll id used to continue/start the scrolled pagination</summary>
		public ScrollDescriptor<T> ScrollId(ScrollIds scrollId) => Assign(a => a.ScrollId = scrollId);
	}
}
