using System.Threading.Tasks;

namespace Nest
{
	public static class GetExtensions
	{
		public static IGetResponse<T> Get<T>(this IElasticClient client, string id, string index = null, string type = null)
			where T : class
		{
			return client.Get<T>(s => s.Id(id).Index(index).Type(type));
		}

		public static IGetResponse<T> Get<T>(this IElasticClient client, int id, string index = null, string type = null)
			where T : class
		{
			return client.Get<T>(s => s.Id(id).Index(index).Type(type));
		}

		public static Task<IGetResponse<T>> GetAsync<T>(this IElasticClient client, string id, string index = null, string type = null)
			where T : class
		{
			return client.GetAsync<T>(s => s.Id(id).Index(index).Type(type));
		}

		public static Task<IGetResponse<T>> GetAsync<T>(this IElasticClient client, int id, string index = null, string type = null)
			where T : class
		{
			return client.GetAsync<T>(s => s.Id(id).Index(index).Type(type));
		}
	}
}