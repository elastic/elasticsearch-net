using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;

namespace Nest
{
	/// <summary>
	/// Implements several handy alias extensions.
	/// </summary>
	public static class IndicesPointingToAliasExtensions
	{
		/// <summary>
		/// Returns a collection of indices that have the specified alias(es) applied to them. Simplified version of GetAlias.
		/// </summary>
		/// <param name="client">The client</param>
		/// <param name="alias">The alias name(s)</param>
		public static IEnumerable<string> GetIndicesPointingToAlias(this IElasticClient client, Names alias)
		{
			var response = client.GetAlias(a => a.Name(alias));
			return IndicesPointingToAlias(client.ConnectionSettings, alias, response);
		}

		/// <summary>
		/// Returns a collection of indices that have the specified alias(es) applied to them. Simplified version of GetAlias.
		/// </summary>
		/// <param name="client">The client</param>
		/// <param name="alias">The alias name(s)</param>
		public static async Task<IEnumerable<string>> GetIndicesPointingToAliasAsync(this IElasticClient client, Names alias)
		{
			var response = await client.GetAliasAsync(a => a.Name(alias)).ConfigureAwait(false);
			return IndicesPointingToAlias(client.ConnectionSettings, alias, response);
		}

		private static IEnumerable<string> IndicesPointingToAlias(IConnectionConfigurationValues settings, Names alias, IGetAliasResponse aliasesResponse)
		{
			if (!aliasesResponse.IsValid
			    || !aliasesResponse.Indices.HasAny())
				return new string[] { };

			var aliases = alias.GetString(settings).Split(',');

			var indices = from i in aliasesResponse.Indices
				where i.Value.Any(a => aliases.Contains(a.Name))
				select i.Key;

			return indices.ToList();
		}
	}
}
