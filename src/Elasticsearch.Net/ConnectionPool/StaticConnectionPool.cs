// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Elasticsearch.Net.Extensions;

namespace Elasticsearch.Net
{
	public class StaticConnectionPool : IConnectionPool
	{
		protected int GlobalCursor = -1;
		private readonly Func<Node, float> _nodeScorer;

		public StaticConnectionPool(IEnumerable<Uri> uris, bool randomize = true, IDateTimeProvider dateTimeProvider = null)
			: this(uris.Select(uri => new Node(uri)), randomize, null, dateTimeProvider) { }

		public StaticConnectionPool(IEnumerable<Node> nodes, bool randomize = true, IDateTimeProvider dateTimeProvider = null)
			: this(nodes, randomize, null, dateTimeProvider) { }

		protected StaticConnectionPool(IEnumerable<Node> nodes, bool randomize, int? randomizeSeed = null, IDateTimeProvider dateTimeProvider = null)
		{
			Randomize = randomize;
			Random = !randomize || !randomizeSeed.HasValue
				? new Random()
				: new Random(randomizeSeed.Value);

			Initialize(nodes, dateTimeProvider);
		}
		//this constructor is protected because nodeScorer only makes sense on subclasses that support reseeding
		//otherwise just manually sort `nodes` before instantiating.
		protected StaticConnectionPool(IEnumerable<Node> nodes, Func<Node, float> nodeScorer = null, IDateTimeProvider dateTimeProvider = null)
		{
			_nodeScorer = nodeScorer;
			Initialize(nodes, dateTimeProvider);
		}

		private void Initialize(IEnumerable<Node> nodes, IDateTimeProvider dateTimeProvider)
		{
			var nodesProvided = nodes?.ToList() ?? throw new ArgumentNullException(nameof(nodes));
			nodesProvided.ThrowIfEmpty(nameof(nodes));
			DateTimeProvider = dateTimeProvider ?? Net.DateTimeProvider.Default;

			string scheme = null;
			foreach (var node in nodesProvided)
			{
				if (scheme == null)
				{
					scheme = node.Uri.Scheme;
					UsingSsl = scheme == "https";
				}
				else if (scheme != node.Uri.Scheme)
					throw new ArgumentException("Trying to instantiate a connection pool with mixed URI Schemes");
			}

			InternalNodes = SortNodes(nodesProvided)
				.DistinctBy(n => n.Uri)
				.ToList();
			LastUpdate = DateTimeProvider.Now();
		}

		/// <inheritdoc />
		public DateTime LastUpdate { get; protected set; }

		/// <inheritdoc />
		public int MaxRetries => InternalNodes.Count - 1;

		/// <inheritdoc />
		public virtual IReadOnlyCollection<Node> Nodes => InternalNodes;

		/// <inheritdoc />
		public bool SniffedOnStartup { get; set; }

		/// <inheritdoc />
		public virtual bool SupportsPinging => true;

		/// <inheritdoc />
		public virtual bool SupportsReseeding => false;

		/// <inheritdoc />
		public bool UsingSsl { get; private set; }

		protected List<Node> AliveNodes
		{
			get
			{
				var now = DateTimeProvider.Now();
				return InternalNodes
					.Where(n => n.IsAlive || n.DeadUntil <= now)
					.ToList();
			}
		}

		protected IDateTimeProvider DateTimeProvider { get; private set; }

		protected List<Node> InternalNodes { get; set; }
		protected Random Random { get; }
		protected bool Randomize { get; }

		/// <summary>
		/// Creates a view of all the live nodes with changing starting positions that wraps over on each call
		/// e.g Thread A might get 1,2,3,4,5 and thread B will get 2,3,4,5,1.
		/// if there are no live nodes yields a different dead node to try once
		/// </summary>
		public virtual IEnumerable<Node> CreateView(Action<AuditEvent, Node> audit = null)
		{
			var nodes = AliveNodes;

			var globalCursor = Interlocked.Increment(ref GlobalCursor);

			if (nodes.Count == 0)
			{
				//could not find a suitable node retrying on first node off globalCursor
				yield return RetryInternalNodes(globalCursor, audit);

				yield break;
			}

			var localCursor = globalCursor % nodes.Count;
			foreach (var aliveNode in SelectAliveNodes(localCursor, nodes, audit)) yield return aliveNode;
		}

		/// <inheritdoc />
		public virtual void Reseed(IEnumerable<Node> nodes) { } //ignored


		void IDisposable.Dispose() => DisposeManagedResources();

		protected virtual Node RetryInternalNodes(int globalCursor, Action<AuditEvent, Node> audit = null)
		{
			audit?.Invoke(AuditEvent.AllNodesDead, null);
			var node = InternalNodes[globalCursor % InternalNodes.Count];
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
			_nodeScorer != null
				? nodes.OrderByDescending(_nodeScorer)
				: nodes.OrderBy(n => Randomize ? Random.Next() : 1);

		protected virtual void DisposeManagedResources() { }
	}
}
