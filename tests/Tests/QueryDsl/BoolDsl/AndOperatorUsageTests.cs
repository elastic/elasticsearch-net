// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Tests.Domain;
using Xunit;

namespace Tests.QueryDsl.BoolDsl;

public class AndOperatorUsageTests : OperatorUsageBase
{
	private static readonly int Iterations = 10000;

	[U]
	public void AndShouldNotBeViralAnyWayYouComposeIt()
	{
		Query andQuery = new TermQuery("a") { Value = "a" };
		andQuery &= new TermQuery("b") { Value = "b" };

		var original = andQuery;

		original.TryGet<BoolQuery>(out var originalBoolQuery).Should().BeTrue();
		originalBoolQuery.Must.Should().HaveCount(2);

		var result = andQuery && new TermQuery("c") { Value = "c" };

		result.TryGet<BoolQuery>(out var resultBoolQuery).Should().BeTrue();
		resultBoolQuery.Must.Should().HaveCount(3);

		original.TryGet(out originalBoolQuery).Should().BeTrue();
		originalBoolQuery.Must.Should().HaveCount(2);
	}

	[U]
	public void ReturnsBoolWithMust_CombiningTwoQueries() =>
		ReturnsBool(TermQuery && TermQuery, b =>
		{
			b.Must.Should().NotBeEmpty().And.HaveCount(2);
			b.Should.Should().BeNull();
			b.MustNot.Should().BeNull();
			b.Filter.Should().BeNull();
		});

	[U]
	public void ReturnsOriginalTermQuery_WhenOtherQueryIsNull()
	{
		ReturnsSingleQuery<TermQuery>(TermQuery && NullQuery,
			c => c.Value.Should().NotBeNull());

		ReturnsSingleQuery<TermQuery>(NullQuery && TermQuery,
			c => c.Value.Should().NotBeNull());
	}

	[U]
	public void ReturnsOriginalTermQuery_WhenOtherQueriesAreNull()
	{
		ReturnsSingleQuery<TermQuery>(TermQuery && NullQuery && NullQuery,
			c => c.Value.Should().NotBeNull());

		ReturnsSingleQuery<TermQuery>(NullQuery && TermQuery && NullQuery && NullQuery,
			c => c.Value.Should().NotBeNull());
	}

	[U]
	public void ReturnsNull_WhenBothQueriesAreNull() => ReturnsNull(NullQuery && NullQuery);

	[U]
	public void CombiningManyUsingAggregate()
	{
		var lotsOfOrs = Enumerable.Range(0, Iterations).Aggregate((SearchQuery)NullQuery, (q, c) => q && TermQuery, q => q);
		AssertLotsOfAnds(lotsOfOrs);
	}

	[U]
	public void CombiningManyUsingForEachInitializingWithNull()
	{
		Query container = null;

		foreach (var i in Enumerable.Range(0, Iterations))
			container &= TermQuery;

		AssertLotsOfAnds(container);
	}

	[U]
	public void CombiningTwoBools()
	{
		var queries = new Query[] { TermQuery };

		ReturnsBool(
			new BoolQuery { Must = queries, Should = queries }
			&& new BoolQuery { MustNot = queries, Should = queries }
			, b =>
			{
				b.Must.Should().NotBeEmpty().And.HaveCount(2);

				var first = b.Must.First();
				var last = b.Must.Last();

				first.TryGet<BoolQuery>(out var firstBool).Should().BeTrue();
				last.TryGet<BoolQuery>(out var lastBool).Should().BeTrue();

				firstBool.Should.Should().NotBeEmpty().And.HaveCount(1);
				firstBool.Must.Should().NotBeEmpty().And.HaveCount(1);

				lastBool.Should.Should().NotBeEmpty().And.HaveCount(1);
				lastBool.MustNot.Should().NotBeEmpty().And.HaveCount(1);
			});
	}

	[U]
	public void AndIntoBoolWithMustAndShould()
	{
		var queries = new Query[] { TermQuery };

		CombineBothWays(
			new BoolQuery { Must = queries, Should = queries }, TermQuery
			, l => l.TryGet<BoolQuery>(out _).Should().BeTrue()
			, r => r.TryGet<TermQuery>(out _).Should().BeTrue()
			, b => b.Must.Should().NotBeEmpty().And.HaveCount(2)
		);
	}

	[U]
	public void AndIntoBoolWithMustAndMustNot()
	{
		var queries = new Query[] { TermQuery };
		CombineBothWays(
			new BoolQuery { Must = queries, MustNot = queries }, TermQuery
			, l => l.TryGet<TermQuery>(out _).Should().BeTrue()
			, r => r.TryGet<TermQuery>(out _).Should().BeTrue()
			, b =>
			{
				b.Must.Should().NotBeEmpty().And.HaveCount(2);
				b.MustNot.Should().NotBeEmpty().And.HaveCount(1);
			}
		);
	}

	[U]
	public void AndIntoBoolWithMust()
	{
		var queries = new Query[] { TermQuery };

		CombineBothWays(
			new BoolQuery { Must = queries }, TermQuery
			, l => l.TryGet<TermQuery>(out _).Should().BeTrue()
			, r => r.TryGet<TermQuery>(out _).Should().BeTrue()
			, b => { b.Must.Should().NotBeEmpty().And.HaveCount(2); }
		);
	}

	[U]
	public void AndIntoBoolWithShould()
	{
		var queries = new Query[] { TermQuery };

		CombineBothWays(
			new BoolQuery { Should = queries }, TermQuery
			, l =>
			{
				l.TryGet<BoolQuery>(out var boolQuery).Should().BeTrue();
				boolQuery.Should.Should().NotBeNullOrEmpty();
			}, r => r.TryGet<TermQuery>(out _).Should().BeTrue()
			, b => b.Must.Should().NotBeEmpty().And.HaveCount(2)
		);
	}

	[U]
	public void AndIntoNamedBool()
	{
		var queries = new Query[] { TermQuery };

		CombineBothWays(
			new BoolQuery { Should = queries, QueryName = "name" }, TermQuery
			, l =>
			{
				l.TryGet<BoolQuery>(out var boolQuery).Should().BeTrue();
				boolQuery.Should.Should().NotBeNullOrEmpty();
				boolQuery.QueryName.Should().Be("name");
			}
			, r => r.TryGet<TermQuery>(out _).Should().BeTrue()
			, b => b.Must.Should().NotBeEmpty().And.HaveCount(2)
		);
	}

	[U]
	public void AndAssigningManyBoolShouldQueries()
	{
		var query = Query.Bool(new BoolQuery
		{
			Should = new Query[] { new TermQuery(Infer.Field<Project>(f => f.Name)) { Value = "foo" } }
		});

		var container = AndAssignManyBoolQueries(query);
		container.TryGet<BoolQuery>(out var boolQuery).Should().BeTrue();
		boolQuery.Must.Should().OnlyContain(s => s.VariantName == "bool" && (BoolQuery)s.Variant != null && ((BoolQuery)s.Variant).Should != null);
	}

	[U]
	public void AndAssigningManyBoolMustNotQueries()
	{
		var query = Query.Bool(new BoolQuery
		{
			MustNot = new Query[] { new TermQuery(Infer.Field<Project>(f => f.Name)) { Value = "foo" } }
		});

		var container = AndAssignManyBoolQueries(query);
		container.TryGet<BoolQuery>(out var boolQuery).Should().BeTrue();
		boolQuery.MustNot.Should().NotBeNullOrEmpty().And.HaveCount(Iterations);
		boolQuery.MustNot.Should().OnlyContain(s => s.VariantName == "term" && (TermQuery)s.Variant != null);
	}

	[U]
	public void AndAssigningBoolMustQueries()
	{
		var query = Query.Bool(new BoolQuery
		{
			Must = new Query[] { new TermQuery(Infer.Field<Project>(f => f.Name)) { Value = "foo" } }
		});

		var container = AndAssignManyBoolQueries(query);
		container.TryGet<BoolQuery>(out var boolQuery).Should().BeTrue();
		boolQuery.Must.Should().NotBeNullOrEmpty().And.HaveCount(Iterations);
		boolQuery.Must.Should().OnlyContain(s => s.VariantName == "term" && (TermQuery)s.Variant != null);
	}

	[U]
	public void AndAssigningBoolShouldQueriesWithMustClauses()
	{
		var query = Query.Bool(new BoolQuery
		{
			Should = new Query[] { new TermQuery(Infer.Field<Project>(f => f.Name)) { Value = "foo" } },
			Must = new Query[] { new TermQuery(Infer.Field<Project>(f => f.Name)) { Value = "foo" } }
		});

		var container = AndAssignManyBoolQueries(query);
		container.TryGet<BoolQuery>(out var boolQuery).Should().BeTrue();
		boolQuery.Must.Should().NotBeNullOrEmpty().And.HaveCount(Iterations);
		boolQuery.Must.Should().OnlyContain(s => s.VariantName == "bool" &&
			(BoolQuery)s.Variant != null &&
			((BoolQuery)s.Variant).Must != null &&
			((BoolQuery)s.Variant).Should != null);
	}

	[U]
	public void AndAssigningBoolShouldQueriesWithMustNotClauses()
	{
		var query = Query.Bool(new BoolQuery
		{
			Should = new Query[] { new TermQuery(Infer.Field<Project>(f => f.Name)) { Value = "foo" } },
			MustNot = new Query[] { new TermQuery(Infer.Field<Project>(f => f.Name)) { Value = "foo" } }
		});

		var container = AndAssignManyBoolQueries(query);
		container.TryGet<BoolQuery>(out var boolQuery).Should().BeTrue();
		boolQuery.Must.Should().NotBeNullOrEmpty().And.HaveCount(Iterations);
		boolQuery.Must.Should().OnlyContain(s => s.VariantName == "bool" &&
			(BoolQuery)s.Variant != null &&
			((BoolQuery)s.Variant).MustNot != null &&
			((BoolQuery)s.Variant).Should != null);
	}

	[U]
	public void AndAssigningBoolMustQueriesWithMustNotClauses()
	{
		var query = Query.Bool(new BoolQuery
		{
			Must = new Query[] { new TermQuery(Infer.Field<Project>(f => f.Name)) { Value = "foo" } },
			MustNot = new Query[] { new TermQuery(Infer.Field<Project>(f => f.Name)) { Value = "foo" } }
		});

		var container = AndAssignManyBoolQueries(query);
		container.TryGet<BoolQuery>(out var boolQuery).Should().BeTrue();
		boolQuery.Must.Should().NotBeNullOrEmpty().And.HaveCount(Iterations);
		boolQuery.Must.Should().OnlyContain(s => s.VariantName == "term" && (TermQuery)s.Variant != null);
	}

	[U]
	public void AndAssigningNamedBoolShouldQueries()
	{
		var query = Query.Bool(new BoolQuery
		{
			Should = new Query[] { new TermQuery(Infer.Field<Project>(f => f.Name)) { Value = "foo" } },
			QueryName = "name"
		});

		var container = AndAssignManyBoolQueries(query);
		container.TryGet<BoolQuery>(out var boolQuery).Should().BeTrue();
		boolQuery.Must.Should().NotBeNullOrEmpty().And.HaveCount(Iterations);
		boolQuery.Must.Should().OnlyContain(s => s.VariantName == "bool" &&
			(BoolQuery)s.Variant != null &&
			((BoolQuery)s.Variant).Should != null &&
			((BoolQuery)s.Variant).QueryName == "name");
	}

	[U]
	public void AndAssigningShouldNotModifyOriginalBoolQuery()
	{
		// Based on https://github.com/elastic/elasticsearch-net/issues/5076

		// create a bool query with a filter that might be reused by the consuming code and expected not to change.
		Query filterQueryContainerExpectedToNotChange = +new TermQuery("do-not") { Value = "change" };

		filterQueryContainerExpectedToNotChange.TryGet<BoolQuery>(out var boolQueryBefore).Should().BeTrue();

		boolQueryBefore.Must.Should().BeNull();
		boolQueryBefore.Filter.Single().TryGet<TermQuery>(out var termQueryBefore).Should().BeTrue();
		termQueryBefore.Field.Should().Be("do-not");
		termQueryBefore.Value.TryGetString(out var termValue).Should().BeTrue();
		termValue.Should().Be("change");
		
		var queryToExecute = new BoolQuery
		{
			Should = new Query[] { new MatchAllQuery() },
			Filter = new Query[] { new MatchAllQuery() } // if either of these are removed, it works as expected
		}
		&& filterQueryContainerExpectedToNotChange;

		filterQueryContainerExpectedToNotChange.TryGet<BoolQuery>(out var boolQueryAfter).Should().BeTrue();
		boolQueryAfter.Must.Should().BeNull();
		boolQueryAfter.Filter.Single().TryGet<TermQuery>(out var termQueryAfter).Should().BeTrue();
		termQueryAfter.Field.Should().Be("do-not");
		termQueryAfter.Value.TryGetString(out termValue).Should().BeTrue();
		termValue.Should().Be("change");

		queryToExecute.TryGet<BoolQuery>(out var boolToExecute).Should().BeTrue();
		boolToExecute.Filter.Should().HaveCount(1);
		boolToExecute.Must.Should().HaveCount(1);
		boolToExecute.Filter.Single().TryGet<TermQuery>(out var queryToExecuteTermQuery).Should().BeTrue();
		queryToExecuteTermQuery.Field.Should().Be("do-not");
		queryToExecuteTermQuery.Value.TryGetString(out termValue).Should().BeTrue();
		termValue.Should().Be("change");

		boolToExecute.Must.Single().TryGet<BoolQuery>(out var mustClauseBool).Should().BeTrue();
		mustClauseBool.Filter.Should().HaveCount(1);
		mustClauseBool.Should.Should().HaveCount(1);
	}

	private static void AssertLotsOfAnds(Query lotsOfAnds)
	{
		lotsOfAnds.Should().NotBeNull();
		lotsOfAnds.TryGet<BoolQuery>(out var boolQuery).Should().BeTrue();
		boolQuery.Must.Should().NotBeEmpty().And.HaveCount(Iterations);
	}

	protected static void CombineBothWays(
			Query ois1,
			Query ois2,
			Action<Query> assertLeft,
			Action<Query> assertRight,
			Action<BoolQuery> assertContainer = null
		)
	{
		var oisLeft = ois1 && ois2;

		ReturnsBool(oisLeft, b =>
		{
			var left = b.Must.First();
			var right = b.Must.Last();
			assertLeft(left);
			assertRight(right);
			assertContainer?.Invoke(b);
		});

		var oisRight = ois2 && ois1;

		ReturnsBool(oisRight, b =>
		{
			var left = b.Must.First();
			var right = b.Must.Last();
			assertRight(left);
			assertLeft(right);
			assertContainer?.Invoke(b);
		});
	}

	private static Query AndAssignManyBoolQueries(Query query)
	{
		Query container = null;

		Action act = () =>
		{
			for (var i = 0; i < Iterations; i++)
				container &= query;
		};

		act.Should().NotThrow();

		return container;
	}
}
