using System.Threading.Tasks;

namespace Nest
{
	public static class ScrollExtensions
	{
		public static IQueryResponse<T> Scroll<T>(this IElasticClient client, string scrollTime, string scrollId) where T : class
		{
			return client.Scroll<T>(s => s.Scroll(scrollTime).ScrollId(scrollId));
		}

		public static Task<IQueryResponse<T>> ScrollAsync<T>(this IElasticClient client, string scrollTime, string scrollId) where T : class
		{
			return client.ScrollAsync<T>(s => s.Scroll(scrollTime).ScrollId(scrollId));
		}
	}
}