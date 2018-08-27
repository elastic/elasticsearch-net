using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Newtonsoft.Json.Linq;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Domain;

namespace Tests.QueryDsl.Geo
{
	public abstract class GeoShapeSerializationTestsBase :
		ApiIntegrationTestBase<IntrusiveOperationCluster,
			ISearchResponse<Domain.Shape>,
			ISearchRequest,
			SearchDescriptor<Domain.Shape>,
			SearchRequest<Domain.Shape>>
	{
		protected GeoShapeSerializationTestsBase(IntrusiveOperationCluster cluster, EndpointUsage usage)
			: base(cluster, usage) { }

		protected abstract string Index { get; }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Search<Domain.Shape>(f),
			fluentAsync: (client, f) => client.SearchAsync<Domain.Shape>(f),
			request: (client, r) => client.Search<Domain.Shape>(r),
			requestAsync: (client, r) => client.SearchAsync<Domain.Shape>(r)
		);

		private readonly IEnumerable<GeoCoordinate> _coordinates =
			Domain.Shape.Shapes.First().Envelope.Coordinates;

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
		protected override string UrlPath => $"/{Index}/doc/_search";
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override SearchRequest<Domain.Shape> Initializer => new SearchRequest<Domain.Shape>(Index)
		{
			Query = new GeoShapeQuery
			{
				Name = "named_query",
				Boost = 1.1,
				Field = Infer.Field<Domain.Shape>(p => p.Envelope),
				Shape = new EnvelopeGeoShape(this._coordinates),
				Relation = GeoShapeRelation.Intersects,
				IgnoreUnmapped = true,
			}
		};

		protected override Func<SearchDescriptor<Domain.Shape>, ISearchRequest> Fluent => s => s
			.Index(Index)
			.Query(q => q
				.GeoShape(c => c
					.Name("named_query")
					.Boost(1.1)
					.Field(p => p.Envelope)
					.Shape(sh => sh
						.Envelope(this._coordinates)
					)
					.Relation(GeoShapeRelation.Intersects)
					.IgnoreUnmapped()
				)
			);

		protected override void ExpectResponse(ISearchResponse<Domain.Shape> response)
		{
			response.IsValid.Should().BeTrue();
			response.Documents.Count.Should().Be(10);
		}
	}

	public class GeoShapeSerializationTests : GeoShapeSerializationTestsBase
	{
		public GeoShapeSerializationTests(IntrusiveOperationCluster cluster, EndpointUsage usage)
			: base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			if (client.IndexExists(Index).Exists)
				return;

			var createIndexResponse = client.CreateIndex(Index, c => c
				.Settings(s => s
					.NumberOfShards(1)
					.NumberOfReplicas(0)
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
				.Index(Index)
				.IndexMany(Domain.Shape.Shapes)
				.Refresh(Refresh.WaitFor)
			);

			if (!bulkResponse.IsValid)
				throw new Exception($"Error indexing shapes for integration test: {bulkResponse.DebugInformation}");
		}

		protected override string Index => "geoshapes";
	}

	[SkipVersion("<6.2.0", "Support for WKT in Elasticsearch 6.2.0+")]
	public class GeoShapeWKTSerializationTests : GeoShapeSerializationTestsBase
	{
		public GeoShapeWKTSerializationTests(IntrusiveOperationCluster cluster, EndpointUsage usage)
			: base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			if (client.IndexExists(Index).Exists)
				return;

			var createIndexResponse = client.CreateIndex(Index, c => c
				.Settings(s => s
					.NumberOfShards(1)
					.NumberOfReplicas(0)
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

			var bulk = new List<object>();

			// use the low level client to force WKT
			var typeName = this.Client.Infer.TypeName<Domain.Shape>();
			foreach (var shape in Domain.Shape.Shapes)
			{
				bulk.Add(new { index = new { _index = Index, _type = typeName, _id = shape.Id } });
				bulk.Add(new
				{
					id = shape.Id,
					geometryCollection = GeoWKTWriter.Write(shape.GeometryCollection),
					envelope = GeoWKTWriter.Write(shape.Envelope),
					circle = shape.Circle
				});
			}

			var bulkResponse = this.Client.LowLevel.Bulk<BulkResponse>(
				PostData.MultiJson(bulk),
				new BulkRequestParameters{ Refresh = Refresh.WaitFor }
			);

			if (!bulkResponse.IsValid)
				throw new Exception($"Error indexing shapes for integration test: {bulkResponse.DebugInformation}");
		}

		protected override string Index => "wkt-geoshapes";

		protected override void ExpectResponse(ISearchResponse<Domain.Shape> response)
		{
			base.ExpectResponse(response);

			// index shapes again
			var bulkResponse = this.Client.Bulk(b => b
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
