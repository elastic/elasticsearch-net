using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	public partial class ScrollDescriptor<T> :
		IPathInfo<ScrollQueryString>
		where T : class
	{
		ElasticsearchPathInfo<ScrollQueryString> IPathInfo<ScrollQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = new ElasticsearchPathInfo<ScrollQueryString>();
			// force POST scrollId can be quite big
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
			pathInfo.ScrollId = this._QueryString._scroll_id;
			// force scroll id out of querystring (potentially very large)
			this._QueryString._QueryStringDictionary.Remove("scroll_id");
			pathInfo.QueryString = this._QueryString;
			return pathInfo;
		}
	}
}
