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

using System.IO;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.QueryDsl.FullText.Match
{
	public class MatchUsageTests : QueryDslUsageTestsBase
	{
		public MatchUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IMatchQuery>(a => a.Match)
		{
			q => q.Query = null,
			q => q.Query = string.Empty,
			q => q.Field = null
		};

		protected override QueryContainer QueryInitializer => new MatchQuery
		{
			Field = Field<Project>(p => p.Description),
			Analyzer = "standard",
			Boost = 1.1,
			Name = "named_query",
			Query = "hello world",
			Fuzziness = Fuzziness.AutoLength(3, 6),
			FuzzyTranspositions = true,
			MinimumShouldMatch = 2,
			FuzzyRewrite = MultiTermQueryRewrite.TopTermsBlendedFreqs(10),
			Lenient = true,
			Operator = Operator.Or,
			AutoGenerateSynonymsPhraseQuery = false
		};

		protected override object QueryJson => new
		{
			match = new
			{
				description = new
				{
					_name = "named_query",
					boost = 1.1,
					query = "hello world",
					analyzer = "standard",
					fuzzy_rewrite = "top_terms_blended_freqs_10",
					fuzziness = "AUTO:3,6",
					fuzzy_transpositions = true,
					lenient = true,
					minimum_should_match = 2,
					@operator = "or",
					auto_generate_synonyms_phrase_query = false
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Match(c => c
				.Field(p => p.Description)
				.Analyzer("standard")
				.Boost(1.1)
				.Query("hello world")
				.Fuzziness(Fuzziness.AutoLength(3, 6))
				.Lenient()
				.FuzzyTranspositions()
				.MinimumShouldMatch(2)
				.Operator(Operator.Or)
				.FuzzyRewrite(MultiTermQueryRewrite.TopTermsBlendedFreqs(10))
				.Name("named_query")
				.AutoGenerateSynonymsPhraseQuery(false)
			);

		//hide
		[U] public void DeserializeShortForm()
		{
			using var stream = new MemoryStream(ShortFormQuery);
			var query = Client.RequestResponseSerializer.Deserialize<IMatchQuery>(stream);
			query.Should().NotBeNull();
			query.Field.Should().Be(new Field("description"));
			query.Query.Should().Be("project description");
		}
	}
}
