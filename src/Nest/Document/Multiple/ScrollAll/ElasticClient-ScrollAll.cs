/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
