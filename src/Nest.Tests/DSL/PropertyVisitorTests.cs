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
  public class PropertyVisitorTests
  {
    [Test]
    public void TestFromSize()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .Query(q => q.Term(f => f.Name.Suffix("sort"), "value"));
      var json = ElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10,
            query: {
          term: {
            ""name.sort"": {
              value: ""value""
            }
          }
        }
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
  }
}
