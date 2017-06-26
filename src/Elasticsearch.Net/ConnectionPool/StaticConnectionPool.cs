using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Elasticsearch.Net
{
	public class StaticConnectionPool : IConnectionPool
	{
		private readonly Func<Node, float> _nodeScorer;
		protected IDateTimeProvider DateTimeProvider { get; }
		protected Random Random { get; } = new Random();
		protected bool Randomize { get; }

		protected List<Node> InternalNodes { get; set; }

		protected List<Node> AliveNodes
		{
			get
			{
				var now = DateTimeProvider.Now();
                return this.InternalNodes
                    .Where(n => n.IsAlive || n.DeadUntil <= now)
                    .ToList();
			}
		}

		/// <inheritdoc/>
		public virtual IReadOnlyCollection<Node> Nodes => this.InternalNodes;

		/// <inheritdoc/>
		public int MaxRetries => this.InternalNodes.Count - 1;

		/// <inheritdoc/>
		public virtual bool SupportsReseeding => false;
		/// <inheritdoc/>
		public virtual bool SupportsPinging => true;

		/// <inheritdoc/>
		public virtual void Reseed(IEnumerable<Node> nodes) { } //ignored

		/// <inheritdoc/>
		public bool UsingSsl { get; }

		/// <inheritdoc/>
		public bool SniffedOnStartup { get; set; }

		/// <inheritdoc/>
		public DateTime LastUpdate { get; protected set; }

		public StaticConnectionPool(IEnumerable<Uri> uris, bool randomize = true, IDateTimeProvider dateTimeProvider = null)
			: this(uris.Select(uri => new Node(uri)), randomize, dateTimeProvider)
		{ }

		public StaticConnectionPool(IEnumerable<Node> nodes, bool randomize = true, IDateTimeProvider dateTimeProvider = null)
			: this(nodes, null, dateTimeProvider)
		{
			this.Randomize = randomize;
		}

		//this constructor is protected because nodeScorer only makes sense on subclasses that support reseeding
		//otherwise just manually sort `nodes` before instantiating.
		protected StaticConnectionPool(IEnumerable<Node> nodes, Func<Node, float> nodeScorer, IDateTimeProvider dateTimeProvider = null)
		{
			nodes.ThrowIfEmpty(nameof(nodes));
			this.DateTimeProvider = dateTimeProvider ?? Elasticsearch.Net.DateTimeProvider.Default;

			var nn = nodes.ToList();
			var uris = nn.Select(n => n.Uri).ToList();
			if (uris.Select(u => u.Scheme).Distinct().Count() > 1)
				throw new ArgumentException("Trying to instantiate a connection pool with mixed URI Schemes");

			this.UsingSsl = uris.Any(uri => uri.Scheme == "https");

			this._nodeScorer = nodeScorer;
			this.InternalNodes = this.SortNodes(nn)
				.DistinctBy(n => n.Uri)
				.ToList();
			this.LastUpdate = this.DateTimeProvider.Now();
		}

		protected int GlobalCursor = -1;
		/// <summary>
		/// Creates a view of all the live nodes with changing starting positions that wraps over on each call
		/// e.g Thread A might get 1,2,3,4,5 and thread B will get 2,3,4,5,1.
		/// if there are no live nodes yields a different dead node to try once
		/// </summary>
		public virtual IEnumerable<Node> CreateView(Action<AuditEvent, Node> audit = null)
		{
			var nodes = this.AliveNodes;

			var globalCursor = Interlocked.Increment(ref GlobalCursor);

			if (nodes.Count == 0)
			{
				//could not find a suitable node retrying on first node off globalCursor
				yield return this.RetryInternalNodes(globalCursor, audit);
				yield break;
			}

			var localCursor = globalCursor % nodes.Count;
			foreach (var aliveNode in SelectAliveNodes(localCursor, nodes, audit))
			{
				yield return aliveNode;
			}
		}

		protected virtual Node RetryInternalNodes(int globalCursor, Action<AuditEvent, Node> audit = null)
		{
			audit?.Invoke(AuditEvent.AllNodesDead, null);
			var node = this.InternalNodes[globalCursor % this.InternalNodes.Count];
			node.IsResurrected = true;
			audit?.Invoke(AuditEvent.Resurrection, node);

			return node;
		}

		protected virtual IEnumerable<Node> SelectAliveNodes(int cursor, List<Node> aliveNodes, Action<AuditEvent, Node> audit = null)
		{
			for (var attempts = 0; attempts < aliveNodes.Count; attempts++)
			{
				var node = aliveNodes[cursor];
				cursor = (cursor + 1) % aliveNodes.Count;
				//if this node is not alive or no longer dead mark it as resurrected
				if (!node.IsAlive)
				{
					audit?.Invoke(AuditEvent.Resurrection, node);
					node.IsResurrected = true;
				}

				yield return node;
			}
		}

		protected IOrderedEnumerable<Node> SortNodes(IEnumerable<Node> nodes) =>
			this._nodeScorer != null
				? nodes.OrderByDescending(_nodeScorer)
				: nodes.OrderBy(n => this.Randomize ? this.Random.Next() : 1);


		void IDisposable.Dispose() => this.DisposeManagedResources();

		protected virtual void DisposeManagedResources() { }
	}
}
