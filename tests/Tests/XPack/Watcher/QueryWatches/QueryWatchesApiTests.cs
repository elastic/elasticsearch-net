// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Watcher.QueryWatches
{
	public class QueryWatchesApiTests
		: ApiIntegrationTestBase<WatcherCluster, QueryWatchesResponse, IQueryWatchesRequest, QueryWatchesDescriptor, QueryWatchesRequest>
	{
		private readonly Dictionary<string, object> _termCondition = new() { { "metadata.name", new { value = "value" } } };

		public QueryWatchesApiTests(WatcherCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			size = 5, from = 0, query = new { term = _termCondition }, sort = new[] { new { _id = new { order = "asc" } } }
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<QueryWatchesDescriptor, IQueryWatchesRequest> Fluent => p => p
			.From(0)
			.Size(5)
			.Sort(s => s.Ascending("_id"))
			.Query(q => q.Term(t => t.Field("metadata.name").Value("value")));

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override QueryWatchesRequest Initializer => new()
		{
			From = 0,
			Size = 5,
			Sort = new List<ISort> { new FieldSort { Field = "_id", Order = SortOrder.Ascending } },
			Query = new TermQuery { Field = "metadata.name", Value = "value" }
		};

		protected override string UrlPath => "/_watcher/_query/watches";

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
					.Metadata(m => m.Add("name", "value"))
				);

				if (!putWatchResponse.IsValid)
					throw new Exception("Problem setting up integration test");
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Watcher.QueryWatches(f),
			(client, f) => client.Watcher.QueryWatchesAsync(f),
			(client, r) => client.Watcher.QueryWatches(r),
			(client, r) => client.Watcher.QueryWatchesAsync(r)
		);

		protected override void ExpectResponse(QueryWatchesResponse response)
		{
			response.IsValid.Should().BeTrue();
			response.Count.Should().Be(4);
			response.Watches.Count.Should().Be(4);

			foreach (var watchResult in response.Watches)
			{
				watchResult.Id.Should().NotBeNullOrEmpty();
				watchResult.SequenceNumber.Should().BeGreaterOrEqualTo(0);
				watchResult.PrimaryTerm.Should().Be(1);
				watchResult.Status.State.Active.Should().BeTrue();
				watchResult.Watch.Metadata["name"].Should().Be("value");
			}
		}
	}
}
