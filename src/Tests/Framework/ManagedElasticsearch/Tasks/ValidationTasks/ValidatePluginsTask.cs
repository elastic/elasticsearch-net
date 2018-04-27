using System;
using System.Linq;
using Nest;
using Tests.Document.Multiple.UpdateByQuery;
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

			// 6.2.4 splits out X-Pack into separate plugin names
			if (requiredMonikers.Contains(ElasticsearchPlugin.XPack.Moniker()) && TestClient.VersionUnderTestSatisfiedBy(">=6.2.4"))
			{
				requiredMonikers.Remove(ElasticsearchPlugin.XPack.Moniker());
				requiredMonikers.Add(ElasticsearchPlugin.XPack.Moniker() + "-core");
			}

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
