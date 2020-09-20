// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.DeleteCalendar
{
	[SkipVersion("<6.4.0", "Calendar functions for machine learning introduced in 6.4.0")]
	public class DeleteCalendarApiTests : MachineLearningIntegrationTestBase<DeleteCalendarResponse, IDeleteCalendarRequest, DeleteCalendarDescriptor, DeleteCalendarRequest>
	{
		public DeleteCalendarApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
				PutCalendar(client, callUniqueValue.Value);
		}

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => null;

		protected override int ExpectStatusCode => 200;

		protected override Func<DeleteCalendarDescriptor, IDeleteCalendarRequest> Fluent => f => f;

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeleteCalendarRequest Initializer => new DeleteCalendarRequest(CallIsolatedValue);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"_ml/calendars/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.DeleteCalendar(CallIsolatedValue, f),
			(client, f) => client.MachineLearning.DeleteCalendarAsync(CallIsolatedValue, f),
			(client, r) => client.MachineLearning.DeleteCalendar(r),
			(client, r) => client.MachineLearning.DeleteCalendarAsync(r)
		);

		protected override DeleteCalendarDescriptor NewDescriptor() => new DeleteCalendarDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(DeleteCalendarResponse response) => response.ShouldBeValid();
	}
}
