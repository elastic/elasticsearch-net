using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Document.Multiple.ReindexOnServer
{
	[SkipVersion("<2.3.0", "")]
	public class ReindexOnServerApiTests : ApiIntegrationTestBase<IntrusiveOperationCluster, IReindexOnServerResponse, IReindexOnServerRequest, ReindexOnServerDescriptor, ReindexOnServerRequest>
	{
		public class Test
		{
			public long Id { get; set; }
			public string Flag { get; set; }
		}

		public ReindexOnServerApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var index in values.Values)
			{
				this.Client.Index(new Test { Id = 1, Flag = "bar" }, i => i.Index(index).Refresh(Refresh.True));
				this.Client.Index(new Test { Id = 2, Flag = "bar" }, i => i.Index(index).Refresh(Refresh.True));
			}
		}
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.ReindexOnServer(f),
			fluentAsync: (client, f) => client.ReindexOnServerAsync(f),
			request: (client, r) => client.ReindexOnServer(r),
			requestAsync: (client, r) => client.ReindexOnServerAsync(r)
		);
		protected override void OnAfterCall(IElasticClient client) => client.Refresh(CallIsolatedValue);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override string UrlPath => $"/_reindex?refresh=true";

		protected override bool SupportsDeserialization => false;

		protected virtual string PainlessScript { get; } = "if (ctx._source.flag == 'bar') {ctx._source.remove('flag')}";

		protected override Func<ReindexOnServerDescriptor, IReindexOnServerRequest> Fluent => d => d
			.Source(s => s
				.Index(CallIsolatedValue)
				.Type("test")
				.Size(100)
				.Query<Test>(q => q
					.Match(m => m
						.Field(p => p.Flag)
						.Query("bar")
					)
				)
				.Sort<Test>(sort => sort
					.Ascending("id")
				)
			)
			.Destination(s => s
				.Index(CallIsolatedValue + "-clone")
				.Type("test")
				.OpType(OpType.Create)
				.VersionType(VersionType.Internal)
				.Routing(ReindexRouting.Discard)
			)
			.Script(ss => ss.Source(PainlessScript))
			.Conflicts(Conflicts.Proceed)
			.Refresh();

		protected override ReindexOnServerRequest Initializer => new ReindexOnServerRequest()
		{
			Source = new ReindexSource
			{
				Index = CallIsolatedValue,
				Type = "test",
				Query = new MatchQuery { Field = Field<Test>(p => p.Flag), Query = "bar" },
				Sort = new List<ISort> { new SortField { Field = "id", Order = SortOrder.Ascending } },
				Size = 100

			},
			Destination = new ReindexDestination
			{
				Index = CallIsolatedValue + "-clone",
				Type = Type<Test>(),
				OpType = OpType.Create,
				VersionType = VersionType.Internal,
				Routing = ReindexRouting.Discard
			},
			Script = new InlineScript(PainlessScript),
			Conflicts = Conflicts.Proceed,
			Refresh = true,
		};

		protected override void ExpectResponse(IReindexOnServerResponse response)
		{
			response.Task.Should().BeNull();
			response.Took.Should().BeGreaterThan(TimeSpan.FromMilliseconds(0));
			response.Total.Should().Be(2);
			response.Updated.Should().Be(0);
			response.Created.Should().Be(2);
			response.Batches.Should().Be(1);

			var search = this.Client.Search<Test>(s => s
				.Index(CallIsolatedValue + "-clone")
			);
			search.Total.Should().BeGreaterThan(0);
			search.Documents.Should().OnlyContain(t => string.IsNullOrWhiteSpace(t.Flag));
		}

		protected override object ExpectJson =>
			new
			{
				dest = new
				{
					index = $"{CallIsolatedValue}-clone",
					op_type = "create",
					routing = "discard",
					type = "test",
					version_type = "internal"
				},
				script = new
				{
					source = this.PainlessScript,
				},
				source = new
				{
					index = CallIsolatedValue,
					query = new { match = new { flag = new { query = "bar" } } },
					sort = new[] { new { id = new { order = "asc" } } },
					type = new[] { "test" },
					size = 100
				},
				conflicts = "proceed"
			};
	}
}
