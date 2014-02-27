using System;
using System.Threading.Tasks;

namespace Nest
{
	/// <summary></summary>
	public static class CountExtensions
	{
		/// <summary>
		/// Counts the specified client.
		/// </summary>
		/// <param name="client">The client.</param>
		/// <param name="countSelector">The count selector.</param>
		/// <returns>ICountResponse.</returns>
		public static ICountResponse Count(this IElasticClient client,
			Func<CountDescriptor<dynamic>, CountDescriptor<dynamic>> countSelector = null)
		{
			countSelector = countSelector ?? (s => s);
			return client.Count<dynamic>(c => countSelector(c.AllIndices().AllTypes()));
		}

		/// <summary>
		/// Counts the asynchronous.
		/// </summary>
		/// <param name="client">The client.</param>
		/// <param name="countSelector">The count selector.</param>
		/// <returns>Task{ICountResponse}.</returns>
		public static Task<ICountResponse> CountAsync(this IElasticClient client,
			Func<CountDescriptor<dynamic>, CountDescriptor<dynamic>> countSelector = null)
		{
			countSelector = countSelector ?? (s => s);
			return client.CountAsync<dynamic>(c => countSelector(c.AllIndices().AllTypes()));
		}
	}
}