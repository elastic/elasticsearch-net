using System;
using System.IO;
using System.Linq;
using Tests.Framework.Integration;
using Tests.Framework.Versions;

namespace Tests.Framework.ManagedElasticsearch.InstallationTasks
{
	public class InstallPlugins : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, INodeFileSystem fileSystem)
		{
			var v = config.ElasticsearchVersion;
			var plugins =
				from plugin in ElasticsearchPluginCollection.Supported
				let validForCurrentVersion = plugin.IsValid(v)
				let requiredByNode = config.RequiredPlugins.Contains(plugin.Plugin)
				let alreadyInstalled = AlreadyInstalled(plugin, fileSystem)
				where !alreadyInstalled && validForCurrentVersion && requiredByNode
				select plugin;

			foreach (var plugin in plugins)
			{
				var installParameter = !v.IsSnapshot ? plugin.Moniker : this.DownloadSnapshotIfNeeded(fileSystem, plugin, v);
				this.ExecuteBinary(fileSystem.PluginBinary, $"install elasticsearch plugin: {plugin.Moniker}", "install --batch", installParameter);
			}
		}

		private static bool AlreadyInstalled(ElasticsearchPluginConfiguration plugin, INodeFileSystem fileSystem)
		{
			var folder = plugin.FolderName;
			var pluginFolder = Path.Combine(fileSystem.ElasticsearchHome, "plugins", folder);

			// assume plugin already installed
			return Directory.Exists(pluginFolder);
		}

		private string DownloadSnapshotIfNeeded(INodeFileSystem fileSystem, ElasticsearchPluginConfiguration plugin, ElasticsearchVersion v)
		{
			var downloadLocation = Path.Combine(fileSystem.RoamingFolder, plugin.SnapshotZip(v));
			this.DownloadPluginSnapshot(downloadLocation, plugin, v);
			//transform downloadLocation to file uri and use that to install from
			return new Uri(downloadLocation).AbsoluteUri;
		}

		private void DownloadPluginSnapshot(string downloadLocation, ElasticsearchPluginConfiguration plugin, ElasticsearchVersion v)
		{
			if (File.Exists(downloadLocation)) return;
			var downloadUrl = plugin.SnapshotDownloadUrl(v);
			Console.WriteLine($"Download plugin snapshot {plugin.Moniker}: {downloadUrl}");
			this.DownloadFile(downloadUrl, downloadLocation);
			Console.WriteLine($"Download plugin snapshot {plugin.Moniker}");
		}
	}
}