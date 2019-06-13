using System;
using System.Threading;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Helper method that can parallelize a scroll using the sliced scroll feature of elasticsearch and return the results as an IObservable.
		/// </summary>
		/// <param name="scrollTime">The time to keep the scroll active on the server until we send another scroll request</param>
		/// <param name="numberOfSlices">
		/// The number of slices to chop the scroll into, typically the number of shards but can be higher and using a
		/// custom routing key
		/// </param>
		public static IObservable<ScrollAllResponse<T>> ScrollAll<T>(this IElasticClient client,Time scrollTime, int numberOfSlices,
			Func<ScrollAllDescriptor<T>, IScrollAllRequest> selector = null, CancellationToken ct = default
		)
			where T : class;

		/// <summary>
		/// Helper method that can parallelize a scroll using the sliced scroll feature of elasticsearch and return the results as an IObservable.
		/// </summary>
		public static IObservable<ScrollAllResponse<T>> ScrollAll<T>(this IElasticClient client,IScrollAllRequest request, CancellationToken ct = default)
			where T : class;
	}

}
