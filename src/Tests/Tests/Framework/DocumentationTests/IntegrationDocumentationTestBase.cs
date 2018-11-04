using System;
using Nest;
using Tests.Core.Client;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Framework
{
	public abstract class DocumentationTestBase
	{
		protected IElasticClient Client => TestClient.DefaultInMemoryClient;

		protected static string RandomString() => Guid.NewGuid().ToString("N").Substring(0, 8);
	}

	public abstract class IntegrationDocumentationTestBase
	{
		private readonly ClientTestClusterBase _cluster;

		protected IntegrationDocumentationTestBase(ClientTestClusterBase cluster) => _cluster = cluster;

		protected IElasticClient Client => _cluster.Client;

		protected static string RandomString() => Guid.NewGuid().ToString("N").Substring(0, 8);
	}
}
