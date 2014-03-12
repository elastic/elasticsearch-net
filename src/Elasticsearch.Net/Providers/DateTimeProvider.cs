using System;
using Elasticsearch.Net.Connection;

namespace Elasticsearch.Net.Providers
{
	public class DateTimeProvider : IDateTimeProvider
	{

		public virtual DateTime Now()
		{
			return DateTime.UtcNow;
		}

		public virtual DateTime DeadTime(Uri uri, int attempts, int? timeoutFactor = null, int? maxDeadTimeout = null)
		{
			var timeout = timeoutFactor.GetValueOrDefault(60 * 1000);
			var maxTimeout = maxDeadTimeout.GetValueOrDefault(60 * 1000 * 30);
			var seconds = Math.Min(timeout * 2 * Math.Pow(2, (attempts * 0.5 - 1)), maxTimeout);
			return DateTime.UtcNow.AddMilliseconds(seconds);
		}
		
		public virtual DateTime AliveTime(Uri uri, int attempts)
		{
			return new DateTime();
		}
	}
}