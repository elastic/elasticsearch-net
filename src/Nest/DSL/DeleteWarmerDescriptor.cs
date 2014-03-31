using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{
	[DescriptorFor("IndicesDeleteWarmer")]
	public partial class DeleteWarmerDescriptor : 
		IndicesOptionalTypesNamePathDecriptor<DeleteWarmerDescriptor, DeleteWarmerRequestParameters>
		, IPathInfo<DeleteWarmerRequestParameters>
	{
		ElasticsearchPathInfo<DeleteWarmerRequestParameters> IPathInfo<DeleteWarmerRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo<DeleteWarmerRequestParameters>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;

			return pathInfo;
		}
	}
}
