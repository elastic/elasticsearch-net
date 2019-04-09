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
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IGetAliasResponse> GetAliasAsync(IGetAliasRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetAliasResponse GetAlias(Func<GetAliasDescriptor, IGetAliasRequest> selector = null) =>
			GetAlias(selector.InvokeOrDefault(new GetAliasDescriptor()));

		/// <inheritdoc />
		public IGetAliasResponse GetAlias(IGetAliasRequest request)
		{
			ForceConfiguration(request, c => c.AllowedStatusCodes = AllStatusCodes);
			return Dispatch2<IGetAliasRequest, GetAliasResponse>(request, request.RequestParameters);
		}

		/// <inheritdoc />
		public Task<IGetAliasResponse> GetAliasAsync(
			Func<GetAliasDescriptor, IGetAliasRequest> selector = null,
			CancellationToken ct = default
		) => GetAliasAsync(selector.InvokeOrDefault(new GetAliasDescriptor()), ct);

		/// <inheritdoc />
		public Task<IGetAliasResponse> GetAliasAsync(IGetAliasRequest request, CancellationToken ct = default)
		{
			ForceConfiguration(request, c => c.AllowedStatusCodes = AllStatusCodes);
			return Dispatch2Async<IGetAliasRequest, IGetAliasResponse, GetAliasResponse>(request, request.RequestParameters, ct);
		}
	}
}
