using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.TestData.Domain;

namespace Nest.Tests.Dsl.Json.Facets
{
  [TestFixture]
  public class FilterFacetJson
  {
    [Test]
    public void FilterFacet()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .QueryRaw(@"{ raw : ""query""}")
        .FacetFilter("wow_facet", filter=>filter
          .Exists(f=>f.Name)
        );

      var json = ElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""wow_facet"" :  {
                filter : {
                  exists : { field : ""name"" }
               } 
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
   
  }
}
