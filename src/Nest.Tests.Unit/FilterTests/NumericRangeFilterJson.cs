using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.FilterTests
{
	[TestFixture]
	public class NumericRangeFilterJson
	{
		[Test]
		public void NumericRange()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff=>ff
					.NumericRange(n=>n
						.OnField(f=>f.LOC)
						.From(10)
						.To(20)
						.FromExclusive()
					)
				);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						numeric_range: {
							""loc.sort"": {
								from: 10,
								to: 20,
								include_lower: false
							}
						}

					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
		[Test]
		public void NumericRangeGtLtWithCache()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff => ff.Cache(true)
					.NumericRange(n => n
						.OnField(f => f.LOC)
						.GreaterOrEquals(10)
						.LowerOrEquals(20)
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						numeric_range: {
							""loc.sort"": {
								from: 10,
								to: 20,
								include_lower: true,
								include_upper: true
							},
							_cache: true
						}

					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void NumericRangeDoubles()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff => ff
					.NumericRange(n => n
						.OnField(f => f.LOC)
						.From(10.0)
						.To(20.0)
						.FromExclusive()
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						numeric_range: {
							""loc.sort"": {
								from: 10.0,
								to: 20.0,
								include_lower: false
							}
						}

					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void NumericRangeDates()
		{
			var format = "yyyy/MM/dd";
			var lowerBound = DateTime.UtcNow.AddYears(-1);
			var upperBound = DateTime.UtcNow.AddYears(1);
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff => ff
					.NumericRange(n => n
						.OnField(f => f.StartedOn)
						.From(lowerBound, format)
						.To(upperBound, format)
						.FromExclusive()
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						numeric_range: {
							""startedOn"": {
								from: """ + lowerBound.ToString(format) + @""",
								to: """ + upperBound.ToString(format) + @""",
								include_lower: false
							}
						}

					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
