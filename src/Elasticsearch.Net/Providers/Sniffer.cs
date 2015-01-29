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

			public Uri GetFirstAddress(TransportAddressScheme addressScheme)
			{
				switch (addressScheme)
				{
					case TransportAddressScheme.Http:
					case TransportAddressScheme.Https:
						var schema = addressScheme == TransportAddressScheme.Http ? "http" : "https";
						if (!http_address.IsNullOrEmpty())
							return Parse(schema, this.http_address);
						break;
					case TransportAddressScheme.Thrift:
						if (!thrift_address.IsNullOrEmpty())
							return Parse("thrift", this.thrift_address);
						break;
				}
				return null;
			}

		}

		private static Uri Parse(string scheme, string inetString)
		{
			var match = _uriParse.Match(inetString);
			if (!match.Success) return null;
			var host = match.Groups[1].Value;
			var port = match.Groups[2].Value;
			return new Uri("{0}://{1}:{2}".F(scheme, host, port));

		}
		public static IList<Uri> FromStream(IElasticsearchResponse response, Stream stream, IElasticsearchSerializer serializer, TransportAddressScheme? preferedTransportOrder = null)
		{
			var order = preferedTransportOrder.GetValueOrDefault(TransportAddressScheme.Http);
			var result = serializer.Deserialize<NodeInfoResponse>(stream);
			return result.nodes.Values
				.Select(kv => kv.GetFirstAddress(order))
				.Where(url => url != null)
				.ToList();
		}
	}
}