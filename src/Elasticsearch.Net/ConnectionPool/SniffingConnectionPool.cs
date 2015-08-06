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

		public override bool SupportsReseeding => true;
		public override bool SupportsPinging => true;

		public SniffingConnectionPool(IEnumerable<Uri> uris, bool randomize = true, IDateTimeProvider dateTimeProvider = null)
			: base(uris, randomize, dateTimeProvider)
		{ }

		public SniffingConnectionPool(IEnumerable<Node> nodes, bool randomize = true, IDateTimeProvider dateTimeProvider = null)
			: base(nodes, randomize, dateTimeProvider)
		{ }

		public override IReadOnlyCollection<Node> Nodes
		{
			get
			{
				try
				{
					//since internalnodes can be changed after returning we return
					//a completely new list of cloned nodes
					this._readerWriter.EnterReadLock();
					return this.InternalNodes.Select(n => n.Clone()).ToList();
				}
				finally
				{
					this._readerWriter.ExitReadLock();
				}
            }
		}

		public override void Reseed(IEnumerable<Node> nodes)
		{
			if (nodes.HasAny()) return;

			try
			{
				this._readerWriter.EnterWriteLock();
				this.InternalNodes = nodes
					.OrderBy((item) => this.Randomize ? this.Random.Next() : 1)
					.DistinctBy(n => n.Uri)
					.ToList();
			}
			finally
			{
				this._readerWriter.ExitWriteLock();
			}
		}

		public override Node GetNext(int? cursor, out int? newCursor)
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