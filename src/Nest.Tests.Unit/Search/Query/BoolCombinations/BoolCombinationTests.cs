using System;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Diagnostics;

namespace Nest.Tests.Unit.Search.Query.BoolCombinations
{
	[TestFixture]
	public class BoolCombinationTests : BaseJsonTests
	{
		[Test]
		public void CombineNotAnd()
		{
			var s = !Query<ElasticSearchProject>.Term(f => f.Name, "foo") 
			 && !Query<ElasticSearchProject>.Term(f => f.Name, "bar")
			 && Query<ElasticSearchProject>.Term(f => f.Name, "derp");
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void CombineNotAndShould()
		{
			//The should may not join the boolean query as this would change the semantics.
			//expected is a should query with a nested boolean query that has a must and must not clause 
			//and a simple term query
			var sw = Stopwatch.StartNew();
			var s = !Query<ElasticSearchProject>.Term(f => f.Name, "foo")
			 && !Query<ElasticSearchProject>.Term(f => f.Name, "bar")
			 && Query<ElasticSearchProject>.Term(f => f.Name, "derp")
			 || Query<ElasticSearchProject>.Term(f => f.Name, "blah");
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
			sw.Stop();
			Assert.LessOrEqual(sw.ElapsedMilliseconds, 1000);
		}
		[Test]
		public void ForceBranchOr()
		{   
			var s = (Query<ElasticSearchProject>.Term(f => f.Name, "foo")
			 && Query<ElasticSearchProject>.Term(f => f.Name, "bar")
			 || Query<ElasticSearchProject>.Term(f => f.Name, "blah"))
			 ||
			 (Query<ElasticSearchProject>.Term(f => f.Name, "foo2")
			 && Query<ElasticSearchProject>.Term(f => f.Name, "bar2")
			 || Query<ElasticSearchProject>.Term(f => f.Name, "blah2"));
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void ForceBranchAnd() 
		{ 
			var s = (Query<ElasticSearchProject>.Term(f => f.Name, "foo")
			 && !Query<ElasticSearchProject>.Term(f => f.Name, "bar")
			 && Query<ElasticSearchProject>.Term(f => f.Name, "blah"))
			 &&
			 (Query<ElasticSearchProject>.Term(f => f.Name, "foo2")
			 || Query<ElasticSearchProject>.Term(f => f.Name, "bar2")
			 || !Query<ElasticSearchProject>.Term(f => f.Name, "blah2"));
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void RandomOrderAnd()
		{ 
			var s = Query<ElasticSearchProject>.Term(f => f.Name, "foo")
			 && !Query<ElasticSearchProject>.Term(f => f.Name, "bar")
			 && Query<ElasticSearchProject>.Term(f => f.Name, "blah");
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void RandomOrderOr()
		{ 
			var s = Query<ElasticSearchProject>.Term(f => f.Name, "foo2")
			 || Query<ElasticSearchProject>.Term(f => f.Name, "bar2")
			 || !Query<ElasticSearchProject>.Term(f => f.Name, "blah2");
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
    [Test]
    public void OrWithQueryString()
    {
      var s = Query<ElasticSearchProject>.QueryString(q => q.Query("blah"))
       || Query<ElasticSearchProject>.Term(f => f.Name, "bar2")
       || Query<ElasticSearchProject>.Term(f => f.Name, "blah2");
      this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
    }
    [Test]
    public void OrWithQueryStringLambda()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .Query(q=>
          q.Term(f => f.Name, "bar2")
          || q.Term(f => f.Name, "blah2")
          || q.QueryString(qs => qs.Query("blah"))
        );
      this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
    }
	[Test]
	public void OrWithQueryStringLambdaSimple()
	{
		var s = new SearchDescriptor<ElasticSearchProject>()
		  .Query(q =>
			q.Term(f => f.Name, "blah2")
			|| q.QueryString(qs => qs.Query("blah"))
		  );
		this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
	}
    [Test]
    public void OrSimpleLambda()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .Query(q =>
          q.Term(f => f.Name, "foo2")
          || q.Term(f => f.Name, "bar2")
          || q.Term(f => f.Name, "blah2")
        );
      this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
    }
	} 
}
