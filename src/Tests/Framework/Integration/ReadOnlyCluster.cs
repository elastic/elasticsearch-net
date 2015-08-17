using System;
using System.Collections.Generic;
using Nest;
using Xunit;
using System.Reactive.Linq;
using System.Threading;

namespace Tests.Framework.Integration
{
	public static class IntegrationContext
	{
		public const string ReadOnly = "ReadOnly Cluster";
		public const string Indexing = "Indexing Cluster";
	}

	public interface IIntegrationCluster
	{
		ElasticsearchNode Node { get; }
	}

	public abstract class ClusterBase : IIntegrationCluster, IDisposable
	{
		public ElasticsearchNode Node { get; }
		protected IObservable<ElasticsearchMessage> ConsoleOut { get; set; }

		public ClusterBase()
		{
			this.Node = new ElasticsearchNode(TestClient.ElasticsearchVersion, TestClient.RunIntegrationTests);
			this.Node.BootstrapWork.Subscribe(handle =>
			{
				this.Boostrap();
				handle.Set();
			});
			this.ConsoleOut = this.Node.Start();
		}

		public virtual void Boostrap() { }

		public void Dispose() => this.Node?.Dispose();
	}

	public class ReadOnlyCluster : ClusterBase
	{
		public override void Boostrap() => new Seeder(this.Node.Port).SeedNode();
	}

	public class ApiUsage
	{
		private readonly object _lock = new object();
		private bool _called = false;
		private LazyResponses _responses = null;

		public LazyResponses CallOnce(Func<LazyResponses> clientUsage)
		{
			if (_called) return _responses;
			lock (_lock)
			{
				if (_called) return _responses;
				this._responses = clientUsage();
				_called = true;
			}
			return _responses;
		}
	}



	[CollectionDefinition(IntegrationContext.ReadOnly)]
	public class DatabaseCollection : ICollectionFixture<ReadOnlyCluster>, IClassFixture<ApiUsage>
	{

	}

}
