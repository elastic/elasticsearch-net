using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Nest
{
	public static class GetManyExtensions
	{
		public static IEnumerable<IMultiGetHit<T>> GetMany<T>(this IElasticClient client, IEnumerable<string> ids, string index = null, string type = null)
			where T : class
		{
			var result = client.MultiGet(s => s.GetMany<T>(ids, (gs, i) => gs.Index(index).Type(type)));
			return result.GetMany<T>(ids);
		}

		public static IEnumerable<IMultiGetHit<T>> GetMany<T>(this IElasticClient client, IEnumerable<int> ids, string index = null, string type = null)
			where T : class
		{
			return client.GetMany<T>(ids.Select(i => i.ToString(CultureInfo.InvariantCulture)), index, type);
		}

		public static Task<IEnumerable<IMultiGetHit<T>>> GetManyAsync<T>(this IElasticClient client, IEnumerable<string> ids, string index = null, string type = null)
			where T : class
		{
			return client.MultiGetAsync(s => s.GetMany<T>(ids, (gs, i) => gs.Index(index).Type(type)))
				.ContinueWith(t => t.Result.GetMany<T>(ids));
		}

		public static Task<IEnumerable<IMultiGetHit<T>>> GetManyAsync<T>(this IElasticClient client, IEnumerable<int> ids, string index = null, string type = null)
			where T : class
		{
			return client.GetManyAsync<T>(ids.Select(i => i.ToString(CultureInfo.InvariantCulture)), index, type);
		}
	}
}