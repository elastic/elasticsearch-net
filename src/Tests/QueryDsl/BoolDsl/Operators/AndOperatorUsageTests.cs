using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.QueryDsl.BoolDsl.Operators
{
	public class AndOperatorUsageTests : OperatorUsageBase
	{
		[U] public void And()
		{
			ReturnsBool(Query && Query, q => q.Query() && q.Query(), b =>
			{
				b.Must.Should().NotBeEmpty().And.HaveCount(2);
				b.Should.Should().BeNull();
				b.MustNot.Should().BeNull();
				b.Filter.Should().BeNull();
			});

			ReturnsBool(Query && Query && ConditionlessQuery, q => q.Query() && q.Query() && q.ConditionlessQuery(), b =>
			{
				b.Must.Should().NotBeEmpty().And.HaveCount(2);
				b.Should.Should().BeNull();
				b.MustNot.Should().BeNull();
				b.Filter.Should().BeNull();
			});

			ReturnsSingleQuery(Query && ConditionlessQuery, q => q.Query() && q.ConditionlessQuery(),
				c => c.Term.Value.Should().NotBeNull());

			ReturnsSingleQuery(ConditionlessQuery && Query, q => q.ConditionlessQuery() && q.Query(),
				c => c.Term.Value.Should().NotBeNull());

			ReturnsSingleQuery(Query && NullQuery, q => q.Query() && q.NullQuery(),
				c => c.Term.Value.Should().NotBeNull());

			ReturnsSingleQuery(NullQuery && Query, q=> q.NullQuery() && q.Query(), 
				c => c.Term.Value.Should().NotBeNull());

			ReturnsSingleQuery(ConditionlessQuery && ConditionlessQuery && ConditionlessQuery && Query,
				q => q.ConditionlessQuery() && q.ConditionlessQuery() && q.ConditionlessQuery() && q.Query(),
				c => c.Term.Value.Should().NotBeNull());

			ReturnsSingleQuery(
				NullQuery && NullQuery && ConditionlessQuery && Query, 
				q=>q.NullQuery() && q.NullQuery() && q.ConditionlessQuery() && q.Query(),
				c => c.Term.Value.Should().NotBeNull());

			ReturnsNull(NullQuery && ConditionlessQuery, q=> q.NullQuery() && q.ConditionlessQuery());
			ReturnsNull(ConditionlessQuery && NullQuery, q=>q.ConditionlessQuery() && q.NullQuery());
			ReturnsNull(ConditionlessQuery && ConditionlessQuery, q=>q.ConditionlessQuery() && q.ConditionlessQuery());
			ReturnsNull(
				ConditionlessQuery && ConditionlessQuery && ConditionlessQuery && ConditionlessQuery,
				q=>q.ConditionlessQuery() && q.ConditionlessQuery() && q.ConditionlessQuery() && q.ConditionlessQuery()

			);
			ReturnsNull(
				NullQuery && ConditionlessQuery && ConditionlessQuery && ConditionlessQuery,
				q=>q.NullQuery() && q.ConditionlessQuery() && q.ConditionlessQuery() && q.ConditionlessQuery()
			);

		}
		[U] public void CombiningManyUsingAggregate()
		{
			var lotsOfAnds = Enumerable.Range(0, 100).Aggregate(new QueryContainer(), (q, c) => q && Query, q => q);
			LotsOfAnds(lotsOfAnds);
		}

		[U] public void CombiningManyUsingForeachInitializingWithNull()
		{
			QueryContainer container = null; 
			foreach(var i in Enumerable.Range(0, 100))
				container &= Query;
			LotsOfAnds(container);
		}

		[U] public void CombiningManyUsingForeachInitializingWithDefault()
		{
			var container = new QueryContainer(); 
			foreach(var i in Enumerable.Range(0, 100))
				container &= Query;
			LotsOfAnds(container);
		}

		private void LotsOfAnds(IQueryContainer lotsOfAnds)
		{
			lotsOfAnds.Should().NotBeNull();
			lotsOfAnds.Bool.Should().NotBeNull();
			lotsOfAnds.Bool.Must.Should().NotBeEmpty().And.HaveCount(100);
		}

	}
}
