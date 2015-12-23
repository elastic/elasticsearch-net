using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.NestSpecific.Raw
{
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
