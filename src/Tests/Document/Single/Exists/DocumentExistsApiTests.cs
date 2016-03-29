using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Document.Single.Exists
{
	[Collection(IntegrationContext.Indexing)]
	public class DocumentExistsApiTests : ApiIntegrationTestBase<IExistsResponse, IDocumentExistsRequest, DocumentExistsDescriptor<Project>, DocumentExistsRequest<Project>>
	{
		public DocumentExistsApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void BeforeAllCalls(IElasticClient client, IDictionary<ClientMethod, string> values)
		{
			foreach (var id in values.Values)
				this.Client.Index(Project.Instance, i=>i.Id(id));
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.DocumentExists<Project>(CallIsolatedValue),
			fluentAsync: (client, f) => client.DocumentExistsAsync<Project>(CallIsolatedValue),
			request: (client, r) => client.DocumentExists(r),
			requestAsync: (client, r) => client.DocumentExistsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.HEAD;
		protected override string UrlPath => $"/project/project/{CallIsolatedValue}";

		protected override bool SupportsDeserialization => false;

		protected override Func<DocumentExistsDescriptor<Project>, IDocumentExistsRequest> Fluent => null;
		protected override DocumentExistsRequest<Project> Initializer => new DocumentExistsRequest<Project>(CallIsolatedValue);
	}
}
