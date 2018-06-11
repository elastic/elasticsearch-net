using System.IO;

namespace Elasticsearch.Net
{
	/// <summary>
	/// A factory for creating memory streams
	/// </summary>
	public interface IMemoryStreamFactory
	{
		/// <summary>
		/// Creates a memory stream
		/// </summary>
		MemoryStream Create();

		/// <summary>
		/// Creates a memory stream with the bytes written to the stream
		/// </summary>
		MemoryStream Create(byte[] bytes);
	}
}
