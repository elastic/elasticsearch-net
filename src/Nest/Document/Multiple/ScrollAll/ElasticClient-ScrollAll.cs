// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Helper method that can parallelize a scroll using the sliced scroll feature of Elasticsearch, and return the results as an
		/// <see cref="IObservable{T}"/>.
		/// </summary>
		/// <param name="scrollTime">The time to keep the scroll active on the server until we send another scroll request</param>
		/// <param name="numberOfSlices">
		/// The number of slices to chop the scroll into, typically the number of shards but can be higher and using a
		/// custom routing key
		/// </param>
		IObservable<ScrollAllResponse<T>> ScrollAll<T>(Time scrollTime, int numberOfSlices,
			Func<ScrollAllDescriptor<T>, IScrollAllRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)
		)
			where T : class;

		/// <summary>
		/// Helper method that can parallelize a scroll using the sliced scroll feature of Elasticsearch and return the results as an
		/// <see cref="IObservable{T}"/>.
		/// </summary>
		IObservable<ScrollAllResponse<T>> ScrollAll<T>(IScrollAllRequest request, CancellationToken cancellationToken = default(CancellationToken))
			where T : class;
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IObservable<ScrollAllResponse<T>> ScrollAll<T>(Time scrollTime, int numberOfSlices,
			Func<ScrollAllDescriptor<T>, IScrollAllRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)
		)
			where T : class =>
			ScrollAll<T>(selector.InvokeOrDefault(new ScrollAllDescriptor<T>(scrollTime, numberOfSlices)), cancellationToken);

		/// <inheritdoc />
		public IObservable<ScrollAllResponse<T>> ScrollAll<T>(IScrollAllRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where T : class =>
			new ScrollAllObservable<T>(this, request, cancellationToken);
	}
}
