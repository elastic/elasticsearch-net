using System;
using Elasticsearch.Net;
using FluentAssertions;
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
		protected override Func<DocumentExistsDescriptor<Project>, IDocumentExistsRequest> Fluent => d => d;
		protected override HttpMethod HttpMethod => HttpMethod.HEAD;


		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/project/_doc/{CallIsolatedValue}?routing={CallIsolatedValue}";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var id in values.Values)
				Client.Index(Project.Instance, i => i.Id(id).Routing(id));
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.DocumentExists<Project>(CallIsolatedValue, f),
			(client, f) => client.DocumentExistsAsync<Project>(CallIsolatedValue, f),
			(client, r) => client.DocumentExists(r),
			(client, r) => client.DocumentExistsAsync(r)
		);
		protected override DocumentExistsRequest<Project> Initializer => new DocumentExistsRequest<Project>(CallIsolatedValue) { Routing = CallIsolatedValue };

		protected override DocumentExistsDescriptor<Project> NewDescriptor() => new DocumentExistsDescriptor<Project>(CallIsolatedValue).Routing(CallIsolatedValue);

		protected override void ExpectResponse(IExistsResponse response)
		{
			response.Should().NotBeNull();
			response.Exists.Should().BeTrue();
		}
	}
}
