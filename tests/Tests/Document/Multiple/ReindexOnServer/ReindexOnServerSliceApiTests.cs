// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elastic.Transport;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Document.Multiple.ReindexOnServer
{
	public class ReindexOnServerSliceApiTests
		: ApiIntegrationTestBase<IntrusiveOperationCluster, ReindexOnServerResponse, IReindexOnServerRequest, ReindexOnServerDescriptor,
			ReindexOnServerRequest>
	{
		public ReindexOnServerSliceApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson =>
			new
			{
				dest = new
				{
					index = $"{CallIsolatedValue}-clone",
					op_type = "create",
					routing = "discard",
					version_type = "internal"
				},
				source = new
				{
					index = CallIsolatedValue,
					query = new { match_all = new { } },
					sort = new[] { new { id = new { order = "asc" } } },
					size = 100,
					slice = new
					{
						field = "id",
						id = 0,
						max = 2
					}
				},
				conflicts = "proceed"
			};

		protected override int ExpectStatusCode => 200;

#pragma warning disable 618
		protected override Func<ReindexOnServerDescriptor, IReindexOnServerRequest> Fluent => d => d
			.Source(s => s
				.Index(CallIsolatedValue)
				.Size(100)
				.Query<Test>(q => q
					.MatchAll()
				)
				.Sort<Test>(sort => sort
					.Ascending("id")
				)
				.Slice<Test>(ss => ss
					.Field(f => f.Id)
					.Id(0)
					.Max(2)
				)
			)
			.Destination(s => s
				.Index(CallIsolatedValue + "-clone")
				.OpType(OpType.Create)
				.VersionType(VersionType.Internal)
				.Routing(ReindexRouting.Discard)
			)
			.Conflicts(Conflicts.Proceed)
			.Refresh();
#pragma warning restore 618

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ReindexOnServerRequest Initializer => new ReindexOnServerRequest
		{
			Source = new ReindexSource
			{
				Index = CallIsolatedValue,
				Query = new MatchAllQuery(),
#pragma warning disable 618
				Sort = new List<ISort> { new FieldSort { Field = "id", Order = SortOrder.Ascending } },
#pragma warning restore 618
				Size = 100,
				Slice = new SlicedScroll { Field = "id", Id = 0, Max = 2 }
			},
			Destination = new ReindexDestination
			{
				Index = CallIsolatedValue + "-clone",
				OpType = OpType.Create,
				VersionType = VersionType.Internal,
				Routing = ReindexRouting.Discard
			},
			Conflicts = Conflicts.Proceed,
			Refresh = true,
		};

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath => $"/_reindex?refresh=true";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var index in values.Values)
				Client.Bulk(b => b
					.Index(index)
					.IndexMany(new[]
					{
						new Test { Id = 1, Flag = "bar" },
						new Test { Id = 2, Flag = "bar" }
					})
					.Refresh(Refresh.WaitFor)
				);
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.ReindexOnServer(f),
			(client, f) => client.ReindexOnServerAsync(f),
			(client, r) => client.ReindexOnServer(r),
			(client, r) => client.ReindexOnServerAsync(r)
		);

		protected override void ExpectResponse(ReindexOnServerResponse response) => response.SliceId.Should().Be(0);

		public class Test
		{
			public string Flag { get; set; }
			public long Id { get; set; }
		}
	}
}
