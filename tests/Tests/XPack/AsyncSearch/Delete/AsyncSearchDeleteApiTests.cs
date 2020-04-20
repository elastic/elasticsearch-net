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

namespace Tests.XPack.AsyncSearch.Delete
{
	[SkipVersion("<7.7.0", "Introduced in 7.7.0")]
	public class AsyncSearchDeleteApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, AsyncSearchDeleteResponse, IAsyncSearchDeleteRequest, AsyncSearchDeleteDescriptor, AsyncSearchDeleteRequest>
	{
		public AsyncSearchDeleteApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override HttpMethod HttpMethod => DELETE;
		protected override string UrlPath => $"/_async_search/{U(SearchId)}";

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		private string SearchId => RanIntegrationSetup ? ExtendedValue<string>("searchId") : CallIsolatedValue;

		protected override AsyncSearchDeleteRequest Initializer => new AsyncSearchDeleteRequest(SearchId);

		protected override AsyncSearchDeleteDescriptor NewDescriptor() =>  new AsyncSearchDeleteDescriptor(SearchId);

		protected override void OnBeforeCall(IElasticClient client)
		{
			var response = client.AsyncSearch.Submit<Project>(s => s
				.MatchAll()
				.KeepOnCompletion()
				.WaitForCompletionTimeout(-1)
			);

			if (!response.IsValid)
				throw new Exception($"Error setting up async search for test: {response.DebugInformation}");

			ExtendedValue("searchId", response.Id);
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.AsyncSearch.Delete(SearchId, f),
			(client, f) => client.AsyncSearch.DeleteAsync(SearchId, f),
			(client, r) => client.AsyncSearch.Delete(r),
			(client, r) => client.AsyncSearch.DeleteAsync(r)
		);

		protected override void ExpectResponse(AsyncSearchDeleteResponse response)
		{
			response.ShouldBeValid();
			response.Acknowledged.Should().BeTrue();
		}
	}
}
