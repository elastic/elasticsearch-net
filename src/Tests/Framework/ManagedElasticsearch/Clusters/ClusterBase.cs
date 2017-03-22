using System;
using Nest;
using Tests.Framework.Configuration;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Nodes;
using Tests.Framework.ManagedElasticsearch.Tasks;
using Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	public abstract class ClusterBase : IDisposable
	{
		protected ClusterBase()
		{
			this.NodeConfiguration = new NodeConfiguration(TestClient.Configuration, this);
			this.TaskRunner = new NodeTaskRunner(this.NodeConfiguration);
			this.Node = new ElasticsearchNode(this.NodeConfiguration);
		}

		public ElasticsearchNode Node { get; }
		public IElasticClient Client => this.Node.Client;
		private NodeConfiguration NodeConfiguration { get; }
		private NodeTaskRunner TaskRunner { get; }

		public virtual int MaxConcurrency => 0;
		protected virtual string[] AdditionalServerSettings { get; } = { };
		protected virtual InstallationTaskBase[] AdditionalInstallationTasks { get; } = { };

		public virtual bool EnableSsl { get; }
		public virtual bool SkipValidation { get; }

		public virtual int DesiredPort { get; } = 9200;

		public virtual ConnectionSettings ClusterConnectionSettings(ConnectionSettings s) => s;

		protected virtual void SeedNode() { }

		public void Start()
		{
			this.TaskRunner.Install(this.AdditionalInstallationTasks);
			var nodeSettings = this.NodeConfiguration.CreateSettings(this.AdditionalServerSettings);
			this.TaskRunner.OnBeforeStart(nodeSettings);
			this.Node.Start(nodeSettings);
			if (!this.SkipValidation)
				this.TaskRunner.ValidateAfterStart(this.Node.Client);
			if (this.NodeConfiguration.RunIntegrationTests && this.Node.Port != this.DesiredPort)
				throw new Exception($"The cluster that was started runs on {this.Node.Port} but this cluster wants {this.DesiredPort}");
			this.SeedNode();
		}

		public void Dispose()
		{
			this.Node?.Dispose();
			this.TaskRunner?.Dispose();
		}

	}
}
