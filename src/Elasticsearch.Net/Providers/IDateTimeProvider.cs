using System;

namespace Elasticsearch.Net.Providers
{
	public interface IDateTimeProvider
	{
		DateTime Now();
		DateTime DeadTime(Uri uri, int attempts);
		DateTime AliveTime(Uri uri, int attempts);
	}
}