using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.DeleteCalendar
{
	[SkipVersion("<6.4.0", "Calendar functions for machine learning introduced in 6.4.0")]
	public class DeleteCalendarApiTests : MachineLearningIntegrationTestBase<IDeleteCalendarResponse, IDeleteCalendarRequest, DeleteCalendarDescriptor, DeleteCalendarRequest>
	{
		public DeleteCalendarApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutCalendar(client, callUniqueValue.Value);
			}
		}

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => null;

		protected override int ExpectStatusCode => 200;

		protected override Func<DeleteCalendarDescriptor, IDeleteCalendarRequest> Fluent => f => f;

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeleteCalendarRequest Initializer => new DeleteCalendarRequest(CallIsolatedValue);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"_xpack/ml/calendars/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.DeleteCalendar(CallIsolatedValue, f),
			(client, f) => client.DeleteCalendarAsync(CallIsolatedValue, f),
			(client, r) => client.DeleteCalendar(r),
			(client, r) => client.DeleteCalendarAsync(r)
		);

		protected override DeleteCalendarDescriptor NewDescriptor() => new DeleteCalendarDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(IDeleteCalendarResponse response) => response.ShouldBeValid();
	}
}
