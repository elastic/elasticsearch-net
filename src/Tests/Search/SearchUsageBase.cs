using System;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using Elasticsearch.Net;

namespace Tests.Search
{
	[Collection(IntegrationContext.ReadOnly)]
	public abstract class SearchUsageBase : ApiCallIntegrationBase<ISearchResponse<Project>, ISearchRequest, SearchDescriptor<Project>, SearchRequest<Project>>
	{
		protected SearchUsageBase(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		public override bool ExpectIsValid => true;

		public override int ExpectStatusCode => 200;

		public override HttpMethod HttpMethod => HttpMethod.POST;

		public override string UrlPath => "/project/project/_search";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Search<Project>(f),
			fluentAsync: (client, f) => client.SearchAsync<Project>(f),
			request: (client, r) => client.Search<Project>(r),
			requestAsync: (client, r) => client.SearchAsync<Project>(r)
		);
	}
}
