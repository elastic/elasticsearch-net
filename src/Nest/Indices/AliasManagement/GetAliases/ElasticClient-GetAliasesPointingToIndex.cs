using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nest
{
	public static class AliasPointingToIndexExtensions
	{
		/// <summary>
		/// Returns a list of aliases that point to the specified index, simplified version of GetAliases.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="indexName">The exact indexname we want to know aliases of</param>
		public static IList<AliasDefinition> GetAliasesPointingToIndex(this IElasticClient client, string indexName)
		{
			var aliasesResponse = client.GetAlias(a => a.Index(indexName));
			return AliasesPointingToIndex(indexName, aliasesResponse);
		}

		/// <summary>
		/// Returns a list of aliases that point to the specified index, simplified version of GetAliases.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="indexName">The exact indexname we want to know aliases of</param>
		public static async Task<IList<AliasDefinition>> GetAliasesPointingToIndexAsync(this IElasticClient client, string indexName)
		{
			var response =  await client.GetAliasAsync(a => a.Index(indexName)).ConfigureAwait(false);
			return AliasesPointingToIndex(indexName, response);
		}

		private static IList<AliasDefinition> AliasesPointingToIndex(string indexName, IGetAliasesResponse aliasesResponse)
		{
			IList<AliasDefinition> aliases;
			if (!aliasesResponse.IsValid
				|| !aliasesResponse.Indices.HasAny()
				|| !aliasesResponse.Indices.TryGetValue(indexName, out aliases))
				return new AliasDefinition[] { };

			return aliases;
		}

	}
}