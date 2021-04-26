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
using Nest;
using Tests.Framework.EndpointTests.TestState;
using Tests.Core.Extensions;

namespace Tests.XPack.MachineLearning.DeleteCalendarEvent
{
	[SkipVersion("<6.4.0", "Calendar functions for machine learning introduced in 6.4.0")]
	public class DeleteCalendarEventApiTests : MachineLearningIntegrationTestBase<DeleteCalendarEventResponse, IDeleteCalendarEventRequest, DeleteCalendarEventDescriptor, DeleteCalendarEventRequest>
	{
		public DeleteCalendarEventApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) {}

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutCalendar(client, callUniqueValue.Value);
				PostCalendarEvent(client, callUniqueValue.Value);
			}
		}

		protected override void OnBeforeCall(IElasticClient client)
		{
			var events = client.MachineLearning.GetCalendarEvents(CallIsolatedValue, f => f);
			var eventId = events.Events.First().EventId;
			ExtendedValue("eventId", eventId);
		}

		private Id EventId => TryGetExtendedValue<Id>("eventId", out var eventId) ? eventId : "event_id";

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => null;

		protected override int ExpectStatusCode => 200;

		protected override Func<DeleteCalendarEventDescriptor, IDeleteCalendarEventRequest> Fluent => f => f;

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeleteCalendarEventRequest Initializer => new DeleteCalendarEventRequest(CallIsolatedValue, EventId);

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath => $"_ml/calendars/{CallIsolatedValue}/events/{EventId}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.DeleteCalendarEvent(CallIsolatedValue, EventId, f),
			(client, f) => client.MachineLearning.DeleteCalendarEventAsync(CallIsolatedValue, EventId, f),
			(client, r) => client.MachineLearning.DeleteCalendarEvent(r),
			(client, r) => client.MachineLearning.DeleteCalendarEventAsync(r)
		);

		protected override DeleteCalendarEventDescriptor NewDescriptor() => new DeleteCalendarEventDescriptor(CallIsolatedValue, EventId);

		protected override void ExpectResponse(DeleteCalendarEventResponse response) => response.ShouldBeValid();
	}
}
