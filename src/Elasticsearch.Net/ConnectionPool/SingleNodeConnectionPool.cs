/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;

namespace Elasticsearch.Net
{
	/// <summary> A connection pool to a single node or endpoint </summary>
	public class SingleNodeConnectionPool : IConnectionPool
	{
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
