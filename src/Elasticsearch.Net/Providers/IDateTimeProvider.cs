using System;

namespace Elasticsearch.Net.Providers
{
	public interface IDateTimeProvider
	{
		DateTime Now();
		DateTime DeadTime(Uri uri, int attempts, int? timeoutFactor = null, int? maxDeadTimeout = null);
		DateTime AliveTime(Uri uri, int attempts);
	}
}