using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.TestData.Domain;

namespace Nest.Tests.Dsl.Json.Facets
{
  [TestFixture]
  public class HistogramFacetJson
  {
    [Test]
    public void HistogramTest()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .QueryRaw(@"{ raw : ""query""}")
        .FacetHistogram(h => h.OnField(f=>f.LOC).Interval(100));
      var json = ElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""loc.sort"" :  {
                histogram : {
                    field : ""loc.sort"",
                    interval : 100
                } 
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
    [Test]
    public void HistogramTestTimeInterval()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .QueryRaw(@"{ raw : ""query""}")
        .FacetHistogram(h => h.OnField(f => f.StartedOn).TimeInterval("1.5h"));
      var json = ElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""startedOn"" :  {
                histogram : {
                    field : ""startedOn"",
                    time_interval : ""1.5h""
                } 
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
    [Test]
    public void HistogramTestTimeSpanInterval()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .QueryRaw(@"{ raw : ""query""}")
        .FacetHistogram(h => h
          .OnField(f => f.StartedOn)
          .TimeInterval(TimeSpan.FromHours(1.5))
        );
      var json = ElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""startedOn"" :  {
                histogram : {
                    field : ""startedOn"",
                    time_interval : ""01:30:00""
                } 
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
    [Test]
    public void HistogramTestKeyField()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .QueryRaw(@"{ raw : ""query""}")
        .FacetHistogram("needs_a_name", h => h
          .KeyField("key_field_name")
          .ValueField("value_field_name")
          .Interval(100)
        );
      var json = ElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""needs_a_name"" :  {
                histogram : {
                    key_field : ""key_field_name"",
                    value_field : ""value_field_name"",
                    interval : 100
                } 
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
    [Test]
    public void HistogramTestKeyScript()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .QueryRaw(@"{ raw : ""query""}")
        .FacetHistogram("needs_a_name", h => h
          .KeyScript("doc['date'].date.minuteOfHour")
          .ValueScript("doc['num1'].value")
          .Interval(100)
        );
      var json = ElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""needs_a_name"" :  {
                histogram : {
                    key_script : ""doc['date'].date.minuteOfHour"",
                    value_script : ""doc['num1'].value"",
                    interval : 100
                } 
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
    [Test]
    public void HistogramTestKeyScriptParams()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .QueryRaw(@"{ raw : ""query""}")
        .FacetHistogram("needs_a_name", h => h
          .KeyScript("doc['date'].date.minuteOfHour * factor1")
          .ValueScript("doc['num1'].value * factor2")
          .Interval(100)
          .Params(p=>p
            .Add("factor1", 2)
            .Add("factor2", 3)
            .Add("randomString", "stringy")
          )
        );
      var json = ElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""needs_a_name"" :  {
                histogram : {
                    key_script : ""doc['date'].date.minuteOfHour * factor1"",
                    value_script : ""doc['num1'].value * factor2"",
                    interval : 100,
                    params : {
                      factor1 : 2,
                      factor2 : 3,
                      randomString: ""stringy""
                    }
                } 
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
  }
}
