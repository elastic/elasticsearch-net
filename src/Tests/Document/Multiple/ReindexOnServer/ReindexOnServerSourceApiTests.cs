using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using static Nest.Infer;

namespace Tests.Document.Multiple.ReindexOnServer
{
	public class ReindexOnServerSourceApiTests : ApiIntegrationTestBase<IntrusiveOperationCluster, IReindexOnServerResponse, IReindexOnServerRequest, ReindexOnServerDescriptor, ReindexOnServerRequest>
	{
		public class Test
		{
			public long Id { get; set; }
			public string Flag { get; set; }
		}

		public ReindexOnServerSourceApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var index in values.Values)
			{
				this.Client.Bulk(b => b
					.Index(index)
					.IndexMany(new []
					{
						new Test { Id = 1, Flag = "bar" },
						new Test { Id = 2, Flag = "bar" }
					})
					.Refresh(Refresh.WaitFor)
				);
			}
		}
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.ReindexOnServer(f),
			fluentAsync: (client, f) => client.ReindexOnServerAsync(f),
			request: (client, r) => client.ReindexOnServer(r),
			requestAsync: (client, r) => client.ReindexOnServerAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override string UrlPath => $"/_reindex?refresh=true";

		protected override bool SupportsDeserialization => false;

		protected override Func<ReindexOnServerDescriptor, IReindexOnServerRequest> Fluent => d => d
			.Source(s => s
				.Index(CallIsolatedValue)
				.Type("test")
				.Source<Test>(f => f
					.Field(ff => ff.Id)
					.Field(ff => ff.Flag)
				)
			)
			.Destination(s => s
				.Index(CallIsolatedValue + "-clone")
				.Type("test")
			)
			.Conflicts(Conflicts.Proceed)
			.Refresh();

		protected override ReindexOnServerRequest Initializer => new ReindexOnServerRequest
		{
			Source = new ReindexSource
			{
				Index = CallIsolatedValue,
				Type = "test",
				Source = Infer.Fields<Test>(
					ff => ff.Id,
					ff => ff.Flag
				)
			},
			Destination = new ReindexDestination
			{
				Index = CallIsolatedValue + "-clone",
				Type = Type<Test>(),
			},
			Conflicts = Conflicts.Proceed,
			Refresh = true,
		};

		protected override void ExpectResponse(IReindexOnServerResponse response)
		{
			response.ShouldBeValid();
		}

		protected override object ExpectJson =>
			new
			{
				dest = new
				{
					index = $"{CallIsolatedValue}-clone",
					type = "test",
				},
				source = new
				{
					index = CallIsolatedValue,
					_source = new [] { "id", "flag" },
					type = new[] { "test" },
				},
				conflicts = "proceed"
			};
	}
}
