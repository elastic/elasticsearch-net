using System;
using Xunit;

namespace Tests._Internals.Integration
{
	public static class IntegrationContext
	{
		public const string ReadOnly = "ReadOnly Collection";
	}

	public class ReadOnlyIntegration: IDisposable
	{
		public ElasticsearchNode Node { get; }
		private IObservable<ElasticsearchMessage> _consoleOut;

		public ReadOnlyIntegration()
		{
			this.Node = new ElasticsearchNode(TestClient.ElasticsearchVersion, TestClient.RunIntegrationTests);
			this._consoleOut = this.Node.Start();
		}

		public void Dispose() =>
			this.Node?.Dispose();

	}

	[CollectionDefinition(IntegrationContext.ReadOnly)]
	public class DatabaseCollection : ICollectionFixture<ReadOnlyIntegration> { }
	
}
