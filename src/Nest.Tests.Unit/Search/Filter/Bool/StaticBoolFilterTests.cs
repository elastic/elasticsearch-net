using System.Reflection;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Filter.Bool
{
	[TestFixture]
	public class StaticBoolFilterTests : BaseJsonTests
	{
		[Test]
		public void StaticBoolTest()
		{
			var s = Filter<ElasticsearchProject>.Term(f=>f.Name, "foo")
				&& Filter<ElasticsearchProject>.Term(f=>f.Name, "bar");

			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void StaticBoolAssignTest()
		{
			var s = Filter<ElasticsearchProject>.Term(f => f.Name, "foo");
			s &= Filter<ElasticsearchProject>.Term(f => f.Name, "bar");

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void StaticBoolAssignNotTest()
		{
			var s = !Filter<ElasticsearchProject>.Term(f => f.Name, "foo") 
			 && !Filter<ElasticsearchProject>.Term(f => f.Name, "bar");

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test] 
		public void StaticBoolAssignMultipleTest()
		{ 
			var s = Filter<ElasticsearchProject>.Term(f => f.Name, "foo");
			s &= Filter<ElasticsearchProject>.Term(f => f.Name, "bar");
			s &= Filter<ElasticsearchProject>.Term(f => f.Name, "blah");
			s &= Filter<ElasticsearchProject>.Term(f => f.Name, "derp");

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void StaticBoolAssignMultipleOrTest()
		{ 
			var s = Filter<ElasticsearchProject>.Term(f => f.Name, "foo");
			s |= Filter<ElasticsearchProject>.Term(f => f.Name, "bar");
			s |= Filter<ElasticsearchProject>.Term(f => f.Name, "blah");
			s |= Filter<ElasticsearchProject>.Term(f => f.Name, "derp");

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void StaticBoolAssignMultipleOrCombineWithAndTest()
		{   
			var or1 = Filter<ElasticsearchProject>.Term(f => f.Name, "foo");
			or1 |= Filter<ElasticsearchProject>.Term(f => f.Name, "bar");
			or1 |= Filter<ElasticsearchProject>.Term(f => f.Name, "blah");

			var or2 = Filter<ElasticsearchProject>.Term(f => f.Name, "foo2");
			or2 |= Filter<ElasticsearchProject>.Term(f => f.Name, "bar2");
			or2 |= Filter<ElasticsearchProject>.Term(f => f.Name, "blah2");

			var s = or1 && or2;

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void SearchDescriptorPicksUpBaseFilterTest()
		{
			var or1 = Filter<ElasticsearchProject>.Term(f => f.Name, "foo");
			or1 |= Filter<ElasticsearchProject>.Term(f => f.Name, "bar");
			or1 |= Filter<ElasticsearchProject>.Term(f => f.Name, "blah");

			var or2 = Filter<ElasticsearchProject>.Term(f => f.Name, "foo");
			or2 |= Filter<ElasticsearchProject>.Term(f => f.Name, "bar");
			or2 |= Filter<ElasticsearchProject>.Term(f => f.Name, "blah");
			 
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(20)
				.Filter(or1 && or2);
			

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void SearchDescriptorPicksUpBaseFilterWithinLambdaTest()
		{ 
			var or1 = Filter<ElasticsearchProject>.Term(f => f.Name, "foo");
			or1 |= Filter<ElasticsearchProject>.Term(f => f.Name, "bar");
			or1 |= Filter<ElasticsearchProject>.Term(f => f.Name, "blah");

			var or2 = Filter<ElasticsearchProject>.Term(f => f.Name, "foo2");
			or2 |= Filter<ElasticsearchProject>.Term(f => f.Name, "bar2");
			or2 |= Filter<ElasticsearchProject>.Term(f => f.Name, "blah2");

			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(20)
				.Filter(q=> {
					return or1 && or2;
				});


			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void SearchDescriptorPicksUpBaseFilterWithinComplexLambdaTest()
		{ 
			var or2 = Filter<ElasticsearchProject>.Term(f => f.Name, "foo2");
			or2 |= Filter<ElasticsearchProject>.Term(f => f.Name, "bar2");
			or2 |= Filter<ElasticsearchProject>.Term(f => f.Name, "blah2");

			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(20)
				.Filter(q =>
					(q.Term(f => f.Name, "foo") || q.Term(f => f.Name, "bar") || q.Term(f => f.Name, "blah"))
					&& or2
				);


			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void BranchLambdaTest()
		{ 
			var or2 = Filter<ElasticsearchProject>.Term(f => f.Name, "foo2");
			or2 |= Filter<ElasticsearchProject>.Term(f => f.Name, "bar2");
			or2 |= Filter<ElasticsearchProject>.Term(f => f.Name, "blah2");

			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(20)
				.Filter(q =>
					{
						var b = (q.Term(f => f.Name, "foo") || q.Term(f => f.Name, "bar") || q.Term(f => f.Name, "blah"));
						if (1 == 1)
						{
							b &= or2;
						}
						return b;
					}
				);


			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
	} 
}
