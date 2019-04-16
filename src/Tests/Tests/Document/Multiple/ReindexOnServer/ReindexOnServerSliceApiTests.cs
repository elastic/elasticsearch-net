using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using static Nest.Infer;

namespace Tests.Document.Multiple.ReindexOnServer
{
	public class ReindexOnServerSliceApiTests
		: ApiIntegrationTestBase<IntrusiveOperationCluster, IReindexOnServerResponse, IReindexOnServerRequest, ReindexOnServerDescriptor,
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

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ReindexOnServerRequest Initializer => new ReindexOnServerRequest
		{
			Source = new ReindexSource
			{
				Index = CallIsolatedValue,
				Query = new MatchAllQuery(),
				Sort = new List<ISort> { new SortField { Field = "id", Order = SortOrder.Ascending } },
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

		protected override void ExpectResponse(IReindexOnServerResponse response) => response.SliceId.Should().Be(0);

		public class Test
		{
			public string Flag { get; set; }
			public long Id { get; set; }
		}
	}
}
