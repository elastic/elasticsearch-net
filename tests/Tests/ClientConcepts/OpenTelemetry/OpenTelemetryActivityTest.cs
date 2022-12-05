// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Tests.Core.Client.Settings;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.ClientConcepts.OpenTelemetry;

/// <summary>
/// Asserts that <see cref="ElasticsearchClient"/> emits an OpenTelemetry compatible <see cref="Activity"/>.
/// </summary>
public class OpenTelemetryActivityTest : ApiIntegrationTestBase<OpenTelemtryCluster, PingResponse, PingRequestDescriptor, PingRequest>
{
	Activity _oTelActivity = null;
	ActivityListener _listener = null;

	public OpenTelemetryActivityTest(OpenTelemtryCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override bool ExpectIsValid => true;
	protected override int ExpectStatusCode => 200;
	protected override HttpMethod ExpectHttpMethod => HttpMethod.HEAD;
	protected override string ExpectedUrlPathAndQuery => "/";

	protected override void IntegrationSetup(ElasticsearchClient client, CallUniqueValues values)
	{
		_listener = new ActivityListener
		{
			ActivityStarted = _ => { },
			ActivityStopped = activity => _oTelActivity = activity,
			ShouldListenTo = activitySource => activitySource.Name == "Elastic.Clients.Elasticsearch.ElasticsearchClient",
			Sample = (ref ActivityCreationOptions<ActivityContext> _) => ActivitySamplingResult.AllData
		};

		ActivitySource.AddActivityListener(_listener);
	}

	protected override void IntegrationTeardown(ElasticsearchClient client, CallUniqueValues values)
	{
		_listener.Dispose();
	}

	protected override LazyResponses ClientUsage() => Calls(
		(client, f) => client.Ping(),
		(client, f) => client.PingAsync(),
		(client, r) => client.Ping(r),
		(client, r) => client.PingAsync(r)
	);

	protected override void OnAfterCall(ElasticsearchClient client)
	{
		_oTelActivity.Should().NotBeNull();

		_oTelActivity.Kind.Should().Be(ActivityKind.Client);

		_oTelActivity.DisplayName.Should().Be("Elasticsearch: HEAD /");
		_oTelActivity.OperationName.Should().Be("Elasticsearch: HEAD /");

		_oTelActivity.Tags.Should().Contain(n => n.Key == "db.system" && n.Value == "elasticsearch");
		_oTelActivity.Tags.Should().Contain(n => n.Key == "http.url" && n.Value == $"http://{TestElasticsearchClientSettings.LocalOrProxyHost}:9200/?pretty=true&error_trace=true");
		_oTelActivity.Tags.Should().Contain(n => n.Key == "net.peer.name" && n.Value == TestElasticsearchClientSettings.LocalOrProxyHost);

		_oTelActivity.Status.Should().Be(ActivityStatusCode.Ok);
	}
}
