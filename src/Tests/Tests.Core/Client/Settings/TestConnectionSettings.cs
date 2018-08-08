using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using Tests.Configuration;
using Tests.Core.Xunit;
using Tests.Core.Extensions;

namespace Tests.Core.Client.Settings
{
	public class TestConnectionSettings : ConnectionSettings
	{
		public static readonly bool RunningFiddler = Process.GetProcessesByName("fiddler").Any();

		private static string LocalHost => "localhost";
		public static string LocalOrProxyHost => (RunningFiddler) ? "ipv4.fiddler" : LocalHost;

		public TestConnectionSettings(Func<ICollection<Uri>, IConnectionPool> createPool = null,
			ISerializerFactory serializerFactory = null,
			bool forceInMemory = false,
			int port = 9200)
			: base(
				CreatePool(createPool, port),
				TestConfiguration.Instance.CreateConnection(forceInMemory),
				serializerFactory
			) =>
			this.ApplyTestSettings();

		private static int ConnectionLimitDefault =>
			int.TryParse(Environment.GetEnvironmentVariable("NEST_NUMBER_OF_CONNECTIONS"), out var x)
				? x
				: ConnectionConfiguration.DefaultConnectionLimit;

		internal ConnectionSettings ApplyTestSettings() => this
			.ApplyFiddlerProxyWhenNeeded()
			//TODO make this random
			//.EnableHttpCompression()
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

		private ConnectionSettings ApplyFiddlerProxyWhenNeeded()
		{
			if (!TestConfiguration.Instance.Random.OldConnection) return this;
			if (!RunningFiddler) return this;
			return this.Proxy(new Uri("http://localhost:8888"), null, null);
		}

		private static IConnectionPool CreatePool(Func<ICollection<Uri>, IConnectionPool> createPool = null, int port = 9200)
		{
			createPool = createPool ?? (uris => new StaticConnectionPool(uris));
			var connectionPool = createPool(new [] { CreateUri(port) });
			return connectionPool;
		}

		public static Uri CreateUri(int port = 9200) => new UriBuilder("http://", LocalOrProxyHost, port).Uri;
	}
}
