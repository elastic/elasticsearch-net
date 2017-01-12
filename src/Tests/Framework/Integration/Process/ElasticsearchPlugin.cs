using System;

namespace Tests.Framework.Integration
{
	public class ElasticsearchPlugin
	{
		private readonly string _name;
		private readonly Func<ElasticsearchVersionInfo, bool> _valid;
		private readonly Func<ElasticsearchVersionInfo, string> _pluginDirectory;
		private readonly Func<ElasticsearchVersionInfo, string> _pluginVersion;

		public ElasticsearchPlugin(string name) : this(name, null, null, null) {}

		public ElasticsearchPlugin(string name, Func<ElasticsearchVersionInfo, bool> valid) : this(name, valid, null, null) {}

		public ElasticsearchPlugin(
			string name,
			Func<ElasticsearchVersionInfo, bool> valid,
			Func<ElasticsearchVersionInfo, string> pluginDirectory,
			Func<ElasticsearchVersionInfo, string> pluginVersion)
		{
			if (name == null) throw new ArgumentNullException(nameof(name));

			_name = name;
			_valid = valid;
			_pluginDirectory = pluginDirectory;
			_pluginVersion = pluginVersion;
		}

		public string Name => _name;

		public string PluginDirectory(ElasticsearchVersionInfo elasticsearchVersion) =>
			_pluginDirectory?.Invoke(elasticsearchVersion) ?? _name;

		public string PluginVersion(ElasticsearchVersionInfo elasticsearchVersion) =>
			_pluginVersion?.Invoke(elasticsearchVersion) ?? _name;

		public bool IsValid(ElasticsearchVersionInfo version) =>
			_valid?.Invoke(version) ?? true;

	}
}
