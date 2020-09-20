// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.IndexSettings.IndexTemplates.IndexTemplateExists
{
	public class IndexTemplateExistsApiTests
		: ApiTestBase<WritableCluster, ExistsResponse, IIndexTemplateExistsRequest, IndexTemplateExistsDescriptor, IndexTemplateExistsRequest>
	{
		public IndexTemplateExistsApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<IndexTemplateExistsDescriptor, IIndexTemplateExistsRequest> Fluent => d => d;

		protected override HttpMethod HttpMethod => HttpMethod.HEAD;

		protected override IndexTemplateExistsRequest Initializer => new IndexTemplateExistsRequest(CallIsolatedValue);
		protected override string UrlPath => $"/_template/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.TemplateExists(CallIsolatedValue, f),
			(client, f) => client.Indices.TemplateExistsAsync(CallIsolatedValue, f),
			(client, r) => client.Indices.TemplateExists(r),
			(client, r) => client.Indices.TemplateExistsAsync(r)
		);

		protected override IndexTemplateExistsDescriptor NewDescriptor() => new IndexTemplateExistsDescriptor(CallIsolatedValue);
	}
}
