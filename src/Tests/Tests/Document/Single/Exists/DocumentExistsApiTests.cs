using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Document.Single.Exists
{
	public class DocumentExistsApiTests : ApiIntegrationTestBase<WritableCluster, IExistsResponse, IDocumentExistsRequest, DocumentExistsDescriptor<Project>, DocumentExistsRequest<Project>>
	{
		public DocumentExistsApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var id in values.Values)
				this.Client.Index(Project.Instance, i=>i.Id(id));
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.DocumentExists<Project>(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.DocumentExistsAsync<Project>(CallIsolatedValue, f),
			request: (client, r) => client.DocumentExists(r),
			requestAsync: (client, r) => client.DocumentExistsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.HEAD;
		protected override string UrlPath => $"/project/doc/{CallIsolatedValue}?routing={U(Project.Routing)}";

		protected override bool SupportsDeserialization => false;

		protected override DocumentExistsDescriptor<Project> NewDescriptor() => new DocumentExistsDescriptor<Project>(CallIsolatedValue);
		protected override Func<DocumentExistsDescriptor<Project>, IDocumentExistsRequest> Fluent => d => d.Routing(Project.Routing);
		protected override DocumentExistsRequest<Project> Initializer => new DocumentExistsRequest<Project>(CallIsolatedValue)
		{
			Routing = Project.Routing
		};

		protected override void ExpectResponse(IExistsResponse response)
		{
			response.Should().NotBeNull();
			response.Exists.Should().BeTrue();
		}
	}
}
