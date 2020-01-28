using System.IO;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;

namespace Tests.QueryDsl.TermLevel.Term
{
	public class TermQueryUsageTests : QueryDslUsageTestsBase
	{
		public TermQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ITermQuery>(q => q.Term)
		{
			q => q.Field = null,
			q => q.Value = "  ",
			q => q.Value = null
		};

		protected override QueryContainer QueryInitializer => new TermQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			Value = "project description"
		};

		protected override object QueryJson => new
		{
			term = new
			{
				description = new
				{
					_name = "named_query",
					boost = 1.1,
					value = "project description"
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Term(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.Value("project description")
			);

		//hide
		[U] public void DeserializeShortForm()
		{
			using var stream = new MemoryStream(ShortFormQuery);
			var query = Client.RequestResponseSerializer.Deserialize<ITermQuery>(stream);
			query.Should().NotBeNull();
			query.Field.Should().Be(new Field("description"));
			query.Value.Should().Be("project description");
		}
	}

	/**[float]
	*== Verbatim term query
	 *
	 * By default an empty term is conditionless so will be rewritten. Sometimes sending an empty term to
	 * match nothing makes sense. You can either use the `ConditionlessQuery` construct from NEST to provide a fallback or make the
	 * query verbatim as followed:
	*/
	public class VerbatimTermQueryUsageTests : TermQueryUsageTests
	{
		public VerbatimTermQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => null;

		protected override QueryContainer QueryInitializer => new TermQuery
		{
			IsVerbatim = true,
			Field = "description",
			Value = "",
		};

		protected override object QueryJson => new
		{
			term = new
			{
				description = new
				{
					value = ""
				}
			}
		};

		//when reading back the json the notion of is conditionless is lost
		protected override bool SupportsDeserialization => false;

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Term(c => c
				.Verbatim()
				.Field(p => p.Description)
				.Value(string.Empty)
			);
	}
}
