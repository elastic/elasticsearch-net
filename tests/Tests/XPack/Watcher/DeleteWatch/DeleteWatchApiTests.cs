// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Watcher.DeleteWatch
{
	public class DeleteWatchApiTests
		: ApiIntegrationTestBase<WatcherCluster, DeleteWatchResponse, IDeleteWatchRequest, DeleteWatchDescriptor, DeleteWatchRequest>
	{
		public DeleteWatchApiTests(WatcherCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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
				var putWatchResponse = client.Watcher.Put(callUniqueValue.Value, p => p
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
			(client, f) => client.Watcher.Delete(CallIsolatedValue, f),
			(client, f) => client.Watcher.DeleteAsync(CallIsolatedValue, f),
			(client, r) => client.Watcher.Delete(r),
			(client, r) => client.Watcher.DeleteAsync(r)
		);

		protected override DeleteWatchDescriptor NewDescriptor() => new DeleteWatchDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(DeleteWatchResponse response)
		{
			response.Version.Should().Be(2);
			response.Found.Should().BeTrue();
			response.Id.Should().Be(CallIsolatedValue);
		}
	}

	public class DeleteNonExistentWatchApiTests
		: ApiIntegrationTestBase<WatcherCluster, DeleteWatchResponse, IDeleteWatchRequest, DeleteWatchDescriptor, DeleteWatchRequest>
	{
		public DeleteNonExistentWatchApiTests(WatcherCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;

		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 404;

		protected override Func<DeleteWatchDescriptor, IDeleteWatchRequest> Fluent => p => p;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeleteWatchRequest Initializer => new DeleteWatchRequest(CallIsolatedValue);

		protected override string UrlPath => $"/_watcher/watch/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Watcher.Delete(CallIsolatedValue, f),
			(client, f) => client.Watcher.DeleteAsync(CallIsolatedValue, f),
			(client, r) => client.Watcher.Delete(r),
			(client, r) => client.Watcher.DeleteAsync(r)
		);

		protected override DeleteWatchDescriptor NewDescriptor() => new DeleteWatchDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(DeleteWatchResponse response)
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
