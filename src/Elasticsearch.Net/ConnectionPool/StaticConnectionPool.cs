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
		protected IDictionary<Uri, EndpointState> _uriLookup;
		protected IList<Uri> _nodeUris;

		public int MaxRetries { get { return _nodeUris.Count - 1;  } }
		
		private int _current = -1;
		private readonly IDateTimeProvider _dateTimeProvider;

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

		public virtual Uri GetNext()
		{
			var initialOffset = Interlocked.Increment(ref _current);
			var count = _nodeUris.Count;
			int i = initialOffset % count, attempts = 0;
			Uri uri = null;
			do
			{
				uri = this._nodeUris[i];
				var state = this._uriLookup[uri];
				lock (state)
				{
					if (state.date <= _dateTimeProvider.Now())
					{
						state._attempts = 0;
						return uri;
					}
				}
				Interlocked.Increment(ref state._attempts);
				++attempts;
				i = (++initialOffset) % count;
			} while (attempts < count);

			//could not find a suitable node retrying on node that has been dead longest.
			return this._nodeUris[0]; //todo random;
		}

		public virtual void MarkDead(Uri uri)
		{	
			EndpointState state = null;
			if (!this._uriLookup.TryGetValue(uri, out state))
				return;
			lock(state)
			{
				state.date = this._dateTimeProvider.DeadTime(uri, state._attempts);
			}
		}

		public virtual void MarkAlive(Uri uri)
		{
			EndpointState state = null;
			if (!this._uriLookup.TryGetValue(uri, out state))
				return;
			lock (state)
			{
				state.date = this._dateTimeProvider.AliveTime(uri, state._attempts);
				state._attempts = 0;
			}
		}

		public virtual void Sniff(IConnection connection, bool fromStartupHint = false)
		{
			//NOOP on static connection class
		}
	}
}