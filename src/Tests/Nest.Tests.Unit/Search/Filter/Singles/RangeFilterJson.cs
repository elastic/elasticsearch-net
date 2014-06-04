using System;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Filter.Singles
{
	[TestFixture]
	public class RangeFilterJson
	{
		[Test]
		public void Range()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff=>ff
					.Range(n => n
						.OnField(f=>f.LOC)
						.GreaterOrEquals("10")
						.Lower("20")
					)
				);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
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
		public void NuRangeGtLtWithCache()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
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
								gte: ""10"",
								lte: ""20"",
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
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff => ff
					.Range(n => n
						.OnField(f => f.LOC)
						.GreaterOrEquals(10)
						.Lower(20)
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
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
				.Filter(ff => ff
					.Range(n => n
						.OnField(f => f.LOC)
						.GreaterOrEquals(10.3)
						.LowerOrEquals(20.4)
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						range: {
							""loc"": {
								gte: ""10.3"",
								lte: ""20.4"",
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
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff => ff
					.Range(n => n
						.OnField(f => f.StartedOn)
						.GreaterOrEquals(lowerBound, format)
						.Lower(upperBound, format)
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						range: {
							""startedOn"": {
								gte: """ + lowerBound.ToString(format) + @""",
								lt: """ + upperBound.ToString(format) + @""",
							}
						}

					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
