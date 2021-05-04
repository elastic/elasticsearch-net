// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

#pragma warning disable 618 //Testing an obsolete method

namespace Tests.QueryDsl.Specialized.Pinned
{
	/**
	 * Promotes selected documents to rank higher than those matching a given query. This feature is typically used to
	 * guide searchers to curated documents that are promoted over and above any "organic" matches for a search. The promoted or "pinned"
	 * documents are identified using the document IDs stored in the _id field.
	 * See the Elasticsearch documentation on {ref_current}/query-dsl-pinned-query.html[pinned query] for more details.
	*/
	public class PinnedQueryUsageTests : QueryDslUsageTestsBase
	{
		public PinnedQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IPinnedQuery>(a => a.Pinned)
		{
			q =>
			{
				q.Ids = null;
				q.Organic = null;
			},
			q =>
			{
				q.Ids = Array.Empty<Id>();
				q.Organic = ConditionlessQuery;
			},
		};

		protected override NotConditionlessWhen NotConditionlessWhen => new NotConditionlessWhen<IPinnedQuery>(a => a.Pinned)
		{
			q => q.Organic = VerbatimQuery,
		};

		protected override QueryContainer QueryInitializer => new PinnedQuery()
		{
			Name = "named_query",
			Boost = 1.1,
			Organic = new MatchAllQuery { Name = "organic_query" },
			Ids = new Id[] { 1,11,22 },
		};

		protected override object QueryJson => new
		{
			pinned = new
			{
				_name = "named_query",
				boost = 1.1,
				organic = new
				{
					match_all = new { _name = "organic_query" }
				},
				ids = new [] { 1, 11, 22},
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Pinned(c => c
				.Name("named_query")
				.Boost(1.1)
				.Organic(qq => qq.MatchAll(m => m.Name("organic_query")))
				.Ids(1, 11, 22)
			);
	}
}
