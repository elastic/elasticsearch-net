using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{
	[DescriptorFor("Info")]
	public partial class InfoDescriptor : 
		 IPathInfo<InfoQueryString>
	{
		ElasticSearchPathInfo<InfoQueryString> IPathInfo<InfoQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = new ElasticSearchPathInfo<InfoQueryString>();
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
			pathInfo.QueryString = this._QueryString;

			return pathInfo;
		}
	}
}
