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

namespace Nest.Tests.Unit.FilterJson.FilterCalls
{
	[TestFixture]
  public class FilterCallsTests : BaseJsonTests
	{
		[Test]
		public void AndFilterCombines()
		{
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Take(10)
        .Filter(ff=>
          ff.And(af=> 
            af.Term(f=>f.Name, "foo")
            || af.Term(f => f.Name, "bar")
          ));
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
    [Test]
    public void AndFilterMultipleCombines()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Take(10)
        .Filter(ff =>
          ff.And(af =>
            af.Term(f => f.Name, "foo")
            || af.Term(f => f.Name, "bar")
            , af =>
               af.Term(f => f.Name, "foo2")
            || af.Term(f => f.Name, "bar2")
          ));
      this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
    }
    [Test]
    public void OrFilterCombines()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Take(10)
        .Filter(ff =>
          ff.Or(of =>
            of.Term(f => f.Name, "foo")
            && of.Term(f => f.Name, "bar")
          ));
      this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
    }
    [Test]
    public void OrFilterMultipleCombines()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Take(10)
        .Filter(ff =>
          ff.Or(of =>
            of.Term(f => f.Name, "foo")
            && of.Term(f => f.Name, "bar")
            , of =>
               of.Term(f => f.Name, "foo2")
            && of.Term(f => f.Name, "bar2")
          ));
      this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
    }

    [Test]
    public void NotFilterCombines()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Take(10)
        .Filter(ff =>
          ff.Not(of =>
            of.Term(f => f.Name, "foo")
            && of.Term(f => f.Name, "bar")
          ));
      this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
    }
   
	} 
}
