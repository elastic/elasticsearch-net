using System;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Search
{
	[Collection(IntegrationContext.ReadOnly)]
	public abstract class SearchUsageBase : EndpointUsageBase<ISearchResponse<Project>, ISearchRequest, SearchDescriptor<Project>, SearchRequest<Project>>
	{
		protected SearchUsageBase(ReadOnlyIntegration integration) { this.IntegrationPort = integration.Node.Port; }

		public override bool ExpectIsValid => true;

		public override int ExpectStatusCode => 200;

		public override string UrlPath => "/project/project/_search";

		protected override void ClientUsage() =>
			this.Calls(
				fluent: (client, f) => client.Search<Project>(f),
				fluentAsync: (client, f) => client.SearchAsync<Project>(f),
				request: (client, r) => client.Search<Project>(r),
				requestAsync: (client, r) => client.SearchAsync<Project>(r)
			);
	}
}
