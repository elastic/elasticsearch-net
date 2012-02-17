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
	public class TypeFilterJson
	{
		[Test]
		public void TypeFilter()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(f=>f.Type("my_type"));
				
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						type : { 
							value : ""my_type""
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
	}
}
