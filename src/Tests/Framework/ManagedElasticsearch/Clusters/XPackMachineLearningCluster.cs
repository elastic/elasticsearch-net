using System;
using System.IO;
using System.Linq;
using Elastic.Managed.Ephemeral;
using Elastic.Managed.Ephemeral.Tasks;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	public class XPackMachineLearningCluster : XPackCluster
	{
		public XPackMachineLearningCluster() : base(new XPackClusterConfiguration
		{
			MaxConcurrency = 1,
			DefaultNodeSettings =
			{
				{ "xpack.ml.node_concurrent_job_allocations", "4", ">=5.4.0"},
				{ "node.attr.ml.max_open_jobs", "30", ">=5.4.0 <6.0.0"},
				{ "xpack.ml.max_open_jobs", "30", ">=6.0.0"}
			},
			AdditionalBeforeNodeStartedTasks =
			{
				new DownloadMachineLearningSampleDataDistribution()
			},
			Timeout = TimeSpan.FromMinutes(10),
		}) { }

		protected override void SeedCluster() => new MachineLearningSeeder(this.Client, this.ClusterConfiguration.FileSystem).SeedNode();
	}

	public class DownloadMachineLearningSampleDataDistribution : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			var to = Path.Combine(cluster.FileSystem.LocalFolder, "server_metrics.tar.gz");
			if (!File.Exists(to))
			{
				var from = "https://download.elasticsearch.org/demos/machine_learning/gettingstarted/server_metrics.tar.gz";
				Console.WriteLine($"Download machine learning sample data from: {from}");
				DownloadFile(from, to);
				Console.WriteLine($"Downloaded machine learning sample data to: {to}");
			}

			var directoryTarget = Path.Combine(cluster.FileSystem.LocalFolder, "server_metrics");

			if (Directory.Exists(directoryTarget)) return;

			Directory.CreateDirectory(directoryTarget);
			Console.WriteLine($"Unzipping machine learning sample data: {to} ...");
			using (var inStream = File.OpenRead(to))
			using (var gzipStream = new GZipInputStream(inStream))
			using (var tarArchive = TarArchive.CreateInputTarArchive(gzipStream))
			{
				tarArchive.ExtractContents(directoryTarget);
				tarArchive.Close();
			}
		}
	}
}
