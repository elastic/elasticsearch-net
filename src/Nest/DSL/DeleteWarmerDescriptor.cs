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
	[DescriptorFor("IndicesDeleteWarmer")]
	public partial class DeleteWarmerDescriptor : 
		IndicesOptionalTypesNamePathDecriptor<DeleteWarmerDescriptor, DeleteWarmerQueryString>
		, IPathInfo<DeleteWarmerQueryString>
	{
		ElasticSearchPathInfo<DeleteWarmerQueryString> IPathInfo<DeleteWarmerQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<DeleteWarmerQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;

			return pathInfo;
		}
	}
}
