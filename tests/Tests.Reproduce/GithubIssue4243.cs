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

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Elasticsearch.Net;
using Elasticsearch.Net.Specification.CatApi;
using FluentAssertions;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Reproduce
{
	public class GithubIssue4243 : IClusterFixture<ReadOnlyCluster>
	{
		private readonly ReadOnlyCluster _cluster;

		public GithubIssue4243(ReadOnlyCluster cluster) => _cluster = cluster;

		[I]
		public async Task UsingFormatJsonIsSuccessfulResponse()
		{
			var connectionConfiguration = new ConnectionConfiguration(_cluster.Client.ConnectionSettings.ConnectionPool);
			var lowLevelClient = new ElasticLowLevelClient(connectionConfiguration);
			var response = await lowLevelClient.Cat.MasterAsync<StringResponse>(new CatMasterRequestParameters { Format = "JSON" });

			response.Success.Should().BeTrue();
			response.ApiCall.HttpStatusCode.Should().Be(200);
			response.OriginalException.Should().BeNull();
		}
	}
}
