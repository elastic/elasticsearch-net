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
	public partial class ScrollDescriptor<T> :
		IPathInfo<ScrollRequestParameters>,
		IHideObjectMembers
		where T : class
	{
		ElasticsearchPathInfo<ScrollRequestParameters> IPathInfo<ScrollRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = new ElasticsearchPathInfo<ScrollRequestParameters>();
			// force POST scrollId can be quite big
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
			pathInfo.ScrollId = this._QueryString._scroll_id;
			// force scroll id out of RequestParameters (potentially very large)
			this._QueryString._QueryStringDictionary.Remove("scroll_id");
			pathInfo.RequestParameters = this._QueryString;
			return pathInfo;
		}
	}
}
