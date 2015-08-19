using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Providers;

namespace Elasticsearch.Net.ConnectionPool
{
	public class StaticConnectionPool : IConnectionPool
	{
		protected IDateTimeProvider DateTimeProvider { get; }
		protected Random Random { get; } = new Random();
		protected bool Randomize { get; }

		protected List<Node> InternalNodes { get; set; } = new List<Node>();

		public virtual IReadOnlyCollection<Node> Nodes => this.InternalNodes;

		public int MaxRetries => this.InternalNodes.Count - 1;

		public virtual bool SupportsReseeding => false;
		public virtual bool SupportsPinging => true;

		public virtual void Reseed(IEnumerable<Node> nodes) { } //ignored

		public bool UsingSsl { get; }

		public bool SniffedOnStartup { get; set; }

		public DateTime? LastUpdate { get; set; }

		public StaticConnectionPool(IEnumerable<Uri> uris, bool randomize = true, IDateTimeProvider dateTimeProvider = null)
			: this(uris.Select(uri=>new Node(uri)), randomize, dateTimeProvider) { }

		public StaticConnectionPool(IEnumerable<Node> nodes, bool randomize = true, IDateTimeProvider dateTimeProvider = null)
		{
			nodes.ThrowIfEmpty(nameof(nodes));
			this.Randomize = randomize;
			this.DateTimeProvider = dateTimeProvider ?? new DateTimeProvider();

			var uris = nodes.Select(n => n.Uri).ToList();
			if (uris.Select(u => u.Scheme).Distinct().Count() > 1)
				throw new ArgumentException("Trying to instantiate a connection pool with mixed URI Schemes");

			this.UsingSsl = uris.Any(uri => uri.Scheme == Uri.UriSchemeHttps);

			this.InternalNodes = nodes
				.OrderBy((item) => randomize ? this.Random.Next() : 1)
				.DistinctBy(n=>n.Uri)
				.ToList();
		}

		private int _globalCursor = -1;
		/// <summary>
		/// Get the next node in a thread safe fashion that tries to keep order over multiple threads. 
		/// e.g Thread A might get 1,2,3,4,5 and thread B will get 2,3,4,5,1.
		/// </summary>
		/// <param name="cursor">On first use pass in null, this will take the global round cursor, on repeated uses pass in seed as a private cursor</param>
		/// <param name="newCursor">A private cursor that can be used in repeated calls to GetNext</param>
		public virtual Node GetNext(int? cursor, out int? newCursor)
		{
			var count = this.InternalNodes.Count;

			//if we have a local cursor use that otherwise use the globalcursor
			int privateCursor;
			if (cursor.HasValue) privateCursor = cursor.Value + 1;
			else privateCursor = Interlocked.Increment(ref _globalCursor);

			newCursor = privateCursor % count;

			Node node = null;
			for (int attempts = 0; attempts < count; attempts++)
			{
				node = this.InternalNodes[newCursor.Value];
				var now = this.DateTimeProvider.Now();
				//node is not dead or no longer dead
				if (node.DeadUntil <= now)
				{
					//if this node is not alive mark it as resurrected
					if (!node.IsAlive) node.IsResurrected = true;
					return node;
				}
				newCursor = (++privateCursor) % count;
			}

			//could not find a suitable node retrying on node that has been dead longest.
			node = this.InternalNodes[newCursor.Value];
			node.IsResurrected = true;
			return node;
		}

	}
}