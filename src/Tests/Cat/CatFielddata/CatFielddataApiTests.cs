using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Cat.CatFielddata
{
	public class CatFielddataApiTests : ApiIntegrationTestBase<ReadOnlyCluster, ICatResponse<CatFielddataRecord>, ICatFielddataRequest, CatFielddataDescriptor, CatFielddataRequest>
	{
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
			var response = client.Search<Project>(s => s
				.Query(q => q
					.Terms(t => t
						.Field(p => p.CuratedTags.First().Name)
						.Terms(Tag.Generator.Generate(50).Select(ct => ct.Name))
					)
				)
			);

			if (!response.IsValid)
				throw new Exception($"Failure setting up integration test. {response.DebugInformation}");
		}

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/fielddata";

		protected override void ExpectResponse(ICatResponse<CatFielddataRecord> response)
		{
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
