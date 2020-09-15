// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Nest;
using Tests.Core.Client;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Framework.DocumentationTests
{
	public abstract class DocumentationTestBase
	{
		protected virtual IElasticClient Client => TestClient.DefaultInMemoryClient;

		protected static string RandomString() => Guid.NewGuid().ToString("N").Substring(0, 8);
	}

	public abstract class IntegrationDocumentationTestBase : DocumentationTestBase
	{
		protected readonly ClientTestClusterBase Cluster;

		protected IntegrationDocumentationTestBase(ClientTestClusterBase cluster) => Cluster = cluster;

		protected override IElasticClient Client => Cluster.Client;
	}
}
