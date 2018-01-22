using System;
using System.Linq;
using Nest;
using Tests.Framework.ManagedElasticsearch.Nodes;
using Tests.Framework.ManagedElasticsearch.Plugins;

namespace Tests.Framework.ManagedElasticsearch.Tasks.ValidationTasks
{
	public class ValidatePluginsTask : NodeValidationTaskBase
	{
		public override void Validate(IElasticClient client, NodeConfiguration configuration)
		{
			var v = configuration.ElasticsearchVersion;

			var requiredMonikers = ElasticsearchPluginCollection.Supported
				.Where(plugin => plugin.IsValid(v) && configuration.RequiredPlugins.Contains(plugin.Plugin))
				.Select(plugin => plugin.Moniker)
				.ToList();

			if (!requiredMonikers.Any()) return;
			var checkPlugins = client.CatPlugins();

			if (!checkPlugins.IsValid)
				throw new Exception($"Failed to check plugins: {checkPlugins.DebugInformation}.");

			var missingPlugins = requiredMonikers
				.Except((checkPlugins.Records ?? Enumerable.Empty<CatPluginsRecord>()).Select(r => r.Component))
				.ToList();
			if (!missingPlugins.Any()) return;

			var pluginsString = string.Join(", ", missingPlugins);
			throw new Exception($"Already running elasticsearch missed the following plugin(s): {pluginsString}.");
		}
	}
}
