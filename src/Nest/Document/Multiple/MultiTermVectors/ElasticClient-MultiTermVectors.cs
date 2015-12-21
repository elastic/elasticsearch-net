using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Multi termvectors API allows to get multiple termvectors based on an index, type and id.
		/// <para> </para><a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-multi-termvectors.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-multi-termvectors.html</a>
		/// </summary>
		/// <param name="selector">The descriptor describing the multi termvectors operation</param>
		IMultiTermVectorsResponse MultiTermVectors(Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> selector = null);

		/// <inheritdoc/>
		IMultiTermVectorsResponse MultiTermVectors(IMultiTermVectorsRequest request);

		/// <inheritdoc/>
		Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> selector = null);

		/// <inheritdoc/>
		Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(IMultiTermVectorsRequest request);
	}

	public partial class ElasticClient
	{
		///<inheritdoc/>
		public IMultiTermVectorsResponse MultiTermVectors(Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> selector = null) =>
			this.MultiTermVectors(selector.InvokeOrDefault(new MultiTermVectorsDescriptor()));

		///<inheritdoc/>
		public IMultiTermVectorsResponse MultiTermVectors(IMultiTermVectorsRequest request) => 
			this.Dispatcher.Dispatch<IMultiTermVectorsRequest, MultiTermVectorsRequestParameters, MultiTermVectorsResponse>(
				request,
				this.LowLevelDispatch.MtermvectorsDispatch<MultiTermVectorsResponse>
			);

		///<inheritdoc/>
		public Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> selector = null) =>
			this.MultiTermVectorsAsync(selector.InvokeOrDefault(new MultiTermVectorsDescriptor()));

		///<inheritdoc/>
		public Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(IMultiTermVectorsRequest request) => 
			this.Dispatcher.DispatchAsync<IMultiTermVectorsRequest, MultiTermVectorsRequestParameters, MultiTermVectorsResponse, IMultiTermVectorsResponse>(
				request,
				this.LowLevelDispatch.MtermvectorsDispatchAsync<MultiTermVectorsResponse>
			);
	}
}
