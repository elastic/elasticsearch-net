using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Elasticsearch.Net
{
	public class StickySniffingConnectionPool : SniffingConnectionPool
	{
		public StickySniffingConnectionPool(IEnumerable<Uri> uris, Func<Node, float> nodeScorer, IDateTimeProvider dateTimeProvider = null)
			: base(uris.Select(uri => new Node(uri)), nodeScorer ?? DefaultNodeScore, dateTimeProvider) { }

		public StickySniffingConnectionPool(IEnumerable<Node> nodes, Func<Node, float> nodeScorer, IDateTimeProvider dateTimeProvider = null)
			: base(nodes, nodeScorer ?? DefaultNodeScore, dateTimeProvider) { }

		public override bool SupportsPinging => true;
		public override bool SupportsReseeding => true;

		public override IEnumerable<Node> CreateView(Action<AuditEvent, Node> audit = null)
		{
			var nodes = AliveNodes;

			if (nodes.Count == 0)
			{
				var globalCursor = Interlocked.Increment(ref GlobalCursor);

				//could not find a suitable node retrying on first node off globalCursor
				yield return RetryInternalNodes(globalCursor, audit);

				yield break;
			}

			// If the cursor is greater than the default then it's been
			// set already but we now have a live node so we should reset it
			if (GlobalCursor > -1)
				Interlocked.Exchange(ref GlobalCursor, -1);

			var localCursor = 0;
			foreach (var aliveNode in SelectAliveNodes(localCursor, nodes, audit))
				yield return aliveNode;
		}

		private static float DefaultNodeScore(Node node) => 0f;
	}
}
