using System;
using System.Collections.Specialized;
using System.Threading;
using Elasticsearch.Net;
using Nest;

namespace Tests.Framework.Integration
{
	public abstract class ClusterBase : IDisposable
	{
		protected virtual bool DoNotSpawnIfAlreadyRunning => TestClient.Configuration.DoNotSpawnIfAlreadyRunning;

		public ElasticsearchNode Node { get; }

		public IElasticClient Client => this.Node.Client;

		protected virtual bool EnableShield => false;

		protected virtual bool EnableWatcher => false;

		public virtual int MaxConcurrency => 0;

		protected ClusterBase()
		{
			var name = this.GetType().Name.Replace("Cluster", "");
			this.Node = new ElasticsearchNode(
				TestClient.Configuration.ElasticsearchVersion,
				TestClient.Configuration.RunIntegrationTests,
				DoNotSpawnIfAlreadyRunning, 
				name, 
				EnableShield, 
				EnableWatcher);

			this.Node.BootstrapWork.Subscribe(handle =>
			{
				this.Bootstrap();
				handle.Set();
			});
		}

		public void Start()
		{
			this.Node.Start(this.ServerSettings);
		}

		protected virtual string[] ServerSettings { get; } = new string[] { };

		public virtual void Bootstrap() { }

		public void Dispose() => this.Node?.Dispose();

	}
}
