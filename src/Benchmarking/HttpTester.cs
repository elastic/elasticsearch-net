using Nest;

namespace Benchmarking
{
	public class HttpTester : Tester
	{
		public HttpTester(int port) : base(port)
		{
		}

		public override IElasticClient CreateClient()
		{
			var settings = this.CreateSettings();
			var client = new ElasticClient(settings);
			return client;
		}
	}
}