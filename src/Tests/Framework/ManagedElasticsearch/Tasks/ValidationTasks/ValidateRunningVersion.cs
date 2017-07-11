using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework.Configuration;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Nodes;
using Tests.Framework.Versions;

namespace Tests.Framework.ManagedElasticsearch.Tasks.ValidationTasks
{
	public class ValidateRunningVersion : NodeValidationTaskBase
	{
		public override void Validate(IElasticClient client, NodeConfiguration configuration)
		{
			if (!configuration.TestAgainstAlreadyRunningElasticsearch) return;
			var alreadyUp = client.RootNodeInfo();
			if (!alreadyUp.IsValid) return;
			var v = configuration.ElasticsearchVersion;

			var alreadyUpVersion = ElasticsearchVersion.GetOrAdd(alreadyUp.Version.Number);
			var alreadyUpSnapshotVersion = ElasticsearchVersion.GetOrAdd(alreadyUp.Version.Number + "-SNAPSHOT");
			if (v != alreadyUpVersion && v != alreadyUpSnapshotVersion)
				throw new Exception($"running elasticsearch is version {alreadyUpVersion} but the test config dictates {v}");
		}
	}
}
