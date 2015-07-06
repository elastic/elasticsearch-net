using Nest;
using Tests._Internals;
using Tests._Internals.Integration;
using Tests._Internals.MockData;
using Xunit;

namespace Tests.SearchAPIs
{
	[Collection(IntegrationContext.ReadOnly)]
	public abstract class SearchUsageBase : EndpointUsageTests<ISearchResponse<Project>, ISearchRequest, SearchDescriptor<Project>, SearchRequest<Project>>
	{
		protected SearchUsageBase(ReadonlyIntegration integration) { this.IntegrationPort = integration.Node.Port; }

		protected override void ClientUsage() =>
			this.Calls(
				fluent: (client, f) => client.Search<Project>(f),
				fluentAsync: (client, f) => client.SearchAsync<Project>(f),
				request: (client, r) => client.Search<Project>(r),
				requestAsync: (client, r) => client.SearchAsync<Project>(r)
			);
	}
}
