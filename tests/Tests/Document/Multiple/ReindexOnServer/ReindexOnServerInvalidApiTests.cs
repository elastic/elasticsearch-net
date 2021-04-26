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

using System.Linq;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Document.Multiple.ReindexOnServer
{
	[SkipVersion("<2.3.0", "")]
	public class ReindexOnServerInvalidApiTests : ReindexOnServerApiTests
	{
		public ReindexOnServerInvalidApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 400;

		//bad painless script - missing opening ( in front of ctx.
		protected override string PainlessScript { get; } = "if ctx._source.flag == 'bar') {ctx._source.remove('flag')}";

		protected override void ExpectResponse(ReindexOnServerResponse response)
		{
			response.ServerError.Should().NotBeNull();
			response.ServerError.Status.Should().Be(400);
			response.ServerError.Error.Should().NotBeNull();
			response.ServerError.Error.RootCause.Should().NotBeNullOrEmpty();
			response.ServerError.Error.RootCause.First().Reason.Should().Contain("compile");
			response.ServerError.Error.RootCause.First().Type.Should().Be("script_exception");
		}

		// https://youtrack.jetbrains.com/issue/RIDER-19912
		[I] public override async Task ReturnsExpectedStatusCode() => await base.ReturnsExpectedResponse();

		[I] public override async Task ReturnsExpectedIsValid() => await base.ReturnsExpectedIsValid();

		[I] public override async Task ReturnsExpectedResponse() => await base.ReturnsExpectedResponse();

	}
}
