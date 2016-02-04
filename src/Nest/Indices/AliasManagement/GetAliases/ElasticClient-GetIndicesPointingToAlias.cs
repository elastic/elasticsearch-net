using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nest
{
	/// <summary>Implements several handy alias extensions.
	/// </summary>
	public static class IndicesPointingToAliasExtensions
	{
		/// <summary>
		/// Returns a list of indices that have the specified aliasName applied to them. Simplified version of GetAliases.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="aliasName">The exact alias name</param>
		public static IList<string> GetIndicesPointingToAlias(this IElasticClient client, string aliasName)
		{
			var aliasesResponse = client.GetAlias(a => a.Alias(aliasName));
			return IndicesPointingToAlias(aliasName, aliasesResponse);
		}

		/// <summary>
		/// Returns a list of indices that have the specified aliasName applied to them. Simplified version of GetAliases.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="aliasName">The exact alias name</param>
		public static async Task<IList<string>> GetIndicesPointingToAliasAsync(this IElasticClient client, string aliasName)
		{
			var response = await client.GetAliasAsync(a => a.Index(aliasName)).ConfigureAwait(false);
			return IndicesPointingToAlias(aliasName, response);
		}

		private static IList<string> IndicesPointingToAlias(string aliasName, IGetAliasesResponse aliasesResponse)
		{
			if (!aliasesResponse.IsValid
				|| !aliasesResponse.Indices.HasAny())
				return new string [] { };

			var indices = from i in aliasesResponse.Indices
						  where i.Value.Any(a => a.Name == aliasName)
						  select i.Key;

			return indices.ToList();
		}
	}
}