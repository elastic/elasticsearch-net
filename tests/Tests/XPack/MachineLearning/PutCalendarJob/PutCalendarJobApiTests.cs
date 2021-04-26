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
using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.PutCalendarJob
{
	[SkipVersion("<6.4.0", "Calendar functions for machine learning introduced in 6.4.0")]
	public class PutCalendarJobApiTests : MachineLearningIntegrationTestBase<PutCalendarJobResponse, IPutCalendarJobRequest, PutCalendarJobDescriptor, PutCalendarJobRequest>
	{
		public PutCalendarJobApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value + "_job");
				PutCalendar(client, callUniqueValue.Value + "_calendar");
			}
		}


		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => null;

		protected override int ExpectStatusCode => 200;

		protected override Func<PutCalendarJobDescriptor, IPutCalendarJobRequest> Fluent => f => f;

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override PutCalendarJobRequest Initializer => new PutCalendarJobRequest(CallIsolatedValue + "_calendar",CallIsolatedValue + "_job");

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"_ml/calendars/{CallIsolatedValue}_calendar/jobs/{CallIsolatedValue}_job";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.PutCalendarJob(CallIsolatedValue + "_calendar",CallIsolatedValue + "_job", f),
			(client, f) => client.MachineLearning.PutCalendarJobAsync(CallIsolatedValue + "_calendar",CallIsolatedValue + "_job", f),
			(client, r) => client.MachineLearning.PutCalendarJob(r),
			(client, r) => client.MachineLearning.PutCalendarJobAsync(r)
		);

		protected override PutCalendarJobDescriptor NewDescriptor() => new PutCalendarJobDescriptor(CallIsolatedValue + "_calendar",CallIsolatedValue + "_job");

		protected override void ExpectResponse(PutCalendarJobResponse response)
		{
			response.ShouldBeValid();

			response.CalendarId.Should().Be(CallIsolatedValue + "_calendar");

			response.JobIds.Should().NotBeNull();

			response.JobIds.First().Should().Be(CallIsolatedValue + "_job");

			response.Description.Should().Be("Planned outages");
		}
	}
}
