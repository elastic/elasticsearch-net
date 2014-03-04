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
		public DateTime date = DateTime.UtcNow.AddYears(-1);
	}

	public class StaticConnectionPool : IConnectionPool
	{
		private readonly IDictionary<Uri, EndpointState> _uriLookup;
		private readonly IList<Uri> _uris;

		public int MaxRetries { get { return _uris.Count - 1;  } }
		
		private int _current = -1;

		public StaticConnectionPool(IEnumerable<Uri> uris, bool randomizeOnStartup = true)
		{
			var rnd = new Random();
			uris.ThrowIfEmpty("uris");
			_uris = uris.ToList();
			if (randomizeOnStartup)
				_uris = _uris.OrderBy((item) => rnd.Next()).ToList();
			_uriLookup = _uris.ToDictionary(k=>k, v=> new EndpointState());
		}

		public Uri GetNext()
		{
			var attempts = 0;
			Uri uri = null;
			do
			{
				var c = Interlocked.Increment(ref _current);
				var i = c%_uris.Count;
				uri = this._uris[i];
				var state = this._uriLookup[uri];
				if (state.date <= Now())
					return uri;

				++attempts;
			} while (attempts < _uris.Count);
			throw new OutOfNodesException("Tried {0} different nodes".F(attempts));
		}

		public virtual DateTime Now()
		{
			return DateTime.UtcNow;
		}

		public void MarkDead(Uri uri)
		{
			//DateTime dateTime = DateTime.UtcNow;
			//if (!this._uriLookup.TryGetValue(uri, out dateTime))
			//	return;
		}

		public void MarkAlive(Uri uri)
		{
			throw new NotImplementedException();
		}
	}


	//public class ConnectionPool
	//{
	//	public Dictionary<Uri, > 


	//	public ConnectionPool(IConnection connection)
	//	{
	//		connection.ThrowIfNull("connection");
	//		_connection = connection;
	//	}

	//	public Uri GetNextEndpoint()
	//	{
			
	//	}

	//}
}
