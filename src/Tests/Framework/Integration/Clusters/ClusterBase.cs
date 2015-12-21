using System;
using Nest;

namespace Tests.Framework.Integration
{
	public abstract class ClusterBase : IIntegrationCluster, IDisposable
	{
		protected virtual bool DoNotSpawnIfAlreadyRunning => TestClient.Configuration.DoNotSpawnIfAlreadyRunning;
		public ElasticsearchNode Node { get; }
		protected IObservable<ElasticsearchMessage> ConsoleOut { get; set; }

		public ClusterBase()
		{
			this.Node = new ElasticsearchNode(TestClient.Configuration.ElasticsearchVersion, TestClient.Configuration.RunIntegrationTests, DoNotSpawnIfAlreadyRunning);
			this.Node.BootstrapWork.Subscribe(handle =>
			{
				this.Boostrap();
				handle.Set();
			});
			this.ConsoleOut = this.Node.Start();
		}

		public virtual void Boostrap() { }

		public void Dispose() => this.Node?.Dispose();

		public IElasticClient Client(Func<ConnectionSettings, ConnectionSettings> settings = null)
		{
			var port = this.Node.Started ? this.Node.Port : 9200;
			return TestClient.GetClient(settings, port);
		}
	}
}