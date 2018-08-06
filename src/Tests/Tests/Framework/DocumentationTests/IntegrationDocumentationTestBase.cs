using System;
using Nest;
using Tests.Core.Client;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Framework
{
	public abstract class DocumentationTestBase
	{
		protected static string RandomString() => Guid.NewGuid().ToString("N").Substring(0, 8);

		protected IElasticClient Client => TestClient.DefaultInMemoryClient;

	}

	public abstract class IntegrationDocumentationTestBase
	{
		protected static string RandomString() => Guid.NewGuid().ToString("N").Substring(0, 8);

		private readonly ClientTestClusterBase _cluster;
		protected IElasticClient Client => this._cluster.Client;

		protected IntegrationDocumentationTestBase(ClientTestClusterBase cluster) => this._cluster = cluster;
	}
}
