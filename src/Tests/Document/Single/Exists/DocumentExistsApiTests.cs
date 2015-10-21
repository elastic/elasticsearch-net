using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.DocumentExists<Project>(Project.Instance),
			fluentAsync: (client, f) => client.DocumentExistsAsync<Project>(Project.Instance),
			request: (client, r) => client.DocumentExists(r),
			requestAsync: (client, r) => client.DocumentExistsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.HEAD;
		protected override string UrlPath => $"/project/project/{Project.Instance.Name}";

		protected override bool SupportsDeserialization => false;

		protected override Func<DocumentExistsDescriptor<Project>, IDocumentExistsRequest> Fluent => null;
		protected override DocumentExistsRequest<Project> Initializer => new DocumentExistsRequest<Project>(Project.Instance);

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
		});
	}
}
