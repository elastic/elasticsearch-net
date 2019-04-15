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
		MultiTermVectorsResponse MultiTermVectors(Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> selector = null);

		/// <inheritdoc cref="MultiTermVectors(System.Func{Nest.MultiTermVectorsDescriptor,Nest.IMultiTermVectorsRequest})" />
		MultiTermVectorsResponse MultiTermVectors(IMultiTermVectorsRequest request);

		/// <inheritdoc cref="MultiTermVectors(System.Func{Nest.MultiTermVectorsDescriptor,Nest.IMultiTermVectorsRequest})" />
		Task<MultiTermVectorsResponse> MultiTermVectorsAsync(Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="MultiTermVectors(System.Func{Nest.MultiTermVectorsDescriptor,Nest.IMultiTermVectorsRequest})" />
		Task<MultiTermVectorsResponse> MultiTermVectorsAsync(IMultiTermVectorsRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public MultiTermVectorsResponse MultiTermVectors(Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> selector = null) =>
			MultiTermVectors(selector.InvokeOrDefault(new MultiTermVectorsDescriptor()));

		/// <inheritdoc />
		public MultiTermVectorsResponse MultiTermVectors(IMultiTermVectorsRequest request) =>
			DoRequest<IMultiTermVectorsRequest, MultiTermVectorsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<MultiTermVectorsResponse> MultiTermVectorsAsync(
			Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> selector = null,
			CancellationToken ct = default
		) => MultiTermVectorsAsync(selector.InvokeOrDefault(new MultiTermVectorsDescriptor()), ct);

		/// <inheritdoc />
		public Task<MultiTermVectorsResponse> MultiTermVectorsAsync(IMultiTermVectorsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IMultiTermVectorsRequest, MultiTermVectorsResponse>(request, request.RequestParameters, ct);
	}
}
