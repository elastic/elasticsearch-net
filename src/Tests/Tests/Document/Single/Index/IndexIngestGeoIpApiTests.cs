using System;
using System.Collections.Generic;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Domain.Extensions;
using Tests.Framework;
using Tests.Framework.Integration;
using static Tests.Domain.Helpers.TestValueHelper;

namespace Tests.Document.Single.Index
{
	//TODO 6.6. release revalidate this ticket on server
	[SkipVersion(">6.4.0", "https://github.com/elastic/elasticsearch/issues/37909")]
	public class IndexIngestGeoIpApiTests
		: ApiIntegrationTestBase<IntrusiveOperationCluster, IIndexResponse, IIndexRequest<Project>, IndexDescriptor<Project>, IndexRequest<Project>>
	{
		public IndexIngestGeoIpApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson =>
			new
			{
				name = CallIsolatedValue,
				type = Project.TypeName,
				join = Document.Join.ToAnonymousObject(),
				leadDeveloper = new { ipAddress = "193.4.250.122", gender = "Male", id = 1 },
				state = "Stable",
				visibility = "Public",
				startedOn = FixedDate,
				lastActivity = FixedDate,
				numberOfContributors = 0,
				curatedTags = new[] { new { name = "x", added = FixedDate } },
				sourceOnly = Dependant(null, new { notWrittenByDefaultSerializer = "written" }),
			};

		protected override int ExpectStatusCode => 201;

		protected override Func<IndexDescriptor<Project>, IIndexRequest<Project>> Fluent => s => s
			.Refresh(Refresh.True)
			.Pipeline(PipelineId);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool IncludeNullInExpected => false;

		protected override IndexRequest<Project> Initializer =>
			new IndexRequest<Project>(Document)
			{
				Refresh = Refresh.True,
				Pipeline = PipelineId
			};

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath
			=> $"/project/doc/{CallIsolatedValue}?refresh=true&pipeline={PipelineId}&routing={CallIsolatedValue}";

		private Project Document => new Project
		{
			State = StateOfBeing.Stable,
			Name = CallIsolatedValue,
			LeadDeveloper = new Developer { Gender = Gender.Male, Id = 1, IpAddress = "193.4.250.122" },
			StartedOn = FixedDate,
			LastActivity = FixedDate,
			CuratedTags = new List<Tag> { new Tag { Name = "x", Added = FixedDate } },
			SourceOnly = TestClient.Configuration.Random.SourceSerializer ? new SourceOnlyObject() : null
		};

		private static string PipelineId { get; } = "pipeline-" + Guid.NewGuid().ToString("N").Substring(0, 8);

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values) => client.PutPipeline(
			new PutPipelineRequest(PipelineId)
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

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Index<Project>(Document, f),
			(client, f) => client.IndexAsync<Project>(Document, f),
			(client, r) => client.Index(r),
			(client, r) => client.IndexAsync(r)
		);

		protected override IndexDescriptor<Project> NewDescriptor() => new IndexDescriptor<Project>(Document);

		protected override void ExpectResponse(IIndexResponse response)
		{
			response.ShouldBeValid();

			var getResponse = Client.Get<Project>(response.Id, g => g.Routing(CallIsolatedValue));

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
