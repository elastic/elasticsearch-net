using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Search.Query
{
	[TestFixture]
	public class FunctionScoreQueryTest : IntegrationTests
	{
		[Test]
		public void RandomScore()
		{
			var results1 = Client.Search<ElasticsearchProject>(s => s
				.Query(q =>
					q.FunctionScore(fs => fs
						.Functions(ff => ff
							.RandomScore(1337)
						)
					)
				)
				.Take(1)
			);

			var results2 = Client.Search<ElasticsearchProject>(s => s
				.Query(q =>
					q.FunctionScore(fs => fs
						.Functions(ff => ff
							.RandomScore(1338)
						)
					)
				)
				.Take(1)
			);

			results1.IsValid.Should().BeTrue();
			results2.IsValid.Should().BeTrue();

			results1.Documents.FirstOrDefault().Should().NotBeNull();
			results2.Documents.FirstOrDefault().Should().NotBeNull();

			results1.Documents.First().Id.Should().NotBe(results2.Documents.First().Id);
		}
	}
}