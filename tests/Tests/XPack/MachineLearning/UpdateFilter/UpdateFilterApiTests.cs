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
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;
using static Elastic.Transport.HttpMethod;

namespace Tests.XPack.MachineLearning.UpdateFilter
{
	[SkipVersion("<6.4.0", "Filter functions for machine learning stabilised in 6.4.0")]
	public class UpdateFilterApiTests : MachineLearningIntegrationTestBase<UpdateFilterResponse, IUpdateFilterRequest, UpdateFilterDescriptor, UpdateFilterRequest>
	{
		public UpdateFilterApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
				PutFilter(client, callUniqueValue.Value);
		}

		protected override void IntegrationTeardown(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
				DeleteFilter(client, callUniqueValue.Value);
		}

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			description = "A list of safe domains",
			add_items = new[] { "*.microsoft.com" },
			remove_items = new[] { "*.google.com" }
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<UpdateFilterDescriptor, IUpdateFilterRequest> Fluent => f => f
			.Description("A list of safe domains")
			.AddItems("*.microsoft.com")
			.RemoveItems("*.google.com");

		protected override HttpMethod HttpMethod => POST;

		protected override UpdateFilterRequest Initializer =>
			new UpdateFilterRequest(CallIsolatedValue)
			{
				Description = "A list of safe domains",
				AddItems = new [] { "*.microsoft.com" },
				RemoveItems = new [] { "*.google.com" }
			};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"_ml/filters/{CallIsolatedValue}/_update";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.UpdateFilter(CallIsolatedValue, f),
			(client, f) => client.MachineLearning.UpdateFilterAsync(CallIsolatedValue, f),
			(client, r) => client.MachineLearning.UpdateFilter(r),
			(client, r) => client.MachineLearning.UpdateFilterAsync(r)
		);

		protected override UpdateFilterDescriptor NewDescriptor() => new UpdateFilterDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(UpdateFilterResponse response)
		{
			response.ShouldBeValid();
			response.FilterId.Should().Be(CallIsolatedValue);
			response.Items.Should().NotBeNull()
				.And.HaveCount(2)
				.And.Contain("*.microsoft.com")
				.And.Contain("wikipedia.org");

			response.Description.Should().Be("A list of safe domains");
		}
	}
}
