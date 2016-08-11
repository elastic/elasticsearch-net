using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using FluentAssertions;
using Tests.Document.Multiple.Reindex;
using Tests.Framework;
using Xunit;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.Reproduce
{
	[SkipVersion("<2.1.0", "")]
	public class GithubIssue1863 : IClusterFixture<ReadOnlyCluster>
	{
		private readonly ReadOnlyCluster _cluster;

		public GithubIssue1863(ReadOnlyCluster cluster)
		{
			_cluster = cluster;
		}

		[I]
		public void ConcreteTypeConverterThrowsExceptionOnNullScore()
		{
			var client = _cluster.Client;
			var response = client.Search<Project>(s => s
				.ConcreteTypeSelector((d,h) => typeof(Project))
				.Sort(srt => srt.Ascending(p => p.StartedOn))
			);
			response.Hits.Count().Should().BeGreaterThan(0);
			response.Hits.All(h => h.Score == 0).Should().BeTrue();
		}
	}
}
