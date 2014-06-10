using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class ConstantScoreQueryTests : ParseQueryTestsBase
	{
		[Test]
		public void ConstantScore_Query_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.ConstantScore,
				f=>f.ConstantScore(cs=>cs
					.Boost(2.0)	
					.Query(csq=>Query3)
					)
				);
			q.Boost.Should().Be(2.0);
			AssertIsTermQuery(q.Query, Query3);
		}
		
		[Test]
		public void ConstantScore_Filter_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.ConstantScore,
				f=>f.ConstantScore(cs=>cs
					.Boost(2.0)	
					.Filter(ff=>Filter1)
					)
				);
			q.Boost.Should().Be(2.0);
			AssertIsTermFilter(q.Filter, Filter1);
		}
	}
}