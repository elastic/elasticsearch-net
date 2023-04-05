// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Tests.Domain;

namespace Tests.QueryDsl.BoolDsl;

public abstract class OperatorUsageBase
{
	protected static readonly TermQuery NullQuery = null;
	protected static readonly TermQuery Query = new("x") { Value = "y" };

	protected static void ReturnsNull(Query combined, Action<QueryDescriptor<Project>> selector)
	{
		combined.Should().BeNull();

		var descriptor = new QueryDescriptor<Project>();
		selector(descriptor);
		descriptor.TestHasVariant.Should().BeFalse();
	}

	//protected void ReturnsBool(QueryContainer combined, Func<QueryContainerDescriptor<Project>, QueryContainer> selector,
	//	Action<IBoolQuery> boolQueryAssert
	//)
	//{
	//	ReturnsBool(combined, boolQueryAssert);
	//	ReturnsBool(selector.Invoke(new QueryContainerDescriptor<Project>()), boolQueryAssert);
	//}

	//private void ReturnsBool(QueryContainer combined, Action<IBoolQuery> boolQueryAssert)
	//{
	//	combined.Should().NotBeNull();
	//	IQueryContainer c = combined;
	//	c.Bool.Should().NotBeNull();
	//	boolQueryAssert(c.Bool);
	//}

	//protected void ReturnsSingleQuery(QueryContainer combined, Func<QueryContainerDescriptor<Project>, QueryContainer> selector,
	//	Action<IQueryContainer> containerAssert
	//)
	//{
	//	ReturnsSingleQuery(combined, containerAssert);
	//	ReturnsSingleQuery(selector.Invoke(new QueryContainerDescriptor<Project>()), containerAssert);
	//}

	//private void ReturnsSingleQuery(QueryContainer combined, Action<IQueryContainer> containerAssert)
	//{
	//	combined.Should().NotBeNull();
	//	IQueryContainer c = combined;
	//	containerAssert(c);
	//}
}
