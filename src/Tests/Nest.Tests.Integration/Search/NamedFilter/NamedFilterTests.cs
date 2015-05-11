using FluentAssertions;
using System.Linq;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Search.NamedFilter
{
	[TestFixture]
	public class NamedFilterTest : IntegrationTests
	{
		private string _LookFor = NestTestData.Data.First().Name;

		[Test]
		public void SimpleNamedFilter()
		{
			var queryResults = this.Client.Search<ElasticsearchProject>(s => s
				.From(0)
				.Size(10)
				.Fields(p => p.Name)
				.PostFilter(f =>
					f.Name("myfilter").Terms(p => p.Name.Suffix("sort"), new[] {_LookFor.ToLowerInvariant()})
					|| f.Name("myfilter2").Terms(p => p.Name.Suffix("sort"), new[] {"nest"})
				)
				);
			queryResults.IsValid.Should().BeTrue();
			foreach(var hit in queryResults.Hits)
			{
				hit.MatchedQueries.Should().NotBeNull().And.NotBeEmpty();
				hit.MatchedQueries.Any(mq => mq.Contains("myfilter") || mq.Contains("myfilter2"));
			}
		}
	}

}