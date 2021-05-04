// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

#pragma warning disable 618 //Testing an obsolete method

namespace Tests.QueryDsl.Compound.Dismax
{
	public class DismaxQueryUsageTests : QueryDslUsageTestsBase
	{
		public DismaxQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IDisMaxQuery>(a => a.DisMax)
		{
			q => q.Queries = null,
			q => q.Queries = Enumerable.Empty<QueryContainer>(),
			q => q.Queries = new[] { ConditionlessQuery },
		};

		protected override NotConditionlessWhen NotConditionlessWhen => new NotConditionlessWhen<IDisMaxQuery>(a => a.DisMax)
		{
			q => q.Queries = new[] { VerbatimQuery },
			q => q.Queries = new[] { VerbatimQuery, ConditionlessQuery },
		};

		protected override QueryContainer QueryInitializer => new DisMaxQuery()
		{
			Name = "named_query",
			Boost = 1.1,
			TieBreaker = 0.11,
			Queries = new QueryContainer[]
			{
				new MatchAllQuery() { Name = "query1" },
				new MatchAllQuery() { Name = "query2" },
			}
		};

		protected override object QueryJson => new
		{
			dis_max = new
			{
				_name = "named_query",
				boost = 1.1,
				queries = new[]
				{
					new { match_all = new { _name = "query1" } },
					new { match_all = new { _name = "query2" } }
				},
				tie_breaker = 0.11
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.DisMax(c => c
				.Name("named_query")
				.Boost(1.1)
				.TieBreaker(0.11)
				.Queries(
					qq => qq.MatchAll(m => m.Name("query1")),
					qq => qq.MatchAll(m => m.Name("query2"))
				)
			);

		[U]
		public void NullQueryDoesNotCauseANullReferenceException()
		{
			Action query = () => Client.Search<Project>(s => s
				.Query(q => q
					.DisMax(dm => dm
						.Queries(
							dmq => dmq.Term(t => t.Name, null)
						)
					)
				)
			);

			query.Should().NotThrow();
		}
	}
}
