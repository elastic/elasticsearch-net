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

	public class ScrollRequest : BaseRequest<ScrollRequestParameters>, IScrollRequest
	{
		public string ScrollId { get; set; }
		public string Scroll { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ScrollRequestParameters> pathInfo)
		{
			// force POST scrollId can be quite big
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
			pathInfo.ScrollId = this.ScrollId ?? this.Request.RequestParameters.GetQueryStringValue<string>("scroll_id");
			// force scroll id out of RequestParameters (potentially very large)
			this.Request.RequestParameters.RemoveQueryString("scroll_id");
			this.Request.RequestParameters.AddQueryString("scroll", this.Scroll);
		}
	}


	public partial class ScrollDescriptor<T> : BasePathDescriptor<ScrollDescriptor<T>, ScrollRequestParameters>, IScrollRequest,
		IHideObjectMembers
		where T : class
	{
		private IScrollRequest Self { get { return this; } }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ScrollRequestParameters> pathInfo)
		{
			// force POST scrollId can be quite big
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
			pathInfo.ScrollId = this.Request.RequestParameters.GetQueryStringValue<string>("scroll_id");
			// force scroll id out of RequestParameters (potentially very large)
			this.Request.RequestParameters.RemoveQueryString("scroll_id");
		}

		string IScrollRequest.ScrollId { get; set; }
		string IScrollRequest.Scroll { get; set; }
	}
}
