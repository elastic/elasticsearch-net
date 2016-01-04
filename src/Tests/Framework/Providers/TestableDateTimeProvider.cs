using System;
using Elasticsearch.Net;

namespace Tests.Framework
{
	public class TestableDateTimeProvider : DateTimeProvider
	{
		private DateTime MutableNow { get; set; } = DateTime.UtcNow;

		public override DateTime Now() => MutableNow;

		public void ChangeTime(Func<DateTime, DateTime> change) => this.MutableNow = change(this.MutableNow);
	}
}
