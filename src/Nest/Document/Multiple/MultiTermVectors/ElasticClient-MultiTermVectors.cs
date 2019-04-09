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

		/// <inheritdoc cref="MultiTermVectors(System.Func{Nest.MultiTermVectorsDescriptor,Nest.IMultiTermVectorsRequest})" />
		IMultiTermVectorsResponse MultiTermVectors(IMultiTermVectorsRequest request);

		/// <inheritdoc cref="MultiTermVectors(System.Func{Nest.MultiTermVectorsDescriptor,Nest.IMultiTermVectorsRequest})" />
		Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="MultiTermVectors(System.Func{Nest.MultiTermVectorsDescriptor,Nest.IMultiTermVectorsRequest})" />
		Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(IMultiTermVectorsRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IMultiTermVectorsResponse MultiTermVectors(Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> selector = null) =>
			MultiTermVectors(selector.InvokeOrDefault(new MultiTermVectorsDescriptor()));

		/// <inheritdoc />
		public IMultiTermVectorsResponse MultiTermVectors(IMultiTermVectorsRequest request) =>
			Dispatch2<IMultiTermVectorsRequest, MultiTermVectorsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(
			Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> selector = null,
			CancellationToken ct = default
		) => MultiTermVectorsAsync(selector.InvokeOrDefault(new MultiTermVectorsDescriptor()), ct);

		/// <inheritdoc />
		public Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(IMultiTermVectorsRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IMultiTermVectorsRequest, IMultiTermVectorsResponse, MultiTermVectorsResponse>(request, request.RequestParameters, ct);
	}
}
