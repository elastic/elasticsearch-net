using System;
using System.Collections.Generic;
using System.IO;
using Tests.Framework.ManagedElasticsearch.InstallationTasks;

namespace Tests.Framework.Integration
{
	public class ElasticsearchInstaller : IDisposable
	{
		public NodeConfiguration NodeConfiguration { get; }
		private static readonly object Lock = new object();

		public ElasticsearchInstaller(NodeConfiguration nodeConfiguration)
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

		public void Install()
		{
			if (!this.NodeConfiguration.RunIntegrationTests) return;
			lock (ElasticsearchInstaller.Lock)
				foreach (var task in InstallationTasks)
					task.Run(this.NodeConfiguration, this.NodeConfiguration.FileSystem);
		}

		public void OnBeforeStart(string [] serverSettings)
		{
			if (!this.NodeConfiguration.RunIntegrationTests) return;
			lock (ElasticsearchInstaller.Lock)
				foreach (var task in BeforeStart)
					task.Run(this.NodeConfiguration, this.NodeConfiguration.FileSystem, serverSettings);
		}

		public void Dispose()
		{
			if (!this.NodeConfiguration.RunIntegrationTests) return;
			lock (ElasticsearchInstaller.Lock)
				foreach (var task in NodeStoppedTasks)
					task.Run(this.NodeConfiguration, this.NodeConfiguration.FileSystem);
		}
	}
}
