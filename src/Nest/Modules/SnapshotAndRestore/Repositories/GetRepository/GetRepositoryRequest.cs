using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetRepositoryRequest : IRepositoryOptionalPath<GetRepositoryRequestParameters>
	{
	}

	public partial class GetRepositoryRequest : RepositoryOptionalPathBase<GetRepositoryRequestParameters>, IGetRepositoryRequest
	{
		public GetRepositoryRequest() { }
		public GetRepositoryRequest(string repositoryName) : base(repositoryName) { }

	}

	[DescriptorFor("SnapshotGetRepository")]
	public partial class GetRepositoryDescriptor : RepositoryOptionalPathDescriptor<GetRepositoryDescriptor, GetRepositoryRequestParameters>, IGetRepositoryRequest
	{
	}
}
