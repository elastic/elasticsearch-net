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
		public IPutAliasResponse PutAlias(IPutAliasRequest putAliasRequest)
		{
			return this.Dispatcher.Dispatch<IPutAliasRequest, PutAliasRequestParameters, PutAliasResponse>(
				putAliasRequest,
				(p, d) => this.LowLevelDispatch.IndicesPutAliasDispatch<PutAliasResponse>(p, d)
			);
		}

		/// <inheritdoc/>
		public Task<IPutAliasResponse> PutAliasAsync(IPutAliasRequest putAliasRequest)
		{
			return this.Dispatcher.DispatchAsync<IPutAliasRequest, PutAliasRequestParameters, PutAliasResponse, IPutAliasResponse>(
				putAliasRequest,
				(p, d) => this.LowLevelDispatch.IndicesPutAliasDispatchAsync<PutAliasResponse>(p, d)
			);
		}

		/// <inheritdoc/>
		public IPutAliasResponse PutAlias(Func<PutAliasDescriptor, PutAliasDescriptor> putAliasDescriptor)
		{
			return this.Dispatcher.Dispatch<PutAliasDescriptor, PutAliasRequestParameters, PutAliasResponse>(
				putAliasDescriptor,
				(p, d) => this.LowLevelDispatch.IndicesPutAliasDispatch<PutAliasResponse>(p, d)
			);
		}

		/// <inheritdoc/>
		public Task<IPutAliasResponse> PutAliasAsync(Func<PutAliasDescriptor, PutAliasDescriptor> putAliasDescriptor)
		{
			return this.Dispatcher.DispatchAsync<PutAliasDescriptor, PutAliasRequestParameters, PutAliasResponse, IPutAliasResponse>(
				putAliasDescriptor,
				(p, d) => this.LowLevelDispatch.IndicesPutAliasDispatchAsync<PutAliasResponse>(p, d)
			);
		}
	}
}