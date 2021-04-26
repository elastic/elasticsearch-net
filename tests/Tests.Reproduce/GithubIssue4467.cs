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

namespace Tests.Reproduce
{
	public class GithubIssue4467
	{
		[U]
		public void CanDeserializeRepositories()
		{
			var json = @"{
			  ""test_repository"": {
			    ""type"": ""azure"",
			    ""settings"": {
			      ""container"": ""test-backup-container"",
			      ""client"": ""default"",
			      ""base_path"": ""test-backups"",
			      ""chunk_size"": ""64mb""
			    }
			  },
			  ""repo_aligncare_v7"": {
			    ""type"": ""azure"",
			    ""settings"": {
			      ""container"": ""aligncare-backup-container-02292020"",
			      ""base_path"": ""aligncare-backups"",
			      ""chunk_size"": ""64m"",
			      ""compress"": ""true""
			    }
			  }
			}";

			var bytes = Encoding.UTF8.GetBytes(json);
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(pool, new InMemoryConnection(bytes));
			var client = new ElasticClient(connectionSettings);

			Func<GetRepositoryResponse> responseAction = () =>  client.Snapshot.GetRepository();
			responseAction.Should().NotThrow();

			var response = responseAction();
			response.Repositories.Should().HaveCount(2);

			var azureRepository = response.Azure("repo_aligncare_v7");
			azureRepository.Should().NotBeNull();
			azureRepository.Settings.Compress.Should().BeTrue();
		}
	}
}
