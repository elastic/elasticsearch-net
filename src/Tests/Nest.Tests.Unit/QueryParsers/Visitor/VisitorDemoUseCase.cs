using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FakeItEasy;
using FluentAssertions;
using Nest.DSL.Visitor;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Visitor
{
	[TestFixture]
	public class VisitorDemoUseCase
	{

		public IQueryDescriptor NewQuery(Func<QueryDescriptor<ElasticsearchProject>, BaseQuery> selector)
		{
			return selector(new QueryDescriptor<ElasticsearchProject>());
		}

		[Test]
		public void PrettyPrintQueryGraph()
		{
			var visitor = new DslPrettyPrintVisitor(new ConnectionSettings());
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
				|| q.Prefix(p=>p.Content, "prefix")
			);
			query.Accept(visitor);
			/* visitor.PrettyPrint will hold:
			 * 
			 * query: bool ()
			 *  should: bool ()
			 *    must: filtered ()
			 *      filter: and ()
			 *        filter: term (field: term)
			 *        filter: term (field: term1)
			 *        filter: term (field: 3erm1)
			 *    must: term (field: term2)
			 *  should: prefix (field: content)
			 */
			Assert.Pass(visitor.PrettyPrint);
		}
	
	}
}
