// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.IndexSettings.IndexTemplates.DeleteIndexTemplate
{
	public class DeleteIndexTemplateApiTests
		: ApiTestBase<WritableCluster, DeleteIndexTemplateResponse, IDeleteIndexTemplateRequest, DeleteIndexTemplateDescriptor,
			DeleteIndexTemplateRequest>
	{
		public DeleteIndexTemplateApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeleteIndexTemplateRequest Initializer => new DeleteIndexTemplateRequest(CallIsolatedValue);
		protected override string UrlPath => $"/_template/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.DeleteTemplate(CallIsolatedValue),
			(client, f) => client.Indices.DeleteTemplateAsync(CallIsolatedValue),
			(client, r) => client.Indices.DeleteTemplate(r),
			(client, r) => client.Indices.DeleteTemplateAsync(r)
		);
	}
}
