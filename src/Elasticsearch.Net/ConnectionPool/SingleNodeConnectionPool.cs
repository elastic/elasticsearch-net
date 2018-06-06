using System;
using System.Collections.Generic;

namespace Elasticsearch.Net
{
	/// <summary>
	/// A connection pool to a single node or endpoint
	/// </summary>
	public class SingleNodeConnectionPool : IConnectionPool
	{
		/// <inheritdoc/>
		public int MaxRetries => 0;

		/// <inheritdoc/>
		public bool SupportsReseeding => false;

		/// <inheritdoc/>
		public bool SupportsPinging => false;

		/// <inheritdoc/>
		public void Reseed(IEnumerable<Node> nodes) { } //ignored

		/// <inheritdoc/>
		public bool UsingSsl { get; }

		/// <inheritdoc/>
		public bool SniffedOnStartup { get => true; set {  } }

		/// <inheritdoc/>
		public IReadOnlyCollection<Node> Nodes { get; }

		/// <inheritdoc/>
		public DateTime LastUpdate { get; }

		/// <inheritdoc/>
		public SingleNodeConnectionPool(Uri uri, IDateTimeProvider dateTimeProvider = null)
		{
			var node = new Node(uri);
			this.UsingSsl = node.Uri.Scheme == "https";
			this.Nodes = new List<Node> { node };
			this.LastUpdate = (dateTimeProvider ?? DateTimeProvider.Default).Now();
		}

		/// <inheritdoc/>
		public IEnumerable<Node> CreateView(Action<AuditEvent, Node> audit = null) => this.Nodes;

		void IDisposable.Dispose() => this.DisposeManagedResources();

		protected virtual void DisposeManagedResources() { }
	}
}
