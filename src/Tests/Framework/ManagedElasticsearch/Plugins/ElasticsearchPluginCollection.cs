using System.Collections.ObjectModel;
using Tests.Framework.Versions;

namespace Tests.Framework.ManagedElasticsearch.Plugins
{
	public class ElasticsearchPluginCollection : KeyedCollection<ElasticsearchPlugin, ElasticsearchPluginConfiguration>
	{
		public static ElasticsearchPluginCollection Supported { get; } =
			new ElasticsearchPluginCollection
			{
				new ElasticsearchPluginConfiguration(ElasticsearchPlugin.DeleteByQuery, version => version < new ElasticsearchVersion("5.0.0")),
				new ElasticsearchPluginConfiguration(ElasticsearchPlugin.CloudAzure, version => version < new ElasticsearchVersion("5.0.0")),
				new ElasticsearchPluginConfiguration(ElasticsearchPlugin.License, version => version < new ElasticsearchVersion("5.0.0")),
				new ElasticsearchPluginConfiguration(ElasticsearchPlugin.Shield, version => version < new ElasticsearchVersion("5.0.0")),
				new ElasticsearchPluginConfiguration(ElasticsearchPlugin.Watcher, version => version < new ElasticsearchVersion("5.0.0")),
				new ElasticsearchPluginConfiguration(ElasticsearchPlugin.Graph, v => v >= new ElasticsearchVersion("2.3.0") && v < new ElasticsearchVersion("5.0.0")),

				new ElasticsearchPluginConfiguration(ElasticsearchPlugin.MapperMurmer3),
				new ElasticsearchPluginConfiguration(ElasticsearchPlugin.AnalysisKuromoji),
				new ElasticsearchPluginConfiguration(ElasticsearchPlugin.AnalysisIcu),

				new ElasticsearchPluginConfiguration(ElasticsearchPlugin.MapperAttachments, version => version >= new ElasticsearchVersion("2.4.0")),
				new ElasticsearchPluginConfiguration(ElasticsearchPlugin.XPack, version => version >= new ElasticsearchVersion("5.0.0")),
				new ElasticsearchPluginConfiguration(ElasticsearchPlugin.IngestGeoIp, version => version >= new ElasticsearchVersion("5.0.0")),
				new ElasticsearchPluginConfiguration(ElasticsearchPlugin.IngestAttachment, version => version >= new ElasticsearchVersion("5.0.0")),
			};

		protected override ElasticsearchPlugin GetKeyForItem(ElasticsearchPluginConfiguration item) => item.Plugin;
	}
}
