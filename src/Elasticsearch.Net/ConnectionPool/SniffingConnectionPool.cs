using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Elasticsearch.Net
{
	public class SniffingConnectionPool : StaticConnectionPool
	{
		private readonly ReaderWriterLockSlim _readerWriter = new ReaderWriterLockSlim();

		public SniffingConnectionPool(IEnumerable<Uri> uris, bool randomize = true, IDateTimeProvider dateTimeProvider = null)
			: base(uris, randomize, dateTimeProvider) { }

		public SniffingConnectionPool(IEnumerable<Node> nodes, bool randomize = true, IDateTimeProvider dateTimeProvider = null)
			: base(nodes, randomize, dateTimeProvider) { }

		public SniffingConnectionPool(IEnumerable<Node> nodes, Func<Node, float> nodeScorer, IDateTimeProvider dateTimeProvider = null)
			: base(nodes, nodeScorer, dateTimeProvider) { }

		/// <inheritdoc />
		public override IReadOnlyCollection<Node> Nodes
		{
			get
			{
				try
				{
					//since internalnodes can be changed after returning we return
					//a completely new list of cloned nodes
					_readerWriter.EnterReadLock();
					return InternalNodes.Select(n => n.Clone()).ToList();
				}
				finally
				{
					_readerWriter.ExitReadLock();
				}
			}
		}

		/// <inheritdoc />
		public override bool SupportsPinging => true;

		/// <inheritdoc />
		public override bool SupportsReseeding => true;

		/// <inheritdoc />
		public override void Reseed(IEnumerable<Node> nodes)
		{
			if (!nodes.HasAny()) return;

			try
			{
				_readerWriter.EnterWriteLock();
				var sortedNodes = SortNodes(nodes)
					.DistinctBy(n => n.Uri)
					.ToList();

				InternalNodes = sortedNodes;
				GlobalCursor = -1;
				LastUpdate = DateTimeProvider.Now();
			}
			finally
			{
				_readerWriter.ExitWriteLock();
			}
		}

		/// <inheritdoc />
		public override IEnumerable<Node> CreateView(Action<AuditEvent, Node> audit = null)
		{
			_readerWriter.EnterReadLock();
			try
			{
				return base.CreateView(audit);
			}
			finally
			{
				_readerWriter.ExitReadLock();
			}
		}

		protected override void DisposeManagedResources()
		{
			_readerWriter?.Dispose();
			base.DisposeManagedResources();
		}
	}
}
