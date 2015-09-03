using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using GetWarmerConverter = Func<IApiCallDetails, Stream, WarmerResponse>;
	using CrazyWarmerResponse = Dictionary<string, Dictionary<string, IWarmers>>;

	public partial interface IElasticClient
	{
		/// <summary>
		/// Allows to put a warmup search request on a specific index (or indices), with the body composing of a regular 
		/// search request. Types can be provided as part of the URI if the search request is designed to be run only 
		/// against the specific types.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-warmers.html#warmer-adding
		/// </summary>
		/// <param name="name">The name for the warmer that you want to register</param>
		/// <param name="selector">A descriptor that further describes what the warmer should look like</param>
		IIndicesOperationResponse PutWarmer(string name, Func<PutWarmerDescriptor, IPutWarmerRequest> selector);

		/// <inheritdoc/>
		IIndicesOperationResponse PutWarmer(IPutWarmerRequest putWarmerRequest);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> PutWarmerAsync(string name, Func<PutWarmerDescriptor, IPutWarmerRequest> selector);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> PutWarmerAsync(IPutWarmerRequest putWarmerRequest);

	}

	public partial class ElasticClient
	{
		//TODO AllIndices() seems odd here

		/// <inheritdoc/>
		public IIndicesOperationResponse PutWarmer(string name, Func<PutWarmerDescriptor, IPutWarmerRequest> selector) =>
			this.PutWarmer(selector?.Invoke(new PutWarmerDescriptor().Name(name).AllIndices()));

		/// <inheritdoc/>
		public IIndicesOperationResponse PutWarmer(IPutWarmerRequest putWarmerRequest) => 
			this.Dispatcher.Dispatch<IPutWarmerRequest, PutWarmerRequestParameters, IndicesOperationResponse>(
				putWarmerRequest,
				this.LowLevelDispatch.IndicesPutWarmerDispatch<IndicesOperationResponse>
			);

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> PutWarmerAsync(string name, Func<PutWarmerDescriptor, IPutWarmerRequest> selector) => 
			this.PutWarmerAsync(selector?.Invoke(new PutWarmerDescriptor().Name(name).AllIndices()));

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> PutWarmerAsync(IPutWarmerRequest putWarmerRequest) => 
			this.Dispatcher.DispatchAsync<IPutWarmerRequest, PutWarmerRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				putWarmerRequest,
				this.LowLevelDispatch.IndicesPutWarmerDispatchAsync<IndicesOperationResponse>
			);
	}
}