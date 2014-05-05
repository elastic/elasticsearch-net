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
	[DescriptorFor("IndicesDeleteMapping")]
	public partial class DeleteMappingDescriptor : 
		IndexTypePathDescriptor<DeleteMappingDescriptor, DeleteMappingRequestParameters>
		, IPathInfo<DeleteMappingRequestParameters>
	{
		ElasticsearchPathInfo<DeleteMappingRequestParameters> IPathInfo<DeleteMappingRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;

			return pathInfo;
		}
	}
}
