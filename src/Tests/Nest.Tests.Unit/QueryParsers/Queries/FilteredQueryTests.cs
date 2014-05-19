using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class FilteredQueryTests : ParseQueryTestsBase
	{
		
		[Test]
		public void Filtered_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.Filtered,
				f=>f.Filtered(fq=>fq
					.Filter(ff=>Filter1)
					.Query(qq=>Query1)
					)
				);
			AssertIsTermFilter(q.Filter, Filter1);
			AssertIsTermQuery(q.Query, Query1);
		}
	}
}