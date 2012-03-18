using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.TestData.Domain;

namespace Nest.Tests.Dsl.Json.Filter
{
	[TestFixture]
	public class MissingFilterJson
	{
		[Test]
		public void MissingFilter()
		{
			var s = new SearchDescriptor<ElasticSearchProject>().From(0).Size(10)
				.Filter(ff=>ff.Missing(f=>f.Name));
				
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						missing : { field : ""name"" }
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
	}
}
