// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Threading.Tasks;
using Tests.Core.Client;
using Tests.Domain;

namespace Tests.ClientConcepts.OpenTelemetry;

public class ActivityTest
{
	[U]
	public async Task BasicOpenTelemetryTest()
	{
		Activity oTelActivity = null;
		var listener = new ActivityListener
		{
			ActivityStarted = _ => { },
			ActivityStopped = activity => oTelActivity = activity,
			ShouldListenTo = activitySource => activitySource.Name == "Elastic.Clients.Elasticsearch.ElasticsearchClient",
			Sample = (ref ActivityCreationOptions<ActivityContext> _) => ActivitySamplingResult.AllData
		};
		ActivitySource.AddActivityListener(listener);

		var client = TestClient.DefaultInMemoryClient;

		client.Ping();

		VerifyActivity(oTelActivity);

		await client.PingAsync();

		VerifyActivity(oTelActivity);

		await client.SearchAsync<Project>(s => s.Index("test").Query(q => q.MatchAll()));

		VerifyActivity(oTelActivity, "Elasticsearch: POST /{index}/_search", "http://localhost:9200/test/_search?pretty=true&error_trace=true");

		static void VerifyActivity(Activity oTelActivity, string name = null, string url = null)
		{
			oTelActivity.Should().NotBeNull();

			oTelActivity.Kind.Should().Be(ActivityKind.Client);

			oTelActivity.DisplayName.Should().Be(name ?? "Elasticsearch: HEAD /");
			oTelActivity.OperationName.Should().Be(name ?? "Elasticsearch: HEAD /");

			oTelActivity.Tags.Should().Contain(n => n.Key == "db.system" && n.Value == "elasticsearch");
			oTelActivity.Tags.Should().Contain(n => n.Key == "http.url" && n.Value == (url ?? "http://localhost:9200/?pretty=true&error_trace=true"));
			oTelActivity.Tags.Should().Contain(n => n.Key == "net.peer.name" && n.Value == "localhost");

			oTelActivity.Status.Should().Be(ActivityStatusCode.Ok);
		}
	}
}
