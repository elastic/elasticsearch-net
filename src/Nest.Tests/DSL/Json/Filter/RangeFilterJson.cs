using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Nest.DSL;
using Nest.TestData.Domain;

namespace Nest.Tests.Dsl.Json.Filter
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
						.From(10)
						.To(20)
						.FromExclusive()
					)
				);
				
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						range: {
							""loc.sort"": {
								from: 10,
								to: 20,
								from_inclusive: false
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
						.GreaterOrEquals(10)
						.LowerOrEquals(20)
					)
				);

			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						range: {
							""loc.sort"": {
								from: 10,
								to: 20,
								from_inclusive: true,
								to_inclusive: true
							},
							_cache: true,
							_name : ""my_name""
						}

					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
