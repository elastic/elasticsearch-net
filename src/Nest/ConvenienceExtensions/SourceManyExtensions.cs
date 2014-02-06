using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Nest
{
	public static class SourceManyExtensions
	{
		public static IEnumerable<T> SourceMany<T>(this IElasticClient client, IEnumerable<string> ids, string index = null, string type = null)
			where T : class
		{
			var result = client.MultiGet(s => s.GetMany<T>(ids, (gs, i) => gs.Index(index).Type(type)));
			return result.SourceMany<T>(ids);
		}

		public static IEnumerable<T> SourceMany<T>(this IElasticClient client, IEnumerable<int> ids, string index = null, string type = null)
			where T : class
		{
			return client.SourceMany<T>(ids.Select(i => i.ToString(CultureInfo.InvariantCulture)), index, type);
		}

		public static Task<IEnumerable<T>> SourceManyAsync<T>(this IElasticClient client, IEnumerable<string> ids, string index = null, string type = null)
			where T : class
		{
			return client.MultiGetAsync(s => s.GetMany<T>(ids, (gs, i) => gs.Index(index).Type(type)))
				.ContinueWith(t => t.Result.SourceMany<T>(ids));
		}

		public static Task<IEnumerable<T>> SourceManyAsync<T>(this IElasticClient client, IEnumerable<int> ids, string index = null, string type = null)
			where T : class
		{
			return client.SourceManyAsync<T>(ids.Select(i => i.ToString(CultureInfo.InvariantCulture)), index, type);
		}
	}
}