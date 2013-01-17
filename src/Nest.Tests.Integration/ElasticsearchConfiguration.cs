using System;
using System.Diagnostics;

namespace Nest.Tests.Integration
{
	public static class ElasticsearchConfiguration
	{
		public static readonly string DefaultIndex = Test.Default.DefaultIndex + "-" + Process.GetCurrentProcess().Id.ToString();

		public readonly static IConnectionSettings Settings =
			new ConnectionSettings(Test.Default.Host, Test.Default.Port)
				.SetDefaultIndex(ElasticsearchConfiguration.DefaultIndex)
				.SetMaximumAsyncConnections(Test.Default.MaximumAsyncConnections)
				.UsePrettyResponses();


		public static readonly ElasticClient Client = new ElasticClient(Settings);

		public static string NewUniqueIndexName()
		{
			return DefaultIndex + "_" + Guid.NewGuid().ToString();
		}

	}
}