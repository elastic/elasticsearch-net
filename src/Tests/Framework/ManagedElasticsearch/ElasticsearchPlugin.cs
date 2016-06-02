using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Tests.Framework.Versions;
using static Tests.Framework.Integration.ElasticsearchPlugin;

namespace Tests.Framework.Integration
{
	[AttributeUsage(AttributeTargets.Field)]
	public class MonikerAttribute : Attribute
	{
		public string Moniker { get; }

		public MonikerAttribute(string moniker)
		{
			if (moniker == null) throw new ArgumentNullException(nameof(moniker));
			if (moniker.Length == 0) throw new ArgumentException("must have a value");
			Moniker = moniker;
		}
	}

	public enum ElasticsearchPlugin
	{
		[Moniker("delete-by-query")]
		DeleteByQuery,

		[Moniker("cloud-azure")]
		CloudAzure,

		[Moniker("mapper-attachments")]
		MapperAttachments,

		[Moniker("mapper-murmur3")]
		MapperMurmer3,

		[Moniker("x-pack")]
		XPack
	}

	public static class ElasticsearchPluginExtensions
	{
		public static string Moniker(this ElasticsearchPlugin plugin)
		{
			var info = typeof(ElasticsearchPlugin).GetField(plugin.ToString());
			var da = info.GetCustomAttribute<MonikerAttribute>();

			if (da == null) throw new InvalidOperationException($"{plugin} plugin must have a {nameof(MonikerAttribute)}");
			return da.Moniker;
		}
	}

	public class ElasticsearchPluginCollection : KeyedCollection<ElasticsearchPlugin, ElasticsearchPluginConfiguration>
	{
		public static ElasticsearchPluginCollection Supported { get; } =
			new ElasticsearchPluginCollection
			{
				new ElasticsearchPluginConfiguration(DeleteByQuery,
					version => version < new ElasticsearchVersion("5.0.0-alpha3")),
				new ElasticsearchPluginConfiguration(MapperAttachments),
				new ElasticsearchPluginConfiguration(MapperMurmer3),
				new ElasticsearchPluginConfiguration(ElasticsearchPlugin.XPack),
			};

		protected override ElasticsearchPlugin GetKeyForItem(ElasticsearchPluginConfiguration item)
		{
			return item.Plugin;
		}
	}

	public class ElasticsearchPluginConfiguration
	{
		private readonly Func<ElasticsearchVersion, bool> _isValid;
		private readonly Func<ElasticsearchVersion, string> _installParameter;

		public ElasticsearchPlugin Plugin { get; }

		/// <summary>
		/// The moniker the plugin is known by in Elasticsearch e.g what /_cat/plugins will return for it
		/// </summary>
		public string Moniker { get; internal set; }

		/// <summary>
		/// The folder name under /plugins, defaults to moniker
		/// </summary>
		public string FolderName { get; internal set; }

		public ElasticsearchPluginConfiguration(ElasticsearchPlugin plugin)
			: this(plugin, null, null)
		{
		}

		public ElasticsearchPluginConfiguration(
			ElasticsearchPlugin plugin,
			Func<ElasticsearchVersion, bool> isValid)
			: this(plugin, isValid, null)
		{
		}

		public ElasticsearchPluginConfiguration(
			ElasticsearchPlugin plugin,
			Func<ElasticsearchVersion, bool> isValid,
			Func<ElasticsearchVersion, string> installParameter)
		{
			Plugin = plugin;
			Moniker = plugin.Moniker();
			FolderName = plugin.Moniker();
			_installParameter = installParameter ?? (v => Moniker);
			_isValid = isValid ?? (v => true);
		}

		public string InstallParamater(ElasticsearchVersion version) => _installParameter(version);

		public bool IsValid(ElasticsearchVersion version) => _isValid(version);
	}
}

