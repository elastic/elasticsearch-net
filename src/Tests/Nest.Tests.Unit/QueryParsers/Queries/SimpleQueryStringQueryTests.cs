using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class SimpleQueryStringQueryTests : ParseQueryTestsBase
	{
		[Test]
		public void SimpleQueryString_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.SimpleQueryString,
				f=>f.SimpleQueryString(sq=>sq
					.Analyzer("my-analyzer")
					.DefaultOperator(Operator.And)
					.Flags("ASFAS")
					.Locale("en")
					.LowercaseExpendedTerms()
					.DefaultField(p=>p.Name)
					.OnFields(p=>p.Name, p=>p.Origin)
					.Query("some query")
					)
				);
			q.Analyzer.Should().Be("my-analyzer");
			q.DefaultOperator.Should().Be(Operator.And);
			q.Flags.Should().Be("ASFAS");
			q.Locale.Should().Be("en");
			q.LowercaseExpendedTerms.Should().BeTrue();
			q.DefaultField.Should().Be("name");
			q.Fields.Should().BeEquivalentTo(new []{ "name", "origin"});
			q.Query.Should().Be("some query");

		}
	}
}