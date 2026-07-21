// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Tests.Domain;

namespace Tests.QueryDsl.BoolDsl;

public class OrOperatorUsageTests : OperatorUsageBase
{
	private static readonly int Iterations = 10000;

	[U]
	public void ReturnsBoolWithShould_CombiningTwoQueries() => ReturnsBool(TermQuery || TermQuery, b =>
		{
			b.Should.Should().NotBeEmpty().And.HaveCount(2);
			b.Must.Should().BeNull();
			b.MustNot.Should().BeNull();
			b.Filter.Should().BeNull();
		});

	[U]
	public void ReturnsOriginalTermQuery_WhenOtherQueryIsNull()
	{
		ReturnsSingleQuery<TermQuery>(TermQuery || NullQuery,
			c => c.Value.Should().NotBeNull());

		ReturnsSingleQuery<TermQuery>(NullQuery || TermQuery,
			c => c.Value.Should().NotBeNull());
	}

	[U]
	public void ReturnsOriginalTermQuery_WhenOtherQueriesAreNull()
	{
		ReturnsSingleQuery<TermQuery>(TermQuery || NullQuery || NullQuery,
			c => c.Value.Should().NotBeNull());

		ReturnsSingleQuery<TermQuery>(NullQuery || TermQuery || NullQuery || NullQuery,
			c => c.Value.Should().NotBeNull());
	}

	[U]
	public void ReturnsNull_WhenBothQueriesAreNull() => ReturnsNull(NullQuery || NullQuery);

	[U]
	public void CombiningManyUsingAggregate()
	{
		var lotsOfOrs = Enumerable.Range(0, Iterations).Aggregate((SearchQuery)NullQuery, (q, c) => q || TermQuery, q => q);
		AssertLotsOfOrs(lotsOfOrs);
	}

	[U]
	public void CombiningManyUsingForEachInitializingWithNull()
	{
		Query container = null;

		foreach (var i in Enumerable.Range(0, Iterations))
			container |= TermQuery;

		AssertLotsOfOrs(container);
	}

	[U]
	public void CombiningTwoBools()
	{
		var queries = new Query[] { TermQuery };

		ReturnsBool(
			new BoolQuery { Must = queries, Should = queries }
			|| new BoolQuery { MustNot = queries, Should = queries }
			, b =>
			{
				b.Should.Should().NotBeEmpty().And.HaveCount(2);

				var first = b.Should.First();
				var last = b.Should.Last();

				first.TryGet<BoolQuery>(out var firstBool).Should().BeTrue();
				last.TryGet<BoolQuery>(out var lastBool).Should().BeTrue();

				firstBool.Should.Should().NotBeEmpty().And.HaveCount(1);
				firstBool.Must.Should().NotBeEmpty().And.HaveCount(1);

				lastBool.Should.Should().NotBeEmpty().And.HaveCount(1);
				lastBool.MustNot.Should().NotBeEmpty().And.HaveCount(1);
			});
	}

	[U]
	public void OrIntoBoolWithMustAndShould()
	{
		var queries = new Query[] { TermQuery };

		CombineBothWays(
			new BoolQuery { Must = queries, Should = queries }, TermQuery
			, l => l.TryGet<BoolQuery>(out _).Should().BeTrue()
			, r => r.TryGet<TermQuery>(out _).Should().BeTrue()
			, b => b.Should.Should().NotBeEmpty().And.HaveCount(2)
		);
	}

	[U]
	public void OrIntoBoolWithMustAndMustNot()
	{
		var queries = new Query[] { TermQuery };
		CombineBothWays(
			new BoolQuery { Must = queries, MustNot = queries }, TermQuery
			, l => l.TryGet<BoolQuery>(out _).Should().BeTrue()
			, r => r.TryGet<TermQuery>(out _).Should().BeTrue()
			, b => { b.Should.Should().NotBeEmpty().And.HaveCount(2); }
		);
	}

	[U]
	public void OrIntoBoolWithMust()
	{
		var queries = new Query[] { TermQuery };

		CombineBothWays(
			new BoolQuery { Must = queries }, TermQuery
			, l => l.TryGet<BoolQuery>(out _).Should().BeTrue()
			, r => r.TryGet<TermQuery>(out _).Should().BeTrue()
			, b => { b.Should.Should().NotBeEmpty().And.HaveCount(2); }
		);
	}

	[U]
	public void OrIntoBoolWithShould()
	{
		var queries = new Query[] { TermQuery };

		CombineBothWays(
			new BoolQuery { Should = queries }, TermQuery
			, l => l.TryGet<TermQuery>(out _).Should().BeTrue()
			, r => r.TryGet<TermQuery>(out _).Should().BeTrue()
			, b => b.Should.Should().NotBeEmpty().And.HaveCount(2)
		);
	}

	[U]
	public void OrIntoNamedBool()
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
			, b => b.Should.Should().NotBeEmpty().And.HaveCount(2)
		);
	}

	[U]
	public void OrAssigningManyBoolMustQueries()
	{
		var query = Query.Bool(new BoolQuery
		{
			Must = new Query[] { new TermQuery(Infer.Field<Project>(f => f.Name)) { Value = "foo" } }
		});

		var container = OrAssignManyBoolQueries(query);
		container.TryGet<BoolQuery>(out var boolQuery).Should().BeTrue();
		boolQuery.Should.Should().NotBeNullOrEmpty().And.HaveCount(Iterations);
		boolQuery.Should.Should().OnlyContain(s => s.VariantName == "bool" && (BoolQuery)s.Variant != null && ((BoolQuery)s.Variant).Must != null);
	}

	[U]
	public void OrAssigningManyBoolMustNotQueries()
	{
		var query = Query.Bool(new BoolQuery
		{
			MustNot = new Query[] { new TermQuery(Infer.Field<Project>(f => f.Name)) { Value = "foo" } }
		});

		var container = OrAssignManyBoolQueries(query);
		container.TryGet<BoolQuery>(out var boolQuery).Should().BeTrue();
		boolQuery.Should.Should().NotBeNullOrEmpty().And.HaveCount(Iterations);
		boolQuery.Should.Should().OnlyContain(s => s.VariantName == "bool" && (BoolQuery)s.Variant != null && ((BoolQuery)s.Variant).MustNot != null);
	}

	[U]
	public void OrAssigningBoolShouldQueries()
	{
		// |= assigning many bool queries with only should clauses flattens even further to a single bool with only term queries in the should

		var query = Query.Bool(new BoolQuery
		{
			Should = new Query[] { new TermQuery(Infer.Field<Project>(f => f.Name)) { Value = "foo" } }
		});

		var container = OrAssignManyBoolQueries(query);
		container.TryGet<BoolQuery>(out var boolQuery).Should().BeTrue();
		boolQuery.Should.Should().NotBeNullOrEmpty().And.HaveCount(Iterations);
		boolQuery.Should.Should().OnlyContain(s => s.VariantName == "term" && (TermQuery)s.Variant != null);
	}

	[U]
	public void OrAssigningBoolShouldQueriesWithMustClauses()
	{
		var query = Query.Bool(new BoolQuery
		{
			Should = new Query[] { new TermQuery(Infer.Field<Project>(f => f.Name)) { Value = "foo" } },
			Must = new Query[] { new TermQuery(Infer.Field<Project>(f => f.Name)) { Value = "foo" } }
		});

		var container = OrAssignManyBoolQueries(query);
		container.TryGet<BoolQuery>(out var boolQuery).Should().BeTrue();
		boolQuery.Should.Should().NotBeNullOrEmpty().And.HaveCount(Iterations);
		boolQuery.Should.Should().OnlyContain(s => s.VariantName == "bool" &&
			(BoolQuery)s.Variant != null &&
			((BoolQuery)s.Variant).Must != null &&
			((BoolQuery)s.Variant).Should != null);
	}

	[U]
	public void OrAssigningBoolShouldQueriesWithMustNotClauses()
	{
		var query = Query.Bool(new BoolQuery
		{
			Should = new Query[] { new TermQuery(Infer.Field<Project>(f => f.Name)) { Value = "foo" } },
			MustNot = new Query[] { new TermQuery(Infer.Field<Project>(f => f.Name)) { Value = "foo" } }
		});

		var container = OrAssignManyBoolQueries(query);
		container.TryGet<BoolQuery>(out var boolQuery).Should().BeTrue();
		boolQuery.Should.Should().NotBeNullOrEmpty().And.HaveCount(Iterations);
		boolQuery.Should.Should().OnlyContain(s => s.VariantName == "bool" &&
			(BoolQuery)s.Variant != null &&
			((BoolQuery)s.Variant).MustNot != null &&
			((BoolQuery)s.Variant).Should != null);
	}

	[U]
	public void OrAssigningBoolMustQueriesWithMustNotClauses()
	{
		var query = Query.Bool(new BoolQuery
		{
			Must = new Query[] { new TermQuery(Infer.Field<Project>(f => f.Name)) { Value = "foo" } },
			MustNot = new Query[] { new TermQuery(Infer.Field<Project>(f => f.Name)) { Value = "foo" } }
		});

		var container = OrAssignManyBoolQueries(query);
		container.TryGet<BoolQuery>(out var boolQuery).Should().BeTrue();
		boolQuery.Should.Should().NotBeNullOrEmpty().And.HaveCount(Iterations);
		boolQuery.Should.Should().OnlyContain(s => s.VariantName == "bool" &&
			(BoolQuery)s.Variant != null &&
			((BoolQuery)s.Variant).MustNot != null &&
			((BoolQuery)s.Variant).Must != null);
	}

	[U]
	public void OrAssigningNamedBoolShouldQueries()
	{
		var query = Query.Bool(new BoolQuery
		{
			Should = new Query[] { new TermQuery(Infer.Field<Project>(f => f.Name)) { Value = "foo" } },
			QueryName = "name"
		});

		var container = OrAssignManyBoolQueries(query);
		container.TryGet<BoolQuery>(out var boolQuery).Should().BeTrue();
		boolQuery.Should.Should().NotBeNullOrEmpty().And.HaveCount(Iterations);
		boolQuery.Should.Should().OnlyContain(s => s.VariantName == "bool" &&
			(BoolQuery)s.Variant != null &&
			((BoolQuery)s.Variant).Should != null &&
			((BoolQuery)s.Variant).QueryName == "name");
	}

	private static Query OrAssignManyBoolQueries(Query query)
	{
		Query container = null;

		Action act = () =>
		{
			for (var i = 0; i < Iterations; i++)
				container |= query;
		};

		act.Should().NotThrow();
		AssertLotsOfOrs(container);

		return container;
	}

	private static void AssertLotsOfOrs(Query lotsOfOrs)
	{
		lotsOfOrs.Should().NotBeNull();
		lotsOfOrs.TryGet<BoolQuery>(out var boolQuery).Should().BeTrue();
		boolQuery.Should.Should().NotBeEmpty().And.HaveCount(Iterations);
	}

	protected static void CombineBothWays(
			Query ois1,
			Query ois2,
			Action<Query> assertLeft,
			Action<Query> assertRight,
			Action<BoolQuery> assertContainer = null
		)
	{
		var oisLeft = ois1 || ois2;

		ReturnsBool(oisLeft, b =>
		{
			var left = b.Should.First();
			var right = b.Should.Last();
			assertLeft(left);
			assertRight(right);
			assertContainer?.Invoke(b);
		});

		var oisRight = ois2 || ois1;

		ReturnsBool(oisRight, b =>
		{
			var left = b.Should.First();
			var right = b.Should.Last();
			assertRight(left);
			assertLeft(right);
			assertContainer?.Invoke(b);
		});
	}
}
