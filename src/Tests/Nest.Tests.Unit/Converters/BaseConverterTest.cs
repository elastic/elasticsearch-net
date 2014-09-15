using System.Globalization;
using System.IO;
using System.Threading;
using Elasticsearch.Net.Connection;

namespace Nest.Tests.Unit.Converters
{
	public abstract class BaseConverterTest
	{
		protected IElasticClient Client;

		protected BaseConverterTest()
		{
			Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");
			IConnectionSettingsValues settings = new ConnectionSettings(UnitTestDefaults.Uri, UnitTestDefaults.DefaultIndex)
				.DisablePing()
				.ExposeRawResponse();

			var connection = new InMemoryConnection(settings);

			Client = new ElasticClient(settings, connection);
		}

		protected T SerializeAndDeserialize<T>(T expected)
		{
			var json = Client.Serializer.Serialize(expected);

			using (var ms = new MemoryStream(json))
			{
				return Client.Serializer.Deserialize<T>(ms);
			}
		}

		protected class ConverterTestObject
		{
			public string Name { get; set; }
			public IFuzziness Fuzziness { get; set; }
		}
	}
}