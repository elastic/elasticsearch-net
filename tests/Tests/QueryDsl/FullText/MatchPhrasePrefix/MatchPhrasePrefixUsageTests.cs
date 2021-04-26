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

namespace Tests.QueryDsl.FullText.MatchPhrasePrefix
{
	public class MatchPhrasePrefixUsageTests : QueryDslUsageTestsBase
	{
		public MatchPhrasePrefixUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IMatchPhrasePrefixQuery>(a => a.MatchPhrasePrefix)
		{
			q => q.Query = null,
			q => q.Query = string.Empty,
			q => q.Field = null
		};

		protected override QueryContainer QueryInitializer => new MatchPhrasePrefixQuery
		{
			Field = Field<Project>(p => p.Description),
			Analyzer = "standard",
			Boost = 1.1,
			Name = "named_query",
			Query = "hello worl",
			MaxExpansions = 2,
			Slop = 2
		};

		protected override object QueryJson => new
		{
			match_phrase_prefix = new
			{
				description = new
				{
					_name = "named_query",
					boost = 1.1,
					query = "hello worl",
					analyzer = "standard",
					max_expansions = 2,
					slop = 2
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.MatchPhrasePrefix(c => c
				.Field(p => p.Description)
				.Analyzer("standard")
				.Boost(1.1)
				.Query("hello worl")
				.MaxExpansions(2)
				.Slop(2)
				.Name("named_query")
			);

		//hide
		[U] public void DeserializeShortForm()
		{
			using var stream = new MemoryStream(ShortFormQuery);
			var query = Client.RequestResponseSerializer.Deserialize<IMatchPhrasePrefixQuery>(stream);
			query.Should().NotBeNull();
			query.Field.Should().Be(new Field("description"));
			query.Query.Should().Be("project description");
		}
	}
}
