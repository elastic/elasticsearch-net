using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Facets
{
  [TestFixture]
  public class QueryFacetJson
  {
    [Test]
    public void QueryFacet()
    {
      var s = new SearchDescriptor<ElasticsearchProject>()
        .From(0)
        .Size(10)
        .QueryRaw(@"{ raw : ""query""}")
        .FacetQuery("wow_facet", q=>q
          .Term(f=>f.Name, "elasticsearch.pm")
        );

      var json = TestElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""wow_facet"" :  {
                query : {
                  term : {
                    name : { value : ""elasticsearch.pm"" }
                  }
               } 
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
   
  }
}
