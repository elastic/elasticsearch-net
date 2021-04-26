/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
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
