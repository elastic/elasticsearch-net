using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.UpdateQuery
{
	[TestFixture]
	public class UpdateQueriesTests : BaseJsonTests
	{
		[Test]
		public void FilteredQueryCombines()
		{
			var s = new RoutingQueryPathDescriptor<ElasticSearchProject>()
			  .AllIndices()
			  .AllTypes()
			  .Filtered(fq =>
				  fq.Filter(ff =>
					ff.Term(f => f.Name, "foo")
					|| ff.Term(f => f.Name, "bar")
				  )
			   );

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void FilteredQueryCombinesUsingStatic()
		{
			var s = new RoutingQueryPathDescriptor<ElasticSearchProject>()
			  .AllIndices()
			  .AllTypes()
			  .Filtered(fq =>
				  fq.Filter(ff =>
					Filter<ElasticSearchProject>.Term(f => f.Name, "foo")
					|| Filter<ElasticSearchProject>.Term(f => f.Name, "bar")
				  )
			   );

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
	}
}
