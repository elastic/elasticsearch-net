// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Elastic.Transport;
using Nest;
using Tests.Configuration;
using Tests.Core.Client.Serializers;
using Tests.Core.Extensions;
using Tests.Core.Xunit;

namespace Tests.Core.Client.Settings
{
	public class TestConnectionSettings : ConnectionSettings
	{
		public static readonly bool RunningMitmProxy = Process.GetProcessesByName("mitmproxy").Any();
		public static readonly bool RunningFiddler = Process.GetProcessesByName("fiddler").Any();

		public TestConnectionSettings(
			Func<ICollection<Uri>, IConnectionPool> createPool = null,
			SourceSerializerFactory sourceSerializerFactory = null,
			IPropertyMappingProvider propertyMappingProvider = null,
			bool forceInMemory = false,
			int port = 9200,
			byte[] response = null
		)
			: base(
				CreatePool(createPool, port),
				TestConfiguration.Instance.CreateConnection(forceInMemory, response),
				CreateSerializerFactory(sourceSerializerFactory),
				propertyMappingProvider
			) =>
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
				if (!r.DeprecationWarnings.Any()) return;

				var q = r.Uri.Query;
				//hack to prevent the deprecation warnings from the deprecation response test to be reported
				if (!string.IsNullOrWhiteSpace(q) && q.Contains("routing=ignoredefaultcompletedhandler")) return;

				foreach (var d in r.DeprecationWarnings) XunitRunState.SeenDeprecations.Add(d);
			});

		private ConnectionSettings RerouteToProxyIfNeeded()
		{
			if (!RunningMitmProxy) return this;

			return Proxy(new Uri("http://127.0.0.1:8080"), null, (string)null);
		}

		private static SourceSerializerFactory CreateSerializerFactory(SourceSerializerFactory provided)
		{
			if (provided != null) return provided;
			if (!TestConfiguration.Instance.Random.SourceSerializer) return null;

			return (builtin, values) => new TestSourceSerializerBase(builtin, values);
		}

		private static IConnectionPool CreatePool(Func<ICollection<Uri>, IConnectionPool> createPool = null, int port = 9200)
		{
			createPool ??= (uris => new StaticConnectionPool(uris));
			var connectionPool = createPool(new[] { CreateUri(port) });
			return connectionPool;
		}

		public static Uri CreateUri(int port = 9200) => new UriBuilder("http://", LocalOrProxyHost, port).Uri;
	}
}
