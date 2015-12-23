using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.NestSpecific.Raw
{
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