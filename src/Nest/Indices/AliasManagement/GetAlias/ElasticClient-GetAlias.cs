using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The get index alias api allows to filter by alias name and index name. This api redirects to the master and fetches
		/// the requested index aliases, if available. This api only serialises the found index aliases.
		/// <para> Difference with GetAlias is that this call will also return indices without aliases set</para>
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-aliases.html#alias-retrieving
		/// </summary>
		/// <param name="selector">A descriptor that describes which aliases/indexes we are interested int</param>
		IGetAliasResponse GetAlias(Func<GetAliasDescriptor, IGetAliasRequest> selector = null);

		/// <inheritdoc />
		IGetAliasResponse GetAlias(IGetAliasRequest request);

		/// <inheritdoc />
		Task<IGetAliasResponse> GetAliasAsync(
			Func<GetAliasDescriptor, IGetAliasRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IGetAliasResponse> GetAliasAsync(IGetAliasRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetAliasResponse GetAlias(Func<GetAliasDescriptor, IGetAliasRequest> selector = null) =>
			GetAlias(selector.InvokeOrDefault(new GetAliasDescriptor()));

		/// <inheritdoc />
		public IGetAliasResponse GetAlias(IGetAliasRequest request) =>
			Dispatcher.Dispatch<IGetAliasRequest, GetAliasRequestParameters, GetAliasResponse>(
				ForceConfiguration<IGetAliasRequest, GetAliasRequestParameters>(request, c => c.AllowedStatusCodes = new[] { -1 }),
				(p, d) => LowLevelDispatch.IndicesGetAliasDispatch<GetAliasResponse>(p)
			);

		/// <inheritdoc />
		public Task<IGetAliasResponse> GetAliasAsync(
			Func<GetAliasDescriptor, IGetAliasRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => GetAliasAsync(selector.InvokeOrDefault(new GetAliasDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IGetAliasResponse> GetAliasAsync(IGetAliasRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IGetAliasRequest, GetAliasRequestParameters, GetAliasResponse, IGetAliasResponse>(
				ForceConfiguration<IGetAliasRequest, GetAliasRequestParameters>(request, c => c.AllowedStatusCodes = new[] { -1 }),
				cancellationToken,
				(p, d, c) => LowLevelDispatch.IndicesGetAliasDispatchAsync<GetAliasResponse>(p, c)
			);
	}
}
