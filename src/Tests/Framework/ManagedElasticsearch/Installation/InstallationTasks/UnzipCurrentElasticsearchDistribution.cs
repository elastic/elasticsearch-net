using System;
using System.IO;
using System.IO.Compression;
using Tests.Framework.Integration;

namespace Tests.Framework.ManagedElasticsearch.InstallationTasks
{
	public class UnzipCurrentElasticsearchDistribution : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, INodeFileSystem fileSystem)
		{
			var v = config.ElasticsearchVersion;
			if (Directory.Exists(fileSystem.ElasticsearchHome)) return;
			Console.WriteLine($"Unzipping elasticsearch: {v} ...");
			ZipFile.ExtractToDirectory(fileSystem.DownloadZipLocation, fileSystem.RoamingFolder);
		}
	}
}