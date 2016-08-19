using System;
using System.Collections.Specialized;
using System.Linq;
using Elasticsearch.Net;
using System.Reflection;
using Nest;

namespace Tests.Framework.Integration
{
	public abstract class ClusterBase : IDisposable
	{
		public ElasticsearchNode Node { get; }

		public IElasticClient Client => this.Node.Client;

		public virtual int MaxConcurrency => 0;

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
				this.Bootstrap();
				handle.Set();
			});
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

		public virtual void Bootstrap() { }

		public void Start()
		{
			this.Node.Start(this.ServerSettings);
		}

		public void Dispose() => this.Node?.Dispose();

	}
}
