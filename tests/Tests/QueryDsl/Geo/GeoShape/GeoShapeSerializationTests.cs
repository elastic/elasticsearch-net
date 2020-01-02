using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Newtonsoft.Json.Linq;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.Geo.GeoShape
{
	public abstract class GeoShapeSerializationTestsBase
		: ApiIntegrationTestBase<IntrusiveOperationCluster,
			ISearchResponse<Domain.GeoShape>,
			ISearchRequest,
			SearchDescriptor<Domain.GeoShape>,
			SearchRequest<Domain.GeoShape>>
	{
		private readonly IEnumerable<GeoCoordinate> _coordinates =
			Domain.GeoShape.Shapes.First().Envelope.Coordinates;

		protected GeoShapeSerializationTestsBase(IntrusiveOperationCluster cluster, EndpointUsage usage)
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

		protected override Func<SearchDescriptor<Domain.GeoShape>, ISearchRequest> Fluent => s => s
			.Index(Index)
			.Query(q => q
				.GeoShape(c => c
					.Name("named_query")
					.Boost(1.1)
					.Field(p => p.Envelope)
					.Shape(sh => sh
						.Envelope(_coordinates)
					)
					.Relation(GeoShapeRelation.Intersects)
					.IgnoreUnmapped()
				)
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected abstract string Index { get; }

		protected override SearchRequest<Domain.GeoShape> Initializer => new SearchRequest<Domain.GeoShape>(Index)
		{
			Query = new GeoShapeQuery
			{
				Name = "named_query",
				Boost = 1.1,
				Field = Infer.Field<Domain.GeoShape>(p => p.Envelope),
				Shape = new EnvelopeGeoShape(_coordinates),
				Relation = GeoShapeRelation.Intersects,
				IgnoreUnmapped = true,
			}
		};

		protected override string UrlPath => $"/{Index}/_search";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Search(f),
			(client, f) => client.SearchAsync(f),
			(client, r) => client.Search<Domain.GeoShape>(r),
			(client, r) => client.SearchAsync<Domain.GeoShape>(r)
		);

		protected override void ExpectResponse(ISearchResponse<Domain.GeoShape> response)
		{
			response.IsValid.Should().BeTrue();
			response.Documents.Count.Should().Be(10);
		}
	}

	public class GeoShapeSerializationTests : GeoShapeSerializationTestsBase
	{
		public GeoShapeSerializationTests(IntrusiveOperationCluster cluster, EndpointUsage usage)
			: base(cluster, usage) { }

		protected override string Index => "geoshapes";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			if (client.Indices.Exists(Index).Exists)
				return;

			var createIndexResponse = client.Indices.Create(Index, c => c
				.Settings(s => s
					.NumberOfShards(1)
					.NumberOfReplicas(0)
				)
				.Map<Domain.GeoShape>(mm => mm
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
							.Strategy(GeoStrategy.Recursive)
						)
					)
				)
			);

			if (!createIndexResponse.IsValid)
				throw new Exception($"Error creating index for integration test: {createIndexResponse.DebugInformation}");

			var bulkResponse = Client.Bulk(b => b
				.Index(Index)
				.IndexMany(Domain.GeoShape.Shapes)
				.Refresh(Refresh.WaitFor)
			);

			if (!bulkResponse.IsValid)
				throw new Exception($"Error indexing shapes for integration test: {bulkResponse.DebugInformation}");
		}
	}

	[SkipVersion("<6.2.0", "Support for WKT in Elasticsearch 6.2.0+")]
	public class GeoShapeWKTSerializationTests : GeoShapeSerializationTestsBase
	{
		public GeoShapeWKTSerializationTests(IntrusiveOperationCluster cluster, EndpointUsage usage)
			: base(cluster, usage) { }

		protected override string Index => "wkt-geoshapes";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			if (client.Indices.Exists(Index).Exists)
				return;

			var createIndexResponse = client.Indices.Create(Index, c => c
				.Settings(s => s
					.NumberOfShards(1)
					.NumberOfReplicas(0)
				)
				.Map<Domain.GeoShape>(mm => mm
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
							.Strategy(GeoStrategy.Recursive)
						)
					)
				)
			);

			if (!createIndexResponse.IsValid)
				throw new Exception($"Error creating index for integration test: {createIndexResponse.DebugInformation}");

			var bulk = new List<object>();

			foreach (var shape in Domain.GeoShape.Shapes)
			{
				bulk.Add(new { index = new { _index = Index, _id = shape.Id } });
				bulk.Add(new
				{
					id = shape.Id,
					geometryCollection = GeoWKTWriter.Write(shape.GeometryCollection),
					envelope = GeoWKTWriter.Write(shape.Envelope),
					circle = shape.Circle
				});
			}

			var bulkResponse = Client.LowLevel.Bulk<BulkResponse>(
				PostData.MultiJson(bulk),
				new BulkRequestParameters { Refresh = Refresh.WaitFor }
			);

			if (!bulkResponse.IsValid)
				throw new Exception($"Error indexing shapes for integration test: {bulkResponse.DebugInformation}");
		}

		protected override void ExpectResponse(ISearchResponse<Domain.GeoShape> response)
		{
			base.ExpectResponse(response);

			// index shapes again
			var bulkResponse = Client.Bulk(b => b
				.Index(Index)
				.IndexMany(response.Documents)
				.Refresh(Refresh.WaitFor)
				.RequestConfiguration(r => r
					.DisableDirectStreaming()
				)
			);

			bulkResponse.IsValid.Should().BeTrue();

			// ensure they were indexed as WKT
			var request = Encoding.UTF8.GetString(bulkResponse.ApiCall.RequestBodyInBytes);
			using (var reader = new StringReader(request))
			{
				string line;
				var i = 0;
				while ((line = reader.ReadLine()) != null)
				{
					i++;
					if (i % 2 != 0)
						continue;

					var jObject = JObject.Parse(line);
					var jValue = (JValue)jObject["geometryCollection"];
					jValue.Value.Should().BeOfType<string>();
					jValue = (JValue)jObject["envelope"];
					jValue.Value.Should().BeOfType<string>();
					jObject["circle"].Should().BeOfType<JObject>();
				}
			}
		}
	}
}
