// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Document.Single.Exists
{
	public class DocumentExistsApiTests
		: ApiIntegrationTestBase<WritableCluster, ExistsResponse, IDocumentExistsRequest, DocumentExistsDescriptor<Project>,
			DocumentExistsRequest<Project>>
	{
		public DocumentExistsApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.HEAD;

		protected override string UrlPath => $"/project/_doc/{CallIsolatedValue}?routing={CallIsolatedValue}";

		protected override bool SupportsDeserialization => false;
		protected override DocumentExistsDescriptor<Project> NewDescriptor() => new DocumentExistsDescriptor<Project>(CallIsolatedValue);

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var id in values.Values)
				Client.Index(Project.Instance, i => i.Id(id).Routing(id));
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.DocumentExists(CallIsolatedValue, f),
			(client, f) => client.DocumentExistsAsync(CallIsolatedValue, f),
			(client, r) => client.DocumentExists(r),
			(client, r) => client.DocumentExistsAsync(r)
		);

		protected override DocumentExistsRequest<Project> Initializer => new DocumentExistsRequest<Project>(CallIsolatedValue) { Routing = CallIsolatedValue };
		protected override Func<DocumentExistsDescriptor<Project>, IDocumentExistsRequest> Fluent => d => d.Routing(CallIsolatedValue);

		protected override void ExpectResponse(ExistsResponse response)
		{
			response.Should().NotBeNull();
			response.Exists.Should().BeTrue();
		}
	}
}
