using System.IO;

namespace Tests.Serialization;

public abstract class SourceSerializerTestBase
{
	protected static readonly Serializer _requestResponseSerializer;

	static SourceSerializerTestBase()
	{
		var client = new ElasticClient();
		_requestResponseSerializer = client.RequestResponseSerializer;
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

	protected static string SerializeAndGetJsonString<T>(T data)
	{
		var stream = new MemoryStream();
		_requestResponseSerializer.Serialize(data, stream);
		stream.Position = 0;
		var reader = new StreamReader(stream);
		return reader.ReadToEnd();
	}
}
