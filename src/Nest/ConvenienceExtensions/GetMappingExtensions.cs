using System;
using System.Threading.Tasks;

namespace Nest
{
	public static class GetMappingExtensions
	{
		public static IGetMappingResponse GetMapping<T>(
			this IElasticClient client,
			Func<GetMappingDescriptor, GetMappingDescriptor> selector = null)
			where T : class
		{
			selector = selector ?? (s => s);
			return client.GetMapping(s => selector(s.Index<T>().Type<T>()));
		}

		public static Task<IGetMappingResponse> GetMappingAsync<T>(
			this IElasticClient client,
			Func<GetMappingDescriptor, GetMappingDescriptor> selector = null)
			where T : class
		{
			selector = selector ?? (s => s);
			return client.GetMappingAsync(s => selector(s.Index<T>().Type<T>()));
		}
	}
}