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
using Elastic.Transport;
using FluentAssertions;
using Nest;

namespace Tests.Reproduce
{
	public class GitHubIssue4382
	{
		[U]
		public void CanDeserializeVerifyRepositoryResponse()
		{
			var json = @"{""nodes"":{""1cWG9trDRi--6I-46lOlBw"":{""name"":""DESKTOP-5L01F6I""}}}";

			var bytes = Encoding.UTF8.GetBytes(json);
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(pool, new InMemoryConnection(bytes)).DefaultIndex("default_index");
			var client = new ElasticClient(connectionSettings);

			var response = client.Snapshot.VerifyRepository("repository");
			response.Nodes.Should().NotBeNullOrEmpty();
			response.Nodes["1cWG9trDRi--6I-46lOlBw"].Name.Should().Be("DESKTOP-5L01F6I");
		}
	}
}
