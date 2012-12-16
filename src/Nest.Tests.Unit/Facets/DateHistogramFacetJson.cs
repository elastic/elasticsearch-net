using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Facets
{
  [TestFixture]
  public class DateHistogramFacetJson
  {
    [Test]
    public void DateHistogram()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .QueryRaw(@"{ raw : ""query""}")
        .FacetDateHistogram(h => h
          .OnField(f => f.StartedOn)
          .Interval(DateInterval.Day)
          .Factor(1000)
      );
      var json = TestElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""startedOn"" :  {
                date_histogram : {
                    field : ""startedOn"",
                    interval : ""day"",
                    factor: 1000

                } 
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
    [Test]
    public void DateHistogramRounding()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .QueryRaw(@"{ raw : ""query""}")
        .FacetDateHistogram(h => h
          .OnField(f => f.StartedOn)
          .Interval(DateInterval.Day, DateRounding.Half_Floor)
      );
      var json = TestElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""startedOn"" :  {
                date_histogram : {
                    field : ""startedOn"",
                    interval : ""day:half_floor""
                } 
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
    [Test]
    public void DateHistogramTimeZone()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .QueryRaw(@"{ raw : ""query""}")
        .FacetDateHistogram(h => h
          .OnField(f => f.StartedOn)
          .Interval(DateInterval.Day, DateRounding.Half_Floor)
          .TimeZone("-2")
      );
      var json = TestElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""startedOn"" :  {
                date_histogram : {
                    field : ""startedOn"",
                    interval : ""day:half_floor"",
                    time_zone : ""-2""
                } 
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
    [Test]
    public void DateHistogramTimeZones()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .QueryRaw(@"{ raw : ""query""}")
        .FacetDateHistogram(h => h
          .OnField(f => f.StartedOn)
          .Interval(DateInterval.Day, DateRounding.Half_Floor)
          .TimeZone("-2") //should be unset because we later specify pre_zone
          .TimeZones(Pre: "-3", Post: "-4")
      );
      var json = TestElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""startedOn"" :  {
                date_histogram : {
                    field : ""startedOn"",
                    interval : ""day:half_floor"",
                    pre_zone : ""-3"",
                    post_zone : ""-4""
                } 
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
  }
}
