// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Linq;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace Tests.QueryDsl.BoolDsl;

public class NotOperatorUsageTests : OperatorUsageBase
{
	private static readonly int Iterations = 10000;

	[U]
	public void ReturnsBoolWithMustNot_CombiningTwoQueries() =>
		ReturnsBool(!TermQuery && !TermQuery, b =>
		{
			b.MustNot.Should().NotBeEmpty().And.HaveCount(2);
			b.Must.Should().BeNull();
			b.Should.Should().BeNull();
			b.Filter.Should().BeNull();
		});

	[U]
	public void ReturnsBoolWithShould_CombiningTwoQueries() =>
		ReturnsBool(!TermQuery || !TermQuery, b =>
		{
			b.Should.Should().NotBeEmpty().And.HaveCount(2);
			b.Must.Should().BeNull();
			b.MustNot.Should().BeNull();
			b.Filter.Should().BeNull();

			foreach (var query in b.Should)
			{
				query.TryGet<BoolQuery>(out var boolQuery).Should().BeTrue();
				boolQuery.MustNot.Should().NotBeEmpty().And.HaveCount(1);
			}
		});

	[U]
	public void ReturnsBoolQuery_WhenOtherQueryIsNull()
	{
		ReturnsSingleQuery<BoolQuery>(!TermQuery || !NullQuery,
			c => c.MustNot.Should().NotBeNull().And.HaveCount(1));

		ReturnsSingleQuery<BoolQuery>(!NullQuery || !TermQuery,
			c => c.MustNot.Should().NotBeNull().And.HaveCount(1));
	}

	[U]
	public void ReturnsBoolQuery_WhenOtherQueriesAreNull()
	{
		ReturnsSingleQuery<BoolQuery>(!TermQuery || !NullQuery || !NullQuery,
			c => c.MustNot.Should().NotBeNull().And.HaveCount(1));

		ReturnsSingleQuery<BoolQuery>(!NullQuery || !TermQuery || !NullQuery || !NullQuery,
			c => c.MustNot.Should().NotBeNull().And.HaveCount(1));
	}

	[U]
	public void ReturnsNull_WhenBothQueriesAreNull() => ReturnsNull(!NullQuery && !NullQuery);

	[U]
	public void CombiningManyUsingAggregate()
	{
		var lotsOfNots = Enumerable.Range(0, Iterations).Aggregate(!NullQuery, (q, c) => q && !TermQuery, q => q);
		AssertLotsOfNots(lotsOfNots);
	}

	[U]
	public void CombiningManyUsingForEachInitializingWithNull()
	{
		Query container = null;

		foreach (var i in Enumerable.Range(0, Iterations))
			container &= !TermQuery;

		AssertLotsOfNots(container);
	}

	private static void AssertLotsOfNots(Query lotsOfNots)
	{
		lotsOfNots.Should().NotBeNull();
		lotsOfNots.TryGet<BoolQuery>(out var boolQuery).Should().BeTrue();
		boolQuery.MustNot.Should().NotBeEmpty().And.HaveCount(Iterations);
	}
}
