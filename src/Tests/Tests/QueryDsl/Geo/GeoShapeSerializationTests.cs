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
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.QueryDsl.Geo
{
	public class GeoShapeSerializationTests :
		ApiIntegrationTestBase<IntrusiveOperationCluster,
			ISearchResponse<Domain.Shape>,
			ISearchRequest,
			SearchDescriptor<Domain.Shape>,
			SearchRequest<Domain.Shape>>
	{
		public GeoShapeSerializationTests(IntrusiveOperationCluster cluster, EndpointUsage usage)
			: base(cluster, usage) { }

		private const string Index = "shapes";

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

			var bulkResponse = this.Client.Bulk(b => b
				.IndexMany(Domain.Shape.Shapes)
				.Refresh(Refresh.WaitFor)
			);

			if (!bulkResponse.IsValid)
				throw new Exception($"Error indexing shapes for integration test: {bulkResponse.DebugInformation}");
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Search<Domain.Shape>(f),
			fluentAsync: (client, f) => client.SearchAsync<Domain.Shape>(f),
			request: (client, r) => client.Search<Domain.Shape>(r),
			requestAsync: (client, r) => client.SearchAsync<Domain.Shape>(r)
		);

		private readonly IEnumerable<GeoCoordinate> _coordinates = Domain.Shape.Shapes.First().Envelope.Coordinates;

		protected override object ExpectJson => new
		{
			query = new
			{
				geo_shape = new
				{
					_name="named_query",
					boost = 1.1,
					ignore_unmapped = true,
					envelope = new
					{
						relation = "intersects",
						shape = new
						{
							type = "envelope",
							coordinates = this._coordinates
						}
					}
				}
			}
		};

		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override string UrlPath => $"/shapes/doc/_search";
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override SearchRequest<Domain.Shape> Initializer => new SearchRequest<Domain.Shape>
		{
			Query = new GeoShapeEnvelopeQuery
			{
				Name = "named_query",
				Boost = 1.1,
				Field = Infer.Field<Domain.Shape>(p => p.Envelope),
				Shape = new EnvelopeGeoShape(this._coordinates),
				Relation = GeoShapeRelation.Intersects,
				IgnoreUnmapped = true
			}
		};

		protected override Func<SearchDescriptor<Domain.Shape>, ISearchRequest> Fluent => s => s
			.Query(q => q
				.GeoShapeEnvelope(c => c
					.Name("named_query")
					.Boost(1.1)
					.Field(p => p.Envelope)
					.Coordinates(this._coordinates)
					.Relation(GeoShapeRelation.Intersects)
					.IgnoreUnmapped(true)
				)
			);

		protected override void ExpectResponse(ISearchResponse<Domain.Shape> response)
		{
			response.ShouldBeValid();
			response.Documents.Count.Should().Be(10);
		}
	}
}
