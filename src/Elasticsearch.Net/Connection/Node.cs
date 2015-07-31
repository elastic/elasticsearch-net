using System;
using PurifyNet;
using Elasticsearch.Net.Providers;

namespace Elasticsearch.Net.Connection
{
	public class Node
	{
		private readonly IDateTimeProvider _dateTimeProvider;

		public Node(Uri uri, IDateTimeProvider dateTimeProvider = null)
		{
			this._dateTimeProvider = dateTimeProvider ?? new DateTimeProvider();

			//this makes sure that paths stay relative i.e if the root uri is:
			//http://my-saas-provider.com/instance
			if (!uri.OriginalString.EndsWith("/", StringComparison.Ordinal))
				uri = new Uri(uri.OriginalString + "/");
			this.Uri = uri.Purify();
		}

		public Uri Uri { get; }

		public bool NeedsPing { get; }

		public DateTime DeadUntil { get; internal set; }

		public bool IsAlive { get; internal set; }

		public bool IsResurrected { get; internal set; }

		public int Attempts { get; internal set; }

		public void MarkDead(TimeSpan? timeout, TimeSpan? maxTimeout)
		{
			this.Attempts++;
			this.IsAlive = false;
			this.IsResurrected = false;
			this.DeadUntil = this._dateTimeProvider.DeadTime(this.Attempts, timeout, maxTimeout);
		}

		public void MarkAlive()
		{
			this.Attempts = 0;
			this.IsAlive = true;
			this.IsResurrected = false;
			this.DeadUntil = this._dateTimeProvider.AliveTime(); //TODO really needed or did we have this for mocking ease?
		}

		public Uri CreatePath(string path) => new Uri(this.Uri, path).Purify();
	}
}
