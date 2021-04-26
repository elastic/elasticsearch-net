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

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.QueryDsl.FullText.CombinedFields
{
	/**
	* The `combined_fields` query supports searching multiple text fields as if their contents had been indexed into one combined field. It takes a
	 * term-centric view of the query: first it analyzes the query string into individual terms, then looks for each term in any of the fields.
	*
	* See the Elasticsearch documentation on {ref_current}/query-dsl-combined-fields-query.html[combined fields query] for more details.
	*/
	[SkipVersion("<7.13.0", "Implemented in version 7.13.0")]
	public class CombinedFieldsUsageTests : QueryDslUsageTestsBase
	{
		public CombinedFieldsUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<ICombinedFieldsQuery>(a => a.CombinedFields)
		{
			q => q.Query = null,
			q => q.Query = string.Empty
		};

		protected override QueryContainer QueryInitializer => new CombinedFieldsQuery
		{
			Fields = Field<Project>(p => p.Description).And("myOtherField"),
			Query = "hello world",
			Boost = 1.1,
			Operator = Operator.Or,
			MinimumShouldMatch = "2",
			ZeroTermsQuery = ZeroTermsQuery.All,
			Name = "combined_fields",
			AutoGenerateSynonymsPhraseQuery = false
		};

		protected override object QueryJson => new
		{
			combined_fields = new
			{
				_name = "combined_fields",
				boost = 1.1,
				query = "hello world",
				minimum_should_match = "2",
				@operator = "or",
				fields = new[]
				{
					"description",
					"myOtherField"
				},
				zero_terms_query = "all",
				auto_generate_synonyms_phrase_query = false
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.CombinedFields(c => c
				.Fields(f => f.Field(p => p.Description).Field("myOtherField"))
				.Query("hello world")
				.Boost(1.1)
				.Operator(Operator.Or)
				.MinimumShouldMatch("2")
				.ZeroTermsQuery(ZeroTermsQuery.All)
				.Name("combined_fields")
				.AutoGenerateSynonymsPhraseQuery(false)
			);
	}

	/**[float]
	 * === Combined fields with boost usage
	 */
	[SkipVersion("<7.13.0", "Implemented in version 7.13.0")]
	public class CombinedFieldsWithBoostUsageTests : QueryDslUsageTestsBase
	{
		public CombinedFieldsWithBoostUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override QueryContainer QueryInitializer => new CombinedFieldsQuery
		{
			Fields = Field<Project>(p => p.Description, 2.2).And("myOtherField^1.2"),
			Query = "hello world",
		};

		protected override object QueryJson => new
		{
			combined_fields = new
			{
				query = "hello world",
				fields = new[]
				{
					"description^2.2",
					"myOtherField^1.2"
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.CombinedFields(c => c
				.Fields(Field<Project>(p => p.Description, 2.2).And("myOtherField^1.2"))
				.Query("hello world")
			);
	}
}
