using System;
using System.Reflection;

namespace Elasticsearch.Managed.Plugins
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
}
