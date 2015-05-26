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
		private readonly ElasticsearchNode _node;
		private IObservable<ElasticsearchMessage> _consoleOut;

		public ReadonlyIntegration()
		{
			this._node = new ElasticsearchNode("1.5.2");
			this._consoleOut = this._node.Start();
		}

		public void Dispose()
		{
			_node?.Dispose();
		}

	}

	[CollectionDefinition(IntegrationContext.ReadOnly)]
	public class DatabaseCollection : ICollectionFixture<ReadonlyIntegration> { }
	
}
