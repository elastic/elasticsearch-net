using Nest.Tests.Literate._Internals.Integration;
using Xunit;

namespace Nest.Tests.Literate.SearchAPIs
{
	[Collection(IntegrationContext.ReadOnly)]
	public abstract class SearchUsageBase : EndpointUsageTests<ISearchResponse<object>, ISearchRequest, SearchDescriptor<object>, SearchRequest>
	{
		protected override void ClientUsage() =>
			this.Calls(
				fluent: (client, f) => client.Search<object>(f),
				fluentAsync: (client, f) => client.SearchAsync<object>(f),
				request: (client, r) => client.Search<object>(r),
				requestAsync: (client, r) => client.SearchAsync<object>(r)
			);
	}
}
