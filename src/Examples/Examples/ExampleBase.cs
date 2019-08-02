using Elasticsearch.Net;
using Nest;

namespace Examples
{
	public abstract class ExampleBase
	{
		// ReSharper disable once FieldCanBeMadeReadOnly.Global
		protected IElasticClient client;

		protected ExampleBase()
		{
			var settings = new ConnectionSettings(new InMemoryConnection())
				.DisableDirectStreaming();
			client = new ElasticClient(settings);
		}
	}
}
