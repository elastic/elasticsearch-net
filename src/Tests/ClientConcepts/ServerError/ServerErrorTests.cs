using Elasticsearch.Net;
using Tests.Framework;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.ClientConcepts.ServerError
{

	public class ServerErrorTestsBase
		: RequestResponseApiTestBase<ISearchResponse<Project>, ISearchRequest, SearchDescriptor<Project>, SearchRequest<Project>>
	{
		public ServerErrorTestsBase(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() =>Calls(
			fluent: (client, f) => client.Search<Project>(f),
			fluentAsync: (client, f) => client.SearchAsync<Project>(f),
			request: (client, r) => client.Search<Project>(r),
			requestAsync: (client, r) => client.SearchAsync<Project>(r)
		);

	}




	public class ServerErrorTests : SerializationTestBase
	{
		[U]
		public void CanDeserializeServerError()
		{
			var serverErrorJson = @"{
				""root_cause"": [
				{
					""type"": ""parse_exception"",
					""reason"": ""failed to parse source for create index""
				}],
				""type"": ""parse_exception"",
				""reason"": ""failed to parse source for create index"",
				""caused_by"": {
					""type"": ""json_parse_exception"",
					""reason"": ""Unexpected character ('\""' (code 34)): was expecting a colon to separate field name and value\n at [Source: [B@1231dcb3; line: 6, column: 10]""
				}
			}";

			var serverError = this.Deserialize<Elasticsearch.Net.Error>(serverErrorJson);

			serverError.Should().NotBeNull();

			serverError.Should().NotBeNull();
			serverError.RootCause.Count.Should().Be(1);
			serverError.CausedBy.Should().NotBeNull();
		}
	}
}
