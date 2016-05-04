using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.QueryDsl.Specialized.Script
{
	/**
	* A query allowing to define {ref_current}/modules-scripting.html[scripts] as queries.
	*
	* See the Elasticsearch documentation on {ref_current}/query-dsl-script-query.html[script query] for more details.
	*/
	[Collection(IntegrationContext.ReadOnly)]
	public class ScriptQueryUsageTests : QueryDslUsageTestsBase
	{
		public ScriptQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		private static readonly string _templateString = "doc['numberOfCommits'].value > param1";

		protected override bool ForceInMemory => false;

		protected override object QueryJson => new
		{
			script = new
			{
				_name = "named_query",
				script = new
				{
					inline = "doc['numberOfCommits'].value > param1",
					@params = new { param1 = 50 }
				}
			}
		};

		protected override QueryContainer QueryInitializer => new ScriptQuery
		{
			Name = "named_query",
			Inline = _templateString,
			Params = new Dictionary<string, object>
			{
				{ "param1", 50 }
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Script(sn => sn
				.Name("named_query")
				.Inline(_templateString)
				.Params(p => p.Add("param1", 50))
			);

		protected void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();
			response.Documents.Count().Should().BeGreaterThan(0);
		}

		[I]
		protected async Task ReturnsExpectedResponse() => await this.AssertOnAllResponses(ExpectResponse);

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
