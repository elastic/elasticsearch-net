using System;
using Elasticsearch.Net.Connection;

namespace Elasticsearch.Net.Providers
{
	//TODO find all instantations and replace with DateTimeProvider.Default
	public class DateTimeProvider : IDateTimeProvider
	{
		public static TimeSpan DefaultTimeout = TimeSpan.FromSeconds(60);
		public static TimeSpan MaximumTimeout = TimeSpan.FromMinutes(30);

		public virtual DateTime Now() => DateTime.UtcNow;

		public virtual DateTime DeadTime(int attempts, TimeSpan? timeoutFactor, TimeSpan? maxDeadTimeout)
		{
			var timeout = timeoutFactor.GetValueOrDefault(DefaultTimeout);
			var maxTimeout = maxDeadTimeout.GetValueOrDefault(MaximumTimeout);
			var milliSeconds = Math.Min(timeout.TotalMilliseconds * 2 * Math.Pow(2, (attempts * 0.5 - 1)), maxTimeout.TotalMilliseconds);
			return Now().AddMilliseconds(milliSeconds);
		}
	}
}