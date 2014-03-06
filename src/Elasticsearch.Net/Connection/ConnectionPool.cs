using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Elasticsearch.Net.Exceptions;

namespace Elasticsearch.Net.Connection
{
	public interface IConnectionPool
	{
		int MaxRetries { get; }

		Uri GetNext();
		void MarkDead(Uri uri);
		void MarkAlive(Uri uri);
	}

	public class SingleNodeConnectionPool : IConnectionPool
	{
		private readonly Uri _uri;
		
		public int MaxRetries { get { return 0;  } }

		public SingleNodeConnectionPool(Uri uri)
		{
			//this makes sure that paths stay relative i.e if the root uri is:
			//http://my-saas-provider.com/instance
			if (!uri.OriginalString.EndsWith("/"))
				uri = new Uri(uri.OriginalString + "/");
			_uri = uri;
		}

		public Uri GetNext()
		{
			return _uri;
		}

		public void MarkDead(Uri uri)
		{

		}

		public void MarkAlive(Uri uri)
		{
			
		}
	}

	public class EndpointState
	{
		public int _attempts = 0;
		public DateTime date = new DateTime();
	}


	public interface IDateTimeProvider
	{
		DateTime Now();
		DateTime DeadTime(Uri uri, int attempts);
		DateTime AliveTime(Uri uri, int attempts);
	}

	public class DateTimeProvider : IDateTimeProvider
	{
		public DateTime Now()
		{
			return DateTime.UtcNow;
		}

		public DateTime DeadTime(Uri uri, int attempts)
		{
			return DateTime.UtcNow.AddSeconds(60);
		}
		
		public DateTime AliveTime(Uri uri, int attempts)
		{
			return new DateTime();
		}
	}

	public class StaticConnectionPool : IConnectionPool
	{
		private readonly IDictionary<Uri, EndpointState> _uriLookup;
		private readonly IList<Uri> _nodeUris;

		public int MaxRetries { get { return _nodeUris.Count - 1;  } }
		
		private int _current = -1;
		private readonly IDateTimeProvider _dateTimeProvider;

		public StaticConnectionPool(IEnumerable<Uri> uris, bool randomizeOnStartup = true, IDateTimeProvider dateTimeProvider = null)
		{
			_dateTimeProvider = dateTimeProvider ?? new DateTimeProvider();
			var rnd = new Random();
			uris.ThrowIfEmpty("uris");
			_nodeUris = uris.ToList();
			if (randomizeOnStartup)
				_nodeUris = _nodeUris.OrderBy((item) => rnd.Next()).ToList();
			_uriLookup = _nodeUris.ToDictionary(k=>k, v=> new EndpointState());
		}

		public Uri GetNext()
		{
			var attempts = 0;
			Uri uri = null;
			do
			{
				var c = Interlocked.Increment(ref _current);
				var i = c%_nodeUris.Count;
				uri = this._nodeUris[i];
				var state = this._uriLookup[uri];
				if (state.date <= _dateTimeProvider.Now())
				{
					state._attempts = 0;
					return uri;
				}
				Interlocked.Increment(ref state._attempts);
				++attempts;
			} while (attempts < _nodeUris.Count);

			//could not find a suitable node retrying on node that has been dead longest.
			return this._nodeUris[0]; //todo random;
		}

		public void MarkDead(Uri uri)
		{	
			EndpointState state = null;
			if (!this._uriLookup.TryGetValue(uri, out state))
				return;
			state.date = this._dateTimeProvider.DeadTime(uri, state._attempts);
		}

		public void MarkAlive(Uri uri)
		{
			EndpointState state = null;
			if (!this._uriLookup.TryGetValue(uri, out state))
				return;
			state.date = this._dateTimeProvider.AliveTime(uri, state._attempts);
			state._attempts = 0;
		}
	}
}
