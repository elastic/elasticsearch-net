using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Elasticsearch.Net.Serialization;

namespace Elasticsearch.Net.Providers
{
	public static class Sniffer
	{
		private static readonly Regex _uriParse = new Regex(@"inet\[\/([^:]+):(\d+)");
		private class NodeInfoResponse
		{
			public IDictionary<string, NodeState> nodes { get; set; }
		}

		private class NodeState
		{
			public string http_address { get; set; }
			public string https_address { get; set; }
			public string thrift_address { get; set; }
		}

		private static Uri Parse(string scheme, string inetString)
		{
			var match = _uriParse.Match(inetString);
			var host = match.Groups[1].Value;
			var port = match.Groups[2].Value;
			return new Uri("{0}://{1}:{2}".F(scheme, host, port));

		}
		public static IList<Uri> FromStream(IElasticsearchResponse response, Stream stream, IElasticsearchSerializer serializer)
		{
				var result = serializer.Deserialize<NodeInfoResponse>(stream);
				var l = new List<Uri>();
				foreach(var kv in result.nodes.Values)
				{
					if (!kv.http_address.IsNullOrEmpty())
						l.Add(Parse("http", kv.http_address));
					else if (!kv.https_address.IsNullOrEmpty())
						l.Add(Parse("https",kv.https_address));
					else if (!kv.thrift_address.IsNullOrEmpty())
						l.Add(Parse("http", kv.thrift_address));
				}
				return l;
		}
	}
}