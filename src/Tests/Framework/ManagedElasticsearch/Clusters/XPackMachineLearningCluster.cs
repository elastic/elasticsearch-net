using System;
using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Nodes;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;
using Tests.Framework.ManagedElasticsearch.Plugins;
using Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	[RequiresPlugin(ElasticsearchPlugin.XPack)]
	public class XPackMachineLearningCluster : XPackCluster
	{
		private string[] XPackMachineLearningSettings => TestClient.VersionUnderTestSatisfiedBy(">=5.4.0")
			? new[]
			{
				"xpack.ml.node_concurrent_job_allocations=4",
				TestClient.VersionUnderTestSatisfiedBy("<6.0.0")
					? "node.attr.ml.max_open_jobs=30"
					: "xpack.ml.max_open_jobs=30"
			}
			: new string[] {} ;

		protected override void SeedCluster() => new MachineLearningSeeder(this.Client, this.ClusterConfiguration.FileSystem).SeedNode();

		public override int MaxConcurrency => 1;

		public override TimeSpan StartTimeout => TimeSpan.FromMinutes(10);

		protected override string[] AdditionalServerSettings => base.AdditionalServerSettings.Concat(this.XPackMachineLearningSettings).ToArray();

		protected override InstallationTaskBase[] AdditionalInstallationTasks => base.AdditionalInstallationTasks.Concat(new InstallationTaskBase[]
		{
			new DownloadMachineLearningSampleDataDistribution()
		}).ToArray();
	}
}
