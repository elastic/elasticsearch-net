using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Document.Single.Exists
{
	public class DocumentExistsApiTests
		: ApiIntegrationTestBase<WritableCluster, IExistsResponse, IDocumentExistsRequest, DocumentExistsDescriptor<Project>,
			DocumentExistsRequest<Project>>
	{
		public DocumentExistsApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<DocumentExistsDescriptor<Project>, IDocumentExistsRequest> Fluent => null;
		protected override HttpMethod HttpMethod => HttpMethod.HEAD;
		protected override DocumentExistsRequest<Project> Initializer => new DocumentExistsRequest<Project>(CallIsolatedValue);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/project/project/{CallIsolatedValue}";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var id in values.Values)
				Client.Index(Project.Instance, i => i.Id(id));
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.DocumentExists<Project>(CallIsolatedValue),
			(client, f) => client.DocumentExistsAsync<Project>(CallIsolatedValue),
			(client, r) => client.DocumentExists(r),
			(client, r) => client.DocumentExistsAsync(r)
		);
	}
}
