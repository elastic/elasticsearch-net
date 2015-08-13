using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Serialization;

namespace Elasticsearch.Net.Providers
{
	public static class Sniffer
	{
		private static readonly Regex _uriParse = new Regex(@"(?:inet\[)?\/([^:]+):(\d+)");
		private class NodeInfoResponse
		{
			public IDictionary<string, NodeState> nodes { get; set; }
		}

		private class NodeState
		{
			public string http_address { get; set; }
			public string thrift_address { get; set; }
		}

		private static Uri Parse(string scheme, string inetString)
		{
			var match = _uriParse.Match(inetString);
			if (!match.Success) return null;
			var host = match.Groups[1].Value;
			var port = match.Groups[2].Value;
			return new Uri("{0}://{1}:{2}".F(scheme, host, port));

		}

		//TODO: broke this to make to make it compile for now
		//TODO rewrite: not static, pluggable, taking master nodes into account and current scheme

		public static IList<Uri> FromStream(IApiCallDetails response, Stream stream, IElasticsearchSerializer serializer)
		{
			var result = serializer.Deserialize<NodeInfoResponse>(stream);
			return result.nodes.Values
				.Select(kv => new Uri(kv.http_address))
				.Where(url => url != null)
				.ToList();
		}
	}
}