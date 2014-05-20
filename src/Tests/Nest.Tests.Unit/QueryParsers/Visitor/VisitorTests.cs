using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FakeItEasy;
using FluentAssertions;
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
			var visitBoolQuery = A.CallTo(() => visitor.Visit(A<IBoolQuery>._));
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
			visitBoolQuery.MustHaveHappened(Repeated.Exactly.Once);
		}
		
		[Test]
		public void DepthAndScopeAreSet()
		{
			var visitor = A.Fake<IQueryVisitor>();
			var visitQuery = A.CallTo(() => visitor.Visit(A<IQuery>._));
			var visitFilter = A.CallTo(() => visitor.Visit(A<IFilterBase>._));

			var seenDepths = new List<int>();
			var seenScopes = new List<VisitorScope>();

			Action interceptor = () =>
			{
				seenDepths.Add(visitor.Depth);
				seenScopes.Add(visitor.Scope);
			};
			visitQuery.Invokes(interceptor);
			visitFilter.Invokes(interceptor);
			
			var query = NewQuery(q =>
				q.Filtered(qf => qf
					.Filter(qff => qff
						.And(
							ff => ff.Term("term1", "value"),
							ff => ff.Term("term2", "value2"),
							ff => ff.Term("term3", "value2")
						)
					)
				)
				&& q.Term("term4", "asd")
				|| q.Prefix("prefix5", "value")
			);
			query.Accept(visitor);

			seenScopes.Should().NotBeEmpty()
				.And.HaveCount(9)
				.And.ContainInOrder(new[]
				{
					VisitorScope.Query, //bool
					VisitorScope.Must,  //filtered
					VisitorScope.Filter, //and
					VisitorScope.Filter, //term1
					VisitorScope.Filter,  //term2
					VisitorScope.Filter, //term3
					VisitorScope.Must, //term4 
					VisitorScope.Should, //term4 
				});
			seenDepths.Should().NotBeEmpty()
				.And.ContainInOrder(new[] { 1, 2, 3, 4, 5, 5, 5, 3, 2 });
		}
	}
}
