using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Elasticsearch.Net
{
	internal class SniffResponse
	{

		private static Regex AddressRe { get; } = new Regex(@"^((?<fqdn>[^/]+)/)?(?<ip>[^:]+):(?<port>\d+)$");

		public string cluster_name { get; set; }
		public Dictionary<string, SniffNode> nodes { get; set; }

		public IEnumerable<Node> ToNodes(bool forceHttp = false)
		{
			foreach (var kv in nodes)
			{
				yield return new Node(this.ParseToUri(kv.Value.http_address, forceHttp))
				{
					Name = kv.Value.name,
					Id = kv.Key,
					MasterEligible = kv.Value.MasterEligible,
					HoldsData = kv.Value.HoldsData,
				};
			}
		}

		private Uri ParseToUri(string httpAdress, bool forceHttp)
		{
			var suffix = forceHttp ? "s" : string.Empty;
			var match = AddressRe.Match(httpAdress);
			if (!match.Success) throw new Exception($"Can not parse http_address: {httpAdress} to Uri");

			var fqdn = match.Groups["fqdn"].Value?.Trim();
			var ip = match.Groups["ip"].Value?.Trim();
			var port = match.Groups["port"].Value?.Trim();
			var host = !fqdn.IsNullOrEmpty() ? fqdn : ip;

			return new Uri($"http{suffix}://{host}:{port}");

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
		public IDictionary<string, string> settings { get; set; }

		internal bool MasterEligible => !((this.settings?.ContainsKey("node.master")).GetValueOrDefault(false) && Convert.ToBoolean(this.settings["node.master"]) == false);
		internal bool HoldsData => !((this.settings?.ContainsKey("node.data")).GetValueOrDefault(false) && Convert.ToBoolean(this.settings["node.data"]) == false);
	}

}
