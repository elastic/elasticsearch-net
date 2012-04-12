using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Nest;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using Nest.TestData.Domain;

namespace Nest.Tests.DSL
{
	[TestFixture]
	public class UpdateTests
	{
		[Test]
		public void TestUpdate()
		{
      var s = new UpdateDescriptor<ElasticSearchProject>()
        .Script("ctx._source.counter += count")
        .Params(p => p
            .Add("count", 4)
        );
			var json = ElasticClient.Serialize(s);
			var expected = @"  {
	      script: ""ctx._source.counter += count"",
	      params: {
	        count: 4
	      }
	    }";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
