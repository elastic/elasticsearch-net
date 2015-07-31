using System;

namespace Elasticsearch.Net.Providers
{
	public interface IDateTimeProvider
	{
		DateTime Now();
		DateTime DeadTime(int attempts, TimeSpan? timeoutFactor, TimeSpan? maxDeadTimeout);
		DateTime AliveTime();
	}
}