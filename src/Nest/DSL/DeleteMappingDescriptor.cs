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
	public partial class DeleteMappingDescriptor : IndexTypePathDescriptor<DeleteMappingDescriptor, DeleteMappingRequestParameters>
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteMappingRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
		}
	}
}
