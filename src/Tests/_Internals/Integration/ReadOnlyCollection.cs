using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Nest.Tests.Literate._Internals.Integration
{
	public static class IntegrationContext
	{
		public const string ReadOnly = "ReadOnly Collection";
	}

	public class ReadonlyIntegration: IDisposable
	{
		public ElasticsearchNode Node { get; }
		private IObservable<ElasticsearchMessage> _consoleOut;

		public ReadonlyIntegration()
		{
			this.Node = new ElasticsearchNode("1.5.2");
			this._consoleOut = this.Node.Start();
		}

		public void Dispose() =>
			this.Node.Dispose();

	}

	[CollectionDefinition(IntegrationContext.ReadOnly)]
	public class DatabaseCollection : ICollectionFixture<ReadonlyIntegration> { }
	
}
