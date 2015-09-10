using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDeleteRepositoryRequest : IRepositoryPath<DeleteRepositoryRequestParameters> { }

	public partial class DeleteRepositoryRequest : RepositoryPathBase<DeleteRepositoryRequestParameters>, IDeleteRepositoryRequest
	{
		public DeleteRepositoryRequest(string repositoryName) : base(repositoryName) { }
	}

	[DescriptorFor("SnapshotDeleteRepository")]
	public partial class DeleteRepositoryDescriptor : RepositoryPathDescriptor<DeleteRepositoryDescriptor, DeleteRepositoryRequestParameters>, IDeleteRepositoryRequest
	{
	}
}
