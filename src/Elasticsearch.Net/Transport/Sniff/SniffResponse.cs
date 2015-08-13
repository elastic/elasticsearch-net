using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Elasticsearch.Net.Connection.Sniff
{
	internal class SniffResponse
	{
		private static readonly Regex _uriParse = new Regex(@"(?:inet\[)?\/([^:]+):(\d+)");
		public string cluster_name { get; set; }
		public Dictionary<string, SniffNode> nodes { get; set; }

		public IEnumerable<Node> ToNodes(bool forceHttp = false)
		{
			foreach (var kv in nodes)
				yield return new Node(Parse(forceHttp, kv.Value.http_address))
				{
					Name = kv.Value.name,
					Id = kv.Key,
					//TODO MasterEligable, HoldsData
				};
		}

		private static Uri Parse(bool forceHttp, string inetString)
		{
			var match = _uriParse.Match(inetString);
			if (!match.Success) return null;
			var host = match.Groups[1].Value;
			var port = match.Groups[2].Value;
			return new Uri("http{0}://{1}:{2}".F(forceHttp ? "s" : string.Empty, host, port));
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
	}

}
