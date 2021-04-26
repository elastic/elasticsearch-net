/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
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
