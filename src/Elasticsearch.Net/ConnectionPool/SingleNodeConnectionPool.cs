using System;
using System.Collections.Generic;

namespace Elasticsearch.Net
{
	public class SingleNodeConnectionPool : IConnectionPool
	{
		public int MaxRetries => 0;

		public bool SupportsReseeding => false;
		public bool SupportsPinging => false;

		public void Reseed(IEnumerable<Node> nodes) { } //ignored
		
		public bool UsingSsl { get; }

		public bool SniffedOnStartup { get { return true; } set {  } }

		public IReadOnlyCollection<Node> Nodes { get; }

		public DateTime LastUpdate { get; }

		public SingleNodeConnectionPool(Uri uri, IDateTimeProvider dateTimeProvider = null)
		{
			var node = new Node(uri);
			this.UsingSsl = node.Uri.Scheme == "https";
			this.Nodes = new List<Node> { node };
			this.LastUpdate = (dateTimeProvider ?? DateTimeProvider.Default).Now();
		}

		public IEnumerable<Node> CreateView(Action<AuditEvent, Node> audit = null) => this.Nodes;

		void IDisposable.Dispose() => this.DisposeManagedResources();

		protected virtual void DisposeManagedResources() { }
	}
}
