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
	public class GithubIssue2985 : IClusterFixture<WritableCluster>
	{
		private readonly WritableCluster _cluster;

		public GithubIssue2985(WritableCluster cluster) => _cluster = cluster;

		protected static string RandomString() => Guid.NewGuid().ToString("N").Substring(0, 8);

		[I]
		public void CanReadSingleOrMultipleCommonGramsCommonWordsItem()
		{
			var client = _cluster.Client;
			var index = $"gh2985-{RandomString()}";
			var response = client.CreateIndex(index, i=> i
				.Settings(s=>s
					.Analysis(a=>a
						.Analyzers(an=>an
							.Custom("custom", c=>c.Filters("ascii_folding").Tokenizer("standard"))
						)
					)
				)
			);
			response.OriginalException.Should().NotBeNull().And.BeOfType<ElasticsearchClientException>();
			response.OriginalException.Message.Should()
				.Be("Request failed to execute. Error: Custom Analyzer [custom] failed to find filter under name [ascii_folding]");

			client.DeleteIndex(index);
		}
	}
}
