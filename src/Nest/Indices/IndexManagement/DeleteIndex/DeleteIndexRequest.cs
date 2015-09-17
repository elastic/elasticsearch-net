using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDeleteIndexRequest : IRequest<DeleteIndexRequestParameters> { }

	public partial class DeleteIndexRequest : RequestBase<DeleteIndexRequestParameters>, IDeleteIndexRequest
	{
	}

	[DescriptorFor("IndicesDelete")]
	public partial class DeleteIndexDescriptor : RequestDescriptorBase<DeleteIndexDescriptor, DeleteIndexRequestParameters>, IDeleteIndexRequest
	{
		public DeleteIndexDescriptor(Indices indices) : base(indices) { }
	}
}
