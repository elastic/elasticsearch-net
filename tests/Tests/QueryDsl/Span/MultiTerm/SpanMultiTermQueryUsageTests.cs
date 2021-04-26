/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.Span.MultiTerm
{
	public class SpanMultiTermUsageTests : QueryDslUsageTestsBase
	{
		public SpanMultiTermUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ISpanMultiTermQuery>(a => a.SpanMultiTerm)
		{
			q => q.Match = null,
			q => q.Match = ConditionlessQuery,
		};

		protected override QueryContainer QueryInitializer => new SpanMultiTermQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Match = new PrefixQuery { Field = Infer.Field<Project>(f => f.Description), Value = "pre-*" }
		};

		protected override object QueryJson => new
		{
			span_multi = new
			{
				_name = "named_query",
				boost = 1.1,
				match = new
				{
					prefix = new { description = new { value = "pre-*" } }
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.SpanMultiTerm(c => c
				.Name("named_query")
				.Boost(1.1)
				.Match(sq => sq
					.Prefix(pr => pr.Field(p => p.Description).Value("pre-*"))
				)
			);
	}
}
