// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Document.Multiple.ReindexOnServer
{
	[SkipVersion("<2.3.0", "")]
	public class ReindexOnServerApiTests
		: ApiIntegrationTestBase<IntrusiveOperationCluster, ReindexOnServerResponse, IReindexOnServerRequest, ReindexOnServerDescriptor,
			ReindexOnServerRequest>
	{
		public ReindexOnServerApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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
				script = new
				{
					source = PainlessScript,
				},
				source = new
				{
					index = CallIsolatedValue,
					query = new { match = new { flag = new { query = "bar" } } },
					sort = new[] { new { id = new { order = "asc" } } },
					size = 100
				},
				conflicts = "proceed"
			};

		protected override int ExpectStatusCode => 200;

		// Sort is deprecated
#pragma warning disable 618
		protected override Func<ReindexOnServerDescriptor, IReindexOnServerRequest> Fluent => d => d
			.Source(s => s
				.Index(CallIsolatedValue)
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
				.OpType(OpType.Create)
				.VersionType(VersionType.Internal)
				.Routing(ReindexRouting.Discard)
			)
			.Script(ss => ss.Source(PainlessScript))
			.Conflicts(Conflicts.Proceed)
			.Refresh();
#pragma warning restore 618

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ReindexOnServerRequest Initializer => new ReindexOnServerRequest()
		{
			Source = new ReindexSource
			{
				Index = CallIsolatedValue,
				Query = new MatchQuery { Field = Field<Test>(p => p.Flag), Query = "bar" },
#pragma warning disable 618
				Sort = new List<ISort> { new FieldSort { Field = "id", Order = SortOrder.Ascending } },
#pragma warning restore 618
				Size = 100
			},
			Destination = new ReindexDestination
			{
				Index = CallIsolatedValue + "-clone",
				OpType = OpType.Create,
				VersionType = VersionType.Internal,
				Routing = ReindexRouting.Discard
			},
			Script = new InlineScript(PainlessScript),
			Conflicts = Conflicts.Proceed,
			Refresh = true,
		};

		protected virtual string PainlessScript { get; } = "if (ctx._source.flag == 'bar') {ctx._source.remove('flag')}";

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath => $"/_reindex?refresh=true";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var index in values.Values)
			{
				Client.Index(new Test { Id = 1, Flag = "bar" }, i => i.Index(index).Refresh(Refresh.True));
				Client.Index(new Test { Id = 2, Flag = "bar" }, i => i.Index(index).Refresh(Refresh.True));
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.ReindexOnServer(f),
			(client, f) => client.ReindexOnServerAsync(f),
			(client, r) => client.ReindexOnServer(r),
			(client, r) => client.ReindexOnServerAsync(r)
		);

		protected override void OnAfterCall(IElasticClient client) => client.Indices.Refresh(CallIsolatedValue);

		protected override void ExpectResponse(ReindexOnServerResponse response)
		{
			response.Task.Should().BeNull();
			response.Took.Should().BeGreaterThan(TimeSpan.FromMilliseconds(0));
			response.Total.Should().Be(2);
			response.Updated.Should().Be(0);
			response.Created.Should().Be(2);
			response.Batches.Should().Be(1);

			var search = Client.Search<Test>(s => s
				.Index(CallIsolatedValue + "-clone")
			);
			search.Total.Should().BeGreaterThan(0);
			search.Documents.Should().OnlyContain(t => string.IsNullOrWhiteSpace(t.Flag));
		}

		public class Test
		{
			public string Flag { get; set; }
			public long Id { get; set; }
		}
	}
}
