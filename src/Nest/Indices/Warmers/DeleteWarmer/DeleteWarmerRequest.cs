using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDeleteWarmerRequest : IIndicesOptionalTypesNamePath<DeleteWarmerRequestParameters> { }

	public partial class DeleteWarmerRequest : IndicesOptionalTypesNamePathBase<DeleteWarmerRequestParameters>, IDeleteWarmerRequest
	{
		public DeleteWarmerRequest(string name)
		{
			this.Name = name;
		}
	}

	[DescriptorFor("IndicesDeleteWarmer")]
	public partial class DeleteWarmerDescriptor : IndicesOptionalTypesNamePathDescriptor<DeleteWarmerDescriptor, DeleteWarmerRequestParameters>, IDeleteWarmerRequest
	{
	}
}
