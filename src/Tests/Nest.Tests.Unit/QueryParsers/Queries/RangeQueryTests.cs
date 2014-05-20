using System;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class RangeQueryTests : ParseQueryTestsBase
	{
		[Test]
		public void RangeEquals_Deserializes()
		{
			//todo simplify range query like the range filter to only support gt gte lt lte
			var q = this.SerializeThenDeserialize(
				f=>f.Range,
				f=>f.Range(r=>r
					.OnField(p=>p.Name)
					.Boost(2.1)
					.GreaterOrEquals(2)
					.LowerOrEquals(10)
					)
				);
			q.Field.Should().Be("name");
			q.Boost.Should().Be(2.1);
			q.GreaterThanOrEqualTo.Should().Be("2");
			q.LowerThanOrEqualTo.Should().Be("10");
		}

		[Test]
		public void Range_Deserializes()
		{
			var from = DateTime.UtcNow.Date;
			var to = from.AddDays(2);

			//todo simplify range query like the range filter to only support gt gte lt lte
			var q = this.SerializeThenDeserialize(
				f=>f.Range,
				f=>f.Range(r=>r
					.OnField(p=>p.Name)
					.Boost(2.1)
					.Greater(from, "yyyy-MM-dd")
					.Lower(to)
					)
				);
			q.Field.Should().Be("name");
			q.Boost.Should().Be(2.1);
			q.GreaterThan.Should().Be(from.ToString("yyyy-MM-dd"));
			q.LowerThan.Should().Be(to.ToString("yyyy-MM-dd'T'HH:mm:ss.fff"));
		}
	}
}