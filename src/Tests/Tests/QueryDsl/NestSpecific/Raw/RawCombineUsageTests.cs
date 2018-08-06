using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.QueryDsl.NestSpecific.Raw
{
	/**
	 * NEST's <<raw-query-usage, raw query>> can be combined with other queries using a <<compound-queries, compound query>>
	 * such as a `bool` query.
	 */
	public class RawCombineUsageTests : QueryDslUsageTestsBase
	{
		public RawCombineUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		private static readonly string RawTermQuery = @"{""term"": { ""fieldname"":""value"" } }";

		protected override bool SupportsDeserialization => false;

		protected override object QueryJson => new
		{
			@bool = new {
				must = new object[]
				{
					new { term = new { fieldname = "value" } },
					new { term = new { x = new { value = "y" } } }
				}
			}
		};

		protected override QueryContainer QueryInitializer =>
			new RawQuery(RawTermQuery)
			&& new TermQuery { Field = "x", Value = "y" };

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) =>
			q.Raw(RawTermQuery) && q.Term("x", "y");

	}
}
