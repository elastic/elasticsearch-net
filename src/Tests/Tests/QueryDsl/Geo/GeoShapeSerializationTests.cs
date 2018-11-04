using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.QueryDsl.Geo
{
	public class GeoShapeSerializationTests
		: ApiIntegrationTestBase<IntrusiveOperationCluster,
			ISearchResponse<Domain.Shape>,
			ISearchRequest,
			SearchDescriptor<Domain.Shape>,
			SearchRequest<Domain.Shape>>
	{
		private const string Index = "shapes";

		private readonly IEnumerable<GeoCoordinate> _coordinates = Domain.Shape.Shapes.First().Envelope.Coordinates;

		public GeoShapeSerializationTests(IntrusiveOperationCluster cluster, EndpointUsage usage)
			: base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			query = new
			{
				geo_shape = new
				{
					_name = "named_query",
					boost = 1.1,
					ignore_unmapped = true,
					envelope = new
					{
						relation = "intersects",
						shape = new
						{
							type = "envelope",
							coordinates = _coordinates
						}
					}
				}
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<SearchDescriptor<Domain.Shape>, ISearchRequest> Fluent => s => s
			.Query(q => q
				.GeoShapeEnvelope(c => c
					.Name("named_query")
					.Boost(1.1)
					.Field(p => p.Envelope)
					.Coordinates(_coordinates)
					.Relation(GeoShapeRelation.Intersects)
					.IgnoreUnmapped(true)
				)
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override SearchRequest<Domain.Shape> Initializer => new SearchRequest<Domain.Shape>
		{
			Query = new GeoShapeEnvelopeQuery
			{
				Name = "named_query",
				Boost = 1.1,
				Field = Infer.Field<Domain.Shape>(p => p.Envelope),
				Shape = new EnvelopeGeoShape(_coordinates),
				Relation = GeoShapeRelation.Intersects,
				IgnoreUnmapped = true
			}
		};

		protected override string UrlPath => $"/shapes/doc/_search";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			if (client.IndexExists(Index).Exists)
				return;

			var createIndexResponse = client.CreateIndex(Index, c => c
				.Settings(s => s
					.NumberOfShards(1)
				)
				.Mappings(m => m
					.Map<Domain.Shape>(mm => mm
						.AutoMap()
						.Properties(p => p
							.GeoShape(g => g
								.Name(n => n.GeometryCollection)
							)
							.GeoShape(g => g
								.Name(n => n.Envelope)
							)
							.GeoShape(g => g
								.Name(n => n.Circle)
							)
						)
					)
				)
			);

			if (!createIndexResponse.IsValid)
				throw new Exception($"Error creating index for integration test: {createIndexResponse.DebugInformation}");

			var bulkResponse = Client.Bulk(b => b
				.IndexMany(Domain.Shape.Shapes)
				.Refresh(Refresh.WaitFor)
			);

			if (!bulkResponse.IsValid)
				throw new Exception($"Error indexing shapes for integration test: {bulkResponse.DebugInformation}");
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Search<Domain.Shape>(f),
			(client, f) => client.SearchAsync<Domain.Shape>(f),
			(client, r) => client.Search<Domain.Shape>(r),
			(client, r) => client.SearchAsync<Domain.Shape>(r)
		);

		protected override void ExpectResponse(ISearchResponse<Domain.Shape> response)
		{
			response.ShouldBeValid();
			response.Documents.Count.Should().Be(10);
		}
	}
}
