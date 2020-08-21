// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using System.Linq;
using Elastic.Elasticsearch.Ephemeral;
using Elastic.Elasticsearch.Ephemeral.Tasks;
using Elastic.Elasticsearch.Managed.ConsoleWriters;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning
{
	public class MachineLearningCluster : XPackCluster
	{
		public MachineLearningCluster() : base(new XPackClusterConfiguration
		{
			MaxConcurrency = 1,
			DefaultNodeSettings =
			{
				{ "xpack.ml.node_concurrent_job_allocations", "4", ">=5.4.0" },
				{ "node.attr.ml.max_open_jobs", "30", ">=5.4.0 <6.0.0" },
				{ "xpack.ml.max_open_jobs", "30", ">=6.0.0" },
				// increase machine memory available for ML
				{ "xpack.ml.max_machine_memory_percent", "50" }
			},
			AdditionalBeforeNodeStartedTasks =
			{
				new DownloadMachineLearningSampleDataDistribution()
			},
			Timeout = TimeSpan.FromMinutes(10),
		}) { }

		protected override void SeedNode() => new MachineLearningSeeder(Client, ClusterConfiguration.FileSystem).SeedNode();
	}

	public class DownloadMachineLearningSampleDataDistribution : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			void W(string m) => cluster.Writer.WriteDiagnostic(nameof(DownloadMachineLearningSampleDataDistribution), m);
			var to = Path.Combine(cluster.FileSystem.LocalFolder, "server_metrics.tar.gz");
			if (!File.Exists(to))
			{
				var from = "https://download.elastic.co/demos/machine_learning/gettingstarted/server_metrics.tar.gz";
				W($"Download machine learning sample data from: {from}");
				DownloadFile(from, to);
				W($"Downloaded machine learning sample data to: {to}");
			}
			else
				W($"{to} already exists on disk using cached copy");

			var directoryTarget = Path.Combine(cluster.FileSystem.LocalFolder, "server_metrics");

			if (Directory.Exists(directoryTarget))
			{
				if (Directory.EnumerateFiles(directoryTarget).Any())
				{
					W($"{directoryTarget} already exists and appears to have files, bailing out");
					return;
				}
				MoveFiles(directoryTarget, W);
			}
			else
				W($"{directoryTarget} does not yet exist creating it now to unzip ML data into");

			Directory.CreateDirectory(directoryTarget);
			W($"Unzipping machine learning sample data: {to} ...");
			using (var inStream = File.OpenRead(to))
			using (var gzipStream = new GZipInputStream(inStream))
			using (var tarArchive = TarArchive.CreateInputTarArchive(gzipStream))
			{
				tarArchive.ExtractContents(directoryTarget);
				tarArchive.Close();
			}

			MoveFiles(directoryTarget, W);
		}

		private void MoveFiles(string directoryTarget, Action<string> w)
		{
			var filesSubDirectory = Path.Combine(directoryTarget, "files");
			if (!Directory.Exists(filesSubDirectory))
			{
				w($"{filesSubDirectory} does not exist assuming this is an old download of the ML server metrics tar");
				return;
			}

			w($" moving files in {filesSubDirectory} to expected location");
			foreach (var file in Directory.EnumerateFiles(filesSubDirectory))
			{
				var fileInfo = new FileInfo(file);
				fileInfo.MoveTo(Path.Combine(directoryTarget, fileInfo.Name));
			}

			Directory.Delete(filesSubDirectory, true);
		}
	}
}
