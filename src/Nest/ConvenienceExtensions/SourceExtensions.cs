using System.Threading.Tasks;

namespace Nest
{
	public static class SourceExtensions
	{
		public static T Source<T>(this IElasticClient client, string id, string index = null, string type = null)
			where T : class
		{
			return client.Source<T>(s => s.Id(id).Index(index).Type(type));
		}

		public static T Source<T>(this IElasticClient client, int id, string index = null, string type = null)
			where T : class
		{
			return client.Source<T>(s => s.Id(id).Index(index).Type(type));
		}

		public static Task<T> SourceAsync<T>(this IElasticClient client, string id, string index = null, string type = null)
			where T : class
		{
			return client.SourceAsync<T>(s => s.Id(id).Index(index).Type(type));
		}

		public static Task<T> SourceAsync<T>(this IElasticClient client, int id, string index = null, string type = null)
			where T : class
		{
			return client.SourceAsync<T>(s => s.Id(id).Index(index).Type(type));
		}
	}
}