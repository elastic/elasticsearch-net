using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Newtonsoft.Json.Linq;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Document.Single.Index
{
	public class IndexIngestGeoIpApiTests :
		ApiIntegrationTestBase<IntrusiveOperationCluster, IIndexResponse, IIndexRequest<Project>, IndexDescriptor<Project>, IndexRequest<Project>>
	{
		private static string PipelineId { get; } = "pipeline-" + Guid.NewGuid().ToString("N").Substring(0, 8);

		public IndexIngestGeoIpApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
		}

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			client.PutPipeline(new PutPipelineRequest(PipelineId)
			{
				Description = "Index pipeline test",
				Processors = new List<IProcessor>
				{
					new RenameProcessor
					{
						TargetField = "lastSeen",
						Field = "lastActivity"
					},
					new GeoIpProcessor
					{
						Field = "leadDeveloper.ipAddress",
						TargetField = "leadDeveloper.geoIp"
					},
					new RenameProcessor
					{
						Field = "leadDeveloper.geoIp.continent_name",
						TargetField = "leadDeveloper.geoIp.continentName",
					},
					new RenameProcessor
					{
						Field = "leadDeveloper.geoIp.city_name",
						TargetField = "leadDeveloper.geoIp.cityName",
					},
					new RenameProcessor
					{
						Field = "leadDeveloper.geoIp.country_iso_code",
						TargetField = "leadDeveloper.geoIp.countryIsoCode",
					},
					new RenameProcessor
					{
						Field = "leadDeveloper.geoIp.region_name",
						TargetField = "leadDeveloper.geoIp.regionName",
					}
				}
			});
		}

		private Project Document => new Project
		{
			State = StateOfBeing.Stable,
			Name = CallIsolatedValue,
			LeadDeveloper = new Developer { Gender = Gender.Male, Id  = 1, IpAddress = "193.4.250.122" },
			StartedOn = FixedDate,
			LastActivity = FixedDate,
			CuratedTags = new List<Tag> {new Tag {Name = "x", Added = FixedDate}},
			SourceOnly = TestClient.Configuration.UsingCustomSourceSerializer ? new SourceOnlyObject() : null
		};

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Index<Project>(this.Document, f),
			fluentAsync: (client, f) => client.IndexAsync<Project>(this.Document, f),
			request: (client, r) => client.Index(r),
			requestAsync: (client, r) => client.IndexAsync(r)
			);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 201;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override string UrlPath
			=> $"/project/doc/{CallIsolatedValue}?refresh=true&pipeline={PipelineId}";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson =>
			new
			{
				name = CallIsolatedValue,
				join = Document.Join,
				leadDeveloper = new { ipAddress = "193.4.250.122", gender = "Male", id = 1 },
				state = "Stable",
				startedOn = FixedDate,
				lastActivity = FixedDate,
				curatedTags = new[] {new {name = "x", added = FixedDate}},
				sourceOnly = Dependant(null, new { notWrittenByDefaultSerializer = "written" }),
			};

		protected override IndexDescriptor<Project> NewDescriptor() => new IndexDescriptor<Project>(this.Document);

		protected override Func<IndexDescriptor<Project>, IIndexRequest<Project>> Fluent => s => s
			.Refresh(Refresh.True)
			.Pipeline(PipelineId);

		protected override IndexRequest<Project> Initializer =>
			new IndexRequest<Project>(this.Document)
			{
				Refresh = Refresh.True,
				Pipeline = PipelineId
			};

		protected override void ExpectResponse(IIndexResponse response)
		{
			response.ShouldBeValid();

			var getResponse = this.Client.Get<Project>(response.Id);

			getResponse.ShouldBeValid();
			getResponse.Source.Should().NotBeNull();
			getResponse.Source.LeadDeveloper.Should().NotBeNull();

			var geoIp = getResponse.Source.LeadDeveloper.GeoIp;

			geoIp.Should().NotBeNull();
			geoIp.ContinentName.Should().Be("Europe");
			geoIp.CityName.Should().Be("Reykjavik");
			geoIp.CountryIsoCode.Should().Be("IS");
			geoIp.RegionName.Should().Be("Capital Region");
			geoIp.Location.Should().NotBeNull();
			geoIp.Location.Latitude.Should().Be(64.1383);
			geoIp.Location.Longitude.Should().Be(-21.8959);
		}
	}
}
