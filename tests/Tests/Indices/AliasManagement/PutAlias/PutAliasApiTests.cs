// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.AliasManagement.PutAlias
{
	public class PutAliasApiTests
		: ApiIntegrationAgainstNewIndexTestBase<WritableCluster, PutAliasResponse, IPutAliasRequest, PutAliasDescriptor, PutAliasRequest>
	{
		public PutAliasApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<PutAliasDescriptor, IPutAliasRequest> Fluent => null;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override PutAliasRequest Initializer => new PutAliasRequest(CallIsolatedValue, CallIsolatedValue + "-alias");

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/{CallIsolatedValue}/_alias/{CallIsolatedValue + "-alias"}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.PutAlias(CallIsolatedValue, CallIsolatedValue + "-alias"),
			(client, f) => client.Indices.PutAliasAsync(CallIsolatedValue, CallIsolatedValue + "-alias"),
			(client, r) => client.Indices.PutAlias(r),
			(client, r) => client.Indices.PutAliasAsync(r)
		);
	}

	[SkipVersion("<6.4.0", "is_write_index is a new feature")]
	public class PutAliasIsWriteIndexApiTests
		: ApiIntegrationAgainstNewIndexTestBase<WritableCluster, PutAliasResponse, IPutAliasRequest, PutAliasDescriptor, PutAliasRequest>
	{
		public PutAliasIsWriteIndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new { is_write_index = true };
		protected override int ExpectStatusCode => 200;

		protected override Func<PutAliasDescriptor, IPutAliasRequest> Fluent => f => f.IsWriteIndex();
		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override PutAliasRequest Initializer => new PutAliasRequest(CallIsolatedValue, CallIsolatedValue + "-alias")
		{
			IsWriteIndex = true
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/{CallIsolatedValue}/_alias/{CallIsolatedValue + "-alias"}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.PutAlias(CallIsolatedValue, CallIsolatedValue + "-alias"),
			(client, f) => client.Indices.PutAliasAsync(CallIsolatedValue, CallIsolatedValue + "-alias"),
			(client, r) => client.Indices.PutAlias(r),
			(client, r) => client.Indices.PutAliasAsync(r)
		);

		protected override PutAliasDescriptor NewDescriptor() => new PutAliasDescriptor(CallIsolatedValue, CallIsolatedValue + "-alias");
	}
}
