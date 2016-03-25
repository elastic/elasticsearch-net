using System.Collections.Generic;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.Specialized.Template
{
	public class TemplateUsageTests : QueryDslUsageTestsBase
	{
		public TemplateUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		private static readonly string _templateString = "{ \"match\": { \"text\": \"{{query_string}}\" }}";

		protected override object QueryJson => new
		{
			template = new
			{
				_name = "named_query",
				boost = 1.1,
				inline = _templateString,
				@params = new
				{
					query_string = "all about search"
				}
			}
		};

		protected override QueryContainer QueryInitializer => new TemplateQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Inline = _templateString,
			Params = new Dictionary<string, object>
			{
				{ "query_string", "all about search" }
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Template(sn => sn
				.Name("named_query")
				.Boost(1.1)
				.Inline(_templateString)
				.Params(p=>p.Add("query_string", "all about search"))
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ITemplateQuery>(a => a.Template)
		{
			q => {
				q.Inline = "";
				q.Id = null;
				q.File = "";
			},
			q => {
				q.Inline = null;
				q.Id = null;
				q.File = null;
			}
		};
	}
}
