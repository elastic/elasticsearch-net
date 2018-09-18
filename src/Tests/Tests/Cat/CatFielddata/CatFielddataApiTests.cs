using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Cat.CatFielddata
{
	public class CatFielddataApiTests : ApiIntegrationTestBase<ReadOnlyCluster, ICatResponse<CatFielddataRecord>, ICatFielddataRequest, CatFielddataDescriptor, CatFielddataRequest>
	{
		private ISearchResponse<Project> _initialSearchResponse;
		public CatFielddataApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CatFielddata(),
			fluentAsync: (client, f) => client.CatFielddataAsync(),
			request: (client, r) => client.CatFielddata(r),
			requestAsync: (client, r) => client.CatFielddataAsync(r)
		);

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			// ensure some fielddata is loaded
			this._initialSearchResponse = client.Search<Project>(s => s
				.Query(q => q
					.Terms(t => t
						.Field(p => p.CuratedTags.First().Name)
						.Terms(Project.Projects.SelectMany(p=>p.CuratedTags).Take(50).ToList())
					)
				)
			);

			if (!this._initialSearchResponse.IsValid)
				throw new Exception($"Failure setting up integration test. {this._initialSearchResponse.DebugInformation}");
		}

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/fielddata";

		protected override void ExpectResponse(ICatResponse<CatFielddataRecord> response)
		{
			//this tests is very flaky, only do assertions if the query actually returned
			if (this._initialSearchResponse != null && this._initialSearchResponse.Total <= 0)
				return;

			response.Records.Should().NotBeEmpty();
			foreach (var record in response.Records)
			{
				record.Node.Should().NotBeNullOrEmpty();
				record.Id.Should().NotBeNullOrEmpty();
				record.Host.Should().NotBeNullOrEmpty();
				record.Ip.Should().NotBeNullOrEmpty();
				record.Field.Should().NotBeNullOrEmpty();
				record.Size.Should().NotBeNullOrEmpty();
			}
		}
	}

}
