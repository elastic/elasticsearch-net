using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using GetAliasesConverter = Func<IApiCallDetails, Stream, GetAliasesResponse>;
	using CrazyAliasesResponse = Dictionary<string, Dictionary<string, Dictionary<string, AliasDefinition>>>;

	public partial interface IElasticClient
	{
		/// <summary>
		/// The get index alias api allows to filter by alias name and index name. This api redirects to the master and fetches 
		/// the requested index aliases, if available. This api only serialises the found index aliases.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-aliases.html#alias-retrieving
		/// </summary>
		/// <param name="selector">A descriptor that describes which aliases/indexes we are interested int</param>
		[Obsolete("Deprecated since 1.0, will be removed in 3.0. Use GetAlias which accepts multiple aliases and indices")]
		IGetAliasesResponse GetAliases(Func<GetAliasesDescriptor, IGetAliasesRequest> selector = null);

		/// <inheritdoc/>
		[Obsolete("Deprecated since 1.0, will be removed in 3.0. Use GetAlias which accepts multiple aliases and indices")]
		IGetAliasesResponse GetAliases(IGetAliasesRequest request);

		/// <inheritdoc/>
		[Obsolete("Deprecated since 1.0, will be removed in 3.0. Use GetAlias which accepts multiple aliases and indices")]
		Task<IGetAliasesResponse> GetAliasesAsync(Func<GetAliasesDescriptor, IGetAliasesRequest> selector = null);

		/// <inheritdoc/>
		[Obsolete("Deprecated since 1.0, will be removed in 3.0. Use GetAlias which accepts multiple aliases and indices")]
		Task<IGetAliasesResponse> GetAliasesAsync(IGetAliasesRequest request);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		[Obsolete("Deprecated since 1.0, will be removed in 3.0. Use GetAlias which accepts multiple aliases and indices")]
		public IGetAliasesResponse GetAliases(Func<GetAliasesDescriptor, IGetAliasesRequest> selector = null) =>
			this.GetAliases(selector.InvokeOrDefault(new GetAliasesDescriptor()));

		/// <inheritdoc/>
		[Obsolete("Deprecated since 1.0, will be removed in 3.0. Use GetAlias which accepts multiple aliases and indices")]
		public IGetAliasesResponse GetAliases(IGetAliasesRequest request) => 
			this.Dispatcher.Dispatch<IGetAliasesRequest, GetAliasesRequestParameters, GetAliasesResponse>(
				request,
				new GetAliasesConverter(DeserializeGetAliasesResponse),
				(p, d) => this.LowLevelDispatch.IndicesGetAliasesDispatch<GetAliasesResponse>(p)
			);

		/// <inheritdoc/>
		[Obsolete("Deprecated since 1.0, will be removed in 3.0. Use GetAlias which accepts multiple aliases and indices")]
		public Task<IGetAliasesResponse> GetAliasesAsync(Func<GetAliasesDescriptor, IGetAliasesRequest> selector = null) =>
			this.GetAliasesAsync(selector.InvokeOrDefault(new GetAliasesDescriptor()));

		/// <inheritdoc/>
		[Obsolete("Deprecated since 1.0, will be removed in 3.0. Use GetAlias which accepts multiple aliases and indices")]
		public Task<IGetAliasesResponse> GetAliasesAsync(IGetAliasesRequest request) => 
			this.Dispatcher.DispatchAsync<IGetAliasesRequest, GetAliasesRequestParameters, GetAliasesResponse, IGetAliasesResponse>(
				request,
				new GetAliasesConverter(DeserializeGetAliasesResponse),
				(p, d) => this.LowLevelDispatch.IndicesGetAliasesDispatchAsync<GetAliasesResponse>(p)
			);
		
		//TODO map the response properly, remove list flattening
		/// <inheritdoc/>
		private GetAliasesResponse DeserializeGetAliasesResponse(IApiCallDetails apiCallDetails, Stream stream)
		{
			if (!apiCallDetails.Success)
				return new GetAliasesResponse();

			var dict = this.Serializer.Deserialize<CrazyAliasesResponse>(stream);

			var d = new Dictionary<string, IList<AliasDefinition>>();

			foreach (var kv in dict)
			{
				var indexDict = kv.Key;
				var aliases = new List<AliasDefinition>();
				if (kv.Value != null && kv.Value.ContainsKey("aliases"))
				{
					var aliasDict = kv.Value["aliases"];
					if (aliasDict != null)
						aliases = aliasDict.Select(kva =>
						{
							var alias = kva.Value;
							alias.Name = kva.Key;
							return alias;
						}).ToList();
				}

				d.Add(indexDict, aliases);
			}

			return new GetAliasesResponse() { Indices = d };
		}
	}
}