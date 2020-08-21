// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.Specialized.Script
{
	/**
	* A query allowing to define {ref_current}/modules-scripting.html[scripts] as queries.
	*
	* See the Elasticsearch documentation on {ref_current}/query-dsl-script-query.html[script query] for more details.
	*/
	public class ScriptQueryUsageTests : QueryDslUsageTestsBase
	{
		private static readonly string _templateString = "doc['numberOfCommits'].value > params.param1";

		public ScriptQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IScriptQuery>(a => a.Script)
		{
			q =>
			{
				q.Script = null;
			},
			q =>
			{
				q.Script = new InlineScript(null);
			},
			q =>
			{
				q.Script = new InlineScript("");
			},
			q =>
			{
				q.Script = new IndexedScript(null);
			},
			q =>
			{
				q.Script = new IndexedScript("");
			}
		};

		protected override QueryContainer QueryInitializer => new ScriptQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Script = new InlineScript(_templateString)
			{
				Params = new Dictionary<string, object>
				{
					{ "param1", 50 }
				}
			},
		};

		protected override object QueryJson => new
		{
			script = new
			{
				_name = "named_query",
				boost = 1.1,
				script = new
				{
					source = "doc['numberOfCommits'].value > params.param1",
					@params = new { param1 = 50 }
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Script(sn => sn
				.Name("named_query")
				.Boost(1.1)
				.Script(s => s
					.Source(_templateString)
					.Params(p => p.Add("param1", 50))
				)
			);
	}
}
