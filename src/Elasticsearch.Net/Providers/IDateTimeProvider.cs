using System;

namespace Elasticsearch.Net_5_2_0
{
	public interface IDateTimeProvider
	{
		DateTime Now();
		DateTime DeadTime(int attempts, TimeSpan? timeoutFactor, TimeSpan? maxDeadTimeout);
	}
}