using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IDeleteAliasRequest : IIndexNamePath<DeleteAliasRequestParameters> { }

	public partial class DeleteAliasRequest : IndexNamePathBase<DeleteAliasRequestParameters>, IDeleteAliasRequest
	{
		public DeleteAliasRequest(string index, string name) : base(index, name) { }
	}

	[DescriptorFor("IndicesDeleteAlias")]
	public partial class DeleteAliasDescriptor<T> 
		: IndexNamePathDescriptor<DeleteAliasDescriptor<T>, DeleteAliasRequestParameters, T>, IDeleteAliasRequest
		where T : class
	{
	}
}
