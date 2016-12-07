using System;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Helper method that can parallelize a scroll using the sliced scroll feature of elasticsearch and return the results as an IObservable.
		/// </summary>
		/// <param name="scrollTime">The time to keep the scroll active on the server until we send another scroll request</param>
		/// <param name="numberOfSlices">The number of slices to chop the scroll into, typically the number of shards but can be higher and using a custom routing key</param>
		IObservable<IScrollAllResponse<T>> ScrollAll<T>(Time scrollTime, int numberOfSlices, Func<ScrollAllDescriptor<T>, IScrollAllRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken))
			where T : class;

		/// <summary>
		/// Helper method that can parallelize a scroll using the sliced scroll feature of elasticsearch and return the results as an IObservable.
		/// </summary>
		IObservable<IScrollAllResponse<T>> ScrollAll<T>(IScrollAllRequest request, CancellationToken cancellationToken = default(CancellationToken)) where T : class;
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IObservable<IScrollAllResponse<T>> ScrollAll<T>(Time scrollTime, int numberOfSlices, Func<ScrollAllDescriptor<T>, IScrollAllRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken))
			where T : class =>
			this.ScrollAll<T>(selector.InvokeOrDefault(new ScrollAllDescriptor<T>(scrollTime, numberOfSlices)), cancellationToken);

		/// <inheritdoc/>
		public IObservable<IScrollAllResponse<T>> ScrollAll<T>(IScrollAllRequest request, CancellationToken cancellationToken = default(CancellationToken))
			where T : class =>
			new ScrollAllObservable<T>(this, request, cancellationToken);
	}
}
