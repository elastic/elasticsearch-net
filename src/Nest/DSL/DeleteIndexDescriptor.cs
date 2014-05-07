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
	[DescriptorFor("IndicesDelete")]
	public partial class DeleteIndexDescriptor : 
		IndicesOptionalPathDescriptor<DeleteIndexDescriptor, DeleteIndexRequestParameters>
		, IPathInfo<DeleteIndexRequestParameters>
	{
		ElasticsearchPathInfo<DeleteIndexRequestParameters> IPathInfo<DeleteIndexRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;

			return pathInfo;
		}
	}
}
