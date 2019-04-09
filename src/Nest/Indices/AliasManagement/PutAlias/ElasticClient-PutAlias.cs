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
		IPutAliasResponse PutAlias(IPutAliasRequest request);

		/// <inheritdoc />
		Task<IPutAliasResponse> PutAliasAsync(IPutAliasRequest request, CancellationToken ct = default);

		/// <inheritdoc />
		IPutAliasResponse PutAlias(Indices indices, Name alias, Func<PutAliasDescriptor, IPutAliasRequest> selector = null);

		/// <inheritdoc />
		Task<IPutAliasResponse> PutAliasAsync(
			Indices indices,
			Name alias,
			Func<PutAliasDescriptor, IPutAliasRequest> selector = null,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IPutAliasResponse PutAlias(IPutAliasRequest request) =>
			Dispatch2<IPutAliasRequest, PutAliasResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public IPutAliasResponse PutAlias(Indices indices, Name alias, Func<PutAliasDescriptor, IPutAliasRequest> selector = null) =>
			PutAlias(selector.InvokeOrDefault(new PutAliasDescriptor(indices, alias)));

		/// <inheritdoc />
		public Task<IPutAliasResponse> PutAliasAsync(IPutAliasRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IPutAliasRequest, IPutAliasResponse, PutAliasResponse>(request, request.RequestParameters, ct);

		/// <inheritdoc />
		public Task<IPutAliasResponse> PutAliasAsync(
			Indices indices,
			Name alias,
			Func<PutAliasDescriptor, IPutAliasRequest> selector = null,
			CancellationToken ct = default
		) => PutAliasAsync(selector.InvokeOrDefault(new PutAliasDescriptor(indices, alias)), ct);
	}
}
