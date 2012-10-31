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

namespace Nest.Tests.Unit.QueryJson.Bool
{
	[TestFixture]
	public class StaticBoolQueryTests : BaseJsonTests
	{
		[Test]
		public void StaticBoolTest()
		{
			var s = Query<ElasticSearchProject>.Term(f=>f.Name, "foo")
				&& Query<ElasticSearchProject>.Term(f=>f.Name, "bar");

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void StaticBoolAssignTest()
		{
			var s = Query<ElasticSearchProject>.Term(f => f.Name, "foo");
			s &= Query<ElasticSearchProject>.Term(f => f.Name, "bar");

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void StaticBoolAssignNotTest()
		{
			var s = !Query<ElasticSearchProject>.Term(f => f.Name, "foo") 
			 && !Query<ElasticSearchProject>.Term(f => f.Name, "bar");

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test] 
		public void StaticBoolAssignMultipleTest()
		{ 
			var s = Query<ElasticSearchProject>.Term(f => f.Name, "foo");
			s &= Query<ElasticSearchProject>.Term(f => f.Name, "bar");
			s &= Query<ElasticSearchProject>.Term(f => f.Name, "blah");
			s &= Query<ElasticSearchProject>.Term(f => f.Name, "derp");

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void StaticBoolAssignMultipleOrTest()
		{ 
			var s = Query<ElasticSearchProject>.Term(f => f.Name, "foo");
			s |= Query<ElasticSearchProject>.Term(f => f.Name, "bar");
			s |= Query<ElasticSearchProject>.Term(f => f.Name, "blah");
			s |= Query<ElasticSearchProject>.Term(f => f.Name, "derp");

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void StaticBoolAssignMultipleOrCombineWithAndTest()
		{   
			var or1 = Query<ElasticSearchProject>.Term(f => f.Name, "foo");
			or1 |= Query<ElasticSearchProject>.Term(f => f.Name, "bar");
			or1 |= Query<ElasticSearchProject>.Term(f => f.Name, "blah");

			var or2 = Query<ElasticSearchProject>.Term(f => f.Name, "foo");
			or2 |= Query<ElasticSearchProject>.Term(f => f.Name, "bar");
			or2 |= Query<ElasticSearchProject>.Term(f => f.Name, "blah");

			var s = or1 && or2;

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void SearchDescriptorPicksUpBaseQueryTest()
		{
			var or1 = Query<ElasticSearchProject>.Term(f => f.Name, "foo");
			or1 |= Query<ElasticSearchProject>.Term(f => f.Name, "bar");
			or1 |= Query<ElasticSearchProject>.Term(f => f.Name, "blah");

			var or2 = Query<ElasticSearchProject>.Term(f => f.Name, "foo");
			or2 |= Query<ElasticSearchProject>.Term(f => f.Name, "bar");
			or2 |= Query<ElasticSearchProject>.Term(f => f.Name, "blah");
			 
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(20)
				.Query(or1 && or2);
			

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void SearchDescriptorPicksUpBaseQueryWithinLambdaTest()
		{ 
			var or1 = Query<ElasticSearchProject>.Term(f => f.Name, "foo");
			or1 |= Query<ElasticSearchProject>.Term(f => f.Name, "bar");
			or1 |= Query<ElasticSearchProject>.Term(f => f.Name, "blah");

			var or2 = Query<ElasticSearchProject>.Term(f => f.Name, "foo2");
			or2 |= Query<ElasticSearchProject>.Term(f => f.Name, "bar2");
			or2 |= Query<ElasticSearchProject>.Term(f => f.Name, "blah2");

			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(20)
				.Query(q=> {
					return or1 && or2;
				});


			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void SearchDescriptorPicksUpBaseQueryWithinComplexLambdaTest()
		{ 
			var or2 = Query<ElasticSearchProject>.Term(f => f.Name, "foo2");
			or2 |= Query<ElasticSearchProject>.Term(f => f.Name, "bar2");
			or2 |= Query<ElasticSearchProject>.Term(f => f.Name, "blah2");

			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(20)
				.Query(q =>
					(q.Term(f => f.Name, "foo") || q.Term(f => f.Name, "bar") || q.Term(f => f.Name, "blah"))
					&& or2
				);


			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void BranchLambdaTest()
		{ 
			var or2 = Query<ElasticSearchProject>.Term(f => f.Name, "foo2");
			or2 |= Query<ElasticSearchProject>.Term(f => f.Name, "bar2");
			or2 |= Query<ElasticSearchProject>.Term(f => f.Name, "blah2");

			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(20)
				.Query(q =>
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
