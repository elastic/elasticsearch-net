using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Framework.Versions;
using static Tests.Framework.Integration.ElasticsearchPlugin;

namespace Tests.Framework.Integration
{
	public enum ElasticsearchPlugin
	{
		DeleteByQuery,
		CloudAzure,
		MapperAttachments,
		MapperMurmer3,
		XPack
	}
	public static class ElasticsearchPlugins
	{
		public static AllPlugins Supported { get; } = new AllPlugins
		{
			{ DeleteByQuery,  "delete-by-query" },
			//{ CloudAzure,  "cloud-azure" },
			{ MapperAttachments,  "mapper-attachments" },
			{ MapperMurmer3,  "mapper-murmur3" },
			{ ElasticsearchPlugin.XPack,  "x-pack" },
		};

		public class AllPlugins : Dictionary<ElasticsearchPlugin, InstallCommand>
		{
			public void Add(ElasticsearchPlugin plugin, string moniker) =>
				this.Add(plugin, new InstallCommand(moniker));

			public void Add(ElasticsearchPlugin plugin, string moniker, string installPath) =>
				this.Add(plugin, new InstallCommand(moniker) { FolderName = installPath});

			public void Add(ElasticsearchPlugin plugin, string moniker, string installPath, Func<ElasticsearchVersion, string> versionedMoniker) =>
				this.Add(plugin, new InstallCommand (moniker) { InstallParameter = versionedMoniker, FolderName = installPath});
		}

		public class InstallCommand
		{
			public InstallCommand(string moniker)
			{
				this.Moniker = moniker;
			}

			/// <summary>
			/// The moniker the plugin is known by in Elasticsearch e.g what /_cat/plugins will return for it
			/// </summary>
			public string Moniker { get; private set; }

			/// <summary>
			/// Should return the full (versioned) install parameter e.g plugin.bat install [install-parameter]
			/// If not set defaults to Moniker
			/// </summary>
			public Func<ElasticsearchVersion, string> InstallParameter { get; internal set; }

			/// <summary>
			/// The folder name under /plugins, defaults to moniker
			/// </summary>
			public string FolderName { get; internal set; }
		}
	}
}
