using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Properties
{
	[TestFixture]
	public class PropertiesTests : BaseJsonTests
	{
		[Test]
		public void TopChildrenQuery()
		{
			ISearchDescriptor s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Take(10)
				.Query(q => q
				.TopChildren<Person>(tcq => tcq
					.Query(qq =>
						qq.Term(f => f.FirstName, "foo") || qq.Term(f => f.FirstName, "bar")
					)
				)
			);
			s.Query.Should().NotBeNull();
			s.Query.TopChildren.Should().NotBeNull();
			s.Query.TopChildren.Query.Should().NotBeNull();
			var boolQuery =s.Query.TopChildren.Query.Bool;
			boolQuery.Should().NotBeNull();
			
			boolQuery.Should.Should().NotBeEmpty().And.HaveCount(2);
			var firstTerm = boolQuery.Should.First().Term;

			firstTerm.Value.Should().Be("foo");

		}

	}
}
