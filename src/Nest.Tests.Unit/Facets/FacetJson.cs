using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Facets
{
  [TestFixture]
  public class FacetJson
  {
    [Test]
    public void QueryFacetGlobal()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .QueryRawJson(@"{ raw : ""query""}")
        .FacetTerm(q=>q
          .OnField(f=>f.Name)
          .Global()
          .FacetFilter(ff=>ff.Exists(f=>f.Name))
        );

      var json = ElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""name.sort"" :  
            {
              global: true,
              terms: {
                field: ""name.sort""
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
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .QueryRawJson(@"{ raw : ""query""}")
        .FacetTerm(q=>q
          .Scope("some_nested_query")
          .OnField(f=>f.Name)
          .FacetFilter(ff=>ff.Exists(f=>f.Name))
        );

      var json = ElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""name.sort"" :  
            {
              _scope: ""some_nested_query"",
              terms: {
                field: ""name.sort""
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
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .QueryRawJson(@"{ raw : ""query""}")
        .FacetTerm(q => q
          .Nested("some_nested_query")
          .OnField(f => f.Name)
          .FacetFilter(ff => ff.Exists(f => f.Name))
        );

      var json = ElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""name.sort"" :  
            {
              nested: ""some_nested_query"",
              terms: {
                field: ""name.sort""
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
