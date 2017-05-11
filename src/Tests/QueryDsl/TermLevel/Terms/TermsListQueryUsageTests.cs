using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

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
				description = new[] { "term1", "term2" }
			}
		};

		protected override QueryContainer QueryInitializer => new TermsQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			Terms = new List<string> { "term1", "term2" }
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Terms(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.Terms(new List<string> { "term1", "term2" })
			);
	}

	public class TermsListOfListIntegrationTests : QueryDslIntegrationTestsBase
	{
		public TermsListOfListIntegrationTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private List<List<string>> _terms = new List<List<string>> { new List<string> { "term1", "term2" } };

		protected override int ExpectStatusCode => 400;
		protected override bool ExpectIsValid => false;

		protected override object QueryJson => new
		{
			terms = new
			{
				_name = "named_query",
				boost = 1.1,
				description = new[] { new [] { "term1", "term2" } },
			}
		};

		protected override QueryContainer QueryInitializer => new TermsQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			Terms = _terms
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Terms(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.Terms(_terms)
			);

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldNotBeValid();

			response.ServerError.Should().NotBeNull();
			response.ServerError.Status.Should().Be(400);
			response.ServerError.Error.Should().NotBeNull();
			var rootCauses = response.ServerError.Error.RootCause;
			rootCauses.Should().NotBeNullOrEmpty();
			var rootCause = rootCauses.First();
			rootCause.Type.Should().Be("parsing_exception");
		}
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
				numberOfCommits = new[] { new [] { "term1", "term2" } }
			}
		};

		protected override QueryContainer QueryInitializer => new TermsQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "numberOfCommits",
			Terms = _terms,
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Terms(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.NumberOfCommits)
				.Terms(_terms)
			);

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ServerError.Should().NotBeNull();
			response.ServerError.Status.Should().Be(400);
			response.ServerError.Error.Should().NotBeNull();
			var rootCauses = response.ServerError.Error.RootCause;
			rootCauses.Should().NotBeNullOrEmpty();
			var rootCause = rootCauses.First();
			rootCause.Type.Should().Be("parsing_exception");
		}

	}

}
