using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

#pragma warning disable 618 // DisableCoord and MinimumShouldMatch

namespace Tests.QueryDsl.TermLevel.Terms
{
	public class TermsListQueryUsageTests : QueryDslUsageTestsBase
	{
		public TermsListQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) {}

		protected override object QueryJson => new
		{
			terms = new
			{
				_name = "named_query",
				boost = 1.1,
				description = new[] { "term1", "term2" },
				disable_coord = true,
				minimum_should_match = 2
			}
		};

		protected override QueryContainer QueryInitializer => new TermsQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			Terms = new List<string> { "term1", "term2" },
			DisableCoord = true,
			MinimumShouldMatch = 2
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Terms(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.DisableCoord()
				.MinimumShouldMatch(MinimumShouldMatch.Fixed(2))
				.Terms(new List<string> { "term1", "term2" })
			);
	}

	public class TermsListOfListIntegrationTests : QueryDslIntegrationTestsBase
	{
		public TermsListOfListIntegrationTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private List<List<string>> _terms = new List<List<string>> { new List<string> { "term1", "term2" } };

		protected override object QueryJson => new
		{
			terms = new
			{
				_name = "named_query",
				boost = 1.1,
				description = new[] { new [] { "term1", "term2" } },
				disable_coord = true,
			}
		};

		protected override QueryContainer QueryInitializer => new TermsQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			Terms = _terms,
			DisableCoord = true,
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Terms(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.DisableCoord()
				.Terms(_terms)
			);
	}

	public class TermsListOfListStringAgainstNumericFieldIntegrationTests : QueryDslIntegrationTestsBase
	{
		public TermsListOfListStringAgainstNumericFieldIntegrationTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private List<List<string>> _terms = new List<List<string>> { new List<string> { "term1", "term2" } };


		protected override int ExpectStatusCode => 400;
		protected override bool ExpectIsValid => false;

		protected override object QueryJson => new
		{
			terms = new
			{
				_name = "named_query",
				boost = 1.1,
				numberOfCommits = new[] { new [] { "term1", "term2" } },
				disable_coord = true,
			}
		};

		protected override QueryContainer QueryInitializer => new TermsQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "numberOfCommits",
			Terms = _terms,
			DisableCoord = true,
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Terms(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.NumberOfCommits)
				.DisableCoord()
				.Terms(_terms)
			);

		[I] public Task AsserResponse() => AssertOnAllResponses(r =>
		{
			r.ServerError.Should().NotBeNull();
			r.ServerError.Status.Should().Be(400);
			r.ServerError.Error.Should().NotBeNull();
			var rootCauses = r.ServerError.Error.RootCause;
			rootCauses.Should().NotBeNullOrEmpty();
			var rootCause = rootCauses.First();
			rootCause.Type.Should().Be("number_format_exception");
		});
	}
}
