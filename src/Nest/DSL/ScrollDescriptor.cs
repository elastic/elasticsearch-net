using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;

namespace Nest
{
	public interface IScrollRequest : IRequest<ScrollRequestParameters>
	{
		string ScrollId { get; set; }
		string Scroll { get; set; }
	}

	internal static class ScrollRequestPathInfo
	{
		public static void UpdatePathInfo(
			IScrollRequest request,
			IConnectionSettingsValues settings, 
			ElasticsearchPathInfo<ScrollRequestParameters> pathInfo)
		{
			// force POST scrollId can be quite big
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
			pathInfo.ScrollId = request.ScrollId;
			// force scroll id out of RequestParameters (potentially very large)
			request.RequestParameters.RemoveQueryString("scroll_id");
			request.RequestParameters.AddQueryString("scroll", request.Scroll);
		}
	}

	public partial class ScrollRequest : BaseRequest<ScrollRequestParameters>, IScrollRequest
	{
		public string ScrollId { get; set; }
		public string Scroll { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ScrollRequestParameters> pathInfo)
		{
			ScrollRequestPathInfo.UpdatePathInfo(this, settings, pathInfo);
		}
	}


	public partial class ScrollDescriptor<T> : BasePathDescriptor<ScrollDescriptor<T>, ScrollRequestParameters>, IScrollRequest,
		IHideObjectMembers
		where T : class
	{
		private IScrollRequest Self { get { return this; } }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ScrollRequestParameters> pathInfo)
		{
			ScrollRequestPathInfo.UpdatePathInfo(this, settings, pathInfo);
		}

		string IScrollRequest.ScrollId { get; set; }
		string IScrollRequest.Scroll { get; set; }
		
		///<summary>Specify how long a consistent view of the index should be maintained for scrolled search</summary>
		public ScrollDescriptor<T> Scroll(string scroll)
		{
			Self.Scroll = scroll;
			return this;
		}
		
		///<summary>The scroll id used to continue/start the scrolled pagination</summary>
		public ScrollDescriptor<T> ScrollId(string scrollId)
		{
			Self.ScrollId = scrollId;
			return this;
		}
	}
}
