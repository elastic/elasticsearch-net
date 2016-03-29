using System.Collections.Generic;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.Specialized.Script
{
	public class ScriptUsageTests : QueryDslUsageTestsBase
	{
		public ScriptUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		private static readonly string _templateString = "doc['num1'].value > param1";

		protected override object QueryJson => new
		{
			script = new
			{
				_name = "named_query",
				boost = 1.1,
				inline = "doc['num1'].value > param1",
				@params = new
				{
					param1 = 1
				}
			}



		};

		protected override QueryContainer QueryInitializer => new ScriptQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Inline = _templateString,
			Params = new Dictionary<string, object>
			{
				{ "param1", 1 }
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Script(sn => sn
				.Name("named_query")
				.Boost(1.1)
				.Inline(_templateString)
				.Params(p=>p.Add("param1", 1))
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IScriptQuery>(a => a.Script)
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