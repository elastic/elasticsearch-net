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

using System.Linq;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.TermLevel.Terms
{
	/**
	* Filters documents that have fields that match any of the provided terms (not analyzed).
	*
	* Be sure to read the Elasticsearch documentation on {ref_current}/query-dsl-terms-query.html[Terms query] for more information.
	*/
	public class TermsQueryUsageTests : QueryDslUsageTestsBase
	{
		public TermsQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ITermsQuery>(a => a.Terms)
		{
			q => q.Field = null,
			q => q.Terms = null,
			q => q.Terms = Enumerable.Empty<object>(),
			q => q.Terms = new[] { "" }
		};

		protected virtual string[] ExpectedTerms => new[] { "term1", "term2" };

		protected override QueryContainer QueryInitializer => new TermsQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			Terms = ExpectedTerms,
		};

		protected override object QueryJson => new
		{
			terms = new
			{
				_name = "named_query",
				boost = 1.1,
				description = ExpectedTerms
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Terms(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.Terms("term1", "term2")
			);
	}

	/**[float]
	*== Single term Terms Query
	*/
	public class SingleTermTermsQueryUsageTests : TermsQueryUsageTests
	{
		public SingleTermTermsQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override string[] ExpectedTerms => new[] { "term1" };

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Terms(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.Terms("term1")
			);
	}

	/**[float]
	*== Verbatim terms query
	 *
	 * By default an empty terms array is conditionless so will be rewritten. Sometimes sending an empty an empty array to mean
	 * match nothing makes sense. You can either use the `ConditionlessQuery` construct from NEST to provide a fallback or make the
	 * query verbatim as followed:
	*/
	public class VerbatimTermsQueryUsageTests : TermsQueryUsageTests
	{
		public VerbatimTermsQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => null;

		protected override QueryContainer QueryInitializer => new TermsQuery
		{
			IsVerbatim = true,
			Field = "description",
			Terms = new string[] { },
		};

		protected override object QueryJson => new
		{
			terms = new
			{
				description = new string[] { }
			}
		};

		//when reading back the json the notion of is conditionless is lost
		protected override bool SupportsDeserialization => false;

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Terms(c => c
				.Verbatim()
				.Field(p => p.Description)
				.Terms(new string[] { })
			);
	}
}
