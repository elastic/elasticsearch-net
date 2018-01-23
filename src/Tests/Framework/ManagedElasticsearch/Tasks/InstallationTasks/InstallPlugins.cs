using System;
using System.IO;
using System.Linq;
using Tests.Framework.ManagedElasticsearch.Nodes;
using Tests.Framework.ManagedElasticsearch.Plugins;
using Tests.Framework.Versions;

namespace Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks
{
	public class InstallPlugins : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, NodeFileSystem fileSystem)
		{
			var v = config.ElasticsearchVersion;
			var plugins =
				from plugin in ElasticsearchPluginCollection.Supported
				let validForCurrentVersion = plugin.IsValid(v)
				let alreadyInstalled = AlreadyInstalled(plugin, fileSystem)
				where !alreadyInstalled && validForCurrentVersion
				select plugin;

			foreach (var plugin in plugins)
			{
				var installParameter = v.State == ElasticsearchVersion.ReleaseState.Released ? plugin.Moniker : this.UseHttpPluginLocation(fileSystem, plugin, v);
				this.ExecuteBinary(fileSystem.PluginBinary, $"install elasticsearch plugin: {plugin.Moniker}", "install --batch", installParameter);
			}
		}

		private static bool AlreadyInstalled(ElasticsearchPluginConfiguration plugin, NodeFileSystem fileSystem)
		{
			var folder = plugin.FolderName;
			var pluginFolder = Path.Combine(fileSystem.ElasticsearchHome, "plugins", folder);

			// assume plugin already installed
			return Directory.Exists(pluginFolder);
		}

		private string UseHttpPluginLocation(NodeFileSystem fileSystem, ElasticsearchPluginConfiguration plugin, ElasticsearchVersion v)
		{
			var downloadLocation = Path.Combine(fileSystem.RoamingFolder, $"{plugin.Moniker}-{v}.zip");
			this.DownloadPluginSnapshot(downloadLocation, plugin, v);
			//transform downloadLocation to file uri and use that to install from
			return new Uri(downloadLocation).AbsoluteUri;
		}

		private void DownloadPluginSnapshot(string downloadLocation, ElasticsearchPluginConfiguration plugin, ElasticsearchVersion v)
		{
			if (File.Exists(downloadLocation)) return;
			var downloadUrl = plugin.DownloadUrl(v);
			Console.WriteLine($"Download plugin snapshot {plugin.Moniker}: {downloadUrl}");
			try
			{
				this.DownloadFile(downloadUrl, downloadLocation);
				Console.WriteLine($"Downloaded plugin snapshot {plugin.Moniker}");
			}
			catch (Exception e)
			{
				Console.WriteLine($"Failed downloading plugin snapshot {plugin.Moniker}, {e.Message}");
			}
		}
	}
}
