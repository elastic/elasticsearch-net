using System;
using Tests.Framework.Versions;

namespace Tests.Framework.ManagedElasticsearch.Plugins
{
	public class ElasticsearchPluginConfiguration
	{
		private readonly Func<ElasticsearchVersion, bool> _isValid;

		public ElasticsearchPlugin Plugin { get; }

		/// <summary>
		/// The moniker the plugin is known by in Elasticsearch e.g what /_cat/plugins will return for it
		/// </summary>
		public string Moniker { get; internal set; }

		/// <summary>
		/// The folder name under /plugins, defaults to moniker
		/// </summary>
		public string FolderName { get; internal set; }

		public ElasticsearchPluginConfiguration(ElasticsearchPlugin plugin) : this(plugin, null) { }

		public ElasticsearchPluginConfiguration(ElasticsearchPlugin plugin, Func<ElasticsearchVersion, bool> isValid)
		{
			Plugin = plugin;
			Moniker = plugin.Moniker();
			FolderName = plugin.Moniker();
			_isValid = isValid ?? (v => true);
		}

		public bool IsValid(ElasticsearchVersion version) => _isValid(version);

		public string DownloadUrl(ElasticsearchVersion version)  => version.PluginDownloadUrl(this.Moniker);


	}
}
