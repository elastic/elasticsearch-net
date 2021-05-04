// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Transport;

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
			var response = client.Indices.GetAlias(Indices.All, a => a.Name(alias).RequestConfiguration(r => r.ThrowExceptions()));
			return IndicesPointingToAlias(client.ConnectionSettings, alias, response);
		}

		/// <summary>
		/// Returns a collection of indices that have the specified alias(es) applied to them. Simplified version of GetAlias.
		/// </summary>
		/// <param name="client">The client</param>
		/// <param name="alias">The alias name(s)</param>
		public static async Task<IReadOnlyCollection<string>> GetIndicesPointingToAliasAsync(this IElasticClient client, Names alias)
		{
			var response = await client.Indices.GetAliasAsync(Indices.All, a => a.Name(alias).RequestConfiguration(r => r.ThrowExceptions())).ConfigureAwait(false);
			return IndicesPointingToAlias(client.ConnectionSettings, alias, response);
		}

		private static IReadOnlyCollection<string> IndicesPointingToAlias(IConnectionSettingsValues settings, IUrlParameter alias,
			GetAliasResponse aliasesResponse
		)
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
