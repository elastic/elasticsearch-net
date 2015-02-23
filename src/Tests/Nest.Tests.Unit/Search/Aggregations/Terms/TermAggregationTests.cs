using System.Reflection;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Search.Aggregations.Terms
{
	[TestFixture]
	public class TermAggregationTests : BaseJsonTests
	{
		[Test]
		public void TermAggregationSerializes()
		{
			var s = new TermsAggregationDescriptor<ElasticsearchProject>()
				.CollectMode(TermsAggregationCollectMode.BreadthFirst)
				.ExecutionHint(TermsAggregationExecutionHint.GlobalOrdinalsLowCardinality)
				.Field(p => p.Country)
				.MinimumDocumentCount(1)
				.OrderAscending("_count");

			this.JsonEquals(s, MethodBase.GetCurrentMethod());
		}
	}
}
