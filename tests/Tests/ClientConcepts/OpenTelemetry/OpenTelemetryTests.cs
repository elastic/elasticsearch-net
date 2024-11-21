// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Tests.Core.Client;
using Tests.Domain;
using Xunit;

namespace Tests.ClientConcepts.OpenTelemetry;

[Collection(nameof(NonParallelCollection))]
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
			ShouldListenTo = activitySource => activitySource.Name == Elastic.Transport.Diagnostics.OpenTelemetry.ElasticTransportActivitySourceName,
			Sample = (ref ActivityCreationOptions<ActivityContext> _) => ActivitySamplingResult.AllData
		};
		ActivitySource.AddActivityListener(listener);

		var client = TestClient.DefaultInMemoryClient;

		client.Ping();

		VerifyActivity(oTelActivity, "ping", "HEAD");

		await client.PingAsync();

		VerifyActivity(oTelActivity, "ping", "HEAD");

		await client.SearchAsync<Project>(s => s.Index("test").Query(q => q.MatchAll(m => { })));

		VerifyActivity(oTelActivity, "search", "POST", "http://localhost:9200/test/_search?pretty=true&error_trace=true");

		static void VerifyActivity(Activity oTelActivity, string displayName, string operation, string url = null)
		{
			oTelActivity.Should().NotBeNull();

			oTelActivity.Kind.Should().Be(ActivityKind.Client);

			oTelActivity.OperationName.Should().Be(operation);
			oTelActivity.DisplayName.Should().Be(displayName);

			oTelActivity.Tags.Should().Contain(n => n.Key == "elastic.transport.product.name" && n.Value == "elasticsearch-net");
			oTelActivity.Tags.Should().Contain(n => n.Key == "db.system" && n.Value == "elasticsearch");
			oTelActivity.Tags.Should().Contain(n => n.Key == "db.operation" && n.Value == displayName);
			oTelActivity.Tags.Should().Contain(n => n.Key == "db.user" && n.Value == "elastic");
			oTelActivity.Tags.Should().Contain(n => n.Key == "url.full" && n.Value == (url ?? "http://localhost:9200/?pretty=true&error_trace=true"));
			oTelActivity.Tags.Should().Contain(n => n.Key == "server.address" && n.Value == "localhost");
			oTelActivity.Tags.Should().Contain(n => n.Key == "http.request.method" && n.Value == (displayName == "ping" ? "HEAD" : "POST"));

			switch (displayName)
			{
				case "search":
					oTelActivity.Tags.Should().Contain(n => n.Key == "db.elasticsearch.path_parts.index" && n.Value == "test");
					break;
				default:
					oTelActivity.Tags.Should().NotContain(n => n.Key == "db.elasticsearch.path_parts.index");
					break;
			}

			oTelActivity.Status.Should().Be(ActivityStatusCode.Ok);
		}
	}
}

[CollectionDefinition(nameof(NonParallelCollection), DisableParallelization = true)]
public class NonParallelCollection { }
