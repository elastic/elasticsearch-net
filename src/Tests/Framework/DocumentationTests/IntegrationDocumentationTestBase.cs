using System;
using Nest;
using Tests.Framework.Integration;

namespace Tests.Framework
{
	public abstract class DocumentationTestBase
	{
		protected static string RandomString() => Guid.NewGuid().ToString("N").Substring(0, 8);

		protected IElasticClient Client => TestClient.GetInMemoryClient();

	}
	public abstract class IntegrationDocumentationTestBase
	{
		protected static string RandomString() => Guid.NewGuid().ToString("N").Substring(0, 8);

		readonly IIntegrationCluster _cluster;
		protected IElasticClient Client => this._cluster.Client(s=>s);

		protected IntegrationDocumentationTestBase(IIntegrationCluster cluster)
		{
			this._cluster = cluster;
		}

	}
}
