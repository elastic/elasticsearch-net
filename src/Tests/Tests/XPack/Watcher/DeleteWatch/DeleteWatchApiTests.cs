using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.Xunit;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.XPack.Watcher.DeleteWatch
{
	public class DeleteWatchApiTests
		: ApiIntegrationTestBase<XPackCluster, IDeleteWatchResponse, IDeleteWatchRequest, DeleteWatchDescriptor, DeleteWatchRequest>
	{
		public DeleteWatchApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;

		protected override Func<DeleteWatchDescriptor, IDeleteWatchRequest> Fluent => p => p;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeleteWatchRequest Initializer => new DeleteWatchRequest(CallIsolatedValue);

		protected override string UrlPath => $"/_watcher/watch/{CallIsolatedValue}";

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
			(client, f) => client.DeleteWatch(CallIsolatedValue, f),
			(client, f) => client.DeleteWatchAsync(CallIsolatedValue, f),
			(client, r) => client.DeleteWatch(r),
			(client, r) => client.DeleteWatchAsync(r)
		);

		protected override DeleteWatchDescriptor NewDescriptor() => new DeleteWatchDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(IDeleteWatchResponse response)
		{
			response.Version.Should().Be(2);
			response.Found.Should().BeTrue();
			response.Id.Should().Be(CallIsolatedValue);
		}
	}

	public class DeleteNonExistentWatchApiTests
		: ApiIntegrationTestBase<XPackCluster, IDeleteWatchResponse, IDeleteWatchRequest, DeleteWatchDescriptor, DeleteWatchRequest>
	{
		public DeleteNonExistentWatchApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;

		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 404;

		protected override Func<DeleteWatchDescriptor, IDeleteWatchRequest> Fluent => p => p;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeleteWatchRequest Initializer => new DeleteWatchRequest(CallIsolatedValue);

		protected override string UrlPath => $"/_watcher/watch/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.DeleteWatch(CallIsolatedValue, f),
			(client, f) => client.DeleteWatchAsync(CallIsolatedValue, f),
			(client, r) => client.DeleteWatch(r),
			(client, r) => client.DeleteWatchAsync(r)
		);

		protected override DeleteWatchDescriptor NewDescriptor() => new DeleteWatchDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(IDeleteWatchResponse response)
		{
			//This API returns different results depending on whether `.watches` exists or not
			if (response.ServerError?.Status == 404)
			{
				response.ServerError.Error.Reason.Should().Contain("no such index");
				return;
			}

			response.Version.Should().Be(1);
			response.Found.Should().BeFalse();
			response.Id.Should().Be(CallIsolatedValue);
		}
	}
}
