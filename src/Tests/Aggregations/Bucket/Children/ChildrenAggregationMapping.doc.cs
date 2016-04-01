using Tests.Framework;
using Tests.Framework.MockData;

namespace Tests.Aggregations.Bucket.Children
{
	/** == Child Aggregation Mapping */
	public class ChildrenAggregationMapping
	{
		private void MappingExample()
		{
			/** To use the {ref_current}/search-aggregations-bucket-children-aggregation.html[Children Aggregation],
			 * you have to make sure a `_parent` mapping is in place.
			 *
			 * Here we create the project index with two mapped types, `Project` and `CommitActivity` and
			 * add a `_parent` mapping to `CommitActivity`, specifying the `Project` type as the parent */
			var createProjectIndex = TestClient.GetClient().CreateIndex(typeof(Project), c => c
				.Mappings(map => map
					.Map<Project>(tm => tm.AutoMap())
					.Map<CommitActivity>(tm => tm
						.Parent<Project>() //<1> Set the parent of `CommitActivity` to the `Project` type
					)
				)
			);
		}
	}
}
