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

namespace Tests.XPack.MachineLearning.GetCalendarEvents
{
	[SkipVersion("<6.4.0", "Calendar functions for machine learning introduced in 6.4.0")]
	public class GetCalendarEventsApiTests : MachineLearningIntegrationTestBase<GetCalendarEventsResponse, IGetCalendarEventsRequest, GetCalendarEventsDescriptor, GetCalendarEventsRequest>
	{
		public GetCalendarEventsApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutCalendar(client, callUniqueValue.Value);
				PostCalendarEvents(client, callUniqueValue.Value);
			}
		}

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => null;

		protected override int ExpectStatusCode => 200;

		protected override Func<GetCalendarEventsDescriptor, IGetCalendarEventsRequest> Fluent => f => f;

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetCalendarEventsRequest Initializer => new GetCalendarEventsRequest(CallIsolatedValue);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"_ml/calendars/{CallIsolatedValue}/events";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.GetCalendarEvents(CallIsolatedValue),
			(client, f) => client.MachineLearning.GetCalendarEventsAsync(CallIsolatedValue, f),
			(client, r) => client.MachineLearning.GetCalendarEvents(r),
			(client, r) => client.MachineLearning.GetCalendarEventsAsync(r)
		);

		protected override GetCalendarEventsDescriptor NewDescriptor() => new GetCalendarEventsDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(GetCalendarEventsResponse response)
		{
			response.ShouldBeValid();

			response.Count.Should().Be(10);

			response.Events.Should().NotBeNull();

			response.Events.Count().Should().Be(10);

			var @event = response.Events.First();

			@event.Description.Should().Be("Event 0");
			@event.CalendarId.Should().Be(CallIsolatedValue);
			@event.EventId.Should().NotBeNull();
		}
	}
}
