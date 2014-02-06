using System;
using System.Threading.Tasks;

namespace Nest
{
	public static class DeleteExtensions
	{
		public static IDeleteResponse Delete<T>(this IElasticClient client, int id, Func<DeleteDescriptor<T>, DeleteDescriptor<T>> selector = null) where T : class
		{
			selector = selector ?? (s => s);
			return client.Delete<T>(s => selector(s.Id(id)));
		}
		public static IDeleteResponse Delete<T>(this IElasticClient client, string id, Func<DeleteDescriptor<T>, DeleteDescriptor<T>> selector = null) where T : class
		{
			selector = selector ?? (s => s);
			return client.Delete<T>(s => selector(s.Id(id)));
		}

		public static Task<IDeleteResponse> DeleteAsync<T>(this IElasticClient client, int id, Func<DeleteDescriptor<T>, DeleteDescriptor<T>> selector = null) where T : class
		{
			selector = selector ?? (s => s);
			return client.DeleteAsync<T>(s => selector(s.Id(id)));
		}

		public static Task<IDeleteResponse> DeleteAsync<T>(this IElasticClient client, string id, Func<DeleteDescriptor<T>, DeleteDescriptor<T>> selector = null) where T : class
		{
			selector = selector ?? (s => s);
			return client.DeleteAsync<T>(s => selector(s.Id(id)));
		}
	}
}