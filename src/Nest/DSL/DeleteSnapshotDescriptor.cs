using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	[DescriptorFor("SnapshotDelete")]
	public partial class DeleteSnapshotDescriptor :
		RepositorySnapshotPathDescriptor<DeleteSnapshotDescriptor, DeleteSnapshotRequestParameters>
		, IPathInfo<DeleteSnapshotRequestParameters>
	{

		ElasticsearchPathInfo<DeleteSnapshotRequestParameters> IPathInfo<DeleteSnapshotRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
			
			return pathInfo;
		}

	}
}
