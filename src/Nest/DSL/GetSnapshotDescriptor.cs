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
	public partial class GetSnapshotDescriptor :
		RepositorySnapshotPathDescriptor<GetSnapshotDescriptor, GetSnapshotRequestParameters>
		, IPathInfo<GetSnapshotRequestParameters>
	{

		ElasticsearchPathInfo<GetSnapshotRequestParameters> IPathInfo<GetSnapshotRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo<GetSnapshotRequestParameters>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
			
			return pathInfo;
		}

	}
}
