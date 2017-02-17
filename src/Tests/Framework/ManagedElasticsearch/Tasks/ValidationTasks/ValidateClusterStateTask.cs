using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework.Configuration;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Nodes;

namespace Tests.Framework.ManagedElasticsearch.Tasks.ValidationTasks
{
	public class ValidateClusterStateTask : NodeValidationTaskBase
	{
		private static TimeSpan ClusterHealthTimeout { get; } = TimeSpan.FromSeconds(20);

		public override void Validate(IElasticClient client, NodeConfiguration configuration)
		{
			var healthyCluster = client.ClusterHealth(g => g
				.WaitForStatus(WaitForStatus.Yellow)
				.Timeout(ClusterHealthTimeout)
			);
			if(!healthyCluster.IsValid)
				throw new Exception($"Did not see a healhty cluster before calling onNodeStarted handler." + healthyCluster.DebugInformation);
		}
	}
}
