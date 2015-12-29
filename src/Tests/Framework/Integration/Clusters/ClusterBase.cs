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

		protected ClusterBase()
		{
			var name = this.GetType().Name.Replace("Cluster", "");
			this.Node = new ElasticsearchNode(TestClient.Configuration.ElasticsearchVersion, TestClient.Configuration.RunIntegrationTests, DoNotSpawnIfAlreadyRunning, name);
			this.Node.BootstrapWork.Subscribe(handle =>
			{
				this.Boostrap();
				handle.Set();
			});
			this.ConsoleOut = this.Node.Start();
		}

		public virtual void Boostrap() { }

		public void Dispose() => this.Node?.Dispose();

		public IElasticClient Client(Func<ConnectionSettings, ConnectionSettings> settings = null) => this.Node.Client(settings);
	}
}