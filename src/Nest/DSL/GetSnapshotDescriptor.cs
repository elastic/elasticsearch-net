using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	[DescriptorFor("SnapshotGet")]
	public partial class GetSnapshotDescriptor : RepositorySnapshotPathDescriptor<GetSnapshotDescriptor, GetSnapshotRequestParameters>
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetSnapshotRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}

	}
}
