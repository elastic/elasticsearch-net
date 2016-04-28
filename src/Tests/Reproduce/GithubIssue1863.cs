using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using FluentAssertions;
using Tests.Framework;
using Xunit;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.Reproduce
{
	[Collection(IntegrationContext.ReadOnly)]
	[SkipVersion("<2.1.0", "")]
	public class GithubIssue1863
	{
		[I]
		public void ConcreteTypeConverterThrowsExceptionOnNullScore()
		{
			var client = TestClient.GetClient();
			var response = client.Search<Project>(s => s
				.ConcreteTypeSelector((d,h) => typeof(Project))
				.Sort(srt => srt.Ascending(p => p.StartedOn))
			);
			response.Hits.Count().Should().BeGreaterThan(0);
			response.Hits.All(h => h.Score == 0).Should().BeTrue();
		}
	}
}
