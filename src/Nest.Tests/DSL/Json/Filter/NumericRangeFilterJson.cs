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
				
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						numeric_range: {
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
		public void NumericRangeGtLtWithCache()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff => ff
					.NumericRange(n => n
						.OnField(f => f.LOC)
						.GreaterOrEquals(10)
						.LowerOrEquals(20)
						.Cache()
					)
				);

			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						numeric_range: {
							""loc.sort"": {
								from: 10,
								to: 20,
								from_inclusive: true,
								to_inclusive: true
							},
							_cache: true
						}

					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
