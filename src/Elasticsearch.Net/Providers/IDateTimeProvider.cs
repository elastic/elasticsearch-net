using System;

namespace Elasticsearch.Net
{
	public interface IDateTimeProvider
	{
		DateTime Now();

		DateTime DeadTime(int attempts, TimeSpan? timeoutFactor, TimeSpan? maxDeadTimeout);
	}
}
