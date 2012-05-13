using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.TestData.Domain;

namespace Nest.Tests.Dsl.Json.Query
{
	[TestFixture]
	public class RangeQueryJson
	{
		[Test]
		public void RangeStrings()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(ff => ff
					.Range(n => n
						.OnField(f=>f.LOC)
						.From("10")
						.To("20")
						.FromExclusive()
					)
				);
				
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						range: {
							""loc.sort"": {
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
		public void RangeInts()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(ff => ff
					.Range(n => n
						.OnField(f => f.LOC)
						.From(10)
						.To(20)
						.FromExclusive()
					)
				);

			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						range: {
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
		public void RangeDoubles()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(ff => ff
					.Range(n => n
						.OnField(f => f.LOC)
						.From(10.0)
						.To(20.0)
						.FromExclusive()
					)
				);

			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						range: {
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
	}
}
