using System;

namespace Elasticsearch.Net.Providers
{
	public class DateTimeProvider : IDateTimeProvider
	{
		public virtual DateTime Now()
		{
			return DateTime.UtcNow;
		}

		public virtual DateTime DeadTime(Uri uri, int attempts)
		{
			var seconds = Math.Min(60 * 2 * Math.Pow(2, (attempts * 0.5 - 1)), 60 * 30);
			return DateTime.UtcNow.AddSeconds(seconds);
		}
		
		public virtual DateTime AliveTime(Uri uri, int attempts)
		{
			return new DateTime();
		}
	}
}