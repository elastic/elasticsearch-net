// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Elastic.Transport.Products.Elasticsearch;
using Elastic.Transport.VirtualizedCluster.Components;

namespace Elastic.Transport.VirtualizedCluster.Products.Elasticsearch
{
	public class ElasticsearchClusterFactory
	{
		internal static ElasticsearchClusterFactory Default { get; } = new ElasticsearchClusterFactory();

		private ElasticsearchClusterFactory() { }

		// ReSharper disable once MemberCanBeMadeStatic.Global
		public ElasticsearchVirtualCluster Bootstrap(int numberOfNodes, int startFrom = 9200) =>
			new ElasticsearchVirtualCluster(
				Enumerable.Range(startFrom, numberOfNodes).Select(n => new Node(new Uri($"http://localhost:{n}"))),
				ElasticsearchMockProductRegistration.Default
			);

		// ReSharper disable once MemberCanBeMadeStatic.Global
		public ElasticsearchVirtualCluster Bootstrap(IEnumerable<Node> nodes) => new ElasticsearchVirtualCluster(nodes, ElasticsearchMockProductRegistration.Default);

		// ReSharper disable once MemberCanBeMadeStatic.Global
		public ElasticsearchVirtualCluster BootstrapAllMasterEligableOnly(int numberOfNodes, int startFrom = 9200) =>
			new ElasticsearchVirtualCluster(
				Enumerable.Range(startFrom, numberOfNodes)
					.Select(n => new Node(new Uri($"http://localhost:{n}"), ElasticsearchNodeFeatures.MasterEligableOnly)
					),
				ElasticsearchMockProductRegistration.Default
			);
	}

	public class ElasticsearchVirtualCluster : VirtualCluster
	{
		public ElasticsearchVirtualCluster(IEnumerable<Node> nodes, IMockProductRegistration productRegistration) : base(nodes, productRegistration) { }

		public ElasticsearchVirtualCluster MasterEligible(params int[] ports)
		{
			foreach (var node in InternalNodes.Where(n => !ports.Contains(n.Uri.Port)))
			{
				var currentFeatures = node.Features.Count == 0 ? ElasticsearchNodeFeatures.Default : node.Features;
				node.Features = currentFeatures.Except(new[] { ElasticsearchNodeFeatures.MasterEligible }).ToList().AsReadOnly();
			}
			return this;
		}

		public ElasticsearchVirtualCluster StoresNoData(params int[] ports)
		{
			foreach (var node in InternalNodes.Where(n => ports.Contains(n.Uri.Port)))
			{
				var currentFeatures = node.Features.Count == 0 ? ElasticsearchNodeFeatures.Default : node.Features;
				node.Features = currentFeatures.Except(new[] { ElasticsearchNodeFeatures.HoldsData }).ToList().AsReadOnly();
			}
			return this;
		}

		public VirtualCluster HttpDisabled(params int[] ports)
		{
			foreach (var node in InternalNodes.Where(n => ports.Contains(n.Uri.Port)))
			{
				var currentFeatures = node.Features.Count == 0 ? ElasticsearchNodeFeatures.Default : node.Features;
				node.Features = currentFeatures.Except(new[] { ElasticsearchNodeFeatures.HttpEnabled }).ToList().AsReadOnly();
			}
			return this;
		}

		public ElasticsearchVirtualCluster HasSetting(string key, string value, params int[] ports)
		{
			foreach (var node in InternalNodes.Where(n => ports.Contains(n.Uri.Port)))
				node.Settings = new ReadOnlyDictionary<string, object>(new Dictionary<string, object> { { key, value } });
			return this;
		}


	}
}
