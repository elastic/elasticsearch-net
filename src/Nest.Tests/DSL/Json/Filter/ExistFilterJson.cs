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
	public class ExistFilterJson
	{
		[Test]
		public void ExistFilter()
		{
			var s = new SearchDescriptor<ElasticSearchProject>().From(0).Size(10)
				.Filter(filter=>filter
					.Exists(f=>f.Name)
			);
				
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						exists : { field : ""name"" }
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
	}
}
