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

namespace Tests.QueryDsl.TermLevel.Prefix
{
	public class PrefixQueryUsageTests : QueryDslUsageTestsBase
	{
		public PrefixQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IPrefixQuery>(a => a.Prefix)
		{
			q => q.Field = null,
			q => q.Value = null,
			q => q.Value = string.Empty
		};

		protected override QueryContainer QueryInitializer => new PrefixQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			Value = "proj",
			Rewrite = MultiTermQueryRewrite.TopTerms(10)
		};

		protected override object QueryJson => new
		{
			prefix = new
			{
				description = new
				{
					_name = "named_query",
					boost = 1.1,
					rewrite = "top_terms_10",
					value = "proj"
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Prefix(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.Value("proj")
				.Rewrite(MultiTermQueryRewrite.TopTerms(10))
			);

		//hide
		[U] public void DeserializeShortForm()
		{
			using var stream = new MemoryStream(ShortFormQuery);
			var query = Client.RequestResponseSerializer.Deserialize<IPrefixQuery>(stream);
			query.Should().NotBeNull();
			query.Field.Should().Be(new Field("description"));
			query.Value.Should().Be("project description");
		}
	}
}
