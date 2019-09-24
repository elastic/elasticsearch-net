using System;

namespace Elasticsearch.Net.VirtualizedCluster.Providers
{
	public class TestableDateTimeProvider : DateTimeProvider
	{
		private DateTime MutableNow { get; set; } = DateTime.UtcNow;

		public override DateTime Now() => MutableNow;

		public void ChangeTime(Func<DateTime, DateTime> change) => MutableNow = change(MutableNow);
	}
}
