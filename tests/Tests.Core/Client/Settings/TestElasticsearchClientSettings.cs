using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Elastic.Transport;
using Elastic.Clients.Elasticsearch;
using Tests.Configuration;
using Tests.Core.Extensions;
using Tests.Core.Client.Serializers;

namespace Tests.Core.Client.Settings
{
	public class TestElasticsearchClientSettings : ElasticsearchClientSettings
	{
		public static readonly bool RunningMitmProxy = Process.GetProcessesByName("mitmproxy").Any();
		public static readonly bool RunningFiddler = Process.GetProcessesByName("fiddler").Any() || Process.GetProcessesByName("fiddler everywhere").Any();

		public TestElasticsearchClientSettings(
			Func<ICollection<Uri>, NodePool> createPool = null,
			SourceSerializerFactory sourceSerializerFactory = null,
			IPropertyMappingProvider propertyMappingProvider = null,
			bool forceInMemory = false,
			int port = 9200,
			byte[] response = null
		) : base(CreatePool(createPool, port), TestConfiguration.Instance.CreateConnection(forceInMemory, response), CreateSerializerFactory(sourceSerializerFactory), propertyMappingProvider) =>
			ApplyTestSettings();

		public static string LocalOrProxyHost => RunningFiddler || RunningMitmProxy ? "ipv4.fiddler" : LocalHost;

		private static int ConnectionLimitDefault =>
			int.TryParse(Environment.GetEnvironmentVariable("ELASTICSEARCH_CLIENT_NUMBER_OF_CONNECTIONS"), out var x)
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

		private static SourceSerializerFactory CreateSerializerFactory(SourceSerializerFactory provided)
		{
			if (provided != null)
				return provided;

			if (!TestConfiguration.Instance.Random.SourceSerializer)
				return null;

			return (builtin, values) => new TestSourceSerializer(builtin, values);
		}

		private static NodePool CreatePool(Func<ICollection<Uri>, NodePool> createPool = null,
			int port = 9200)
		{
			createPool ??= uris => new StaticNodePool(uris);
			var nodePool = createPool(new[] {CreateUri(port)});
			return nodePool;
		}

		public static Uri CreateUri(int port = 9200) => new UriBuilder("http://", LocalOrProxyHost, port).Uri;
	}
}
