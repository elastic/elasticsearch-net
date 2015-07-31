using System;
using System.Collections.Generic;
using Elasticsearch.Net.Connection;

namespace Elasticsearch.Net.ConnectionPool
{
	public class SingleNodeConnectionPool : IConnectionPool
	{
		private readonly Node _node;

		public int MaxRetries => 0;

		public bool AcceptsUpdates => false;
		
		public bool UsingSsl { get; }

		public bool SniffedOnStartup { get { return true; } set {  } }

		public IReadOnlyCollection<Node> Nodes { get; }

		public DateTime? LastUpdate { get; set; }

		public SingleNodeConnectionPool(Uri uri)
		{
			this._node = new Node(uri);
			this.UsingSsl = this._node.Uri.Scheme == "https";
			this.Nodes = new List<Node> { this._node };
		}

		public Node GetNext(int? initialSeed, out int seed)
		{
			seed = 0;
			return this._node;
		}

		public void UpdateNodeList(IList<Uri> newClusterState, Uri sniffNode = null) { }

	}
}