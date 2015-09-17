using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IVerifyRepositoryRequest : IRequest<VerifyRepositoryRequestParameters> { }

	public partial class VerifyRepositoryRequest : RequestBase<VerifyRepositoryRequestParameters>, IVerifyRepositoryRequest
	{
	}

	[DescriptorFor("SnapshotVerifyRepository")]
	public partial class VerifyRepositoryDescriptor : RequestDescriptorBase<VerifyRepositoryDescriptor, VerifyRepositoryRequestParameters>, IVerifyRepositoryRequest
	{

	}
}
