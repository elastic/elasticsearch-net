// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.Specialized.ScriptScore
{
	/**
	* A query allowing you to modify the score of documents that are retrieved by a query.
	* This can be useful if, for example, a score function is computationally expensive and
	* it is sufficient to compute the score on a filtered set of documents.
	*
	* See the Elasticsearch documentation on {ref_current}/query-dsl-script-score-query.html[script_score query] for more details.
	*/
	public class ScriptScoreQueryUsageTests : QueryDslUsageTestsBase
	{
		private static readonly string _scriptScoreSource = "decayNumericLinear(params.origin, params.scale, params.offset, params.decay, doc['numberOfCommits'].value)";

		public ScriptScoreQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IScriptScoreQuery>(a => a.ScriptScore)
		{
			q =>
			{
				q.Query = null;
			},
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

		protected override QueryContainer QueryInitializer => new ScriptScoreQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Query = new NumericRangeQuery
			{
				Field = Infer.Field<Project>(f => f.NumberOfCommits),
				GreaterThan = 50
			},
			Script = new InlineScript(_scriptScoreSource)
			{
				Params = new Dictionary<string, object>
				{
					{ "origin", 100 },
					{ "scale", 10 },
					{ "decay", 0.5 },
					{ "offset", 0 }
				}
			},
		};

		protected override object QueryJson => new
		{
			script_score = new
			{
				_name = "named_query",
				boost = 1.1,
				query = new
				{
					range = new
					{
						numberOfCommits = new
						{
							gt = 50.0
						}
					}
				},
				script = new
				{
					source = _scriptScoreSource,
					@params = new
					{
						origin = 100,
						scale = 10,
						decay = 0.5,
						offset = 0
					}
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.ScriptScore(sn => sn
				.Name("named_query")
				.Boost(1.1)
				.Query(qq => qq
					.Range(r => r
						.Field(f => f.NumberOfCommits)
						.GreaterThan(50)
					)
				)
				.Script(s => s
					.Source(_scriptScoreSource)
					.Params(p => p
						.Add("origin", 100)
						.Add("scale", 10)
						.Add("decay", 0.5)
						.Add("offset", 0)
					)
				)
			);
	}
}
