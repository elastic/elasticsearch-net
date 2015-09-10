using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IVerifyRepositoryRequest : IRepositoryPath<VerifyRepositoryRequestParameters> { }

	public partial class VerifyRepositoryRequest : RepositoryPathBase<VerifyRepositoryRequestParameters>, IVerifyRepositoryRequest
	{
		public VerifyRepositoryRequest(string repositoryName) : base(repositoryName) { }

	}

	[DescriptorFor("SnapshotVerifyRepository")]
	public partial class VerifyRepositoryDescriptor : RepositoryPathDescriptor<VerifyRepositoryDescriptor, VerifyRepositoryRequestParameters>, IVerifyRepositoryRequest
	{

	}
}
