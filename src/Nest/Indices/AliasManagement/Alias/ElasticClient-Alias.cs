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
		public IIndicesOperationResponse Alias(IAliasRequest aliasRequest)
		{
			return this.Dispatcher.Dispatch<IAliasRequest, AliasRequestParameters, IndicesOperationResponse>(
				aliasRequest,
				(p, d) => this.LowLevelDispatch.IndicesUpdateAliasesDispatch<IndicesOperationResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IIndicesOperationResponse Alias(Func<AliasDescriptor, AliasDescriptor> aliasSelector)
		{
			return this.Dispatcher.Dispatch<AliasDescriptor, AliasRequestParameters, IndicesOperationResponse>(
				aliasSelector,
				(p, d) => this.LowLevelDispatch.IndicesUpdateAliasesDispatch<IndicesOperationResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> AliasAsync(IAliasRequest aliasRequest)
		{
			return this.Dispatcher.DispatchAsync<IAliasRequest, AliasRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				aliasRequest,
				(p, d) => this.LowLevelDispatch.IndicesUpdateAliasesDispatchAsync<IndicesOperationResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> AliasAsync(Func<AliasDescriptor, AliasDescriptor> aliasSelector)
		{
			return this.Dispatcher.DispatchAsync<AliasDescriptor, AliasRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				aliasSelector,
				(p, d) => this.LowLevelDispatch.IndicesUpdateAliasesDispatchAsync<IndicesOperationResponse>(p, d)
			);
		}

	}
}