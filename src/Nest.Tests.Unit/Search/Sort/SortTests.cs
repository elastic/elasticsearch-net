using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Sort
{
    [TestFixture]
    public class SortTests
    {
        [Test]
        public void TestSort()
        {
            var s = new SearchDescriptor<ElasticSearchProject>()
                .From(0)
                .Size(10)
        .Sort(sort => sort
          .OnField(e => e.Country)
          .MissingLast()
          .Descending()
      );
            var json = TestElasticClient.Serialize(s);
            var expected = @"  {
          from: 0,
          size: 10,
          sort: {
            country: {
              missing: ""_last"",
              order: ""desc""
            }
          }
        }";
            Assert.True(json.JsonEquals(expected), json);
        }

        [Test]
        public void TestSortOnSortField()
        {
            var s = new SearchDescriptor<ElasticSearchProject>()
                .From(0)
                .Size(10)
        .Sort(sort => sort
          .OnField(e => e.Name)
          .MissingLast()
          .Descending()
      );
            var json = TestElasticClient.Serialize(s);
            var expected = @"  {
          from: 0,
          size: 10,
          sort: {
            ""name.sort"": {
              missing: ""_last"",
              order: ""desc""
            }
          }
        }";
            Assert.True(json.JsonEquals(expected), json);
        }

        [Test]
        public void TestSortAscending()
        {
            var s = new SearchDescriptor<ElasticSearchProject>()
                .From(0)
                .Size(10)
                .SortAscending(f => f.Country);
            var json = TestElasticClient.Serialize(s);
            var expected = @"  {
          from: 0,
          size: 10,
          sort: {
            country : ""asc""
            }          
        }";
            Assert.True(json.JsonEquals(expected), json);
        }

        [Test]
        public void TestSortDescending()
        {
            var s = new SearchDescriptor<ElasticSearchProject>()
                .From(0)
                .Size(10)
                .SortDescending(f => f.Country);
            var json = TestElasticClient.Serialize(s);
            var expected = @"  {
          from: 0,
          size: 10,
          sort: {
            country : ""desc""
            }          
        }";
            Assert.True(json.JsonEquals(expected), json);
        }

        [Test]
        public void TestSortAscendingOnSortField()
        {
            var s = new SearchDescriptor<ElasticSearchProject>()
                .From(0)
                .Size(10)
                .SortAscending(f => f.Name);
            var json = TestElasticClient.Serialize(s);
            var expected = @"  {
          from: 0,
          size: 10,
          sort: {
            ""name.sort"" : ""asc""
            }          
        }";
            Assert.True(json.JsonEquals(expected), json);
        }

        [Test]
        public void TestSortDescendingOnSortField()
        {
            var s = new SearchDescriptor<ElasticSearchProject>()
                .From(0)
                .Size(10)
                .SortDescending(f => f.Name);
            var json = TestElasticClient.Serialize(s);
            var expected = @"  {
          from: 0,
          size: 10,
          sort: {
            ""name.sort"" : ""desc""
            }          
        }";
            Assert.True(json.JsonEquals(expected), json);
        }
    [Test]
    public void TestSortGeo()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .SortGeoDistance(sort => sort
          .OnField(e => e.Origin)
          .MissingLast()
          .Descending()
          .PinTo(40, -70)
          .Unit(GeoUnit.km)
      );
      var json = TestElasticClient.Serialize(s);
      var expected = @"  {
          from: 0,
          size: 10,
          sort: {
            _geo_distance: {
              missing: ""_last"",
              order: ""desc"",
              ""origin"": ""40, -70"",
              unit: ""km""
            }
          }
        }";
      Assert.True(json.JsonEquals(expected), json);
    }
    [Test]
    public void TestSortScript()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .SortScript(sort => sort
          .MissingLast()
          .Descending()
          .Script("doc['field_name'].value * factor")
          .Params(p=>p
            .Add("factor", 1.1)
          )
          .Type("number")
      );
      var json = TestElasticClient.Serialize(s);
      var expected = @"  {
          from: 0,
          size: 10,
          sort: {
            _script: {
              missing: ""_last"",
              order: ""desc"",
              type: ""number"",
              script: ""doc['field_name'].value * factor"",
              params: {
                factor: 1.1
              }
            }
          }
        }";
      Assert.True(json.JsonEquals(expected), json);
    }
    }
}
