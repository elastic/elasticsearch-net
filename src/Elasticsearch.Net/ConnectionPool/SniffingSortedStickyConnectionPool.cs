using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Elasticsearch.Net
{
	public class SniffingSortedStickyConnectionPool : IConnectionPool
	{
		private readonly ReaderWriterLockSlim _readerWriter = new ReaderWriterLockSlim();

		private readonly Func<IEnumerable<Node>, IEnumerable<Node>> _sortMethod;

		protected IDateTimeProvider DateTimeProvider { get; }

		protected List<Node> InternalNodes { get; set; }

		/// <inheritdoc/>
		public bool UsingSsl { get; }

		/// <inheritdoc/>
		public bool SniffedOnStartup { get; set; }

		/// <inheritdoc/>
		public IReadOnlyCollection<Node> Nodes => this.InternalNodes;

		/// <inheritdoc/>
		public int MaxRetries => this.InternalNodes.Count - 1;

		/// <inheritdoc/>
		public bool SupportsReseeding => true;

		/// <inheritdoc/>
		public bool SupportsPinging => true;

		/// <inheritdoc/>
		public DateTime LastUpdate { get; protected set; }

		public SniffingSortedStickyConnectionPool(IEnumerable<Uri> uris, Func<IEnumerable<Node>, IEnumerable<Node>> sortMethod, IDateTimeProvider dateTimeProvider = null)
			: this(uris.Select(uri => new Node(uri)), sortMethod, dateTimeProvider)
		{
		}

		public SniffingSortedStickyConnectionPool(IEnumerable<Node> nodes, Func<IEnumerable<Node>, IEnumerable<Node>> sortMethod, IDateTimeProvider dateTimeProvider = null)
		{
			nodes.ThrowIfEmpty(nameof(nodes));

			this.DateTimeProvider = dateTimeProvider ?? Elasticsearch.Net.DateTimeProvider.Default;

			var nn = nodes.ToList();
			var uris = nn.Select(n => n.Uri).ToList();
			if (uris.Select(u => u.Scheme).Distinct().Count() > 1)
				throw new ArgumentException("Trying to instantiate a connection pool with mixed URI Schemes");

			this.UsingSsl = uris.Any(uri => uri.Scheme == "https");

			this.InternalNodes = nn
				.DistinctBy(n => n.Uri)
				.ToList();
			this.LastUpdate = this.DateTimeProvider.Now();

			this._sortMethod = sortMethod;
		}

		protected int GlobalCursor = -1;

		/// <summary>
		/// Creates a view of all the live nodes preserving their order,
		/// if there are no live nodes yields a different dead node to try once
		/// </summary>
		public IEnumerable<Node> CreateView(Action<AuditEvent, Node> audit = null)
		{
			var now = this.DateTimeProvider.Now();
			var nodes = this.InternalNodes.Where(n => n.IsAlive || n.DeadUntil <= now)
				.ToList();
			var count = nodes.Count;
			Node node;

			if (count == 0)
			{
				var globalCursor = Interlocked.Increment(ref this.GlobalCursor);
				//could not find a suitable node retrying on first node off globalCursor
				audit?.Invoke(AuditEvent.AllNodesDead, null);
				node = this.InternalNodes[globalCursor % this.InternalNodes.Count];
				node.IsResurrected = true;
				audit?.Invoke(AuditEvent.Resurrection, node);
				yield return node;
				yield break;
			}

			// If the cursor is greater than the default then it's been
			// set already but we now have a live node so we should reset it
			if (this.GlobalCursor > -1)
			{
				Interlocked.Exchange(ref this.GlobalCursor, -1);
			}

			var localCursor = 0;

			for (var attempts = 0; attempts < count; attempts++)
			{
				node = nodes[localCursor];
				localCursor = (localCursor + 1) % count;
				//if this node is not alive or no longer dead mark it as resurrected
				if (!node.IsAlive)
				{
					audit?.Invoke(AuditEvent.Resurrection, node);
					node.IsResurrected = true;
				}
				yield return node;
			}
		}

		/// <summary>
		/// Update the node list and sort it
		/// </summary>
		public void Reseed(IEnumerable<Node> nodes)
		{
			if (!nodes.HasAny()) return;

			try
			{
				this._readerWriter.EnterWriteLock();
				List<Node> sortedNodes = _sortMethod(nodes).ToList();
				this.InternalNodes = sortedNodes;
				this.LastUpdate = this.DateTimeProvider.Now();
			}
			finally
			{
				this._readerWriter.ExitWriteLock();
			}
		}

		public void Dispose()
		{
			this._readerWriter?.Dispose();
			this.DisposeManagedResources();
		}

		protected void DisposeManagedResources()
		{
			this._readerWriter?.Dispose();
		}
	}
}