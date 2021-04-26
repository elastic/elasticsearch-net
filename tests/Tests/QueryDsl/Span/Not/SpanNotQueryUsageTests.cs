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

namespace Tests.QueryDsl.Span.Not
{
	public class SpanNotUsageTests : QueryDslUsageTestsBase
	{
		public SpanNotUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ISpanNotQuery>(a => a.SpanNot)
		{
			q =>
			{
				q.Include = null;
				q.Exclude = null;
			},
			q =>
			{
				q.Include = new SpanQuery();
				q.Exclude = new SpanQuery();
			},
		};

		protected override QueryContainer QueryInitializer => new SpanNotQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Post = 13,
			Pre = 14,
			Include = new SpanQuery
			{
				SpanTerm = new SpanTermQuery
				{
					Field = "field1", Value = "hoya"
				}
			},
			Exclude = new SpanQuery
			{
				SpanTerm = new SpanTermQuery
				{
					Field = "field1", Value = "hoya2"
				}
			},
		};

		protected override object QueryJson => new
		{
			span_not = new
			{
				_name = "named_query",
				boost = 1.1,
				include = new
				{
					span_term = new { field1 = new { value = "hoya" } }
				},
				exclude = new
				{
					span_term = new { field1 = new { value = "hoya2" } }
				},
				pre = 14,
				post = 13
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.SpanNot(sn => sn
				.Name("named_query")
				.Boost(1.1)
				.Post(13)
				.Pre(14)
				.Include(i => i
					.SpanTerm(st => st.Field("field1").Value("hoya"))
				)
				.Exclude(e => e
					.SpanTerm(st => st.Field("field1").Value("hoya2"))
				)
			);
	}
}
