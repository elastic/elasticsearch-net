using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Nest;
using HackerNews.Indexer.Domain;
using Nest.DSL;
using Nest.TestData.Domain;

namespace Nest.Tests
{
  [TestFixture]
  public class QueryDSLTests : BaseElasticSearchTests
  {
    [Test]
    public void MatchAll()
    {
      var results = this.ConnectedClient.Search<ElasticSearchProject>(s => s
        .From(0)
        .Size(10)
        .Fields(f => f.Id, f => f.Name)
        .SortAscending(f => f.LOC)
        .SortDescending(f => f.Name)
        .Query(q => q
          .MatchAll()
        )
      );
      Assert.NotNull(results);
      Assert.True(results.IsValid);
      Assert.NotNull(results.Documents);
      Assert.GreaterOrEqual(results.Documents.Count(), 10);
    }
    [Test]
    public void MatchAllShortcut()
    {
      var results = this.ConnectedClient.Search<ElasticSearchProject>(s => s
        .From(0)
        .Size(10)
        .Fields(f => f.Id, f => f.Name)
        .SortAscending(f => f.LOC)
        .SortDescending(f => f.Name)
        .MatchAll()
      );
      Assert.NotNull(results);
      Assert.True(results.IsValid);
      Assert.NotNull(results.Documents);
      Assert.GreaterOrEqual(results.Documents.Count(), 10);
      Assert.True(results.Documents.All(d => !string.IsNullOrEmpty(d.Name)));
    }
    [Test]
    public void TestTermQuery()
    {
      var results = this.ConnectedClient.Search<ElasticSearchProject>(s => s
        .From(0)
        .Size(10)
        .Fields(f => f.Id, f => f.Name)
        .SortAscending(f => f.LOC)
        .SortDescending(f => f.Name)
        .Query(q => q
          .Term(f => f.Name, "elasticsearch.pm")
        )
      );
      Assert.NotNull(results);
      Assert.True(results.IsValid);
      Assert.NotNull(results.Documents);
      Assert.GreaterOrEqual(results.Documents.Count(), 1);
    }
    [Test]
    public void TestWildcardQuery()
    {
      var results = this.ConnectedClient.Search<ElasticSearchProject>(s => s
        .From(0)
        .Size(10)
        .Fields(f => f.Id, f => f.Name)
        .SortAscending(f => f.LOC)
        .SortDescending(f => f.Name)
        .Query(q => q
          .Wildcard(f => f.Name, "elasticsearch.*")
        )
      );
      Assert.NotNull(results);
      Assert.True(results.IsValid);
      Assert.NotNull(results.Documents);
      Assert.GreaterOrEqual(results.Documents.Count(), 1);
    }
    [Test]
    public void TestPrefixQuery()
    {
      var results = this.ConnectedClient.Search<ElasticSearchProject>(s => s
        .From(0)
        .Size(10)
        .Fields(f => f.Id, f => f.Name)
        .SortAscending(f => f.LOC)
        .SortDescending(f => f.Name)
        .Query(q => q
          .Prefix(f => f.Name, "el")
        )
      );
      Assert.NotNull(results);
      Assert.True(results.IsValid);
      Assert.NotNull(results.Documents);
      Assert.GreaterOrEqual(results.Documents.Count(), 1);
    }
    [Test]
    public void TestTermFacet()
    {
      var results = this.ConnectedClient.Search<ElasticSearchProject>(s => s
        .From(0)
        .Size(10)
        .Fields(f => f.Id, f => f.Name)
        .SortAscending(f => f.LOC)
        .SortDescending(f => f.Name)
        .MatchAll()
        .FacetTerm(t => t.OnField(f=>f.Country).Size(20))
      );
      Assert.Greater(results.Facet<TermFacet>(f => f.Country).Items.Count(), 0);
      Assert.Greater(results.FacetItems<TermItem>(f=>f.Country).Count(), 0);
    }


  }
}
