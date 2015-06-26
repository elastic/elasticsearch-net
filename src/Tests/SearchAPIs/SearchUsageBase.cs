using Nest;
using Tests._Internals;
using Tests._Internals.Integration;
using Xunit;

namespace Tests.SearchAPIs
{
	[Collection(IntegrationContext.ReadOnly)]
	public abstract class SearchUsageBase : EndpointUsageTests<ISearchResponse<object>, ISearchRequest, SearchDescriptor<object>, SearchRequest>
	{
		public SearchUsageBase(ReadonlyIntegration integration)
		{
			this.DefaultPort = integration.Node.Port;
		}

		protected override void ClientUsage() =>
			this.Calls(
				fluent: (client, f) => client.Search<object>(f),
				fluentAsync: (client, f) => client.SearchAsync<object>(f),
				request: (client, r) => client.Search<object>(r),
				requestAsync: (client, r) => client.SearchAsync<object>(r)
			);
	}
}
