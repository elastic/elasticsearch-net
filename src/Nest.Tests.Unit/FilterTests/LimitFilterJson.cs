using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.FilterTests
{
	[TestFixture]
	public class LimitFilterJson
	{
		[Test]
		public void LimitFilter()
		{
			var s = new SearchDescriptor<ElasticSearchProject>().From(0).Size(10)
				.Filter(filter=>filter
					.Limit(100)
			);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						limit : { 
							value : 100
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
	}
}
