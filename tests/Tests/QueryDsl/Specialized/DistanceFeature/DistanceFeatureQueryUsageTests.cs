// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.Specialized.DistanceFeature
{
	/**
	* Boosts the relevance score of documents closer to a provided `origin` date or point. For example, you can use this query to give
	* more weight to documents closer to a certain date or location.
	*
	* See the Elasticsearch documentation on {ref_current}/query-dsl-distance-feature-query.html[distance feature query] for more details.
	*
	* [float]
	* == Using a date
	*
	* An instance of `DateMath` can be provided as the `origin`, with `pivot` being a `Time` from the origin
	*/
	[SkipVersion("<7.2.0", "Implemented in version 7.2.0")]
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

		protected override QueryContainer QueryInitializer => new DistanceFeatureQuery
		{
			Boost = 1.1,
			Field = Infer.Field<Project>(f => f.StartedOn),
			Origin = DateMath.FromString("now"),
			Pivot = new Time("7d")
		};

		protected override object QueryJson =>
			new
			{
				distance_feature = new
				{
					boost = 1.1,
					field = "startedOn",
					origin = "now",
					pivot = "7d"
				}
			};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.DistanceFeature(rf => rf
				.Boost(1.1)
				.Field(f => f.StartedOn)
				.Origin(DateMath.FromString("now"))
				.Pivot(new Time("7d"))
			);
	}

	/**[float]
	* == Using a location
	*
	* You can use the distance_feature query to find the nearest neighbors to a location. You can also use the query in a bool
	* search''s should filter to add boosted relevance scores to the bool query's scores.
	*
	* An instance of `GeoCoordinate` can be provided as the `origin`, with `pivot` being a `Distance` from the origin
	*/
	[SkipVersion("<7.2.0", "Implemented in version 7.2.0")]
	public class DistanceFeatureDistanceQueryUsageTests : QueryDslUsageTestsBase
	{
		public DistanceFeatureDistanceQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

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
			Name = "name",
			Boost = 1.1,
			Field = Infer.Field<Project>(f => f.LeadDeveloper.Location),
			Origin = new GeoCoordinate(70, -70),
			Pivot = new Distance(100, DistanceUnit.Miles)
		};

		protected override object QueryJson =>
			new
			{
				distance_feature = new
				{
					_name = "name",
					boost = 1.1,
					field = "leadDeveloper.location",
					origin = new [] { -70.0, 70.0 },
					pivot = "100mi"
				}
			};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.DistanceFeature(rf => rf
				.Name("name")
				.Boost(1.1)
				.Field(f => f.LeadDeveloper.Location)
				.Origin(new GeoCoordinate(70, -70))
				.Pivot(new Distance(100, DistanceUnit.Miles))
			);
	}
}
