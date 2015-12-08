using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using GetWarmerConverter = Func<IApiCallDetails, Stream, GetWarmerResponse>;
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
		IAcknowledgedResponse PutWarmer(Name name, Func<PutWarmerDescriptor, IPutWarmerRequest> selector);

		/// <inheritdoc/>
		IAcknowledgedResponse PutWarmer(IPutWarmerRequest request);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> PutWarmerAsync(Name name, Func<PutWarmerDescriptor, IPutWarmerRequest> selector);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> PutWarmerAsync(IPutWarmerRequest request);

	}

	public partial class ElasticClient
	{
		//TODO AllIndices() seems odd here

		/// <inheritdoc/>
		public IAcknowledgedResponse PutWarmer(Name name, Func<PutWarmerDescriptor, IPutWarmerRequest> selector) =>
			this.PutWarmer(selector?.Invoke(new PutWarmerDescriptor(name)));

		/// <inheritdoc/>
		public IAcknowledgedResponse PutWarmer(IPutWarmerRequest request) => 
			this.Dispatcher.Dispatch<IPutWarmerRequest, PutWarmerRequestParameters, AcknowledgedResponse>(
				request,
				this.LowLevelDispatch.IndicesPutWarmerDispatch<AcknowledgedResponse>
			);

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> PutWarmerAsync(Name name, Func<PutWarmerDescriptor, IPutWarmerRequest> selector) => 
			this.PutWarmerAsync(selector?.Invoke(new PutWarmerDescriptor(name)));

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> PutWarmerAsync(IPutWarmerRequest request) => 
			this.Dispatcher.DispatchAsync<IPutWarmerRequest, PutWarmerRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				request,
				this.LowLevelDispatch.IndicesPutWarmerDispatchAsync<AcknowledgedResponse>
			);
	}
}