using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Reproduce
{
	public class GithubIssue2871 : IClusterFixture<WritableCluster>
	{
		private readonly WritableCluster _cluster;

		public GithubIssue2871(WritableCluster cluster) => _cluster = cluster;

		[I]
		public void IsValidFalseAndDeserializedErrorsWhenMultiGetDocHasErrors()
		{
			var index1 = "index1";
			var index2 = "index2";
			var alias = "my_alias";
			var client = _cluster.Client;

			client.CreateIndex(index1, c=> c
				.Mappings(m => m
					.Map<Project>(mm => mm
						.AutoMap()
					)
				)
			);

			client.CreateIndex(index2, c => c
				.Mappings(m => m
					.Map<Project>(mm => mm
						.AutoMap()
					)
				)
			);

			var projects = new[]
			{
				new Project { Name = "project1" },
				new Project { Name = "project2" },
			};

			client.Bulk(b => b
				.IndexMany(projects, (bi, p) => bi.Index(index1).Document(p))
				.IndexMany(projects, (bi, p) => bi.Index(index2).Document(p))
				.Refresh(Refresh.WaitFor)
			);

			client.Alias(a => a
				.Add(add => add
					.Alias(alias)
					.Index(index1)
				)
				.Add(add => add
					.Alias(alias)
					.Index(index2)
				)
			);

			var multiGetRequest = new MultiGetRequest
			{
				Documents = new[] {
					new MultiGetOperation<Project>("project1") {Index = alias },
					new MultiGetOperation<Project>("project2") {Index = alias }
				}
			};

			var response = client.MultiGet(multiGetRequest);
			response.ShouldNotBeValid();

			var firstMultiGetHit = response.Documents.First();
			firstMultiGetHit.Error.Should().NotBeNull();
			firstMultiGetHit.Error.Error.Should().NotBeNull();
			firstMultiGetHit.Error.Error.Type.Should().NotBeNullOrEmpty();
			firstMultiGetHit.Error.Error.Reason.Should().NotBeNullOrEmpty();
			firstMultiGetHit.Error.Error.RootCause.Should().NotBeNull().And.HaveCount(1);

			var lastMultiGetHit = response.Documents.Last();
			lastMultiGetHit.Error.Should().NotBeNull();
			lastMultiGetHit.Error.Error.Should().NotBeNull();
			lastMultiGetHit.Error.Error.Type.Should().NotBeNullOrEmpty();
			lastMultiGetHit.Error.Error.Reason.Should().NotBeNullOrEmpty();
			lastMultiGetHit.Error.Error.RootCause.Should().NotBeNull().And.HaveCount(1);
		}
	}
}
