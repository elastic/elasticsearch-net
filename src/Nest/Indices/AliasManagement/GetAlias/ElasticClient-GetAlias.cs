using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using System.Threading;

	public partial interface IElasticClient
	{
		/// <summary>
		/// The get index alias api allows to filter by alias name and index name. This api redirects to the master and fetches
		/// the requested index aliases, if available. This api only serialises the found index aliases.
		/// <para> Difference with GetAlias is that this call will also return indices without aliases set</para>
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-aliases.html#alias-retrieving
		/// </summary>
		/// <param name="selector">A descriptor that describes which aliases/indexes we are interested int</param>
		IGetAliasResponse GetAlias(Func<GetAliasDescriptor, IGetAliasRequest> selector = null);

		/// <inheritdoc/>
		IGetAliasResponse GetAlias(IGetAliasRequest request);

		/// <inheritdoc/>
		Task<IGetAliasResponse> GetAliasAsync(
			Func<GetAliasDescriptor, IGetAliasRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc/>
		Task<IGetAliasResponse> GetAliasAsync(IGetAliasRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetAliasResponse GetAlias(Func<GetAliasDescriptor, IGetAliasRequest> selector = null) =>
			this.GetAlias(selector.InvokeOrDefault(new GetAliasDescriptor()));

		/// <inheritdoc/>
		public IGetAliasResponse GetAlias(IGetAliasRequest request) =>
			this.Dispatcher.Dispatch<IGetAliasRequest, GetAliasRequestParameters, GetAliasResponse>(
				ForceConfiguration<IGetAliasRequest, GetAliasRequestParameters>(request, c => c.AllowedStatusCodes = new[] { -1 }),
				(p, d) => this.LowLevelDispatch.IndicesGetAliasDispatch<GetAliasResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetAliasResponse> GetAliasAsync(
			Func<GetAliasDescriptor, IGetAliasRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => this.GetAliasAsync(selector.InvokeOrDefault(new GetAliasDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IGetAliasResponse> GetAliasAsync(IGetAliasRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IGetAliasRequest, GetAliasRequestParameters, GetAliasResponse, IGetAliasResponse>(
				ForceConfiguration<IGetAliasRequest, GetAliasRequestParameters>(request, c => c.AllowedStatusCodes = new[] { -1 }),
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.IndicesGetAliasDispatchAsync<GetAliasResponse>(p, c)
			);
	}
}
