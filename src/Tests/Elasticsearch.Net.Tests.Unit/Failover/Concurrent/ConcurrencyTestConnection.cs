using System;
using System.IO;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Connection.Configuration;

namespace Elasticsearch.Net.Tests.Unit.Failover.Concurrent
{
	/// <summary>
	/// This simulates a super flakey elasticsearch cluster.
	/// - if random 1-9 is a muliple of 3 throw a 503
	/// - never throws on node 9202 though so that all calls can be expected to always succeed. 
	/// - Sniff can either get back the full cluster or a sufficient subset of it. 
	/// - Our cluster have 5 nodes the recommendation is to have N/2+1 masters so we should atleast see 3 nodes
	/// - anything less would cause a node to be unavailable which is covered in other tests 
	/// </summary>
	public class ConcurrencyTestConnection : InMemoryConnection
	{
		private readonly Random _rnd = new Random();
		public ConcurrencyTestConnection(IConnectionConfigurationValues settings) 
			: base(settings)
		{
		}

		public override ElasticsearchResponse<Stream> GetSync(Uri uri, IRequestConfiguration requestConfigurationOverrides = null)
		{
			var statusCode = _rnd.Next(1, 9) % 3 == 0 ? 503 : 200;
			if (uri.Port == 9202)
				statusCode = 200;

			return ElasticsearchResponse<Stream>.Create(this.ConnectionSettings, statusCode, "GET", "/", null);
		}
	}
}