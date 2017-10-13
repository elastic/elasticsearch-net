using System;
using System.IO;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;
using Tests.Framework.ManagedElasticsearch.Nodes;

namespace Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks
{
	public class DownloadMachineLearningSampleDataDistribution : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, NodeFileSystem fileSystem)
		{
			var to = Path.Combine(config.FileSystem.RoamingFolder, "server_metrics.tar.gz");
			if (!File.Exists(to))
			{
				var from = "https://download.elasticsearch.org/demos/machine_learning/gettingstarted/server_metrics.tar.gz";
				Console.WriteLine($"Download machine learning sample data from: {from}");
				this.DownloadFile(from, to);
				Console.WriteLine($"Downloaded machine learning sample data to: {to}");
			}

			var directoryTarget = Path.Combine(config.FileSystem.RoamingFolder, "server_metrics");

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
