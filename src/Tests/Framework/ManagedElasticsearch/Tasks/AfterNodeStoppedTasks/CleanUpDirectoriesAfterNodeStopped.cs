using System;
using System.IO;
using Tests.Framework.Configuration;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Nodes;

namespace Tests.Framework.ManagedElasticsearch.Tasks.AfterNodeStoppedTasks
{
	public class CleanUpDirectoriesAfterNodeStopped : AfterNodeStoppedTaskBase
	{
		public override void Run(NodeConfiguration config, NodeFileSystem fs)
		{
			if (Directory.Exists(fs.DataPath))
			{
				Console.WriteLine($"attempting to delete cluster data: {fs.DataPath}");
				Directory.Delete(fs.DataPath, true);
			}

			if (Directory.Exists(fs.LogsPath))
			{
				var files = Directory.GetFiles(fs.LogsPath, config.ClusterName + "*.log");
				foreach (var f in files)
				{
					Console.WriteLine($"attempting to delete log file: {f}");
					File.Delete(f);
				}
			}

			if (Directory.Exists(fs.RepositoryPath))
			{
				Console.WriteLine("attempting to delete repositories");
				Directory.Delete(fs.RepositoryPath, true);
			}
		}
	}
}
