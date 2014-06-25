using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("SnapshotDeleteRepository")]
	public partial class DeleteRepositoryDescriptor : RepositoryPathDescriptor<DeleteRepositoryDescriptor, DeleteRepositoryRequestParameters>
	{
		internal IRepository _Repository { get; private set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteRepositoryRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
		}

	}
}
