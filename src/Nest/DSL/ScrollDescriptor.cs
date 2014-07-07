using Elasticsearch.Net;

namespace Nest
{
	public partial class ScrollDescriptor<T> : BasePathDescriptor<ScrollDescriptor<T>, ScrollRequestParameters>,
		IHideObjectMembers
		where T : class
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ScrollRequestParameters> pathInfo)
		{
			// force POST scrollId can be quite big
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
			pathInfo.ScrollId = this.Request.RequestParameters.GetQueryStringValue<string>("scroll_id");
			// force scroll id out of RequestParameters (potentially very large)
			this.Request.RequestParameters.RemoveQueryString("scroll_id");
		}
	}
}
