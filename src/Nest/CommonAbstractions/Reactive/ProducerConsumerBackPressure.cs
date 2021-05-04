// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	/// <summary>
	/// Simple back pressure implementation that makes sure the minimum max concurrency between producer and consumer
	/// is not amplified by the greedier of the two by more then the backPressureFactor which defaults to 4
	/// </summary>
	public class ProducerConsumerBackPressure
	{
		private readonly int _backPressureFactor;
		private readonly SemaphoreSlim _consumerLimiter;
		private readonly int _slots;

		/// <summary>
		/// Simple back pressure implementation that makes sure the minimum max concurrency between producer and consumer
		/// is not amplified by the greedier of the two by more then the backPressureFactor
		/// </summary>
		/// <param name="backPressureFactor">
		/// The maximum amplification back pressure of the greedier part of the producer consumer pipeline, if null
		/// defaults to 4
		/// </param>
		/// <param name="maxConcurrency">The minimum maximum concurrency which would be the bottleneck of the producer consumer pipeline</param>
		internal ProducerConsumerBackPressure(int? backPressureFactor, int maxConcurrency)
		{
			_backPressureFactor = backPressureFactor.GetValueOrDefault(4);
			_slots = maxConcurrency * _backPressureFactor;
			_consumerLimiter = new SemaphoreSlim(_slots, _slots);
		}

		public Task WaitAsync(CancellationToken token = default(CancellationToken)) =>
			_consumerLimiter.WaitAsync(token);

		public void Release()
		{
			var minimumRelease = _slots - _consumerLimiter.CurrentCount;
			var release = Math.Min(minimumRelease, _backPressureFactor);
			if (release > 0)
				_consumerLimiter.Release(release);
		}
	}
}
