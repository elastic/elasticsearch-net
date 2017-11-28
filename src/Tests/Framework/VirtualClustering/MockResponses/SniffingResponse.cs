using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Elasticsearch.Net;

namespace Tests.Framework.MockResponses
{
	public static class SniffResponse
	{
		private static string ClusterName => "elasticsearch-test-cluster";

		public static byte[] Create(IEnumerable<Node> nodes, string publishAddressOverride, bool randomFqdn = false)
		{
			var response = new
			{
				cluster_name = ClusterName,
				nodes = SniffResponseNodes(nodes, publishAddressOverride, randomFqdn)
			};
			using (var ms = new MemoryStream())
			{
				new LowLevelRequestResponseSerializer().Serialize(response, ms);
				return ms.ToArray();
			}
		}
		private static IDictionary<string, object> SniffResponseNodes(IEnumerable<Node> nodes, string publishAddressOverride, bool randomFqdn) =>
			(from node in nodes
			let id = string.IsNullOrEmpty(node.Id) ? Guid.NewGuid().ToString("N").Substring(0, 8) : node.Id
			let name = string.IsNullOrEmpty(node.Name) ? Guid.NewGuid().ToString("N").Substring(0, 8) : node.Name
			select new { id, name, node })
			.ToDictionary(kv => kv.id, kv => CreateNodeResponse(kv.node, kv.name, publishAddressOverride, randomFqdn));

		private static Random Random = new Random(1337);
		private static object CreateNodeResponse(Node node, string name, string publishAddressOverride, bool randomFqdn)
		{
			var port = node.Uri.Port;
			var fqdn = randomFqdn ? $"fqdn{port}/" : "";
			var host = !string.IsNullOrWhiteSpace(publishAddressOverride) ? publishAddressOverride : "127.0.0.1";

			var settings = new Dictionary<string, object>
			{
				{"cluster.name", ClusterName},
				{"node.name", name}
			};
			foreach (var kv in node.Settings) settings[kv.Key] = kv.Value;

			var nodeResponse = new
			{
				name = name,
				transport_address = $"127.0.0.1:{port + 1000}]",
				host = Guid.NewGuid().ToString("N").Substring(0, 8),
				ip = "127.0.0.1",
				version = TestClient.Configuration.ElasticsearchVersion.Version,
				build_hash = Guid.NewGuid().ToString("N").Substring(0, 8),
				roles = new List<string>(),
				http = node.HttpEnabled ? new
				{
					bound_address = new []
					{
						$"{fqdn}127.0.0.1:{port}"
					},
					//publish_address = $"{fqdn}${publishAddress}"
					publish_address = $"{fqdn}{host}:{port}"
				} : null,
				settings = settings
			};
			if (node.MasterEligible) nodeResponse.roles.Add("master");
			if (node.HoldsData) nodeResponse.roles.Add("data");
			if (node.IngestEnabled) nodeResponse.roles.Add("ingest");
			if (!node.HttpEnabled)
				nodeResponse.settings.Add("http.enabled", false);
			return nodeResponse;
		}

	}
}
