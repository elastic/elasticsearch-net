using System;
using System.Collections.Specialized;
using System.Linq;
using Elasticsearch.Net;
using System.Reflection;
using Nest;
using Tests.Framework.ManagedElasticsearch;
using Tests.Framework.ManagedElasticsearch.InstallationTasks;

namespace Tests.Framework.Integration
{
	public abstract class ClusterBase : IDisposable
	{
		protected ClusterBase()
		{
			this.NodeConfiguration = new NodeConfiguration(TestClient.Configuration, this.GetType());
			this.Node = new ElasticsearchNode(this.NodeConfiguration);
			this.Installer = new ElasticsearchInstaller(this.NodeConfiguration);
		}

		public ElasticsearchNode Node { get; }
		public IElasticClient Client => this.Node.Client;
		private NodeConfiguration NodeConfiguration { get; }
		private ElasticsearchInstaller Installer { get; }

		public virtual int MaxConcurrency => 0;
		protected virtual string[] AdditionalServerSettings { get; } = { };
		protected virtual InstallationTaskBase[] AdditionalInstallationTasks { get; } = { };
		protected virtual void AfterNodeStarts() { }

		public void Start()
		{
			this.Installer.Install();
			var nodeSettings = this.NodeConfiguration.CreateSettings(this.AdditionalServerSettings);
			this.Installer.OnBeforeStart(nodeSettings);
			this.Node.Start(nodeSettings, this.AfterNodeStarts);
		}

		public void Dispose()
		{
			this.Node?.Dispose();
			this.Installer?.Dispose();
		}

	}
}
