using System;
using System.Threading.Tasks;

namespace Nest
{
	public static class DeleteMappingExtensions
	{
		public static IIndicesResponse DeleteMapping<T>(
			this IElasticClient client,
			Func<DeleteMappingDescriptor, DeleteMappingDescriptor> selector = null)
			where T : class
		{
			selector = selector ?? (s => s);
			return client.DeleteMapping(s => selector(s.Index<T>().Type<T>()));
		}

		public static Task<IIndicesResponse> DeleteMappingAsync<T>(
			this IElasticClient client,
			Func<DeleteMappingDescriptor, DeleteMappingDescriptor> selector = null)
			where T : class
		{
			selector = selector ?? (s => s);
			return client.DeleteMappingAsync(s => selector(s.Index<T>().Type<T>()));
		}
	}
}