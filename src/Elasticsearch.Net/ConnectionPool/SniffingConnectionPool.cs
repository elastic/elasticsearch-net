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

		public override bool AcceptsUpdates { get { return true; } }

		public SniffingConnectionPool(
			IEnumerable<Uri> uris, 
			bool randomizeOnStartup = true, 
			IDateTimeProvider dateTimeProvider = null)
			: base(uris, randomizeOnStartup, dateTimeProvider)
		{
		}

		public override void UpdateNodeList(IList<Uri> newClusterState, Uri sniffNode = null)
		{
			try
			{
				this._readerWriter.EnterWriteLock();
				this.NodeUris = newClusterState;
				this.UriLookup = newClusterState.ToDictionary(k => k, v => new EndpointState()
				{
					Attemps = v.Equals(sniffNode) ? 1 : 0
				});
			}
			finally
			{
				this._readerWriter.ExitWriteLock();
			}
		}

		public override Node GetNext(int? initialSeed, out int seed)
		{
			try
			{
				this._readerWriter.EnterReadLock();
				return base.GetNext(initialSeed, out seed);
			}
			finally
			{
				this._readerWriter.ExitReadLock();
			}
		}

	}
}