using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.Specialized.DistanceFeature
{
	/**
	* Boosts the relevance score of documents closer to a provided origin date or point. For example, you can use this query to give
	* more weight to documents closer to a certain date or location.
	* You can use the distance_feature query to find the nearest neighbors to a location. You can also use the query in a bool
	* search’s should filter to add boosted relevance scores to the bool query’s scores.
	*/
	public class DistanceFeatureQueryUsageTests : QueryDslUsageTestsBase
	{
		public DistanceFeatureQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IDistanceFeatureQuery>(a => a.DistanceFeature)
		{
			q =>
			{
				q.Field = null;
				q.Origin = null;
				q.Pivot = null;
			}
		};

		protected override QueryContainer QueryInitializer => new DistanceFeatureQuery()
		{
			Boost = 1.1,
			Field = Infer.Field<Project>(f => f.StartedOn),
			Origin = "now",
			Pivot = "7d"
		};

		protected override object QueryJson =>
			new { distance_feature = new { boost = 1.1, field = "startedOn", origin = "now", pivot = "7d" } };

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.DistanceFeature(rf => rf
				.Boost(1.1)
				.Field(f => f.StartedOn)
				.Origin("now")
				.Pivot("7d")
			);
	}
}
