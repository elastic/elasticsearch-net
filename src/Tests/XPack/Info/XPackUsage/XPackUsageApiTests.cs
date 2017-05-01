using System.Collections.Generic;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.Info.XPackUsage
{
	[SkipVersion("<5.4.0", "")]
	public class XPackUsageApiTests : ApiIntegrationTestBase<XPackCluster, IXPackUsageResponse, IXPackUsageRequest, XPackUsageDescriptor, XPackUsageRequest>
	{
		public XPackUsageApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.XPackUsage(f),
			fluentAsync: (client, f) => client.XPackUsageAsync(f),
			request: (client, r) => client.XPackUsage(r),
			requestAsync: (client, r) => client.XPackUsageAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override string UrlPath => $"/_xpack/usage";

		protected override bool SupportsDeserialization => true;

		protected override XPackUsageRequest Initializer => new XPackUsageRequest();

		protected override void ExpectResponse(IXPackUsageResponse response)
		{
			response.Graph.Should().NotBeNull("graph object");
			response.MachineLearning.Should().NotBeNull("ml object");
			response.MachineLearning.Datafeeds.Should().NotBeEmpty("ml datafeeds");
			var datafeed = response.MachineLearning.Datafeeds["_all"];
			datafeed.Should().NotBeNull("_all datafeed");
			var allJob = response.MachineLearning.Jobs["_all"];
			allJob.Should().NotBeNull("_all job");
			allJob.Detectors.Should().NotBeNull("_all job detectors");
			allJob.ModelSize.Should().NotBeNull("_all job model_size");

			response.Monitoring.Should().NotBeNull("monitoring object");
			response.Monitoring.EnabledExporters.Should().NotBeEmpty("monitoring exporters").And.ContainKey("local");
			response.Monitoring.EnabledExporters["local"].Should().Be(1);

			response.Security.Should().NotBeNull("security object");
			response.Security.Anonymous.Should().NotBeNull("security anonymous object");
			response.Security.Audit.Should().NotBeNull("security audit object");
			response.Security.Audit.Outputs.Should().NotBeEmpty("security audit outputs").And.Contain("logfile");
			response.Security.IpFilter.Should().NotBeNull("security ipfilter object");
			response.Security.Realms.Should().NotBeEmpty("security realms");
			response.Security.Roles.Should().NotBeEmpty("security roles");
			response.Alerting.Should().NotBeNull("alerting object");
			response.Alerting.Count.Should().NotBeNull("alerting exection count object");
			response.Alerting.Execution.Should().NotBeNull("alerting exection object");
			response.Alerting.Execution.Actions.Should().NotBeEmpty("alerting exection actions").And.ContainKey("_all");
		}
	}
}
