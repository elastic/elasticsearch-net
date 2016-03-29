using System.IO;

namespace Elasticsearch.Net
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