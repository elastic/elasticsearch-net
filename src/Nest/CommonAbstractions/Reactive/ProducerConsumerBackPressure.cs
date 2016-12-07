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
		private readonly SemaphoreSlim _consumerLimiter;
		private readonly int _backPressureFactor;
		private readonly int _slots;

		/// <summary>
		/// Simple back pressure implementation that makes sure the minimum max concurrency between producer and consumer
		/// is not amplified by the greedier of the two by more then the backPressureFactor
		/// </summary>
		/// <param name="backPressureFactor">The maximum amplification back pressure of the greedier part of the producer consumer pipeline, if null defaults to 4</param>
		/// <param name="maxConcurrency">The minimum maximum concurrency which would be the bottleneck of the producer consumer pipeline</param>
		internal ProducerConsumerBackPressure(int? backPressureFactor, int maxConcurrency)
		{
			this._backPressureFactor = backPressureFactor.GetValueOrDefault(4);
			this._slots = maxConcurrency * this._backPressureFactor;
			this._consumerLimiter = new SemaphoreSlim(_slots, _slots);
		}

		public Task WaitAsync(CancellationToken token = default(CancellationToken)) =>
			this._consumerLimiter.WaitAsync(token);

		public void Release()
		{
			var minimumRelease = _slots - this._consumerLimiter.CurrentCount;
			var release = Math.Min(minimumRelease, this._backPressureFactor);
			if (release > 0)
				this._consumerLimiter.Release(release);
		}
	}
}