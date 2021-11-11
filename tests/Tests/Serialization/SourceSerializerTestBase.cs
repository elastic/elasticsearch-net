
using System.IO;

namespace Tests.Serialization;

public abstract class SourceSerializerTestBase
{
	protected static readonly Serializer _sourceSerializer;

	static SourceSerializerTestBase()
	{
		var client = new ElasticClient();
		_sourceSerializer = client.SourceSerializer;
	}

	protected static Stream WrapInStream(string json)
	{
		var stream = new MemoryStream();
		var writer = new StreamWriter(stream);
		writer.Write(json);
		writer.Flush();
		stream.Position = 0;
		return stream;
	}
}
