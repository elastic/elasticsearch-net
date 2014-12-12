using System.IO;

namespace Elasticsearch.Net.Providers
{
	public interface IMemoryStreamProvider
	{
		MemoryStream New();
	}
}