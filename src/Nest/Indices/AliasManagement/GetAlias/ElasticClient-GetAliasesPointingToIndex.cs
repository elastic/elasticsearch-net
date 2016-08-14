using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static class AliasPointingToIndexExtensions
	{
		/// <summary>
		/// Returns a collection of aliases that point to the specified index, simplified version of GetAlias.
		/// </summary>
		/// <param name="client">The client</param>
		/// <param name="indices">The index name(s) we want to know aliases of</param>
		public static IEnumerable<AliasDefinition> GetAliasesPointingToIndex(this IElasticClient client, Indices indices)
		{
			var aliasesResponse = client.GetAlias(a => a.Index(indices));
			return AliasesPointingToIndex(client.ConnectionSettings, indices, aliasesResponse);
		}

		/// <summary>
		/// Returns a collection of aliases that point to the specified index, simplified version of GetAlias.
		/// </summary>
		/// <param name="client">The client</param>
		/// <param name="indices">The index name(s) we want to know aliases of</param>
		public static async Task<IEnumerable<AliasDefinition>> GetAliasesPointingToIndexAsync(this IElasticClient client, Indices indices)
		{
			var response = await client.GetAliasAsync(a => a.Index(indices)).ConfigureAwait(false);
			return AliasesPointingToIndex(client.ConnectionSettings, indices, response);
		}

		private static IEnumerable<AliasDefinition> AliasesPointingToIndex(IConnectionConfigurationValues settings, IUrlParameter indices, IGetAliasResponse aliasesResponse)
		{
			if (!aliasesResponse.IsValid || !aliasesResponse.Indices.HasAny())
				return Enumerable.Empty<AliasDefinition>();

			var indexNames = indices.GetString(settings).Split(',');

			var aliases = new List<AliasDefinition>();
			foreach (var indexName in indexNames)
				if (aliasesResponse.Indices.ContainsKey(indexName))
					aliases.AddRange(aliasesResponse.Indices[indexName]);

			return aliases;
		}
	}
}
