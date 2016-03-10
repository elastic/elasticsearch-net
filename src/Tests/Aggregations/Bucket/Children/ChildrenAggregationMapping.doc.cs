using Tests.Framework;
using Tests.Framework.MockData;

namespace Tests.Aggregations.Bucket.Children
{
	/** == Child Aggregation Mapping
	*/
	public class ChildrenAggregationMapping
	{
		private void MappingExample()
		{
			/** To use the child aggregation you have to make sure 
			 *  a `_parent` mapping is in place, here we create the project
			 *  index with two mapped types, `project` and `commitactivity` and 
			 *  we add a `_parent` mapping from `commitactivity` to `parent` */
			var createProjectIndex = TestClient.GetClient().CreateIndex(typeof(Project), c => c
				.Mappings(map => map
					.Map<Project>(tm => tm.AutoMap())
					.Map<CommitActivity>(tm => tm
						.Parent<Project>()
					)
				)
			);
		}
	}
}
