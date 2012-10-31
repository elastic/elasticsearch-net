using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Nest;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.FilterJson.BoolFiltersInFacets
{
	[TestFixture]
  public class BoolFiltersInFacetsTests : BaseJsonTests
	{
		[Test]
		public void FacetFilterOr()
		{
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Take(10)
        .FacetFilter("myfilter", ff=> 
          ff.Term(f=>f.Name, "foo")
          || ff.Term(f => f.Name, "bar")
        );
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
    [Test]
    public void FacetGeoDistanceFilter()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Take(10)
        .FacetGeoDistance(gf=>gf
          .OnField(f=>f.Name)
          .FacetFilter(ff =>
            ff.Term(f => f.Name, "foo")
            || ff.Term(f => f.Name, "bar")
          )
        );
      this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
    }
    [Test]
    public void FacetDateHistogramFilter()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Take(10)
        .FacetDateHistogram(gf => gf
          .OnField(f => f.Name)
          .FacetFilter(ff =>
            ff.Term(f => f.Name, "foo")
            || ff.Term(f => f.Name, "bar")
          )
        );
      this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
    }
    [Test]
    public void FacetHistogramFilter()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Take(10)
        .FacetHistogram(gf => gf
          .OnField(f => f.Name)
          .FacetFilter(ff =>
            ff.Term(f => f.Name, "foo")
            || ff.Term(f => f.Name, "bar")
          )
        );
      this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
    }
    [Test]
    public void FacetRangeFilter()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Take(10)
        .FacetRange<int>(gf => gf
          .OnField(f => f.LOC)
          .FacetFilter(ff =>
            ff.Term(f => f.Name, "foo")
            || ff.Term(f => f.Name, "bar")
          )
        );
      this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
    }
    [Test]
    public void FacetStatisticalFilter()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Take(10)
        .FacetStatistical(gf => gf
          .OnField(f => f.LOC)
          .FacetFilter(ff =>
            ff.Term(f => f.Name, "foo")
            || ff.Term(f => f.Name, "bar")
          )
        );
      this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
    }
    [Test]
    public void FacetTermFilter()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Take(10)
        .FacetTerm(gf => gf
          .OnField(f => f.LOC)
          .FacetFilter(ff =>
            ff.Term(f => f.Name, "foo")
            || ff.Term(f => f.Name, "bar")
          )
        );
      this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
    }
    [Test]
    public void FacetTermsStatsFilter()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Take(10)
        .FacetTermsStats(gf => gf
          .KeyField(f => f.LOC)
          .ValueField(f => f.Name)
          .FacetFilter(ff =>
            ff.Term(f => f.Name, "foo")
            || ff.Term(f => f.Name, "bar")
          )
        );
      this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
    }
    [Test]
    public void FacetQueryFilter()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Take(10)
        .FacetQuery("myquery", gf => gf.Term(f=>f.Name, "foo") && gf.Term(f=>f.Name, "bar"));
      this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
    }		
	} 
}
