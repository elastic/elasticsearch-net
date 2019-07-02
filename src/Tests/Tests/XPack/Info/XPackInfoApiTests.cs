using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Info
{
	[SkipVersion("<6.8.0", "All APIs exist in Elasticsearch 6.8.0")]
	public class XPackInfoApiTests : CoordinatedIntegrationTestBase<XPackCluster>
	{
		private const string XPackInfoStep = nameof(XPackInfoStep);
		private const string XPackUsageStep = nameof(XPackUsageStep);

		public XPackInfoApiTests(XPackCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage)
		{
			{
				XPackInfoStep, u => u.Calls<XPackInfoDescriptor, XPackInfoRequest, IXPackInfoRequest, XPackInfoResponse>(
					v => new XPackInfoRequest(),
					(v, d) => d,
					(v, c, f) => c.XPack.Info(f),
					(v, c, f) => c.XPack.InfoAsync(f),
					(v, c, r) => c.XPack.Info(r),
					(v, c, r) => c.XPack.InfoAsync(r)
				)
			},
			{
				XPackUsageStep, u => u.Calls<XPackUsageDescriptor, XPackUsageRequest, IXPackUsageRequest, XPackUsageResponse>(
					v => new XPackUsageRequest(),
					(v, d) => d,
					(v, c, f) => c.XPack.Usage(f),
					(v, c, f) => c.XPack.UsageAsync(f),
					(v, c, r) => c.XPack.Usage(r),
					(v, c, r) => c.XPack.UsageAsync(r)
				)
			}
		}) { }

		[I] public async Task XPackInfoResponse() => await Assert<XPackInfoResponse>(XPackInfoStep, (v, r) =>
		{
			r.IsValid.Should().BeTrue();
			r.ApiCall.HttpStatusCode.Should().Be(200);

			r.Build.Should().NotBeNull();
			r.Features.Should().NotBeNull();
			r.Features.Ccr.Should().NotBeNull();
			r.Features.Graph.Should().NotBeNull();
			r.Features.Ilm.Should().NotBeNull();
			r.Features.Logstash.Should().NotBeNull();
			r.Features.MachineLearning.Should().NotBeNull();
			r.Features.MachineLearning.NativeCodeInformation.Should().NotBeNull();
			r.Features.Monitoring.Should().NotBeNull();
			r.Features.Rollup.Should().NotBeNull();
			r.Features.Security.Should().NotBeNull();
			r.Features.Sql.Should().NotBeNull();
			r.Features.Watcher.Should().NotBeNull();
			r.License.Should().NotBeNull();
		});

		[I] public async Task XPackUsageResponse() => await Assert<XPackUsageResponse>(XPackUsageStep, (v, r) =>
		{
			r.IsValid.Should().BeTrue();
			r.ApiCall.HttpStatusCode.Should().Be(200);

			r.Ccr.Should().NotBeNull();
			r.Graph.Should().NotBeNull();
			r.Logstash.Should().NotBeNull();
			r.MachineLearning.Should().NotBeNull();
			r.MachineLearning.Datafeeds.Should().NotBeNull();
			r.MachineLearning.Jobs.Should().NotBeNull();
			r.Monitoring.Should().NotBeNull();
			r.Monitoring.EnabledExporters.Should().NotBeNull();
			r.Rollup.Should().NotBeNull();
			r.Security.Should().NotBeNull();
			r.Security.Roles.Should().NotBeNull();
			r.Security.Realms.Should().NotBeNull();
			r.Security.RoleMapping.Should().NotBeNull();
			r.Sql.Should().NotBeNull();
			r.Sql.Features.Should().NotBeNull();
			r.Sql.Queries.Should().NotBeNull();
			r.Alerting.Should().NotBeNull();
			r.Alerting.Count.Should().NotBeNull();
			r.Alerting.Execution.Should().NotBeNull();
			r.Alerting.Watch.Should().NotBeNull();
		});
	}
}
