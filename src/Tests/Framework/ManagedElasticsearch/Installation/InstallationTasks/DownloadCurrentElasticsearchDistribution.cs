using System;
using System.IO;
using Tests.Framework.Integration;

namespace Tests.Framework.ManagedElasticsearch.InstallationTasks
{
	public class DownloadCurrentElasticsearchDistribution : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, INodeFileSystem fileSystem)
		{
			var v = config.ElasticsearchVersion;
			var from = v.DownloadUrl;
			var to = fileSystem.DownloadZipLocation;
			if (File.Exists(to)) return;
			Console.WriteLine($"Download elasticsearch: {v} from {from}");
			this.DownloadFile(from, to);
			Console.WriteLine($"Downloaded elasticsearch: {v}");
		}
	}
}