using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Elasticsearch.Net
{
	public class StickyConnectionPool : IConnectionPool
	{
		protected IDateTimeProvider DateTimeProvider { get; }

		protected List<Node> InternalNodes { get; set; }

		public bool UsingSsl { get; }
		public bool SniffedOnStartup { get; set; }

		public IReadOnlyCollection<Node> Nodes => this.InternalNodes;

		public int MaxRetries => this.InternalNodes.Count - 1;

		public bool SupportsReseeding => false;

		public bool SupportsPinging => true;

		public DateTime LastUpdate { get; protected set; }

		public StickyConnectionPool(IEnumerable<Uri> uris, IDateTimeProvider dateTimeProvider = null)
			: this(uris.Select(uri => new Node(uri)), dateTimeProvider)
		{ }

		public StickyConnectionPool(IEnumerable<Node> nodes, IDateTimeProvider dateTimeProvider = null)
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
		}

		protected int GlobalCursor = -1;

		public IEnumerable<Node> CreateView(Action<AuditEvent, Node> audit = null)
		{
			var now = this.DateTimeProvider.Now();
			var nodes = this.InternalNodes.Where(n => n.IsAlive || n.DeadUntil <= now)
				.ToList();
			var count = nodes.Count;
			Node node;

			if (count == 0)
			{
				var globalCursor = Interlocked.Increment(ref GlobalCursor);
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
			if (GlobalCursor > -1)
			{
				Interlocked.Exchange(ref GlobalCursor, -1);
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

		public void Reseed(IEnumerable<Node> nodes) { }

		void IDisposable.Dispose() => this.DisposeManagedResources();

		protected virtual void DisposeManagedResources() { }
	}
}
