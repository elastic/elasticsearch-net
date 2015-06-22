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
		public IIndicesOperationResponse DeleteWarmer(string name, Func<DeleteWarmerDescriptor, DeleteWarmerDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.Dispatch<DeleteWarmerDescriptor, DeleteWarmerRequestParameters, IndicesOperationResponse>(
				d => selector(d.Name(name).AllIndices()),
				(p, d) => this.RawDispatch.IndicesDeleteWarmerDispatch<IndicesOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public IIndicesOperationResponse DeleteWarmer(IDeleteWarmerRequest deleteWarmerRequest)
		{
			return this.Dispatcher.Dispatch<IDeleteWarmerRequest, DeleteWarmerRequestParameters, IndicesOperationResponse>(
				deleteWarmerRequest,
				(p, d) => this.RawDispatch.IndicesDeleteWarmerDispatch<IndicesOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> DeleteWarmerAsync(string name, Func<DeleteWarmerDescriptor, DeleteWarmerDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.DispatchAsync
				<DeleteWarmerDescriptor, DeleteWarmerRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
					d => selector(d.Name(name).AllIndices()),
					(p, d) => this.RawDispatch.IndicesDeleteWarmerDispatchAsync<IndicesOperationResponse>(p)
				);
		}

		/// <inheritdoc />
		public Task<IIndicesOperationResponse> DeleteWarmerAsync(IDeleteWarmerRequest deleteWarmerRequest)
		{
			return this.Dispatcher.DispatchAsync<IDeleteWarmerRequest, DeleteWarmerRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
					deleteWarmerRequest,
					(p, d) => this.RawDispatch.IndicesDeleteWarmerDispatchAsync<IndicesOperationResponse>(p)
				);
		}

	}
}