using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Elasticsearch.Net
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
			if (!nodes.HasAny()) return;

			try
			{
				this._readerWriter.EnterWriteLock();
				var sortedNodes = nodes
					.OrderBy(item => this.Randomize ? this.Random.Next() : 1)
					.DistinctBy(n => n.Uri)
					.ToList();

				this.InternalNodes = sortedNodes;
				this.GlobalCursor = -1;
				this.LastUpdate = this.DateTimeProvider.Now();
			}
			finally
			{
				this._readerWriter.ExitWriteLock();
			}
		}

		public override IEnumerable<Node> CreateView(Action<AuditEvent, Node> audit = null)
		{
			try
			{
				this._readerWriter.EnterReadLock();
				return base.CreateView(audit);
			}
			finally
			{
				this._readerWriter.ExitReadLock();
			}
		}

		protected override void DisposeManagedResources()
		{
			this._readerWriter?.Dispose();
			base.DisposeManagedResources();
		}
	}
}