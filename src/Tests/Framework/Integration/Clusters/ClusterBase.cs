using System;
using System.Collections.Specialized;
using Elasticsearch.Net;
using Nest;

namespace Tests.Framework.Integration
{
	public abstract class ClusterBase : IIntegrationCluster, IDisposable
	{
		protected virtual bool DoNotSpawnIfAlreadyRunning => TestClient.Configuration.DoNotSpawnIfAlreadyRunning;
		public ElasticsearchNode Node { get; }
		protected IObservable<ElasticsearchMessage> ConsoleOut { get; set; }

		protected virtual bool EnableShield => false;

		protected ClusterBase()
		{
			var name = this.GetType().Name.Replace("Cluster", "");
			this.Node = new ElasticsearchNode(TestClient.Configuration.ElasticsearchVersion, TestClient.Configuration.RunIntegrationTests, DoNotSpawnIfAlreadyRunning, name, EnableShield);
			this.Node.BootstrapWork.Subscribe(handle =>
			{
				this.Boostrap();
				handle.Set();
			});
			this.ConsoleOut = this.Node.Start(name, this.ServerSettings);
		}

		protected virtual string[] ServerSettings { get; } = new string[] { };

		public virtual void Boostrap() { }

		public void Dispose() => this.Node?.Dispose();

		public IElasticClient Client(Func<ConnectionSettings, ConnectionSettings> settings = null, bool forceInMemory = false) => this.Node.Client(settings, forceInMemory);
	}
}
