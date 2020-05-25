// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.IO;
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
