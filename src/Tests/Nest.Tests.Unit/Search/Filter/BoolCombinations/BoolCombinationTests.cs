using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Filter.BoolCombinations
{
	[TestFixture]
	public class BoolCombinationTests : BaseJsonTests
	{
		[Test]
		public void CombineNotAnd()
		{
			var s = !Filter<ElasticsearchProject>.Term(f => f.Name, "foo")
			 && !Filter<ElasticsearchProject>.Term(f => f.Name, "bar")
			 && Filter<ElasticsearchProject>.Term(f => f.Name, "derp");
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void CombineNotAndShould()
		{
			//The should may not join the boolean Filter as this would change the semantics.
			//expected is a should Filter with a nested boolean Filter that has a must and must not clause 
			//and a simple term Filter
			var s = !Filter<ElasticsearchProject>.Term(f => f.Name, "foo")
			 && !Filter<ElasticsearchProject>.Term(f => f.Name, "bar")
			 && Filter<ElasticsearchProject>.Term(f => f.Name, "derp")
			 || Filter<ElasticsearchProject>.Term(f => f.Name, "blah");
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void ForceBranchOr()
		{
			var s = (Filter<ElasticsearchProject>.Term(f => f.Name, "foo")
			 && Filter<ElasticsearchProject>.Term(f => f.Name, "bar")
			 || Filter<ElasticsearchProject>.Term(f => f.Name, "blah"))
			 ||
			 (Filter<ElasticsearchProject>.Term(f => f.Name, "foo2")
			 && Filter<ElasticsearchProject>.Term(f => f.Name, "bar2")
			 || Filter<ElasticsearchProject>.Term(f => f.Name, "blah2"));
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void ForceBranchAnd()
		{
			var s = (Filter<ElasticsearchProject>.Term(f => f.Name, "foo")
			 && !Filter<ElasticsearchProject>.Term(f => f.Name, "bar")
			 && Filter<ElasticsearchProject>.Term(f => f.Name, "blah"))
			 &&
			 (Filter<ElasticsearchProject>.Term(f => f.Name, "foo2")
			 || Filter<ElasticsearchProject>.Term(f => f.Name, "bar2")
			 || !Filter<ElasticsearchProject>.Term(f => f.Name, "blah2"));
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void RandomOrderAnd()
		{
			var s = Filter<ElasticsearchProject>.Term(f => f.Name, "foo")
			 && !Filter<ElasticsearchProject>.Term(f => f.Name, "bar")
			 && Filter<ElasticsearchProject>.Term(f => f.Name, "blah");
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void RandomOrderOr()
		{
			var s = Filter<ElasticsearchProject>.Term(f => f.Name, "foo2")
			 || Filter<ElasticsearchProject>.Term(f => f.Name, "bar2")
			 || !Filter<ElasticsearchProject>.Term(f => f.Name, "blah2");
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void OrWithExists()
		{
			var s = Filter<ElasticsearchProject>.Exists(f => f.Name)
			 || Filter<ElasticsearchProject>.Term(f => f.Name, "bar2")
			 || Filter<ElasticsearchProject>.Term(f => f.Name, "blah2");
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void OrWithExistsLambda()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
			  .Filter(q =>
				q.Term(f => f.Name, "bar2")
				|| q.Term(f => f.Name, "blah2")
				|| q.Exists(f => f.Name)
			  );
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void OrWithExistsLambdaSimple()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
			  .Filter(q =>
				q.Term(f => f.Name, "blah2")
		  || q.Exists(f => f.Name)
			  );
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void OrSimpleLambda()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
			  .Filter(q =>
				q.Term(f => f.Name, "foo2")
				|| q.Term(f => f.Name, "bar2")
				|| q.Term(f => f.Name, "blah2")
			  );
			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}

		
	}
}
