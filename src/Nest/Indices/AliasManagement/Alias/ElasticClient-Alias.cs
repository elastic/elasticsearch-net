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
		/// <inheritdoc/>
		public IIndicesOperationResponse Alias(IBulkAliasRequest aliasRequest)
		{
			return this.Dispatcher.Dispatch<IBulkAliasRequest, BulkAliasRequestParameters, IndicesOperationResponse>(
				aliasRequest,
				(p, d) => this.LowLevelDispatch.IndicesUpdateAliasesDispatch<IndicesOperationResponse>(p, d)
			);
		}

		/// <inheritdoc/>
		public IIndicesOperationResponse Alias(Func<BulkAliasDescriptor, BulkAliasDescriptor> aliasSelector)
		{
			return this.Dispatcher.Dispatch<BulkAliasDescriptor, BulkAliasRequestParameters, IndicesOperationResponse>(
				aliasSelector,
				(p, d) => this.LowLevelDispatch.IndicesUpdateAliasesDispatch<IndicesOperationResponse>(p, d)
			);
		}

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> AliasAsync(IBulkAliasRequest aliasRequest)
		{
			return this.Dispatcher.DispatchAsync<IBulkAliasRequest, BulkAliasRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				aliasRequest,
				(p, d) => this.LowLevelDispatch.IndicesUpdateAliasesDispatchAsync<IndicesOperationResponse>(p, d)
			);
		}

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> AliasAsync(Func<BulkAliasDescriptor, BulkAliasDescriptor> aliasSelector)
		{
			return this.Dispatcher.DispatchAsync<BulkAliasDescriptor, BulkAliasRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				aliasSelector,
				(p, d) => this.LowLevelDispatch.IndicesUpdateAliasesDispatchAsync<IndicesOperationResponse>(p, d)
			);
		}

	}
}