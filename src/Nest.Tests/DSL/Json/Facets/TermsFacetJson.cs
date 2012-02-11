using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Nest.DSL;
using Nest.TestData.Domain;

namespace Nest.Tests.Dsl.Json.Facets
{
  [TestFixture]
  public class TermsFacetJson
  {
    [Test]
    public void TestTermFacet()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .Query(@"{ raw : ""query""}")
        .FacetTerm(t => t.OnField(f => f.Country).Size(20));
      var json = ElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            country :  {
                terms : {
                    field : ""country"",
                    size : 20
                } 
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected));
    }

    [Test]
    public void TestTermFacetAll()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .Query(@"{ raw : ""query""}")
        .FacetTerm(t => t
          .OnField(f => f.Country)
          .Size(20)
          .Order(FacetOrder.reverse_count)
          .Exclude("term1", "term2")
          .AllTerms()
          .Regex(@"\s+", RegexFlags.DOTALL)
          .Script("term + 'aaa'")
          .ScriptField("_source.my_field")
        );
      var json = ElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            country :  {
                terms : {
                    field : ""country"",
                    size : 20,
                    order: ""reverse_count"",
                    all_terms: true,
                    exclude: [ ""term1"", ""term2"" ],        
                    regex: ""\\s+"",
                    regex_flags: ""DOTALL"",
                    script: ""term + 'aaa'"",
                    script_field: ""_source.my_field""
                } 
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected));
    }
    [Test]
    public void TestTermFacetAllMultiFields()
    {
      //when using multifields we cant determine a facet name 
      //automatically
      Assert.Throws<DslException>(() =>
      {
        new SearchDescriptor<ElasticSearchProject>()
          .From(0)
          .Size(10)
          .Query(@"{ raw : ""query""}")
          .FacetTerm(t => t
            .OnFields(f => f.Country, f => f.LOC)
            .Size(20)
            .Order(FacetOrder.reverse_count)
            .Exclude("term1", "term2")
            .AllTerms()
            .Regex(@"\s+", RegexFlags.DOTALL)
            .Script("term + 'aaa'")
            .ScriptField("_source.my_field")
          );
      });

      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .Query(@"{ raw : ""query""}")
        .FacetTerm("i_bet_this_crazy_facet_actually_works", t => t
          .OnFields(f => f.Country, f => f.LOC)
          .Size(20)
          .Order(FacetOrder.reverse_count)
          .Exclude("term1", "term2")
          .AllTerms()
          .Regex(@"\s+", RegexFlags.DOTALL)
          .Script("term + 'aaa'")
          .ScriptField("_source.my_field")
        );

      var json = ElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            i_bet_this_crazy_facet_actually_works :  {
                terms : {
                    fields : [""country"", ""loc.sort""],
                    size : 20,
                    order: ""reverse_count"",
                    all_terms: true,
                    exclude: [ ""term1"", ""term2"" ],        
                    regex: ""\\s+"",
                    regex_flags: ""DOTALL"",
                    script: ""term + 'aaa'"",
                    script_field: ""_source.my_field""
                } 
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected));
    }
  }
}
