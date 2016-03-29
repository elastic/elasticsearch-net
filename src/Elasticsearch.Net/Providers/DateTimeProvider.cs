using System;

namespace Elasticsearch.Net
{
	public class DateTimeProvider : IDateTimeProvider
	{
		private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(60);
		private static readonly TimeSpan MaximumTimeout = TimeSpan.FromMinutes(30);

		public static readonly DateTimeProvider Default = new DateTimeProvider();

		public virtual DateTime Now() => DateTime.UtcNow;

		public virtual DateTime DeadTime(int attempts, TimeSpan? timeoutFactor, TimeSpan? maxDeadTimeout)
		{
			var timeout = timeoutFactor.GetValueOrDefault(DefaultTimeout);
			var maxTimeout = maxDeadTimeout.GetValueOrDefault(MaximumTimeout);
			var milliSeconds = Math.Min(timeout.TotalMilliseconds * 2 * Math.Pow(2, attempts * 0.5 - 1), maxTimeout.TotalMilliseconds);
			return Now().AddMilliseconds(milliSeconds);
		}
	}
}