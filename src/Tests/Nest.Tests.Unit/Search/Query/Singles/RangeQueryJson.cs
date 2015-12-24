using System;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class RangeQueryJson
	{
		[Test]
		public void RangeStrings()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(ff => ff
					.Range(n => n
						.Name("named_query")
						.OnField(f=>f.LOC)
						.GreaterOrEquals("10")
						.Lower("20")
					)
				);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						range: {
							""loc"": {
								_name: ""named_query"",
								gte: ""10"",
								lt: ""20""
							}
						}

					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}

		[Test]
		public void RangeInts()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(ff => ff
					.Range(n => n
						.OnField(f => f.LOC)
						.GreaterOrEquals(10)
						.Lower(20)
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						range: {
							""loc"": {
								gte: ""10"",
								lt: ""20"",
							}
						}

					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void RangeDoubles()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(ff => ff
					.Range(n => n
						.OnField(f => f.LOC)
						.GreaterOrEquals(10.1)
						.LowerOrEquals(20.1)
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						range: {
							""loc"": {
								gte: ""10.1"",
								lte: ""20.1"",
							}
						}

					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
		
		[Test]
		public void RangeDates()
		{
			var lowerBound = DateTime.UtcNow.AddYears(-1);
			var upperBound = DateTime.UtcNow.AddYears(1);
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(ff => ff
					.Range(n => n
						.OnField(f => f.StartedOn)
						.Greater(lowerBound)
						.Lower(upperBound)
						.TimeZone("+1:00")
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						range: {
							""startedOn"": {
								gt: """ + lowerBound.ToString("yyyy-MM-dd'T'HH:mm:ss.fff") + @""",
								lt: """ + upperBound.ToString("yyyy-MM-dd'T'HH:mm:ss.fff") + @""",
								time_zone: ""+1:00""
							}
						}

					}
			}";
			Assert.True(json.JsonEquals(expected), json + Environment.NewLine + expected);
		}
		[Test]
		public void RangeDatesCustom()
		{
			var format = "yyyy/MM/dd";
			var lowerBound = DateTime.UtcNow.AddYears(-1);
			var upperBound = DateTime.UtcNow.AddYears(1);
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(ff => ff
					.Range(n => n
						.OnField(f => f.StartedOn)
						.GreaterOrEquals(lowerBound, format)
						.Lower(upperBound, format)
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						range: {
							""startedOn"": {
								gte: """ + lowerBound.ToString(format) + @""",
								lt: """ + upperBound.ToString(format) + @"""
							}
						}

					}
			}";
			Assert.True(json.JsonEquals(expected), json + Environment.NewLine + expected);
		}
	}
}
