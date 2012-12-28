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

namespace Nest.Tests.Unit.FilterJson.BoolCombinations
{
	[TestFixture]
	public class BoolCombinationTests : BaseJsonTests
	{
		[Test]
		public void CombineNotAnd()
		{
			var s = !Filter<ElasticSearchProject>.Term(f => f.Name, "foo") 
			 && !Filter<ElasticSearchProject>.Term(f => f.Name, "bar")
			 && Filter<ElasticSearchProject>.Term(f => f.Name, "derp");
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void CombineNotAndShould()
		{
			//The should may not join the boolean Filter as this would change the semantics.
			//expected is a should Filter with a nested boolean Filter that has a must and must not clause 
			//and a simple term Filter
			var s = !Filter<ElasticSearchProject>.Term(f => f.Name, "foo")
			 && !Filter<ElasticSearchProject>.Term(f => f.Name, "bar")
			 && Filter<ElasticSearchProject>.Term(f => f.Name, "derp")
			 || Filter<ElasticSearchProject>.Term(f => f.Name, "blah");
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void ForceBranchOr()
		{   
			var s = (Filter<ElasticSearchProject>.Term(f => f.Name, "foo")
			 && Filter<ElasticSearchProject>.Term(f => f.Name, "bar")
			 || Filter<ElasticSearchProject>.Term(f => f.Name, "blah"))
			 ||
			 (Filter<ElasticSearchProject>.Term(f => f.Name, "foo2")
			 && Filter<ElasticSearchProject>.Term(f => f.Name, "bar2")
			 || Filter<ElasticSearchProject>.Term(f => f.Name, "blah2"));
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void ForceBranchAnd() 
		{ 
			var s = (Filter<ElasticSearchProject>.Term(f => f.Name, "foo")
			 && !Filter<ElasticSearchProject>.Term(f => f.Name, "bar")
			 && Filter<ElasticSearchProject>.Term(f => f.Name, "blah"))
			 &&
			 (Filter<ElasticSearchProject>.Term(f => f.Name, "foo2")
			 || Filter<ElasticSearchProject>.Term(f => f.Name, "bar2")
			 || !Filter<ElasticSearchProject>.Term(f => f.Name, "blah2"));
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void RandomOrderAnd()
		{ 
			var s = Filter<ElasticSearchProject>.Term(f => f.Name, "foo")
			 && !Filter<ElasticSearchProject>.Term(f => f.Name, "bar")
			 && Filter<ElasticSearchProject>.Term(f => f.Name, "blah");
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void RandomOrderOr()
		{ 
			var s = Filter<ElasticSearchProject>.Term(f => f.Name, "foo2")
			 || Filter<ElasticSearchProject>.Term(f => f.Name, "bar2")
			 || !Filter<ElasticSearchProject>.Term(f => f.Name, "blah2");
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
    [Test]
    public void OrWithExists()
    {
      var s = Filter<ElasticSearchProject>.Exists(f=>f.Name)
       || Filter<ElasticSearchProject>.Term(f => f.Name, "bar2")
       || Filter<ElasticSearchProject>.Term(f => f.Name, "blah2");
      this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
    }
    [Test]
    public void OrWithExistsLambda()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .Filter(q=>
          q.Term(f => f.Name, "bar2")
          || q.Term(f => f.Name, "blah2")
          || q.Exists(f => f.Name)
        );
      this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
    }
	[Test]
    public void OrWithExistsLambdaSimple()
	{
		var s = new SearchDescriptor<ElasticSearchProject>()
		  .Filter(q =>
			q.Term(f => f.Name, "blah2")
      || q.Exists(f => f.Name)
		  );
		this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
	}
    [Test]
    public void OrSimpleLambda()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .Filter(q =>
          q.Term(f => f.Name, "foo2")
          || q.Term(f => f.Name, "bar2")
          || q.Term(f => f.Name, "blah2")
        );
      this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
    }
	} 
}
