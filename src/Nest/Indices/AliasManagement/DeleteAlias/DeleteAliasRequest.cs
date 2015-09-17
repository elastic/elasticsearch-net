using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IDeleteAliasRequest : IRequest<DeleteAliasRequestParameters> { }

	public partial class DeleteAliasRequest : RequestBase<DeleteAliasRequestParameters>, IDeleteAliasRequest
	{
	}

	[DescriptorFor("IndicesDeleteAlias")]
	public partial class DeleteAliasDescriptor<T> 
		: RequestDescriptorBase<DeleteAliasDescriptor<T>, DeleteAliasRequestParameters>, IDeleteAliasRequest
		where T : class
	{
	}
}
