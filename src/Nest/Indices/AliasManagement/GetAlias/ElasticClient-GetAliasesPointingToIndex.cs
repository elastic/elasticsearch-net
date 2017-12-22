using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static class AliasPointingToIndexExtensions
	{
		/// <summary>
		/// Returns a dictionary of aliases that point to the specified index, simplified version of <see cref="IElasticClient.GetAlias(IGetAliasRequest)"/>..
		/// </summary>
		/// <param name="index">The index name we want to know aliases of</param>
		public static IReadOnlyDictionary<string, AliasDefinition> GetAliasesPointingToIndex(this IElasticClient client, IndexName index)
		{
			var response = client.GetAlias(a => a.Index(index).RequestConfiguration(r=>r.ThrowExceptions()));
			return AliasesPointingToIndex(index, response);
		}

		/// <summary>
		/// Returns a dictionary of aliases that point to the specified index, simplified version of <see cref="IElasticClient.GetAlias(IGetAliasRequest)"/>.
		/// </summary>
		/// <param name="index">The index name we want to know aliases of</param>
		public static async Task<IReadOnlyDictionary<string, AliasDefinition>> GetAliasesPointingToIndexAsync(this IElasticClient client, IndexName index)
		{
			var response = await client.GetAliasAsync(a => a.Index(index).RequestConfiguration(r=>r.ThrowExceptions())).ConfigureAwait(false);
			return AliasesPointingToIndex(index, response);
		}

		private static IReadOnlyDictionary<string, AliasDefinition> AliasesPointingToIndex(IndexName index, IGetAliasResponse response)
		{
			if (!response.IsValid || !response.Indices.HasAny()) return EmptyReadOnly<string, AliasDefinition>.Dictionary;
			return response.Indices[index].Aliases;
		}
	}
}
