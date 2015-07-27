using System;
using System.Collections.Generic;
using Nest;
using Xunit;

namespace Tests.Framework.Integration
{
	public static class IntegrationContext
	{
		public const string ReadOnly = "ReadOnly Cluster";
	}

	public interface IIntegrationCluster
	{
		ElasticsearchNode Node { get; }
	}

	public class ReadOnlyCluster: IIntegrationCluster, IDisposable
	{
		public ElasticsearchNode Node { get; }
		private IObservable<ElasticsearchMessage> _consoleOut;

		public ReadOnlyCluster()
		{
			this.Node = new ElasticsearchNode(TestClient.ElasticsearchVersion, TestClient.RunIntegrationTests);
			this._consoleOut = this.Node.Start();
		}

		public void Dispose() =>
			this.Node?.Dispose();

	}

	public class ApiUsage
	{
		private readonly object _lock = new object();
		private bool _called  = false;
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
