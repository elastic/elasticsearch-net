using System;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.Xunit;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Aggregations.Metric.GeoCentroid
{
	/**
	 * A metric aggregation that computes the weighted centroid from all coordinate values
	 * for a Geo-point datatype field.
	 *
	 * Be sure to read the Elasticsearch documentation on {ref_current}/search-aggregations-metrics-geocentroid-aggregation.html[Geo Centroid Aggregation]
	 */
	public class GeoCentroidAggregationUsageTests : AggregationUsageTestBase
	{
		public GeoCentroidAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			centroid = new
			{
				geo_centroid = new
				{
					field = "location"
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.GeoCentroid("centroid", gb => gb
				.Field(p => p.Location)
			);

		protected override AggregationDictionary InitializerAggs =>
			new GeoCentroidAggregation("centroid", Infer.Field<Project>(p => p.Location));

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var centroid = response.Aggregations.GeoCentroid("centroid");
			centroid.Should().NotBeNull();
			centroid.Count.Should().BeGreaterThan(0);
			centroid.Location.Should().NotBeNull();

			centroid.Location.Latitude.Should().NotBe(0);
			centroid.Location.Longitude.Should().NotBe(0);
		}
	}

	/**
	 *[float]
	 *[[geo-centroid-sub-aggregation]]
	 *=== Geo Centroid Sub Aggregation
	 *
	 * The `geo_centroid` aggregation is more interesting when combined as a sub-aggregation to other bucket aggregations
	 */
	public class NestedGeoCentroidAggregationUsageTests : AggregationUsageTestBase
	{
		public NestedGeoCentroidAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			projects = new
			{
				terms = new
				{
					field = "name"
				},
				aggs = new
				{
					centroid = new
					{
						geo_centroid = new
						{
							field = "location"
						}
					}
				}
			}
		};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.Terms("projects", t => t
				.Field(p => p.Name)
				.Aggregations(sa => sa
					.GeoCentroid("centroid", gb => gb
						.Field(p => p.Location)
					)
				)
			);

		protected override AggregationDictionary InitializerAggs =>
			new TermsAggregation("projects")
			{
				Field = Infer.Field<Project>(p => p.Name),
				Aggregations = new GeoCentroidAggregation("centroid", Infer.Field<Project>(p => p.Location))
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();

			var projects = response.Aggregations.Terms("projects");

			foreach (var bucket in projects.Buckets)
			{
				var centroid = bucket.GeoCentroid("centroid");
				centroid.Should().NotBeNull();
				centroid.Count.Should().BeGreaterThan(0);
				centroid.Location.Should().NotBeNull();

				centroid.Location.Latitude.Should().NotBe(0);
				centroid.Location.Longitude.Should().NotBe(0);
			}
		}
	}

	[NeedsTypedKeys]
	public class GeoCentroidNoResultsAggregationUsageTests : AggregationUsageTestBase
	{
		public GeoCentroidNoResultsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			centroid = new
			{
				geo_centroid = new { field = "location" }
			}
		};

		protected override QueryContainer QueryScope => new TermQuery { Field = Infer.Field<Project>(p=>p.Name), Value = "noresult" };
		protected override object QueryScopeJson { get; } = new {term = new {name = new {value = "noresult"}}};

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.GeoCentroid("centroid", gb => gb
				.Field(p => p.Location)
			);

		protected override AggregationDictionary InitializerAggs =>
			new GeoCentroidAggregation("centroid", Infer.Field<Project>(p => p.Location));

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();
			var centroid = response.Aggregations.GeoCentroid("centroid");
			centroid.Should().NotBeNull();
			centroid.Count.Should().Be(0);
		}
	}
}
