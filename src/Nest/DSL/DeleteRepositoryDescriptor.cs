using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("SnapshotDeleteRepository")]
	public partial class DeleteRepositoryDescriptor :
		RepositoryPathDescriptor<DeleteRepositoryDescriptor, DeleteRepositoryRequestParameters>
		, IPathInfo<DeleteRepositoryRequestParameters>
	{
		internal IRepository _Repository { get; private set; } 

		ElasticsearchPathInfo<DeleteRepositoryRequestParameters> IPathInfo<DeleteRepositoryRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo<DeleteRepositoryRequestParameters>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
			
			return pathInfo;
		}

	}
}
