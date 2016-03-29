using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.QueryDsl.BoolDsl.Operators
{
	public class NotOperatorUsageTests : OperatorUsageBase
	{
		[U] public void Not()
		{
			ReturnsBool(!Query && !Query, q => !q.Query() && !q.Query(), b =>
			{
				b.MustNot.Should().NotBeEmpty().And.HaveCount(2);
				b.Must.Should().BeNull();
				b.Should.Should().BeNull();
				b.Filter.Should().BeNull();
			});

			ReturnsBool(!Query || !Query || !ConditionlessQuery, q => !q.Query() || !q.Query() || !q.ConditionlessQuery(), b =>
			{
				b.Should.Should().NotBeEmpty().And.HaveCount(2);
				b.Must.Should().BeNull();
				b.MustNot.Should().BeNull();
				b.Filter.Should().BeNull();
				foreach (IQueryContainer q in b.Should)
				{
					q.Bool.Should().NotBeNull();
					q.Bool.MustNot.Should().NotBeEmpty().And.HaveCount(1);
				}
			});

			ReturnsSingleQuery(!Query || !ConditionlessQuery, q => !q.Query() || !q.ConditionlessQuery(),
				c => c.Bool.MustNot.Should().NotBeNull().And.HaveCount(1));

			ReturnsSingleQuery(!ConditionlessQuery || !Query, q => !q.ConditionlessQuery() || !q.Query(),
				c => c.Bool.MustNot.Should().NotBeNull().And.HaveCount(1));

			ReturnsSingleQuery(!Query || !NullQuery, q => !q.Query() || !q.NullQuery(),
				c => c.Bool.MustNot.Should().NotBeNull().And.HaveCount(1));

			ReturnsSingleQuery(!NullQuery && !Query, q => !q.NullQuery() && !q.Query(),
				c => c.Bool.MustNot.Should().NotBeNull().And.HaveCount(1));

			ReturnsSingleQuery(!ConditionlessQuery || !ConditionlessQuery && !ConditionlessQuery || !Query,
				q => !q.ConditionlessQuery() || !q.ConditionlessQuery() && !q.ConditionlessQuery() || !q.Query(),
				c => c.Bool.MustNot.Should().NotBeNull().And.HaveCount(1));

			ReturnsSingleQuery(
				!NullQuery || !NullQuery || !ConditionlessQuery || !Query,
				q => !q.NullQuery() || !q.NullQuery() || !q.ConditionlessQuery() || !q.Query(),
				c => c.Bool.MustNot.Should().NotBeNull());

			ReturnsNull(!NullQuery || !ConditionlessQuery, q => !q.NullQuery() || !q.ConditionlessQuery());
			ReturnsNull(!ConditionlessQuery && !NullQuery, q => !q.ConditionlessQuery() && !q.NullQuery());
			ReturnsNull(!ConditionlessQuery || !ConditionlessQuery, q => !q.ConditionlessQuery() || !q.ConditionlessQuery());
			ReturnsNull(
				!ConditionlessQuery || !ConditionlessQuery || !ConditionlessQuery || !ConditionlessQuery,
				q => !q.ConditionlessQuery() || !q.ConditionlessQuery() || !q.ConditionlessQuery() || !q.ConditionlessQuery()

			);
			ReturnsNull(
				!NullQuery || !ConditionlessQuery || !ConditionlessQuery || !ConditionlessQuery,
				q => !q.NullQuery() || !q.ConditionlessQuery() || !q.ConditionlessQuery() || !q.ConditionlessQuery()
			);

		}
		[U]
		public void CombiningManyUsingAggregate()
		{
			var lotsOfNots = Enumerable.Range(0, 100).Aggregate(new QueryContainer(), (q, c) => q || Query, q => q);
			LotsOfNots(lotsOfNots);
		}

		[U]
		public void CombiningManyUsingForeachInitializingWithNull()
		{
			QueryContainer container = null;
			foreach (var i in Enumerable.Range(0, 100))
				container |= Query;
			LotsOfNots(container);
		}

		[U]
		public void CombiningManyUsingForeachInitializingWithDefault()
		{
			var container = new QueryContainer();
			foreach (var i in Enumerable.Range(0, 100))
				container |= Query;
			LotsOfNots(container);
		}

		private void LotsOfNots(IQueryContainer lotsOfNots)
		{
			lotsOfNots.Should().NotBeNull();
			lotsOfNots.Bool.Should().NotBeNull();
			lotsOfNots.Bool.Should.Should().NotBeEmpty().And.HaveCount(100);
		}

	}
}
