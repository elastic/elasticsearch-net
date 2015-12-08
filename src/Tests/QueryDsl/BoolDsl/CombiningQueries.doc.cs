using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using FluentAssertions;
using Tests.Framework.MockData;

namespace Tests.QueryDsl
{
	public static class QueryContainerDescriptorExtensions
	{
		public static QueryContainer Query(this QueryContainerDescriptor<Project> q) => q.Term("x", "y");
		public static QueryContainer ConditionlessQuery(this QueryContainerDescriptor<Project> q) => q.Term("x", "");
		public static QueryContainer NullQuery(this QueryContainerDescriptor<Project> q) => q.Term("x", "");
		public static QueryContainer InvokeQuery<T>(
			this Func<QueryContainerDescriptor<T>, QueryContainer> f,
			QueryContainerDescriptor<T> container)
			where T : class
		{
			var c = f.Invoke(container);
			IQueryContainer ic = c;
			//if query is not conditionless or is verbatim: return a container that holds the query
			if (ic != null && (!ic.IsConditionless || ic.IsVerbatim))
				return c;

			//query is conditionless but the container is marked as strict, throw exception
			if (ic != null && ic.IsStrict)
				throw new DslException("Query is conditionless but strict is turned on") { Offender = c };

			//query is conditionless return an empty container that can later be rewritten
			return QueryContainer.CreateEmptyContainer(c);
		}
	}

	public class CombiningQueries
	{
		private static readonly TermQuery Query = new TermQuery { Field = "x", Value = "y" };
		private static readonly TermQuery ConditionlessQuery = new TermQuery { };
		private static readonly TermQuery NullQuery = null;

		/**
		* 
		*/
		[U]
		public void And()
		{
			ReturnsBool(Query && Query, q => q.Query() && q.Query(), b =>
			{
				b.Must.Should().NotBeEmpty().And.HaveCount(2);
				b.Should.Should().BeNull();
				b.MustNot.Should().BeNull();
				b.Filter.Should().BeNull();
			});

			ReturnsSingleQuery(Query && ConditionlessQuery, q => q.Query() && q.ConditionlessQuery(),
				c => c.Term?.Value.Should().NotBeNull());
			ReturnsSingleQuery(ConditionlessQuery && Query, q => q.ConditionlessQuery() && q.Query(),
				c => c.Term?.Value.Should().NotBeNull());

			ReturnsSingleQuery(Query && NullQuery, q => q.Query() && q.NullQuery(),
				c => c.Term?.Value.Should().NotBeNull());
			ReturnsSingleQuery(NullQuery && Query, q=> q.NullQuery() && q.Query(), 
				c => c.Term?.Value.Should().NotBeNull());

			ReturnsSingleQuery(ConditionlessQuery && ConditionlessQuery && ConditionlessQuery && Query,
				q => q.ConditionlessQuery() && q.ConditionlessQuery() && q.ConditionlessQuery() && q.Query(),
				c => c.Term?.Value.Should().NotBeNull());

			ReturnsSingleQuery(
				NullQuery && NullQuery && ConditionlessQuery && Query, 
				q=>q.NullQuery() && q.NullQuery() && q.ConditionlessQuery() && q.Query(),
				c => c.Term?.Value.Should().NotBeNull());

			(NullQuery && ConditionlessQuery).Should().BeNull();
			(ConditionlessQuery && NullQuery).Should().BeNull();
			(ConditionlessQuery && ConditionlessQuery).Should().BeNull();
			(ConditionlessQuery && ConditionlessQuery && ConditionlessQuery && ConditionlessQuery).Should().BeNull();
			(NullQuery && ConditionlessQuery && ConditionlessQuery && ConditionlessQuery).Should().BeNull();

			IQueryContainer lotsOfAnds = Enumerable.Range(0, 100).Aggregate<int, QueryBase, QueryContainer>(NullQuery, (q, c) => q && Query, q => q);
			lotsOfAnds.Should().NotBeNull();
			lotsOfAnds.Bool.Should().NotBeNull();
			lotsOfAnds.Bool.Must.Should().NotBeEmpty().And.HaveCount(100);
		}

		private void ReturnsBool(QueryContainer combined, Func<QueryContainerDescriptor<Project>, QueryContainer> selector, Action<IBoolQuery> boolQueryAssert)
		{
			ReturnsBool(combined, boolQueryAssert);
			ReturnsBool(selector.InvokeQuery(new QueryContainerDescriptor<Project>()), boolQueryAssert);
		}

		private void ReturnsBool(QueryContainer combined, Action<IBoolQuery> boolQueryAssert)
		{
			combined.Should().NotBeNull();
			IQueryContainer c = combined;
			c.Bool.Should().NotBeNull();
			boolQueryAssert(c.Bool);
		}

		private void ReturnsSingleQuery(QueryContainer combined, Func<QueryContainerDescriptor<Project>, QueryContainer> selector, Action<IQueryContainer> containerAssert)
		{
			ReturnsSingleQuery(combined, containerAssert);
			ReturnsSingleQuery(selector.InvokeQuery(new QueryContainerDescriptor<Project>()), containerAssert);
		}

		private void ReturnsSingleQuery(QueryContainer combined, Action<IQueryContainer> containerAssert)
		{
			combined.Should().NotBeNull();
			IQueryContainer c = combined;
			containerAssert(c);
		}

	}
}
