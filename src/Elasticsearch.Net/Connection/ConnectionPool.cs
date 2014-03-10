using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Elasticsearch.Net.Exceptions;

namespace Elasticsearch.Net.Connection
{
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

	public static class Sniffer
	{
		private static readonly Regex _uriParse = new Regex(@"inet\[\/([^:]+):(\d+)");
		private class NodeInfoResponse
		{
			public IDictionary<string, NodeState> nodes { get; set; }
		}

		private class NodeState
		{
			public string http_address { get; set; }
			public string https_address { get; set; }
			public string thrift_address { get; set; }
		}

		private static Uri Parse(string scheme, string inetString)
		{
			var match = _uriParse.Match(inetString);
			var host = match.Groups[1].Value;
			var port = match.Groups[2].Value;
			return new Uri("{0}://{1}:{2}".F(scheme, host, port));

		}
		public static IList<Uri> FromStream(Stream stream, IElasticsearchSerializer serializer)
		{
			using (var memoryStream = new MemoryStream())
			{
				stream.CopyTo(memoryStream);
				var response = serializer.Deserialize<NodeInfoResponse>(memoryStream.ToArray());
				var l = new List<Uri>();
				foreach(var kv in response.nodes.Values)
				{
					//TODO parse address since its in inet[] form
					if (!kv.http_address.IsNullOrEmpty())
						l.Add(Parse("http", kv.http_address));
					else if (!kv.https_address.IsNullOrEmpty())
						l.Add(Parse("https",kv.https_address));
					else if (!kv.thrift_address.IsNullOrEmpty())
						l.Add(Parse("http", kv.thrift_address));
				}
				return l;
			}
		}
	}

	public class SniffingConnectionPool : StaticConnectionPool
	{
		private readonly ReaderWriterLockSlim _readerWriter = new ReaderWriterLockSlim();

		private bool _seenStartup = false;

		public SniffingConnectionPool(
			IEnumerable<Uri> uris, 
			bool randomizeOnStartup = true, 
			IDateTimeProvider dateTimeProvider = null)
			: base(uris, randomizeOnStartup, dateTimeProvider)
		{
		}

		public override void Sniff(IConnection connection, bool fromStartupHint = false)
		{
			if (fromStartupHint && _seenStartup)
				return;

			try
			{
				var uri = this.GetNext();
				
				this._readerWriter.EnterWriteLock();
				var nodes = connection.Sniff(uri, 50);

				this._nodeUris = nodes;
				this._uriLookup = nodes.ToDictionary(k => k, v => new EndpointState());
				if (fromStartupHint)
					this._seenStartup = true;

			}
			finally
			{
				this._readerWriter.ExitWriteLock();
			}
		}

		public override Uri GetNext()
		{
			try
			{
				this._readerWriter.EnterReadLock();
				return base.GetNext();
			}
			finally
			{
				this._readerWriter.ExitReadLock();
			}
		}

		public override void MarkAlive(Uri uri)
		{
			try
			{
				this._readerWriter.EnterReadLock();
				base.MarkAlive(uri);
			}
			finally
			{
				this._readerWriter.ExitReadLock();
				
			}
		}

		public override void MarkDead(Uri uri)
		{
			try
			{
				this._readerWriter.EnterReadLock();
				base.MarkDead(uri);
			}
			finally
			{
				this._readerWriter.ExitReadLock();
				
			}
		}

	}


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
