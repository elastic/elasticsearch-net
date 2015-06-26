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
			var s = new ValidateQueryDescriptor<ElasticsearchProject>()
				.AllIndices()
				.AllTypes()
				.Query(q=>q
					.Filtered(fq =>
						fq.Filter(ff =>
							ff.Term(f => f.Name, "foo")
							|| ff.Term(f => f.Name, "bar")
						)
					)
				);

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
		[Test]
		public void FilteredQueryCombinesUsingStatic()
		{
			var s = new ValidateQueryDescriptor<ElasticsearchProject>()
				.AllIndices()
				.AllTypes()
				.Query(q=>q
					.Filtered(fq =>
						fq.Filter(ff =>
							Query<ElasticsearchProject>.Term(f => f.Name, "foo")
							|| Query<ElasticsearchProject>.Term(f => f.Name, "bar")
						)
					)
				);

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
	}
}
