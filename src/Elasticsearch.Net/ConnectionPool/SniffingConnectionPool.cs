using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Providers;

namespace Elasticsearch.Net.ConnectionPool
{
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
				int seed; bool shouldPingHint;
				var uri = this.GetNext(null, out seed, out shouldPingHint);
				
				this._readerWriter.EnterWriteLock();
				var nodes = connection.Sniff(uri);
				if (!nodes.HasAny())
					return;

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

		public override Uri GetNext(int? initialSeed, out int seed, out bool shouldPingHint)
		{
			try
			{
				this._readerWriter.EnterReadLock();
				return base.GetNext(initialSeed, out seed, out shouldPingHint);
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

		public override void MarkDead(Uri uri, int? deadTimeout, int? maxDeadTimeout)
		{
			try
			{
				this._readerWriter.EnterReadLock();
				base.MarkDead(uri, deadTimeout, maxDeadTimeout);
			}
			finally
			{
				this._readerWriter.ExitReadLock();
				
			}
		}

	}
}