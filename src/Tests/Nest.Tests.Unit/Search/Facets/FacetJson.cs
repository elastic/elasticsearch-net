using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Facets
{
  [TestFixture]
  public class FacetJson
  {
    [Test]
    public void QueryFacetGlobal()
    {
      var s = new SearchDescriptor<ElasticsearchProject>()
        .From(0)
        .Size(10)
        .QueryRaw(@"{ raw : ""query""}")
        .FacetTerm(q=>q
          .OnField(f=>f.Name)
          .Global()
          .FacetFilter(ff=>ff.Exists(f=>f.Name))
        );

      var json = TestElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""name"" :  
            {
              global: true,
              terms: {
                field: ""name""
              },
              facet_filter: {
                exists: { field: ""name"" }
              }
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
    [Test]
    public void QueryFacetScoped()
    {
      var s = new SearchDescriptor<ElasticsearchProject>()
        .From(0)
        .Size(10)
        .QueryRaw(@"{ raw : ""query""}")
        .FacetTerm(q=>q
          .OnField(f=>f.Name)
          .FacetFilter(ff=>ff.Exists(f=>f.Name))
        );

      var json = TestElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""name"" :  
            {
              terms: {
                field: ""name""
              },
              facet_filter: {
                exists: { field: ""name"" }
              }
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
    [Test]
    public void QueryFacetNested()
    {
      var s = new SearchDescriptor<ElasticsearchProject>()
        .From(0)
        .Size(10)
        .QueryRaw(@"{ raw : ""query""}")
        .FacetTerm(q => q
          .Nested("some_nested_query")
          .OnField(f => f.Name)
          .FacetFilter(ff => ff.Exists(f => f.Name))
        );

      var json = TestElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""name"" :  
            {
              nested: ""some_nested_query"",
              terms: {
                field: ""name""
              },
              facet_filter: {
                exists: { field: ""name"" }
              }
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
  }
}
