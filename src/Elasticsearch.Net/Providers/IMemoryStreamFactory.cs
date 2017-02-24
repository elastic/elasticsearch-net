using System.IO;

namespace Elasticsearch.Net_5_2_0
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