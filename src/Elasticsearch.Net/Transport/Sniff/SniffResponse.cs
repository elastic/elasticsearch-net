using System;
using System.Collections.Generic;

namespace Elasticsearch.Net
{
	internal class SniffResponse
	{
		public string cluster_name { get; set; }
		public Dictionary<string, SniffNode> nodes { get; set; }

		public IEnumerable<Node> ToNodes(bool forceHttp = false)
		{
			var suffix = forceHttp ? "s" : string.Empty;
			foreach (var kv in nodes)
				yield return new Node(new Uri($"http{suffix}://" + kv.Value.http_address))
				{
					Name = kv.Value.name,
					Id = kv.Key,
					MasterEligable = kv.Value.MasterEligable,
					HoldsData = kv.Value.HoldsData,
				};
		}

	}

	internal class SniffNode
	{
		public string name { get; set; }
		public string transport_address { get; set; }
		public string http_address { get; set; }
		public string host { get; set; }
		public string ip { get; set; }
		public string version { get; set; }
		public string build { get; set; }
		public IDictionary<string, object> settings { get; set; }

		internal bool MasterEligable => !((this.settings?.ContainsKey("node.master")).GetValueOrDefault(false) && ((bool)this.settings["node.master"]) == false);
		internal bool HoldsData => !((this.settings?.ContainsKey("node.data")).GetValueOrDefault(false) && ((bool)this.settings["node.data"]) == false);
	}

}
