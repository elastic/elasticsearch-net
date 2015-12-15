using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Elasticsearch.Net
{
	public class StaticConnectionPool : IConnectionPool
	{
		protected IDateTimeProvider DateTimeProvider { get; }
		protected Random Random { get; } = new Random();
		protected bool Randomize { get; }

		protected List<Node> InternalNodes { get; set; }

		public virtual IReadOnlyCollection<Node> Nodes => this.InternalNodes;

		public int MaxRetries => this.InternalNodes.Count - 1;

		public virtual bool SupportsReseeding => false;
		public virtual bool SupportsPinging => true;

		public virtual void Reseed(IEnumerable<Node> nodes) { } //ignored

		public bool UsingSsl { get; }

		public bool SniffedOnStartup { get; set; }

		public DateTime LastUpdate { get; protected set; }

		public StaticConnectionPool(IEnumerable<Uri> uris, bool randomize = true, IDateTimeProvider dateTimeProvider = null)
			: this(uris.Select(uri=>new Node(uri)), randomize, dateTimeProvider) { }

		public StaticConnectionPool(IEnumerable<Node> nodes, bool randomize = true, IDateTimeProvider dateTimeProvider = null)
		{
			nodes.ThrowIfEmpty(nameof(nodes));

			this.Randomize = randomize;
			this.DateTimeProvider = dateTimeProvider ?? Elasticsearch.Net.DateTimeProvider.Default;

			var nn = nodes.ToList();
			var uris = nn.Select(n => n.Uri).ToList();
			if (uris.Select(u => u.Scheme).Distinct().Count() > 1)
				throw new ArgumentException("Trying to instantiate a connection pool with mixed URI Schemes");

			this.UsingSsl = uris.Any(uri => uri.Scheme == Uri.UriSchemeHttps);

			this.InternalNodes = nn
				.OrderBy(item => randomize ? this.Random.Next() : 1)
				.DistinctBy(n=>n.Uri)
				.ToList();
			this.LastUpdate = this.DateTimeProvider.Now();
		}

		protected int GlobalCursor = -1;
		/// <summary>
		/// Creates a view of all the live nodes with changing starting positions that wraps over on each call
		/// e.g Thread A might get 1,2,3,4,5 and thread B will get 2,3,4,5,1.
		/// if there are no live nodes yields a different dead node to try once
		/// </summary>
		public virtual IEnumerable<Node> CreateView()
		{
			//var count = this.InternalNodes.Count;

			var now = this.DateTimeProvider.Now();
			var nodes = this.InternalNodes.Where(n => n.IsAlive || n.DeadUntil <= now).ToList();
			var count = nodes.Count;
			var globalCursor = Interlocked.Increment(ref GlobalCursor);
			var localCursor = globalCursor % count;

			Node node;
			for (var attempts = 0; attempts < count; attempts++)
			{
				node = nodes[localCursor];
				localCursor = (localCursor + 1) % count;
				//node is not dead or no longer dead
				if (node.DeadUntil > now) continue;
				//if this node is not alive mark it as resurrected
				if (!node.IsAlive) node.IsResurrected = true;
				yield return node;
			}
			if (count != 0) yield break;
			//could not find a suitable node retrying on node that has been dead longest.
			node = this.InternalNodes[globalCursor % count];
			node.IsResurrected = true;
			yield return node;
		}

	}
}