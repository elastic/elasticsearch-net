using System.IO;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;

namespace Tests.QueryDsl.TermLevel.Prefix
{
	public class PrefixQueryUsageTests : QueryDslUsageTestsBase
	{
		public PrefixQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IPrefixQuery>(a => a.Prefix)
		{
			q => q.Field = null,
			q => q.Value = null,
			q => q.Value = string.Empty
		};

		protected override QueryContainer QueryInitializer => new PrefixQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			Value = "proj",
			Rewrite = MultiTermQueryRewrite.TopTerms(10)
		};

		protected override object QueryJson => new
		{
			prefix = new
			{
				description = new
				{
					_name = "named_query",
					boost = 1.1,
					rewrite = "top_terms_10",
					value = "proj"
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Prefix(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.Value("proj")
				.Rewrite(MultiTermQueryRewrite.TopTerms(10))
			);

		//hide
		[U] public void DeserializeShortForm()
		{
			using var stream = new MemoryStream(ShortFormQuery);
			var query = Client.RequestResponseSerializer.Deserialize<IPrefixQuery>(stream);
			query.Should().NotBeNull();
			query.Field.Should().Be(new Field("description"));
			query.Value.Should().Be("project description");
		}
	}
}
