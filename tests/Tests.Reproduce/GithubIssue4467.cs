// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
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
