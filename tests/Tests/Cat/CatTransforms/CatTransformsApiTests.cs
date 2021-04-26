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
using Elastic.Transport;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;

namespace Tests.Cat.CatTransforms
{
    [SkipVersion("<7.7.0", "Introduced in 7.7.0")]
	public class CatTransformsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CatResponse<CatTransformsRecord>, ICatTransformsRequest, CatTransformsDescriptor, CatTransformsRequest>
	{
		public CatTransformsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/transforms";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.Transforms(f),
			(client, f) => client.Cat.TransformsAsync(f),
			(client, r) => client.Cat.Transforms(r),
			(client, r) => client.Cat.TransformsAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatTransformsRecord> response) => response.ShouldBeValid();
	}

    [SkipVersion("<7.7.0", "Introduced in 7.7.0")]
	public class CatTransformsFullApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CatResponse<CatTransformsRecord>, ICatTransformsRequest, CatTransformsDescriptor,
			CatTransformsRequest>
	{
		public CatTransformsFullApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<CatTransformsDescriptor, ICatTransformsRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override CatTransformsRequest Initializer { get; } = new CatTransformsRequest();

		protected override string UrlPath => "/_cat/transforms";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.Transforms(f),
			(client, f) => client.Cat.TransformsAsync(f),
			(client, r) => client.Cat.Transforms(r),
			(client, r) => client.Cat.TransformsAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatTransformsRecord> response) => response.ShouldBeValid();
	}
}
