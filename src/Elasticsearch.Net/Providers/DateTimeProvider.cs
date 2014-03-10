using System;

namespace Elasticsearch.Net.Providers
{
	public class DateTimeProvider : IDateTimeProvider
	{
		public DateTime Now()
		{
			return DateTime.UtcNow;
		}

		public DateTime DeadTime(Uri uri, int attempts)
		{
			return DateTime.UtcNow.AddSeconds(60);
		}
		
		public DateTime AliveTime(Uri uri, int attempts)
		{
			return new DateTime();
		}
	}
}