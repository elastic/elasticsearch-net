using System.IO;

namespace Elasticsearch.Net.Providers
{
	public interface IMemoryStreamFactory
	{
		MemoryStream Create();
	}

	public class MemoryStreamFactory : IMemoryStreamFactory
	{
		public MemoryStream Create() => new MemoryStream();
	}
}