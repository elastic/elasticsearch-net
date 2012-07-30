using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.FilterTests
{
	[TestFixture]
	public class RangeFilterJson
	{
		[Test]
		public void Range()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff=>ff
					.Range(n => n
						.OnField(f=>f.LOC)
						.From("10")
						.To("20")
						.FromExclusive()
					)
				);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						range: {
							""loc"": {
								from: ""10"",
								to: ""20"",
								include_lower: false
							}
						}

					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
	
		[Test]
		public void NuRangeGtLtWithCache()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff => ff.Cache(true).Name("my_name")
					.Range(n => n
						.OnField(f => f.LOC)
						.GreaterOrEquals("10")
						.LowerOrEquals("20")
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						range: {
							""loc"": {
								from: ""10"",
								to: ""20"",
								include_lower: true,
								include_upper: true
							},
							_cache: true,
							_name : ""my_name""
						}

					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void RangeInts()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff => ff
					.Range(n => n
						.OnField(f => f.LOC)
						.From(10)
						.To(20)
						.FromExclusive()
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						range: {
							""loc"": {
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
		public void RangeDoubles()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff => ff
					.Range(n => n
						.OnField(f => f.LOC)
						.From(10.0)
						.To(20.0)
						.FromExclusive()
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						range: {
							""loc"": {
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
		public void RangeDatesCustom()
		{
			var format = "yyyy/MM/dd";
			var lowerBound = DateTime.UtcNow.AddYears(-1);
			var upperBound = DateTime.UtcNow.AddYears(1);
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff => ff
					.Range(n => n
						.OnField(f => f.StartedOn)
						.From(lowerBound, format)
						.To(upperBound, format)
						.FromExclusive()
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						range: {
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
