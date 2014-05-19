using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class IndicesQueryTests : ParseQueryTestsBase
	{
		[Test]
		public void Indices_Deserializes()
		{
			var indices = new [] {"index1", "index2"};
			var q = this.SerializeThenDeserialize(
				f=>f.Indices,
				f=>f.Indices(i=>i
					.Indices(indices)
					.NoMatchQuery(qq=>Query1)
					.Query(qq=>Query2)
					)
				);
			q.Indices.ShouldBeEquivalentTo(indices);
			AssertIsTermQuery(q.NoMatchQuery, Query1);
			AssertIsTermQuery(q.Query, Query2);
		}

	}
}