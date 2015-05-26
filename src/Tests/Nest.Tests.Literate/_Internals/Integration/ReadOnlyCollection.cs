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
		public ReadonlyIntegration()
		{

			// ... initialize data in the test database ...
		}

		public void Dispose()
		{
			// ... clean up test data from the database ...
		}

	}

	[CollectionDefinition(IntegrationContext.ReadOnly)]
	public class DatabaseCollection : ICollectionFixture<ReadonlyIntegration> { }
	
}
