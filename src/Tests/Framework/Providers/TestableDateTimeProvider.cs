using Elasticsearch.Net.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Framework
{
	public class TestableDateTimeProvider : DateTimeProvider
	{
		private DateTime MutableNow { get; set; } = DateTime.UtcNow;

		public override DateTime Now() => MutableNow;

		public void ChangeTime(Func<DateTime, DateTime> change) => this.MutableNow = change(this.MutableNow);
	}
}
