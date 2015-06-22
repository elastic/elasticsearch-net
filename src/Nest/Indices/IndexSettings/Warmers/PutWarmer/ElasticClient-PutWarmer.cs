using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using GetWarmerConverter = Func<IElasticsearchResponse, Stream, WarmerResponse>;
	using CrazyWarmerResponse = Dictionary<string, Dictionary<string, Dictionary<string, WarmerMapping>>>;

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IIndicesOperationResponse PutWarmer(string name, Func<PutWarmerDescriptor, PutWarmerDescriptor> selector)
		{
			selector.ThrowIfNull("selector");
			return this.Dispatcher.Dispatch<PutWarmerDescriptor, PutWarmerRequestParameters, IndicesOperationResponse>(
				d => selector(d.Name(name).AllIndices()),
				(p, d) => this.RawDispatch.IndicesPutWarmerDispatch<IndicesOperationResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IIndicesOperationResponse PutWarmer(IPutWarmerRequest putWarmerRequest)
		{
			return this.Dispatcher.Dispatch<IPutWarmerRequest, PutWarmerRequestParameters, IndicesOperationResponse>(
				putWarmerRequest,
				(p, d) => this.RawDispatch.IndicesPutWarmerDispatch<IndicesOperationResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> PutWarmerAsync(string name, Func<PutWarmerDescriptor, PutWarmerDescriptor> selector)
		{
			selector.ThrowIfNull("selector");
			return this.Dispatcher.DispatchAsync<PutWarmerDescriptor, PutWarmerRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				d => selector(d.Name(name).AllIndices()),
				(p, d) => this.RawDispatch.IndicesPutWarmerDispatchAsync<IndicesOperationResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> PutWarmerAsync(IPutWarmerRequest putWarmerRequest)
		{
			return this.Dispatcher.DispatchAsync<IPutWarmerRequest, PutWarmerRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				putWarmerRequest,
				(p, d) => this.RawDispatch.IndicesPutWarmerDispatchAsync<IndicesOperationResponse>(p, d)
			);
		}

	}
}