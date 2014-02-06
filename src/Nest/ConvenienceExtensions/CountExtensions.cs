using System;
using System.Threading.Tasks;

namespace Nest
{
	public static class CountExtensions
	{
		/// <summary>
		/// Untyped count, defaults to all indices and types
		/// </summary>
		/// <param name="countSelector">filter on indices and types or pass pararameters</param>
		public static ICountResponse Count(this IElasticClient client,
			Func<CountDescriptor<dynamic>, CountDescriptor<dynamic>> countSelector = null)
		{
			countSelector = countSelector ?? (s => s);
			return client.Count<dynamic>(c => countSelector(c.AllIndices().AllTypes()));
		}

		/// <summary>
		/// Untyped count, defaults to all indices and types
		/// </summary>
		/// <param name="countSelector">filter on indices and types or pass pararameters</param>
		public static Task<ICountResponse> CountAsync(this IElasticClient client,
			Func<CountDescriptor<dynamic>, CountDescriptor<dynamic>> countSelector = null)
		{
			countSelector = countSelector ?? (s => s);
			return client.CountAsync<dynamic>(c => countSelector(c.AllIndices().AllTypes()));
		}
	}
}