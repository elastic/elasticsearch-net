using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Facets
{
  [TestFixture]
  public class RangeFacetJson
  {
    [Test]
    public void TestRangeFacet()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .QueryRawJson(@"{ raw : ""query""}")
        .FacetRange<int>(t => t
          .OnField(f => f.LOC)
          .Ranges(
            r=>r.To(50),
            r=>r.From(50).To(100),
            r=>r.From(100).To(150),
            r=>r.From(150).To(200),
            r=>r.From(200).To(250),
            r=>r.From(250)
          )
        );
      var json = TestElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""loc.sort"" :  {
                range : {
                    field : ""loc.sort"",
                    ranges: [
                      {
                        to: 50
                      },
                      {
                        from: 50,
                        to: 100
                      },
                      {
                        from: 100,
                        to: 150
                      },
                      {
                        from: 150,
                        to: 200
                      },
                      {
                        from: 200,
                        to: 250
                      },
                      {
                        from: 250
                      }
                    ]
                }
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
    [Test]
    public void TestRangeDoubleFacet()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .QueryRawJson(@"{ raw : ""query""}")
        .FacetRange<double>(t => t
          .OnField(f => f.LOC)
          .Ranges(
            r => r.To(50.0),
            r => r.From(50.0).To(100.0),
            r => r.From(100.0)
          )
        );
      var json = TestElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""loc.sort"" :  {
                range : {
                    field : ""loc.sort"",
                    ranges: [
                      {
                        to: 50.0
                      },
                      {
                        from: 50.0,
                        to: 100.0
                      },
                      {
                        from: 100.0
                      }
                    ]
                }
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
    [Test]
    public void TestRangeDateFacet()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .QueryRawJson(@"{ raw : ""query""}")
        .FacetRange<DateTime>(t => t
          .OnField(f => f.StartedOn)
          .Ranges(
            r => r.To(new DateTime(1990,1,1).Date)
          )
        );
      var json = TestElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""startedOn"" :  {
                range : {
                    field : ""startedOn"",
                    ranges: [
                      {
                        to: ""1990-01-01T00:00:00""
                      }
                    ]
                }
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
    [Test]
    public void TestRangeDateFacetKeyScript()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .QueryRawJson(@"{ raw : ""query""}")
        .FacetRange<DateTime>("needs_a_name", t => t
          .KeyScript("doc['date'].date.minuteOfHour")
          .ValueScript("doc['num1'].value")
          .Ranges(
            r => r.To(new DateTime(1990, 1, 1).Date)
          )
        );
      var json = TestElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""needs_a_name"" : {
                range : {
                    key_script : ""doc['date'].date.minuteOfHour"",
                    value_script : ""doc['num1'].value"",
                    ranges: [
                      {
                        to: ""1990-01-01T00:00:00""
                      }
                    ]
                }
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
    [Test]
    public void TestRangeDateFacetKeyField()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .QueryRawJson(@"{ raw : ""query""}")
        .FacetRange<DateTime>("needs_a_name", t => t
          .KeyField("field_name")
          .ValueField("another_field_name")
          .Ranges(
            r => r.To(new DateTime(1990, 1, 1).Date)
          )
        );
      var json = TestElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""needs_a_name"" : {
                range : {
                    key_field : ""field_name"",
                    value_field : ""another_field_name"",
                    ranges: [
                      {
                        to: ""1990-01-01T00:00:00""
                      }
                    ]
                }
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
  }
}
