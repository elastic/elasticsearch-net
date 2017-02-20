using System;
using System.IO;
using Tests.Framework.Configuration;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Nodes;

namespace Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks
{
	public class DownloadCurrentElasticsearchDistribution : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, NodeFileSystem fileSystem)
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
