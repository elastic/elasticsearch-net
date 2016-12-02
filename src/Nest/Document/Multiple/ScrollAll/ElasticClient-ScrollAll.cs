using System;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		IObservable<IScrollAllResponse<T>> ScrollAll<T>(Time scrollTime, int numberOfSlices, Func<ScrollAllDescriptor<T>, IScrollAllRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken))
			where T : class;

		IObservable<IScrollAllResponse<T>> ScrollAll<T>(IScrollAllRequest request, CancellationToken cancellationToken = default(CancellationToken)) where T : class;
	}

	public partial class ElasticClient
	{
		public IObservable<IScrollAllResponse<T>> ScrollAll<T>(Time scrollTime, int numberOfSlices, Func<ScrollAllDescriptor<T>, IScrollAllRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken))
			where T : class =>
			this.ScrollAll<T>(selector.InvokeOrDefault(new ScrollAllDescriptor<T>(scrollTime, numberOfSlices)), cancellationToken);

		public IObservable<IScrollAllResponse<T>> ScrollAll<T>(IScrollAllRequest request, CancellationToken cancellationToken = default(CancellationToken))
			where T : class =>
			new ScrollAllObservable<T>(this, ConnectionSettings, request, cancellationToken);
	}
}
