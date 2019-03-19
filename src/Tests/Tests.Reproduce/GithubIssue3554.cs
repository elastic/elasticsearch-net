using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Reproduce
{
	public class GithubIssue3554 : IClusterFixture<WritableCluster>
	{
		private readonly WritableCluster _cluster;

		public GithubIssue3554(WritableCluster cluster) => _cluster = cluster;

		[I] public void GetManyDoesNotDeserializeDateTimeLikeStringsToDateTime()
		{
			var doc = new CreatedAtDocument { CreatedAt = "2009-11-15T14:12:12" };
			var client = _cluster.Client;
			var createIndexResponse = client.CreateIndex("githubissue3554", c => c
				.Mappings(m => m
					.Map<CreatedAtDocument>(mm => mm
						.Properties(p => p
							.Keyword(k => k
								.Name(n => n.CreatedAt)
							)
						)
					)
				)
			);

			createIndexResponse.ShouldBeValid();
			var indexResponse = client.Index(doc, i => i.Id(1));
			indexResponse.ShouldBeValid();

			var getManyResponse = client.GetMany<CreatedAtDocument>(new long[] { 1 });
			getManyResponse.First().Source.CreatedAt.Should().Be(doc.CreatedAt);
		}

		public class CreatedAtDocument
		{
			public string CreatedAt { get; set; }
		}
	}
}
