// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Security.GetBuiltinPrivileges
{
	[SkipVersion("<7.7.0", "Introduced in 7.7.0")]
	public class GetBuiltinPrivilegesApiTests
		: ApiIntegrationTestBase<XPackCluster, GetBuiltinPrivilegesResponse, IGetBuiltinPrivilegesRequest, GetBuiltinPrivilegesDescriptor,
			GetBuiltinPrivilegesRequest>
	{
		public GetBuiltinPrivilegesApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<GetBuiltinPrivilegesDescriptor, IGetBuiltinPrivilegesRequest> Fluent => d => d;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetBuiltinPrivilegesRequest Initializer => new GetBuiltinPrivilegesRequest();

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath => $"/_security/privilege/_builtin";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Security.GetBuiltinPrivileges(f),
			(client, f) => client.Security.GetBuiltinPrivilegesAsync(f),
			(client, r) => client.Security.GetBuiltinPrivileges(r),
			(client, r) => client.Security.GetBuiltinPrivilegesAsync(r)
		);

		protected override GetBuiltinPrivilegesDescriptor NewDescriptor() => new GetBuiltinPrivilegesDescriptor();

		protected override void ExpectResponse(GetBuiltinPrivilegesResponse response) => response.ShouldBeValid();
	}
}
