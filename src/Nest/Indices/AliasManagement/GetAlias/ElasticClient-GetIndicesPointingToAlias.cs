/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

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
