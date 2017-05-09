using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Elasticsearch.Net
{
	public class SniffingSortedStickyConnectionPool : SniffingConnectionPool
	{
		public override bool SupportsPinging => true;
		public override bool SupportsReseeding => true;

		private Func<Node, float> _nodeScorer;

		public SniffingSortedStickyConnectionPool(IEnumerable<Uri> uris, Func<Node, float> nodeScorer, IDateTimeProvider dateTimeProvider = null)
			: base(uris.Select(uri => new Node(uri)), false, dateTimeProvider)
		{
			this._nodeScorer = nodeScorer ?? DefaultNodeScore;
		}

		public SniffingSortedStickyConnectionPool(IEnumerable<Node> nodes, Func<Node, float> nodeScorer, IDateTimeProvider dateTimeProvider = null)
			: base(nodes, false, dateTimeProvider)
		{
			this._nodeScorer = nodeScorer ?? DefaultNodeScore;
		}

		public override IEnumerable<Node> CreateView(Action<AuditEvent, Node> audit = null)
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

		protected override IOrderedEnumerable<Node> SortNodes(IEnumerable<Node> nodes)
		{
			return nodes.OrderByDescending(_nodeScorer);
		}

		private static float DefaultNodeScore(Node node)
		{
			return 0f;
		}
	}
}