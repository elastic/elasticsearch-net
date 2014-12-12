using System.Linq;
using FluentAssertions;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using Elasticsearch.Net;

namespace Nest.Tests.Integration.Search
{
	[TestFixture]
	public class TerminateAfterTests : IntegrationTests
	{
		[Test]
		public void TerminatedEarlyIsSet()
		{
			var queryResults = this.Client.Search<ElasticsearchProject>(s=>s
				.TerminateAfter(1)
				.MatchAll() //not explicitly needed.
			);
			queryResults.IsValid.Should().BeTrue();
			queryResults.Total.Should().BeGreaterThan(0);
			queryResults.TerminatedEarly.Should().BeTrue();
		}
		
	}
}