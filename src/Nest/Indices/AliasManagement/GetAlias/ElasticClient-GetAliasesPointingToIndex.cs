// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest.Specification.IndicesApi;

namespace Nest
{
	public static class AliasPointingToIndexExtensions
	{
		/// <summary>
		/// Returns a dictionary of aliases that point to the specified index, simplified version of
		/// <see cref="IndicesNamespace.GetAlias(IGetAliasRequest)" />..
		/// </summary>
		/// <param name="index">The index name we want to know aliases of</param>
		public static IReadOnlyDictionary<string, AliasDefinition> GetAliasesPointingToIndex(this IElasticClient client, IndexName index)
		{
			var response = client.Indices.GetAlias(index, a => a.RequestConfiguration(r => r.ThrowExceptions()));
			return AliasesPointingToIndex(index, response);
		}

		/// <summary>
		/// Returns a dictionary of aliases that point to the specified index, simplified version of
		/// <see cref="IndicesNamespace.GetAlias(IGetAliasRequest)" />..
		/// </summary>
		/// <param name="index">The index name we want to know aliases of</param>
		public static async Task<IReadOnlyDictionary<string, AliasDefinition>> GetAliasesPointingToIndexAsync(this IElasticClient client,
			IndexName index
		)
		{
			var response = await client.Indices.GetAliasAsync(index, a => a.RequestConfiguration(r => r.ThrowExceptions())).ConfigureAwait(false);
			return AliasesPointingToIndex(index, response);
		}

		private static IReadOnlyDictionary<string, AliasDefinition> AliasesPointingToIndex(IndexName index, GetAliasResponse response)
		{
			if (!response.IsValid || !response.Indices.HasAny()) return EmptyReadOnly<string, AliasDefinition>.Dictionary;

			return response.Indices[index].Aliases;
		}
	}
}
