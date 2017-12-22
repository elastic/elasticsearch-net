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
		public static IReadOnlyCollection<string> GetIndicesPointingToAlias(this IElasticClient client, Names alias)
		{
			var response = client.GetAlias(a => a.Name(alias).RequestConfiguration(r=>r.ThrowExceptions()));
			return IndicesPointingToAlias(client.ConnectionSettings, alias, response);
		}

		/// <summary>
		/// Returns a collection of indices that have the specified alias(es) applied to them. Simplified version of GetAlias.
		/// </summary>
		/// <param name="client">The client</param>
		/// <param name="alias">The alias name(s)</param>
		public static async Task<IReadOnlyCollection<string>> GetIndicesPointingToAliasAsync(this IElasticClient client, Names alias)
		{
			var response = await client.GetAliasAsync(a => a.Name(alias).RequestConfiguration(r=>r.ThrowExceptions())).ConfigureAwait(false);
			return IndicesPointingToAlias(client.ConnectionSettings, alias, response);
		}

		private static IReadOnlyCollection<string> IndicesPointingToAlias(IConnectionSettingsValues settings, IUrlParameter alias, IGetAliasResponse aliasesResponse)
		{
			if (!aliasesResponse.IsValid
			    || !aliasesResponse.Indices.HasAny())
				return EmptyReadOnly<string>.Collection;

			var aliases = alias.GetString(settings).Split(',');

			var indices = from i in aliasesResponse.Indices
				where i.Value?.Aliases?.Keys?.Any(key => aliases.Contains(key)) ?? false
				select settings.Inferrer.IndexName(i.Key);

			return indices.ToList();
		}
	}
}
