using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetRepositoryRequest : IRequest<GetRepositoryRequestParameters>
	{
	}

	public partial class GetRepositoryRequest : RequestBase<GetRepositoryRequestParameters>, IGetRepositoryRequest
	{
	}

	[DescriptorFor("SnapshotGetRepository")]
	public partial class GetRepositoryDescriptor : RequestDescriptorBase<GetRepositoryDescriptor, GetRepositoryRequestParameters>, IGetRepositoryRequest
	{
	}
}
