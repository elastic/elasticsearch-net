using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using Tests.Configuration;
using Tests.Core.Client.Serializers;
using Tests.Core.Xunit;
using Tests.Core.Extensions;

namespace Tests.Core.Client.Settings
{
	public class TestConnectionSettings : ConnectionSettings
	{
		public static readonly bool RunningFiddler = Process.GetProcessesByName("fiddler").Any();

		private static string LocalHost => "localhost";
		public static string LocalOrProxyHost => (RunningFiddler) ? "ipv4.fiddler" : LocalHost;

		public TestConnectionSettings(
			Func<ICollection<Uri>, IConnectionPool> createPool = null,
			SourceSerializerFactory sourceSerializerFactory = null,
			IPropertyMappingProvider propertyMappingProvider = null,
			bool forceInMemory = false,
			int port = 9200)
			: base(
				CreatePool(createPool, port),
				TestConfiguration.Instance.CreateConnection(forceInMemory),
				CreateSerializerFactory(sourceSerializerFactory),
				propertyMappingProvider
			) =>
			this.ApplyTestSettings();

		private static int ConnectionLimitDefault =>
			int.TryParse(Environment.GetEnvironmentVariable("NEST_NUMBER_OF_CONNECTIONS"), out var x)
				? x
				: ConnectionConfiguration.DefaultConnectionLimit;

		internal ConnectionSettings ApplyTestSettings() => this
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

		private static SourceSerializerFactory CreateSerializerFactory(SourceSerializerFactory provided)
		{
			if (provided != null) return provided;
			if (!TestConfiguration.Instance.Random.SourceSerializer) return null;
			return (builtin, values) => new TestSourceSerializerBase(builtin, values);
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
