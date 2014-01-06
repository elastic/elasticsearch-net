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
	public partial class ScrollDescriptor<T> 
		where T : class
	{
		internal new ElasticSearchPathInfo<ScrollQueryString> ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = new ElasticSearchPathInfo<ScrollQueryString>();
			// force POST scrollId can be quite big
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
			// force scroll id out of querystring
			this._QueryString.NameValueCollection.Remove("scroll_id");
			pathInfo.QueryString = this._QueryString;
			return pathInfo;
		}
	}
}
