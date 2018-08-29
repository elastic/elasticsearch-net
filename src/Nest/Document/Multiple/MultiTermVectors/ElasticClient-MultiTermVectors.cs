using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

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

		/// <inheritdoc cref="MultiTermVectors(System.Func{Nest.MultiTermVectorsDescriptor,Nest.IMultiTermVectorsRequest})"/>
		IMultiTermVectorsResponse MultiTermVectors(IMultiTermVectorsRequest request);

		/// <inheritdoc cref="MultiTermVectors(System.Func{Nest.MultiTermVectorsDescriptor,Nest.IMultiTermVectorsRequest})"/>
		Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc cref="MultiTermVectors(System.Func{Nest.MultiTermVectorsDescriptor,Nest.IMultiTermVectorsRequest})"/>
		Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(IMultiTermVectorsRequest request, CancellationToken cancellationToken = default(CancellationToken));
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
		public Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.MultiTermVectorsAsync(selector.InvokeOrDefault(new MultiTermVectorsDescriptor()), cancellationToken);

		///<inheritdoc/>
		public Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(IMultiTermVectorsRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IMultiTermVectorsRequest, MultiTermVectorsRequestParameters, MultiTermVectorsResponse, IMultiTermVectorsResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.MtermvectorsDispatchAsync<MultiTermVectorsResponse>
			);
	}
}
