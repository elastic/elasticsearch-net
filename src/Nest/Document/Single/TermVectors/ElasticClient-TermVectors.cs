using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Returns information and statistics on terms in the fields of a particular document as stored in the index.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-termvectors.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-termvectors.html</a>
		/// </summary>
		/// <typeparam name="T">The type of the document</typeparam>
		/// <param name="selector">A descriptor for the terms vector operation</param>
		TermVectorsResponse TermVectors<T>(Func<TermVectorsDescriptor<T>, ITermVectorsRequest<T>> selector)
			where T : class;

		/// <inheritdoc />
		TermVectorsResponse TermVectors<T>(ITermVectorsRequest<T> request)
			where T : class;

		/// <inheritdoc />
		Task<TermVectorsResponse> TermVectorsAsync<T>(
			Func<TermVectorsDescriptor<T>, ITermVectorsRequest<T>> selector,
			CancellationToken ct = default
		) where T : class;

		/// <inheritdoc />
		Task<TermVectorsResponse> TermVectorsAsync<T>(ITermVectorsRequest<T> request,
			CancellationToken ct = default
		)
			where T : class;
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public TermVectorsResponse TermVectors<T>(Func<TermVectorsDescriptor<T>, ITermVectorsRequest<T>> selector) where T : class =>
			TermVectors(selector?.Invoke(new TermVectorsDescriptor<T>(typeof(T))));

		/// <inheritdoc />
		public TermVectorsResponse TermVectors<T>(ITermVectorsRequest<T> request) where T : class =>
			DoRequest<ITermVectorsRequest<T>, TermVectorsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<TermVectorsResponse> TermVectorsAsync<T>(
			Func<TermVectorsDescriptor<T>, ITermVectorsRequest<T>> selector,
			CancellationToken ct = default
		)
			where T : class =>
			TermVectorsAsync(selector?.Invoke(new TermVectorsDescriptor<T>(typeof(T))), ct);

		/// <inheritdoc />
		public Task<TermVectorsResponse> TermVectorsAsync<T>(ITermVectorsRequest<T> request, CancellationToken ct = default)
			where T : class =>
			DoRequestAsync<ITermVectorsRequest<T>, TermVectorsResponse>(request, request.RequestParameters, ct);
	}
}
