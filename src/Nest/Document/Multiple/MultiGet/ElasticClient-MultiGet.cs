using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	using MultiGetConverter = Func<IApiCallDetails, Stream, MultiGetResponse>;
	
	public partial interface IElasticClient
	{
		/// <summary>
		/// Multi GET API allows to get multiple documents based on an index, type (optional) and id (and possibly routing). 
		/// The response includes a docs array with all the fetched documents, each element similar in structure to a document 
		/// provided by the get API.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-multi-get.html
		/// </summary>
		/// <param name="multiGetSelector">A descriptor describing which documents should be fetched</param>
		IMultiGetResponse MultiGet(Func<MultiGetDescriptor, IMultiGetRequest> multiGetSelector = null);

		/// <inheritdoc/>
		IMultiGetResponse MultiGet(IMultiGetRequest multiGetRequest);

		/// <inheritdoc/>
		Task<IMultiGetResponse> MultiGetAsync(Func<MultiGetDescriptor, IMultiGetRequest> multiGetSelector = null);

		/// <inheritdoc/>
		Task<IMultiGetResponse> MultiGetAsync(IMultiGetRequest multiGetRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IMultiGetResponse MultiGet(Func<MultiGetDescriptor, IMultiGetRequest> multiGetSelector = null) =>
			this.MultiGet(multiGetSelector.InvokeOrDefault(new MultiGetDescriptor()));

		/// <inheritdoc/>
		public IMultiGetResponse MultiGet(IMultiGetRequest multiRequest) => 
			this.Dispatcher.Dispatch<IMultiGetRequest, MultiGetRequestParameters, MultiGetResponse>(
				multiRequest,
				new MultiGetConverter((r, s) => this.DeserializeMultiGetResponse(r, s, CreateCovariantMultiGetConverter(multiRequest))),
				this.LowLevelDispatch.MgetDispatch<MultiGetResponse>
			);

		/// <inheritdoc/>
		public Task<IMultiGetResponse> MultiGetAsync(Func<MultiGetDescriptor, IMultiGetRequest> multiGetSelector = null) =>
			this.MultiGetAsync(multiGetSelector.InvokeOrDefault(new MultiGetDescriptor()));

		/// <inheritdoc/>
		public Task<IMultiGetResponse> MultiGetAsync(IMultiGetRequest multiRequest) => 
			this.Dispatcher.DispatchAsync<IMultiGetRequest, MultiGetRequestParameters, MultiGetResponse, IMultiGetResponse>(
				multiRequest,
				new MultiGetConverter((r, s) => this.DeserializeMultiGetResponse(r, s, CreateCovariantMultiGetConverter(multiRequest))),
				this.LowLevelDispatch.MgetDispatchAsync<MultiGetResponse>
			);
		private MultiGetResponse DeserializeMultiGetResponse(IApiCallDetails response, Stream stream, JsonConverter converter)=>
			new NestSerializer(this.ConnectionSettings, converter).Deserialize<MultiGetResponse>(stream);

		private JsonConverter CreateCovariantMultiGetConverter(IMultiGetRequest descriptor) => new MultiGetHitJsonConverter(descriptor);

	}
}