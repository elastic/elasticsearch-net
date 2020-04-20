using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Elasticsearch.Net.HttpMethod;

namespace Tests.XPack.AsyncSearch.Get
{
	[SkipVersion("<7.7.0", "Introduced in 7.7.0")]
	public class AsyncSearchGetApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, AsyncSearchGetResponse<Project>, IAsyncSearchGetRequest, AsyncSearchGetDescriptor, AsyncSearchGetRequest>
	{
		public AsyncSearchGetApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override HttpMethod HttpMethod => GET;
		protected override string UrlPath => $"/_async_search/{SearchId}";

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		private Id SearchId => RanIntegrationSetup ? ExtendedValue<string>("searchId") : CallIsolatedValue;

		protected override AsyncSearchGetRequest Initializer => new AsyncSearchGetRequest(SearchId);

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callValue in values.Value)
			{
				var response = client.AsyncSearch.Submit<Project>(s => s
					.MatchAll()
				);

				if (!response.IsValid)
					throw new Exception($"Error setting up async search for test: {response.DebugInformation}");

				values.ExtendedValue("searchId", response.Id);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.AsyncSearch.Get<Project>(SearchId, f),
			(client, f) => client.AsyncSearch.GetAsync<Project>(SearchId, f),
			(client, r) => client.AsyncSearch.Get<Project>(r),
			(client, r) => client.AsyncSearch.GetAsync<Project>(r)
		);

		protected override void ExpectResponse(AsyncSearchGetResponse<Project> response)
		{
			response.ShouldBeValid();
			response.StartTime.Should().BeOnOrBefore(DateTimeOffset.Now);
			response.Response.Should().NotBeNull();
			response.Response.Hits.Count.Should().Be(10);
		}
	}
}
