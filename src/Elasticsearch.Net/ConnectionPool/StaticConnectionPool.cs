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
		
		protected IDictionary<Uri, EndpointState> _uriLookup;
		protected IList<Uri> _nodeUris;
		protected int _current = -1;
		protected bool _hasSeenStartup;
		private bool _canUpdateNodeList;

		public int MaxRetries { get { return _nodeUris.Count - 1;  } }

		public virtual bool AcceptsUpdates { get { return false; } }

		public bool HasSeenStartup
		{
			get { return _hasSeenStartup; }
		}

		public StaticConnectionPool(
			IEnumerable<Uri> uris, 
			bool randomizeOnStartup = true, 
			IDateTimeProvider dateTimeProvider = null)
		{
			_dateTimeProvider = dateTimeProvider ?? new DateTimeProvider();
			var rnd = new Random();
			uris.ThrowIfEmpty("uris");
			_nodeUris = uris.ToList();
			if (randomizeOnStartup)
				_nodeUris = _nodeUris.OrderBy((item) => rnd.Next()).ToList();
			_uriLookup = _nodeUris.ToDictionary(k=>k, v=> new EndpointState());
		}

		public virtual Uri GetNext(int? initialSeed, out int seed, out bool shouldPingHint)
		{
			var count = _nodeUris.Count;
			if (initialSeed.HasValue)
				initialSeed += 1;

			//always increment our round robin counter
			int increment = Interlocked.Increment(ref _current);
			var initialOffset = initialSeed ?? increment;
			int i = initialOffset % count, attempts = 0;
			seed = i;
			shouldPingHint = false;
			Uri uri = null;
			do
			{
				uri = this._nodeUris[i];
				var state = this._uriLookup[uri];
				lock (state)
				{
					var now = _dateTimeProvider.Now();
					if (state.date <= now)
					{
						if (state._attempts != 0)
							shouldPingHint = true;

						state._attempts = 0;
						return uri;
					}
					Interlocked.Increment(ref _current);
				}
				Interlocked.Increment(ref state._attempts);
				++attempts;
				i = (++initialOffset) % count;
				seed = i;
			} while (attempts < count);

			//could not find a suitable node retrying on node that has been dead longest.
			return this._nodeUris[i]; 
		}

		public virtual void MarkDead(Uri uri, int? deadTimeout, int? maxDeadTimeout)
		{	
			EndpointState state = null;
			if (!this._uriLookup.TryGetValue(uri, out state))
				return;
			lock(state)
			{
				state.date = this._dateTimeProvider.DeadTime(uri, state._attempts, deadTimeout, maxDeadTimeout);
			}
		}

		public virtual void MarkAlive(Uri uri)
		{
			EndpointState state = null;
			if (!this._uriLookup.TryGetValue(uri, out state))
				return;
			lock (state)
			{
				var aliveTime =this._dateTimeProvider.AliveTime(uri, state._attempts); 
				state.date = aliveTime;
				state._attempts = 0;
			}
		}

		public virtual void UpdateNodeList(IList<Uri> newClusterState, bool fromStartupHint = false)
		{
		}

	}
}