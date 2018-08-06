using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Elastic.Managed.Configuration;
using Elasticsearch.Net;
using Elasticsearch.Net.Connections.HttpWebRequestConnection;
using Nest;
using Tests.Configuration;

namespace Tests.Framework.ManagedElasticsearch
{
	public static class TestConfigurationExtensions
	{
		private static IConnection CreateLiveConnection(this ITestConfiguration configuration) =>
			configuration.Random.OldConnection ? (IConnection) new HttpWebRequestConnection() : new HttpConnection();

		public static IConnection CreateConnection(this ITestConfiguration configuration, bool forceInMemory = false) =>
			configuration.RunIntegrationTests && !forceInMemory ? configuration.CreateLiveConnection() : new InMemoryConnection();

		public static bool InRange(this ITestConfiguration configuration, string range) =>
			ElasticsearchVersion.From(configuration.ElasticsearchVersion).InRange(range);
	}
}
