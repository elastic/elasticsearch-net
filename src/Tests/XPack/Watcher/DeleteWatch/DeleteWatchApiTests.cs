using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.Watcher.DeleteWatch
{
	public class DeleteWatchApiTests : ApiIntegrationTestBase<XPackCluster, IDeleteWatchResponse, IDeleteWatchRequest, DeleteWatchDescriptor, DeleteWatchRequest>
	{
		public DeleteWatchApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				var putWatchResponse = client.PutWatch(callUniqueValue.Value, p => p
					.Input(i => i
						.Simple(s => s
							.Add("key", "value")
						)
					)
					.Trigger(t => t
						.Schedule(s => s
							.Cron("0 5 9 * * ?")
						)
					)
					.Actions(a => a
						.Email("reminder_email", e => e
							.To("me@example.com")
							.Subject("Something's strange in the neighbourhood")
							.Body(b => b
								.Text("Who you gonna call?")
							)
						)
					)
				);

				if (!putWatchResponse.IsValid)
					throw new Exception("Problem setting up integration test");
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.DeleteWatch(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.DeleteWatchAsync(CallIsolatedValue, f),
			request: (client, r) => client.DeleteWatch(r),
			requestAsync: (client, r) => client.DeleteWatchAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override string UrlPath => $"/_xpack/watcher/watch/{CallIsolatedValue}";

		protected override bool SupportsDeserialization => true;

		protected override DeleteWatchDescriptor NewDescriptor() => new DeleteWatchDescriptor(CallIsolatedValue);

		protected override object ExpectJson => null;

		protected override Func<DeleteWatchDescriptor, IDeleteWatchRequest> Fluent => p => p;

		protected override DeleteWatchRequest Initializer => new DeleteWatchRequest(CallIsolatedValue);

		protected override void ExpectResponse(IDeleteWatchResponse response)
		{
			response.Version.Should().Be(2);
			response.Found.Should().BeTrue();
			response.Id.Should().Be(CallIsolatedValue);
		}
	}

	public class DeleteNonExistentWatchApiTests : ApiIntegrationTestBase<XPackCluster, IDeleteWatchResponse, IDeleteWatchRequest, DeleteWatchDescriptor, DeleteWatchRequest>
	{
		public DeleteNonExistentWatchApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.DeleteWatch(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.DeleteWatchAsync(CallIsolatedValue, f),
			request: (client, r) => client.DeleteWatch(r),
			requestAsync: (client, r) => client.DeleteWatchAsync(r)
		);

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 404;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override string UrlPath => $"/_xpack/watcher/watch/{CallIsolatedValue}";

		protected override bool SupportsDeserialization => true;

		protected override DeleteWatchDescriptor NewDescriptor() => new DeleteWatchDescriptor(CallIsolatedValue);

		protected override object ExpectJson => null;

		protected override Func<DeleteWatchDescriptor, IDeleteWatchRequest> Fluent => p => p;

		protected override DeleteWatchRequest Initializer => new DeleteWatchRequest(CallIsolatedValue);

		protected override void ExpectResponse(IDeleteWatchResponse response)
		{
			//This API returns different results depending on whether `.watches` exists or not
			if (response.ServerError?.Status == 404)
			{
				response.ServerError.Error.Reason.Should().Be("no such index");
				return;
			}

			response.Version.Should().Be(1);
			response.Found.Should().BeFalse();
			response.Id.Should().Be(CallIsolatedValue);
		}
	}
}
