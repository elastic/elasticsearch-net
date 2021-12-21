using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Elastic.Transport;
using Elastic.Clients.Elasticsearch;
using Tests.Configuration;
using Tests.Core.Extensions;

namespace Tests.Core.Client.Settings
{
	public class TestElasticsearchClientSettings : ElasticsearchClientSettings
	{
		public static readonly bool RunningMitmProxy = Process.GetProcessesByName("mitmproxy").Any();
		public static readonly bool RunningFiddler = Process.GetProcessesByName("fiddler").Any();

		public TestElasticsearchClientSettings(
			Func<ICollection<Uri>, IConnectionPool> createPool = null,
			SourceSerializerFactory sourceSerializerFactory = null,
			bool forceInMemory = false,
			int port = 9200,
			byte[] response = null
		) : base(CreatePool(createPool, port), TestConfiguration.Instance.CreateConnection(forceInMemory, response)) =>
			ApplyTestSettings();

		public static string LocalOrProxyHost => RunningFiddler || RunningMitmProxy ? "ipv4.fiddler" : LocalHost;

		private static int ConnectionLimitDefault =>
			int.TryParse(Environment.GetEnvironmentVariable("NEST_NUMBER_OF_CONNECTIONS"), out var x)
				? x
				: TransportConfiguration.DefaultConnectionLimit;

		private static string LocalHost => "localhost";

		private void ApplyTestSettings() =>
			RerouteToProxyIfNeeded()
				.EnableDebugMode()
				.EnableHttpCompression(TestConfiguration.Instance.Random.HttpCompression)
#if DEBUG
				.EnableDebugMode()
#endif
				.ConnectionLimit(ConnectionLimitDefault)
				.OnRequestCompleted(r =>
				{
					//r.HttpMethod;

					// TODO - Replace this using the request, no longer on IApiCallDetails
					//if (!r.DeprecationWarnings.Any()) return;

					var q = r.Uri.Query;
					//hack to prevent the deprecation warnings from the deprecation response test to be reported
					if (!string.IsNullOrWhiteSpace(q) && q.Contains("routing=ignoredefaultcompletedhandler")) return;

					// TODO: Replace this
					//foreach (var d in r.DeprecationWarnings) XunitRunState.SeenDeprecations.Add(d);
				});

		private ElasticsearchClientSettings RerouteToProxyIfNeeded()
		{
			if (!RunningMitmProxy) return this;

			return Proxy(new Uri("http://127.0.0.1:8080"), null, (string)null);
		}

		//private static SourceSerializerFactory CreateSerializerFactory(SourceSerializerFactory provided)
		//{
		//	if (provided != null)
		//		return provided;

		//	if (!TestConfiguration.Instance.Random.SourceSerializer)
		//		return null;

		//	return (builtin, values) => new TestSourceSerializerBase(builtin, values);
		//}

		private static IConnectionPool CreatePool(Func<ICollection<Uri>, IConnectionPool> createPool = null,
			int port = 9200)
		{
			createPool ??= uris => new StaticConnectionPool(uris);
			var connectionPool = createPool(new[] {CreateUri(port)});
			return connectionPool;
		}

		public static Uri CreateUri(int port = 9200) => new UriBuilder("http://", LocalOrProxyHost, port).Uri;
	}
}
