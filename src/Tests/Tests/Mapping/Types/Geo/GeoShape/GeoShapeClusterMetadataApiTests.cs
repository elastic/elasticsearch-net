using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Mapping.Types.Core.GeoShape
{
	public class GeoShapeClusterMetadataApiTests : ApiIntegrationTestBase<WritableCluster, IPutMappingResponse, IPutMappingRequest, PutMappingDescriptor<Project>,
		PutMappingRequest<Project>>
	{
		public GeoShapeClusterMetadataApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var index in values.Values) client.CreateIndex(index, CreateIndexSettings).ShouldBeValid();
			var indices = Infer.Indices(values.Values.Select(i => (IndexName)i));
			client.ClusterHealth(f => f.WaitForStatus(WaitForStatus.Yellow).Index(indices))
				.ShouldBeValid();
		}

		protected virtual ICreateIndexRequest CreateIndexSettings(CreateIndexDescriptor create) => create;

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<PutMappingDescriptor<Project>, IPutMappingRequest> Fluent => f => f
			.Index(CallIsolatedValue)
			.Properties(FluentProperties);

		private static Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.GeoShape(s => s
				.Name(p => p.Location)
				.Tree(GeoTree.Quadtree)
				.Orientation(GeoOrientation.ClockWise)
				.Strategy(GeoStrategy.Recursive)
				.TreeLevels(3)
				.PointsOnly()
				.DistanceErrorPercentage(1.0)
				.Coerce()
			);

		private static IProperties InitializerProperties => new Properties
		{
			{
				"location", new GeoShapeProperty
				{
					Tree = GeoTree.Quadtree,
					Orientation = GeoOrientation.ClockWise,
					Strategy = GeoStrategy.Recursive,
					TreeLevels = 3,
					PointsOnly = true,
					DistanceErrorPercentage = 1.0,
					Coerce = true
				}
			}
		};

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override PutMappingRequest<Project> Initializer => new PutMappingRequest<Project>(CallIsolatedValue, typeof(Project))
		{
			Properties = InitializerProperties
		};

		protected override string UrlPath => $"/{CallIsolatedValue}/doc/_mapping";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Map(f),
			(client, f) => client.MapAsync(f),
			(client, r) => client.Map(r),
			(client, r) => client.MapAsync(r)
		);

		protected override void ExpectResponse(IPutMappingResponse response)
		{
			// Ensure metadata can be deserialised
			var metadata = Client.ClusterState(r => r.Metric(ClusterStateMetric.Metadata));
			metadata.IsValid.Should().BeTrue();
		}
	}
}
