using System;
using System.Globalization;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class RangeFilterTests : ParseFilterTestsBase 
	{
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Range_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var rangeFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.Range,
				f=>f.Range(n => n
					.OnField(p=>p.LOC)
					.GreaterOrEquals("10")
					.LowerOrEquals("20")
					)
				);

			rangeFilter.Field.Should().Be("loc");
			rangeFilter.LowerThanOrEqualTo.Should().Be("20");
			rangeFilter.GreaterThanOrEqualTo.Should().Be("10");

		}

		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Range_Long_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var rangeFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.Range,
				f=>f.Range(n => n
					.OnField(p=>p.LOC)
					.GreaterOrEquals(10)
					.LowerOrEquals(20)
					)
				);

			rangeFilter.GreaterThanOrEqualTo.Should().Be("10");
			rangeFilter.LowerThanOrEqualTo.Should().Be("20");
		}
		
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Range_DateTime_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var dateFrom = DateTime.UtcNow.AddDays(-1);
			var dateTo = DateTime.UtcNow.AddDays(1);
			var rangeFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.Range,
				f=>f.Range(n => n
					.OnField(p=>p.LOC)
					.GreaterOrEquals(dateFrom)
					.LowerOrEquals(dateTo)
					)
				);
			rangeFilter.GreaterThanOrEqualTo.Should().Be(dateFrom.ToString("yyyy-MM-dd'T'HH:mm:ss.fff", CultureInfo.InvariantCulture));
			rangeFilter.LowerThanOrEqualTo.Should().Be(dateTo.ToString("yyyy-MM-dd'T'HH:mm:ss.fff", CultureInfo.InvariantCulture));
		}
		
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Range_Double_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var rangeFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.Range,
				f=>f.Range(n => n
					.OnField(p=>p.LOC)
					.GreaterOrEquals(20.1)
					.LowerOrEquals(20.22)
					)
				);
			rangeFilter.GreaterThanOrEqualTo.Should().Be("20.1");
			rangeFilter.LowerThanOrEqualTo.Should().Be("20.22");
		}


		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Range_Execution_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var rangeFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache,
				f => f.Range,
				f => f.Range(n => n
					.OnField(p => p.LOC)
					.GreaterOrEquals(20.1)
					.LowerOrEquals(20.22), RangeExecution.FieldData)
				);

			rangeFilter.GreaterThanOrEqualTo.Should().Be("20.1");
			rangeFilter.LowerThanOrEqualTo.Should().Be("20.22");
			rangeFilter.Execution.Should().Be(RangeExecution.FieldData);
		}
	}
}