using System;
using System.Collections.Specialized;
using System.Linq;
using Elasticsearch.Net;
using System.Reflection;
using Nest;

namespace Tests.Framework.Integration
{
	public abstract class ClusterBase : IIntegrationCluster, IDisposable
	{
		public ElasticsearchNode Node { get; }
		private IObservable<ElasticsearchConsoleOut> ConsoleOut { get; set; }

		protected ClusterBase()
		{
			var name = this.GetType().Name.Replace("Cluster", "");
			var nodeConfig = new NodeConfiguration(TestClient.Configuration)
			{
				TypeOfCluster = name,
				RequiredPlugins = RequiredPlugins(this.GetType())
			};
			this.Node = new ElasticsearchNode(nodeConfig, new TestRunnerFileSystem(nodeConfig));
			this.Node.BootstrapWork.Subscribe(handle =>
			{
				this.Boostrap();
				handle.Set();
			});
			this.ConsoleOut = this.Node.Start(this.ServerSettings);
		}

		private static ElasticsearchPlugin[] RequiredPlugins(Type type)
		{
#if !DOTNETCORE
				var attributes = Attribute.GetCustomAttributes(type, typeof(RequiresPluginAttribute), true);
#else
				var attributes =  type.GetTypeInfo().GetCustomAttributes(typeof(RequiresPluginAttribute), true);
#endif
			return attributes
				.Cast<RequiresPluginAttribute>()
				.SelectMany(a => a.Plugins)
				.ToArray();
		}

		protected virtual string[] ServerSettings { get; } = new string[] { };

		protected virtual void Boostrap() { }

		public void Dispose() => this.Node?.Dispose();

		public IElasticClient Client(Func<ConnectionSettings, ConnectionSettings> settings = null, bool forceInMemory = false) => this.Node.Client(settings, forceInMemory);
	}
}
