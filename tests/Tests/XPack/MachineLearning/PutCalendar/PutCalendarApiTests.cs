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
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.PutCalendar
{
	[SkipVersion("<6.4.0", "Calendar functions for machine learning introduced in 6.4.0")]
	public class PutCalendarApiTests : MachineLearningIntegrationTestBase<PutCalendarResponse, IPutCalendarRequest, PutCalendarDescriptor, PutCalendarRequest>
	{
		public PutCalendarApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			description = "Planned outages"
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<PutCalendarDescriptor, IPutCalendarRequest> Fluent => f => f
			.Description("Planned outages");

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override PutCalendarRequest Initializer =>
			new PutCalendarRequest(CallIsolatedValue)
			{
				Description = "Planned outages"
			};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"_ml/calendars/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.PutCalendar(CallIsolatedValue, f),
			(client, f) => client.MachineLearning.PutCalendarAsync(CallIsolatedValue, f),
			(client, r) => client.MachineLearning.PutCalendar(r),
			(client, r) => client.MachineLearning.PutCalendarAsync(r)
		);

		protected override PutCalendarDescriptor NewDescriptor() => new PutCalendarDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(PutCalendarResponse response)
		{
			response.ShouldBeValid();

			response.CalendarId.Should().Be(CallIsolatedValue);

			response.JobIds.Should().NotBeNull();

			response.JobIds.Should().BeEmpty();

			response.Description.Should().Be("Planned outages");
		}
	}
}
