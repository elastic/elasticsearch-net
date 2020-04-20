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

namespace Tests.XPack.AsyncSearch.Submit
{
	[SkipVersion("<7.7.0", "Introduced in 7.7.0")]
	public class AsyncSearchSubmitApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, AsyncSearchSubmitResponse<Project>, IAsyncSearchSubmitRequest, AsyncSearchSubmitDescriptor<Project>, AsyncSearchSubmitRequest<Project>>
	{
		public AsyncSearchSubmitApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override HttpMethod HttpMethod => POST;
		protected override string UrlPath => $"project/_async_search";
		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

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
			(client, f) => client.AsyncSearch.Submit(f),
			(client, f) => client.AsyncSearch.SubmitAsync(f),
			(client, r) => client.AsyncSearch.Submit<Project>(r),
			(client, r) => client.AsyncSearch.SubmitAsync<Project>(r)
		);

		protected override void ExpectResponse(AsyncSearchSubmitResponse<Project> response)
		{
			response.ShouldBeValid();
			response.Response.Should().NotBeNull();
		}
	}
}
