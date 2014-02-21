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
		ElasticsearchPathInfo<InfoQueryString> IPathInfo<InfoQueryString>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = new ElasticsearchPathInfo<InfoQueryString>();
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
			pathInfo.QueryString = this._QueryString;

			return pathInfo;
		}
	}
}
