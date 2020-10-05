// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using Elastic.Transport;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Types.Geo.GeoShape
{
	public class GeoShapeClusterMetadataApiTests : ApiIntegrationTestBase<WritableCluster, PutMappingResponse, IPutMappingRequest, PutMappingDescriptor<Project>,
		PutMappingRequest<Project>>
	{
		public GeoShapeClusterMetadataApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var index in values.Values) client.Indices.Create(index, CreateIndexSettings).ShouldBeValid();
			var indices = Infer.Indices(values.Values.Select(i => (IndexName)i));
			client.Cluster.Health(null, f => f.WaitForStatus(WaitForStatus.Yellow).Index(indices))
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
				.Name(p => p.LocationShape)
				.Orientation(GeoOrientation.ClockWise)
				.Strategy(GeoStrategy.Recursive)
				.Coerce()
			);

		private static IProperties InitializerProperties => new Properties
		{
			{
				"locationShape", new GeoShapeProperty
				{
					Orientation = GeoOrientation.ClockWise,
					Strategy = GeoStrategy.Recursive,
					Coerce = true
				}
			}
		};

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override PutMappingRequest<Project> Initializer => new PutMappingRequest<Project>(CallIsolatedValue)
		{
			Properties = InitializerProperties
		};

		protected override string UrlPath => $"/{CallIsolatedValue}/_mapping";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Map(f),
			(client, f) => client.MapAsync(f),
			(client, r) => client.Map(r),
			(client, r) => client.MapAsync(r)
		);

		protected override void ExpectResponse(PutMappingResponse response)
		{
			// Ensure metadata can be deserialised
			var metadata = Client.Cluster.State(CallIsolatedValue, r => r.Metric(ClusterStateMetric.Metadata));
			metadata.IsValid.Should().BeTrue();
		}
	}
}
