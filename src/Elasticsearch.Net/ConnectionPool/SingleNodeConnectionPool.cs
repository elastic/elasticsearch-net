// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;

namespace Elasticsearch.Net
{
	/// <summary> A connection pool to a single node or endpoint </summary>
	public class SingleNodeConnectionPool : IConnectionPool
	{
		/// <inheritdoc />
		public SingleNodeConnectionPool(Uri uri, IDateTimeProvider dateTimeProvider = null)
		{
			var node = new Node(uri);
			UsingSsl = node.Uri.Scheme == "https";
			Nodes = new List<Node> { node };
			LastUpdate = (dateTimeProvider ?? DateTimeProvider.Default).Now();
		}

		/// <inheritdoc />
		public DateTime LastUpdate { get; }

		/// <inheritdoc />
		public int MaxRetries => 0;

		/// <inheritdoc />
		public IReadOnlyCollection<Node> Nodes { get; }

		/// <inheritdoc />
		public bool SniffedOnStartup
		{
			get => true;
			set { }
		}

		/// <inheritdoc />
		public bool SupportsPinging => false;

		/// <inheritdoc />
		public bool SupportsReseeding => false;

		/// <inheritdoc />
		public bool UsingSsl { get; }

		/// <inheritdoc />
		public IEnumerable<Node> CreateView(Action<AuditEvent, Node> audit = null) => Nodes;

		/// <inheritdoc />
		public void Reseed(IEnumerable<Node> nodes) { } //ignored

		void IDisposable.Dispose() => DisposeManagedResources();

		protected virtual void DisposeManagedResources() { }
	}
}
