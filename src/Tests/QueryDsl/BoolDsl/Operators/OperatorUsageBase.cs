using System;
using FluentAssertions;
using Nest;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.BoolDsl.Operators
{
	public abstract class OperatorUsageBase
	{
		protected static readonly TermQuery Query = new TermQuery { Field = "x", Value = "y" };
		protected static readonly TermQuery ConditionlessQuery = new TermQuery { };
		protected static readonly TermQuery NullQuery = null;

		protected void ReturnsNull(QueryContainer combined, Func<QueryContainerDescriptor<Project>, QueryContainer> selector)
		{
			combined.Should().BeNull(); selector.Invoke(new QueryContainerDescriptor<Project>()).Should().BeNull();
		}

		protected void ReturnsBool(QueryContainer combined, Func<QueryContainerDescriptor<Project>, QueryContainer> selector, Action<IBoolQuery> boolQueryAssert)
		{
			ReturnsBool(combined, boolQueryAssert);
			ReturnsBool(selector.Invoke(new QueryContainerDescriptor<Project>()), boolQueryAssert);
		}

		private void ReturnsBool(QueryContainer combined, Action<IBoolQuery> boolQueryAssert)
		{
			combined.Should().NotBeNull();
			IQueryContainer c = combined;
			c.Bool.Should().NotBeNull();
			boolQueryAssert(c.Bool);
		}

		protected void ReturnsSingleQuery(QueryContainer combined, Func<QueryContainerDescriptor<Project>, QueryContainer> selector, Action<IQueryContainer> containerAssert)
		{
			ReturnsSingleQuery(combined, containerAssert);
			ReturnsSingleQuery(selector.Invoke(new QueryContainerDescriptor<Project>()), containerAssert);
		}

		private void ReturnsSingleQuery(QueryContainer combined, Action<IQueryContainer> containerAssert)
		{
			combined.Should().NotBeNull();
			IQueryContainer c = combined;
			containerAssert(c);
		}
	}
}
