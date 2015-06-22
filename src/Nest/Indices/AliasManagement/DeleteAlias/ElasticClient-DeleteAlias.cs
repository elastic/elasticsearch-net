using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteAliasResponse DeleteAlias(IDeleteAliasRequest deleteAliasRequest)
		{
			return this.Dispatcher.Dispatch<IDeleteAliasRequest, DeleteAliasRequestParameters, DeleteAliasResponse>(
				deleteAliasRequest,
				(p, d) => this.RawDispatch.IndicesDeleteAliasDispatch<DeleteAliasResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IDeleteAliasResponse> DeleteAliasAsync(IDeleteAliasRequest deleteAliasRequest)
		{
			return this.Dispatcher.DispatchAsync<IDeleteAliasRequest, DeleteAliasRequestParameters, DeleteAliasResponse, IDeleteAliasResponse>(
				deleteAliasRequest,
				(p, d) => this.RawDispatch.IndicesDeleteAliasDispatchAsync<DeleteAliasResponse>(p)
			);
		}

		/// <inheritdoc />
		public IDeleteAliasResponse DeleteAlias<T>(Func<DeleteAliasDescriptor<T>, DeleteAliasDescriptor<T>> deleteAliasDescriptor)
			where T : class
		{
			return this.Dispatcher.Dispatch<DeleteAliasDescriptor<T>, DeleteAliasRequestParameters, DeleteAliasResponse>(
				deleteAliasDescriptor,
				(p, d) => this.RawDispatch.IndicesDeleteAliasDispatch<DeleteAliasResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IDeleteAliasResponse> DeleteAliasAsync<T>(Func<DeleteAliasDescriptor<T>, DeleteAliasDescriptor<T>> deleteAliasDescriptor)
			where T : class
		{
			return this.Dispatcher.DispatchAsync<DeleteAliasDescriptor<T>, DeleteAliasRequestParameters, DeleteAliasResponse, IDeleteAliasResponse>(
				deleteAliasDescriptor,
				(p, d) => this.RawDispatch.IndicesDeleteAliasDispatchAsync<DeleteAliasResponse>(p)
			);
		}

	}
}