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
using System.Collections.ObjectModel;
using System.Linq;
using Elasticsearch.Net.VirtualizedCluster.Providers;
using Elasticsearch.Net.VirtualizedCluster.Rules;

namespace Elasticsearch.Net.VirtualizedCluster
{
	public class VirtualCluster
	{
		private readonly List<Node> _nodes;

		public VirtualCluster(IEnumerable<Node> nodes) => _nodes = nodes.ToList();

		public List<IClientCallRule> ClientCallRules { get; } = new List<IClientCallRule>();
		public TestableDateTimeProvider DateTimeProvider { get; } = new TestableDateTimeProvider();

		public IReadOnlyList<Node> Nodes => _nodes;
		public List<IRule> PingingRules { get; } = new List<IRule>();

		public List<ISniffRule> SniffingRules { get; } = new List<ISniffRule>();
		internal string PublishAddressOverride { get; private set; }

		internal bool SniffShouldReturnFqnd { get; private set; }
		internal string ElasticsearchVersion { get; private set; } = "7.0.0";

		public VirtualCluster SniffShouldReturnFqdn()
		{
			SniffShouldReturnFqnd = true;
			return this;
		}
		public VirtualCluster SniffElasticsearchVersionNumber(string version)
		{
			ElasticsearchVersion = version;
			return this;
		}

		public VirtualCluster PublishAddress(string publishHost)
		{
			PublishAddressOverride = publishHost;
			return this;
		}

		public VirtualCluster MasterEligible(params int[] ports)
		{
			foreach (var node in _nodes.Where(n => !ports.Contains(n.Uri.Port)))
				node.MasterEligible = false;
			return this;
		}

		public VirtualCluster StoresNoData(params int[] ports)
		{
			foreach (var node in _nodes.Where(n => ports.Contains(n.Uri.Port)))
				node.HoldsData = false;
			return this;
		}

		public VirtualCluster HasSetting(string key, string value, params int[] ports)
		{
			foreach (var node in _nodes.Where(n => ports.Contains(n.Uri.Port)))
				node.Settings = new ReadOnlyDictionary<string, object>(new Dictionary<string, object> { { key, value } });
			return this;
		}

		public VirtualCluster HttpDisabled(params int[] ports)
		{
			foreach (var node in _nodes.Where(n => ports.Contains(n.Uri.Port)))
				node.HttpEnabled = false;
			return this;
		}

		public VirtualCluster Ping(Func<PingRule, IRule> selector)
		{
			PingingRules.Add(selector(new PingRule()));
			return this;
		}

		public VirtualCluster Sniff(Func<SniffRule, ISniffRule> selector)
		{
			SniffingRules.Add(selector(new SniffRule()));
			return this;
		}

		public VirtualCluster ClientCalls(Func<ClientCallRule, IClientCallRule> selector)
		{
			ClientCallRules.Add(selector(new ClientCallRule()));
			return this;
		}

		public SealedVirtualCluster SingleNodeConnection(Func<IList<Node>, IEnumerable<Node>> seedNodesSelector = null)
		{
			var nodes = seedNodesSelector?.Invoke(_nodes) ?? _nodes;
			return new SealedVirtualCluster(this, new SingleNodeConnectionPool(nodes.First().Uri), DateTimeProvider);
		}

		public SealedVirtualCluster StaticConnectionPool(Func<IList<Node>, IEnumerable<Node>> seedNodesSelector = null)
		{
			var nodes = seedNodesSelector?.Invoke(_nodes) ?? _nodes;
			return new SealedVirtualCluster(this, new StaticConnectionPool(nodes, false, DateTimeProvider), DateTimeProvider);
		}

		public SealedVirtualCluster SniffingConnectionPool(Func<IList<Node>, IEnumerable<Node>> seedNodesSelector = null)
		{
			var nodes = seedNodesSelector?.Invoke(_nodes) ?? _nodes;
			return new SealedVirtualCluster(this, new SniffingConnectionPool(nodes, false, DateTimeProvider), DateTimeProvider);
		}

		public SealedVirtualCluster StickyConnectionPool(Func<IList<Node>, IEnumerable<Node>> seedNodesSelector = null)
		{
			var nodes = seedNodesSelector?.Invoke(_nodes) ?? _nodes;
			return new SealedVirtualCluster(this, new StickyConnectionPool(nodes, DateTimeProvider), DateTimeProvider);
		}

		public SealedVirtualCluster StickySniffingConnectionPool(Func<Node, float> sorter = null,
			Func<IList<Node>, IEnumerable<Node>> seedNodesSelector = null
		)
		{
			var nodes = seedNodesSelector?.Invoke(_nodes) ?? _nodes;
			return new SealedVirtualCluster(this, new StickySniffingConnectionPool(nodes, sorter, DateTimeProvider), DateTimeProvider);
		}
	}
}
