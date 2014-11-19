using System.IO;

namespace Elasticsearch.Net.Providers
{
	public class MemoryStreamProvider : IMemoryStreamProvider
	{
		public MemoryStream New()
		{
			return new MemoryStream();
		}
	}
}