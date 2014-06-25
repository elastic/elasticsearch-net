using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Nest.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

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
