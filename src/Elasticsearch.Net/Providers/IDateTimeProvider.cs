using System;

namespace Elasticsearch.Net
{
	public interface IDateTimeProvider
	{
		DateTime DeadTime(int attempts, TimeSpan? timeoutFactor, TimeSpan? maxDeadTimeout);

		DateTime Now();
	}
}
