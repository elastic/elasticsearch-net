using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nest
{
	/// <summary>Implements several handy alias extensions.
	/// </summary>
	public static class AliasExtensions
	{
		/// <summary>
		/// Returns a list of aliases that point to the specified index, simplified version of GetAliases.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="indexName">The exact indexname we want to know aliases of</param>
		public static IList<AliasDefinition> GetAliasesPointingToIndex(this IElasticClient client, string indexName)
		{
			var aliasesResponse = client.GetAliases(a => a.Index(indexName));
			return AliasesPointingToIndex(indexName, aliasesResponse);
		}	
		/// <summary>
		/// Returns a list of aliases that point to the specified index, simplified version of GetAliases.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="indexName">The exact indexname we want to know aliases of</param>
		public static Task<IList<AliasDefinition>> GetAliasesPointingToIndexAsync(this IElasticClient client, string indexName)
		{
			return client.GetAliasesAsync(a => a.Index(indexName))
				.ContinueWith((t) =>
				{
					var aliasesResponse = t.Result;
					return AliasesPointingToIndex(indexName, aliasesResponse);
				});
		}
		
		/// <summary>
		/// Returns a list of indices that have the specified aliasName applied to them. Simplified version of GetAliases.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="aliasName">The exact alias name</param>
		public static IList<string> GetIndicesPointingToAlias(this IElasticClient client, string aliasName)
		{
			var aliasesResponse = client.GetAliases(a => a.Alias(aliasName));
			return IndicesPointingToAlias(aliasName, aliasesResponse);
		}

		/// <summary>
		/// Returns a list of indices that have the specified aliasName applied to them. Simplified version of GetAliases.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="aliasName">The exact alias name</param>
		public static Task<IList<string>> GetIndicesPointingToAliasAsync(this IElasticClient client, string aliasName)
		{
			return client.GetAliasesAsync(a => a.Index(aliasName))
				.ContinueWith((t) =>
				{
					var aliasesResponse = t.Result;
					return IndicesPointingToAlias(aliasName, aliasesResponse);
				});
		}
		private static IList<AliasDefinition> AliasesPointingToIndex(string indexName, IGetAliasesResponse aliasesResponse)
		{
			IList<AliasDefinition> aliases;
			if (!aliasesResponse.IsValid
			    || !aliasesResponse.Indices.HasAny()
			    || !aliasesResponse.Indices.TryGetValue(indexName, out aliases))
				return new AliasDefinition[] {};

			return aliases;
		}

		private static IList<string> IndicesPointingToAlias(string aliasName, IGetAliasesResponse aliasesResponse)
		{
			IList<AliasDefinition> aliases;
			if (!aliasesResponse.IsValid
			    || !aliasesResponse.Indices.HasAny())
				return new string[] {};

			var indices = from i in aliasesResponse.Indices
						  where i.Value.Any(a => a.Name == aliasName)
						  select i.Key;

			return indices.ToList();
		}
	}
}