// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Domain;

namespace Tests.Reproduce
{
	/*
	 * https://github.com/elastic/elasticsearch-net/pull/4353
	 * Fixed an issue with the GetMany helpers that returned the cartesian product of all ids specified rather
	 * then creating a distinct list if more then one index was targeted.
	 *
	 * This PR also updated the routine in the serializer to omit the index name from each item if the index is
	 * already specified on the url in case of multiple indices
	 *
	 * This updated routine in the `7.6.0` could throw if you are calling:
	 *
	 * client.GetMany<T>(ids, "indexName");
	 *
	 * Without configuring `ConnectionSettings()` with either a default index for T or a global default index.
	 */
	public class GitHubIssue4462
	{
		[U] public void GetManyShouldNotThrowIfIndexIsProvided()
		{
			var json = "{}";

			var bytes = Encoding.UTF8.GetBytes(json);
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(pool, new InMemoryConnection(bytes));
			var client = new ElasticClient(connectionSettings);

			var response = client.GetMany<Project>(new long[] {1, 2, 3}, "indexName");
			response.Should().NotBeNull();
		}

		[U] public void SourceManyShouldNotThrowIfIndexIsProvided()
		{
			var json = "{}";

			var bytes = Encoding.UTF8.GetBytes(json);
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(pool, new InMemoryConnection(bytes));
			var client = new ElasticClient(connectionSettings);

			var response = client.SourceMany<Project>(new long[] {1, 2, 3}, "indexName");
			response.Should().NotBeNull();
		}
	}
}
