using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FakeItEasy;
using Nest.DSL.Visitor;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Visitor
{
	[TestFixture]
	public class VisitorTests
	{

		public IQueryDescriptor NewQuery(Func<QueryDescriptor<object>, BaseQuery> selector)
		{
			return selector(new QueryDescriptor<object>());
		}

		[Test]
		public void SeesAllQueriesAndFilters()
		{
			var visitor = A.Fake<IQueryVisitor>();
			var visitQuery = A.CallTo(() => visitor.Visit(A<IQuery>._));
			var visitFilter = A.CallTo(() => visitor.Visit(A<IFilterBase>._));
			var query = NewQuery(q =>
				q.Filtered(qf => qf
					.Filter(qff => qff
						.And(
							ff => ff.Term("term", "value"),
							ff => ff.Term("term1", "value2"),
							ff => ff.Term("3erm1", "value2")
						)
					)
				)
				&& q.Term("term2", "asd")
			);
			query.Accept(visitor);

			//the two queries and up in a must clause of a wrapped bool query
			visitQuery.MustHaveHappened(Repeated.Exactly.Times(3));
			//and + 3 term filters
			visitFilter.MustHaveHappened(Repeated.Exactly.Times(4));

		}
	}
}
