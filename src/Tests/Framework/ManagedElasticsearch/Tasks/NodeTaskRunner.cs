using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nest;
using Tests.Framework.Configuration;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Nodes;
using Tests.Framework.ManagedElasticsearch.Tasks.AfterNodeStoppedTasks;
using Tests.Framework.ManagedElasticsearch.Tasks.BeforeStartNodeTasks;
using Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks;
using Tests.Framework.ManagedElasticsearch.Tasks.ValidationTasks;

namespace Tests.Framework.ManagedElasticsearch.Tasks
{
	public class NodeTaskRunner : IDisposable
	{
		public NodeConfiguration NodeConfiguration { get; }
		private static readonly object Lock = new object();

		public NodeTaskRunner(NodeConfiguration nodeConfiguration)
		{
			this.NodeConfiguration = nodeConfiguration;
		}

		private static IEnumerable<InstallationTaskBase> InstallationTasks { get; } = new List<InstallationTaskBase>
		{
			new CreateNestApplicationDirectory(),
			new EnsureJavaHomeEnvironmentVariableIsSet(),
			new DownloadCurrentElasticsearchDistribution(),
			new UnzipCurrentElasticsearchDistribution(),
			new CreateEasyRunBatFile(),
			new InstallPlugins(),
			new WriteAnalysisFiles(),
			new EnsureSecurityRolesFileExists(),
			new EnsureWatcherActionConfigurationInElasticsearchYaml(),
			new EnsureSecurityRolesFileExists(),
			new EnsureSecurityRealms(),
			new EnsureSecurityUsersInDefaultRealmAreAdded(),
		};
		private static IEnumerable<BeforeStartNodeTaskBase> BeforeStart { get; } = new List<BeforeStartNodeTaskBase>
		{
			new CreateEasyRunClusterBatFile()
		};
		private static IEnumerable<AfterNodeStoppedTaskBase> NodeStoppedTasks { get; } = new List<AfterNodeStoppedTaskBase>
		{
			new CleanUpDirectoriesAfterNodeStopped()
		};

		private static IEnumerable<NodeValidationTaskBase> ValidationTasks { get; } = new List<NodeValidationTaskBase>
		{
			new ValidateRunningVersion(),
			new ValidateLicenseTask(),
			new ValidatePluginsTask(),
			new ValidateClusterStateTask()
		};

		public void Install(InstallationTaskBase[] additionalInstallationTasks)=>
			Itterate(
				InstallationTasks.Concat(additionalInstallationTasks ?? Enumerable.Empty<InstallationTaskBase>()),
				(t, n,  fs) => t.Run(n, fs)
			);

		public void Dispose() =>
			Itterate(NodeStoppedTasks, (t, n,  fs) => t.Run(n, fs));

		public void OnBeforeStart(string [] serverSettings) =>
			Itterate(BeforeStart, (t, n,  fs) => t.Run(n, fs, serverSettings), log: false);

		public void ValidateAfterStart(IElasticClient client) =>
			Itterate(ValidationTasks, (t, n,  fs) => t.Validate(client, n), log: false);

		private IList<string> GetCurrentRunnerLog()
		{
			var log = this.NodeConfiguration.FileSystem.TaskRunnerFile;
			return !File.Exists(log) ? new List<string>() : File.ReadAllLines(log).ToList();
		}
		private void LogTasks(IList<string> logs)
		{
			var log = this.NodeConfiguration.FileSystem.TaskRunnerFile;
			File.WriteAllText(log, string.Join(Environment.NewLine, logs));
		}

		private void Itterate<T>(IEnumerable<T> collection, Action<T, NodeConfiguration, NodeFileSystem> act, bool log = true)
		{
			if (!this.NodeConfiguration.RunIntegrationTests) return;
			lock (NodeTaskRunner.Lock)
			{
				var taskLog = this.GetCurrentRunnerLog();
				foreach (var task in collection)
				{
					var name = task.GetType().Name;
					if (log && taskLog.Contains(name)) continue;
					act(task,this.NodeConfiguration, this.NodeConfiguration.FileSystem);
					if (log) taskLog.Add(name);
				}
				if (log) this.LogTasks(taskLog);
			}
		}



	}
}
