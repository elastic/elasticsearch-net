using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Providers;

namespace Elasticsearch.Net.ConnectionPool
{
	public class StaticConnectionPool : IConnectionPool
	{
		private readonly IDateTimeProvider _dateTimeProvider;
		
		protected IDictionary<Uri, EndpointState> UriLookup;
		protected IList<Uri> NodeUris;
		protected int Current = -1;
		private Random _random;

		public int MaxRetries { get { return NodeUris.Count - 1;  } }

		public virtual bool AcceptsUpdates { get { return false; } }

		public bool UsingSsl { get; internal set; }

		public StaticConnectionPool(
			IEnumerable<Uri> uris, 
			bool randomizeOnStartup = true, 
			IDateTimeProvider dateTimeProvider = null)
		{
			_random = new Random(1337);
			_dateTimeProvider = dateTimeProvider ?? new DateTimeProvider();
			var rnd = new Random();
			uris.ThrowIfEmpty("uris");
			NodeUris = uris.Distinct().ToList();
			if (uris.Select(u => u.Scheme).Distinct().Count() > 1)
				throw new ArgumentException("Mixed URI schemes detected.");
			this.UsingSsl = uris.All(uri => uri.Scheme == Uri.UriSchemeHttps);
			if (randomizeOnStartup)
				NodeUris = NodeUris.OrderBy((item) => rnd.Next()).ToList();
			UriLookup = NodeUris.ToDictionary(k=>k, v=> new EndpointState());
		}

		public virtual Uri GetNext(int? initialSeed, out int seed, out bool shouldPingHint)
		{
			var count = NodeUris.Count;
			if (initialSeed.HasValue)
				initialSeed += 1;

			//always increment our round robin counter
			int increment = Interlocked.Increment(ref Current);
			var initialOffset = initialSeed ?? increment;
			int i = initialOffset % count, attempts = 0;
			seed = i;
			shouldPingHint = false;
			Uri uri = null;
			do
			{
				uri = this.NodeUris[i];
				var state = this.UriLookup[uri];
				lock (state)
				{
					var now = _dateTimeProvider.Now();
					if (state.Date <= now)
					{
						if (state.Attemps != 0)
							shouldPingHint = true;

						state.Attemps = 0;
						return uri;
					}
					Interlocked.Increment(ref Current);
				}
				Interlocked.Increment(ref state.Attemps);
				++attempts;
				i = (++initialOffset) % count;
				seed = i;
			} while (attempts < count);

			//could not find a suitable node retrying on node that has been dead longest.
			return this.NodeUris[i]; 
		}

		public virtual void MarkDead(Uri uri, int? deadTimeout, int? maxDeadTimeout)
		{	
			EndpointState state = null;
			if (!this.UriLookup.TryGetValue(uri, out state))
				return;
			lock(state)
			{
				state.Date = this._dateTimeProvider.DeadTime(uri, state.Attemps, deadTimeout, maxDeadTimeout);
			}
		}

		public virtual void MarkAlive(Uri uri)
		{
			EndpointState state = null;
			if (!this.UriLookup.TryGetValue(uri, out state))
				return;
			lock (state)
			{
				var aliveTime =this._dateTimeProvider.AliveTime(uri, state.Attemps); 
				state.Date = aliveTime;
				state.Attemps = 0;
			}
		}

		public virtual void UpdateNodeList(IList<Uri> newClusterState, Uri sniffNode = null)
		{
		}

	}
}