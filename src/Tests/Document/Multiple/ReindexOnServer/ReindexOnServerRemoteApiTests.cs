using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Document.Multiple.ReindexOnServer
{
	[SkipVersion("<2.3.0", "")]
	public class ReindexOnServerRemoteApiTests : ApiTestBase<IntrusiveOperationCluster, IReindexOnServerResponse, IReindexOnServerRequest, ReindexOnServerDescriptor, ReindexOnServerRequest>
	{
		private readonly Uri _host = new Uri("http://myremoteserver.example:9200");
		public ReindexOnServerRemoteApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.ReindexOnServer(f),
			fluentAsync: (client, f) => client.ReindexOnServerAsync(f),
			request: (client, r) => client.ReindexOnServer(r),
			requestAsync: (client, r) => client.ReindexOnServerAsync(r)
		);
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/_reindex";
		protected override bool SupportsDeserialization => false;

		protected override Func<ReindexOnServerDescriptor, IReindexOnServerRequest> Fluent => d => d
			.Source(s => s
				.Remote(r=>r.Host(_host).Username("user").Password("changeme"))
				.Index(CallIsolatedValue)
				.Type("test")
				.Size(100)
			)
			.Destination(s => s
				.Index(CallIsolatedValue + "-clone")
				.Type("test")
			);

		protected override ReindexOnServerRequest Initializer => new ReindexOnServerRequest()
		{
			Source = new ReindexSource
			{
				Remote = new RemoteSource
				{
					Host = _host,
					Username = "user",
					Password = "changeme"
				},
				Index = CallIsolatedValue,
				Type = "test",
				Size = 100

			},
			Destination = new ReindexDestination
			{
				Index = CallIsolatedValue + "-clone",
				Type = "test",
			}
		};

		protected override object ExpectJson =>
			new
			{
				dest = new
				{
					index = $"{CallIsolatedValue}-clone",
					type = "test"
				},
				source = new
				{
					remote = new
					{
						host = "http://myremoteserver.example:9200",
						username = "user",
						password = "changeme"
					},
					index = CallIsolatedValue,
					type = new[] { "test" },
					size = 100
				}
			};
	}
}
