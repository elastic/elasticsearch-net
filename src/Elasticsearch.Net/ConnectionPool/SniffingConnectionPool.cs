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

		private List<Node> _nodes = new List<Node>();
		public override IReadOnlyCollection<Node> Nodes => this._nodes;

		public override void Update(IEnumerable<Node> nodes)
		{
			try
			{
				this._readerWriter.EnterWriteLock();
				//TODO ToListOrNull()
				this._nodes = nodes?.ToList() ?? _nodes;
			}
			finally
			{
				this._readerWriter.ExitWriteLock();
			}
		}

		public override Node GetNext(int? cursor, out int newCursor)
		{
			try
			{
				this._readerWriter.EnterReadLock();
				return base.GetNext(cursor, out newCursor);
			}
			finally
			{
				this._readerWriter.ExitReadLock();
			}
		}

	}
}