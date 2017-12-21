using System.Collections.Generic;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.Specialized.Template
{
	public class TemplateQueryUsageTests : QueryDslUsageTestsBase
	{
		public TemplateQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		private static readonly string _templateString = "{ \"match\": { \"text\": \"{{query_string}}\" }}";

		protected override object QueryJson => new
		{
			template = new
			{
				_name = "named_query",
				boost = 1.1,
				source = _templateString,
				@params = new
				{
					query_string = "all about search"
				}
			}
		};

#pragma warning disable 618
		protected override QueryContainer QueryInitializer => new TemplateQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Source = _templateString,
			Params = new Dictionary<string, object>
			{
				{ "query_string", "all about search" }
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Template(sn => sn
				.Name("named_query")
				.Boost(1.1)
				.Source(_templateString)
				.Params(p=>p.Add("query_string", "all about search"))
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ITemplateQuery>(a => a.Template)
		{
			q => {
				q.Source = "";
				q.Id = null;
			},
			q => {
				q.Source = null;
				q.Id = null;
			}
		};
	}
#pragma warning restore 618
}
