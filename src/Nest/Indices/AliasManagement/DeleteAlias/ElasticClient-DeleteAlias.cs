using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Delete an index alias
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-aliases.html#deleting
		/// </summary>
		/// <param name="deleteAliasRequest">A descriptor that describes the delete alias request</param>
		IDeleteAliasResponse DeleteAlias(IDeleteAliasRequest deleteAliasRequest);

		/// <inheritdoc/>
		Task<IDeleteAliasResponse> DeleteAliasAsync(IDeleteAliasRequest deleteAliasRequest);

		/// <inheritdoc/>
		IDeleteAliasResponse DeleteAlias(Indices indices, Names names, Func<DeleteAliasDescriptor, IDeleteAliasRequest> deleteAliasDescriptor = null);

		/// <inheritdoc/>
		Task<IDeleteAliasResponse> DeleteAliasAsync(Indices indices, Names names, Func<DeleteAliasDescriptor, IDeleteAliasRequest> deleteAliasDescriptor = null);
	}


	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IDeleteAliasResponse DeleteAlias(IDeleteAliasRequest deleteAliasRequest) => 
			this.Dispatcher.Dispatch<IDeleteAliasRequest, DeleteAliasRequestParameters, DeleteAliasResponse>(
				deleteAliasRequest,
				(p, d) => this.LowLevelDispatch.IndicesDeleteAliasDispatch<DeleteAliasResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IDeleteAliasResponse> DeleteAliasAsync(IDeleteAliasRequest deleteAliasRequest) => 
			this.Dispatcher.DispatchAsync<IDeleteAliasRequest, DeleteAliasRequestParameters, DeleteAliasResponse, IDeleteAliasResponse>(
				deleteAliasRequest,
				(p, d) => this.LowLevelDispatch.IndicesDeleteAliasDispatchAsync<DeleteAliasResponse>(p)
			);

		/// <inheritdoc/>
		public IDeleteAliasResponse DeleteAlias(Indices indices, Names names, Func<DeleteAliasDescriptor, IDeleteAliasRequest> deleteAliasDescriptor = null) =>
			this.DeleteAlias(deleteAliasDescriptor.InvokeOrDefault(new DeleteAliasDescriptor(indices, names)));

		/// <inheritdoc/>
		public Task<IDeleteAliasResponse> DeleteAliasAsync(Indices indices, Names names, Func<DeleteAliasDescriptor, IDeleteAliasRequest> deleteAliasDescriptor = null) =>
			this.DeleteAliasAsync(deleteAliasDescriptor.InvokeOrDefault(new DeleteAliasDescriptor(indices, names)));
	}
}