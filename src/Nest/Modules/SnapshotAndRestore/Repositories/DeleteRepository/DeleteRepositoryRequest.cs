using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDeleteRepositoryRequest : IRequest<DeleteRepositoryRequestParameters> { }

	public partial class DeleteRepositoryRequest : RequestBase<DeleteRepositoryRequestParameters>, IDeleteRepositoryRequest
	{
	}

	[DescriptorFor("SnapshotDeleteRepository")]
	public partial class DeleteRepositoryDescriptor : RequestDescriptorBase<DeleteRepositoryDescriptor, DeleteRepositoryRequestParameters>, IDeleteRepositoryRequest
	{
	}
}
