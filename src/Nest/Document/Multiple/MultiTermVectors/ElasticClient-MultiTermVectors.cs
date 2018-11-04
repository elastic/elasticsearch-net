using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Multi termvectors API allows to get multiple termvectors based on an index, type and id.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-multi-termvectors.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-multi-termvectors.html</a>
		/// </summary>
		/// <param name="selector">The descriptor describing the multi termvectors operation</param>
		IMultiTermVectorsResponse MultiTermVectors(Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> selector = null);

		/// <inheritdoc />
		IMultiTermVectorsResponse MultiTermVectors(IMultiTermVectorsRequest request);

		/// <inheritdoc />
		Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(IMultiTermVectorsRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IMultiTermVectorsResponse MultiTermVectors(Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> selector = null) =>
			MultiTermVectors(selector.InvokeOrDefault(new MultiTermVectorsDescriptor()));

		/// <inheritdoc />
		public IMultiTermVectorsResponse MultiTermVectors(IMultiTermVectorsRequest request) =>
			Dispatcher.Dispatch<IMultiTermVectorsRequest, MultiTermVectorsRequestParameters, MultiTermVectorsResponse>(
				request,
				LowLevelDispatch.MtermvectorsDispatch<MultiTermVectorsResponse>
			);

		/// <inheritdoc />
		public Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			MultiTermVectorsAsync(selector.InvokeOrDefault(new MultiTermVectorsDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(IMultiTermVectorsRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher
				.DispatchAsync<IMultiTermVectorsRequest, MultiTermVectorsRequestParameters, MultiTermVectorsResponse, IMultiTermVectorsResponse>(
					request,
					cancellationToken,
					LowLevelDispatch.MtermvectorsDispatchAsync<MultiTermVectorsResponse>
				);
	}
}
