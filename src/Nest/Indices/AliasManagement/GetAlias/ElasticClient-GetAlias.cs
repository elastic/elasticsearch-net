using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using GetAliasesConverter = Func<IApiCallDetails, Stream, GetAliasesResponse>;

	public partial interface IElasticClient
	{
		/// <summary>
		/// The get index alias api allows to filter by alias name and index name. This api redirects to the master and fetches 
		/// the requested index aliases, if available. This api only serialises the found index aliases.
		/// <para> Difference with GetAlias is that this call will also return indices without aliases set</para>
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-aliases.html#alias-retrieving
		/// </summary>
		/// <param name="getAliasDescriptor">A descriptor that describes which aliases/indexes we are interested int</param>
		IGetAliasesResponse GetAlias(Func<GetAliasDescriptor, IGetAliasRequest> getAliasDescriptor = null);

		/// <inheritdoc/>
		IGetAliasesResponse GetAlias(IGetAliasRequest getAliasRequest);

		/// <inheritdoc/>
		Task<IGetAliasesResponse> GetAliasAsync(Func<GetAliasDescriptor, IGetAliasRequest> getAliasDescriptor = null);

		/// <inheritdoc/>
		Task<IGetAliasesResponse> GetAliasAsync(IGetAliasRequest getAliasRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetAliasesResponse GetAlias(Func<GetAliasDescriptor, IGetAliasRequest> getAliasDescriptor = null) =>
			this.GetAlias(getAliasDescriptor.InvokeOrDefault(new GetAliasDescriptor()));

		/// <inheritdoc/>
		public IGetAliasesResponse GetAlias(IGetAliasRequest getAliasRequest) => 
			this.Dispatcher.Dispatch<IGetAliasRequest, GetAliasRequestParameters, GetAliasesResponse>(
				getAliasRequest,
				new GetAliasesConverter(DeserializeGetAliasesResponse),
				(p, d) => this.LowLevelDispatch.IndicesGetAliasDispatch<GetAliasesResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetAliasesResponse> GetAliasAsync(Func<GetAliasDescriptor, IGetAliasRequest> getAliasDescriptor = null) =>
			this.GetAliasAsync(getAliasDescriptor.InvokeOrDefault(new GetAliasDescriptor()));

		/// <inheritdoc/>
		public Task<IGetAliasesResponse> GetAliasAsync(IGetAliasRequest getAliasRequest) => 
			this.Dispatcher.DispatchAsync<IGetAliasRequest, GetAliasRequestParameters, GetAliasesResponse, IGetAliasesResponse>(
				getAliasRequest,
				new GetAliasesConverter(DeserializeGetAliasesResponse),
				(p, d) => this.LowLevelDispatch.IndicesGetAliasDispatchAsync<GetAliasesResponse>(p)
			);
	}
}