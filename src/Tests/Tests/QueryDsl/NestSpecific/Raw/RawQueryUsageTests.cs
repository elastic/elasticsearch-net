using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.NestSpecific.Raw
{
	/**
	 * Allows a query represented as a string of JSON to be passed to NEST's Fluent API or Object Initializer syntax.
	 * This can be useful when porting over a query expressed in the query DSL over to NEST.
	 */
	public class RawUsageTests : QueryDslUsageTestsBase
	{
		public RawUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		private static readonly string RawTermQuery = @"{""term"": { ""fieldname"":""value"" } }";

		protected override bool SupportsDeserialization => false;

		protected override object QueryJson => new
		{
			term = new { fieldname = "value" }
		};

		protected override QueryContainer QueryInitializer => new RawQuery(RawTermQuery);

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Raw(RawTermQuery);

	}
}
