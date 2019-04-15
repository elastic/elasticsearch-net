using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Add a single index alias
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-aliases.html#alias-adding
		/// </summary>
		/// <param name="request">A descriptor that describes the put alias request</param>
		PutAliasResponse PutAlias(IPutAliasRequest request);

		/// <inheritdoc />
		Task<PutAliasResponse> PutAliasAsync(IPutAliasRequest request, CancellationToken ct = default);

		/// <inheritdoc />
		PutAliasResponse PutAlias(Indices indices, Name alias, Func<PutAliasDescriptor, IPutAliasRequest> selector = null);

		/// <inheritdoc />
		Task<PutAliasResponse> PutAliasAsync(
			Indices indices,
			Name alias,
			Func<PutAliasDescriptor, IPutAliasRequest> selector = null,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public PutAliasResponse PutAlias(IPutAliasRequest request) =>
			DoRequest<IPutAliasRequest, PutAliasResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public PutAliasResponse PutAlias(Indices indices, Name alias, Func<PutAliasDescriptor, IPutAliasRequest> selector = null) =>
			PutAlias(selector.InvokeOrDefault(new PutAliasDescriptor(indices, alias)));

		/// <inheritdoc />
		public Task<PutAliasResponse> PutAliasAsync(IPutAliasRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IPutAliasRequest, PutAliasResponse, PutAliasResponse>(request, request.RequestParameters, ct);

		/// <inheritdoc />
		public Task<PutAliasResponse> PutAliasAsync(
			Indices indices,
			Name alias,
			Func<PutAliasDescriptor, IPutAliasRequest> selector = null,
			CancellationToken ct = default
		) => PutAliasAsync(selector.InvokeOrDefault(new PutAliasDescriptor(indices, alias)), ct);
	}
}
