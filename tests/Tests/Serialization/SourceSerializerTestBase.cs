using System.IO;
using System.Threading.Tasks;
using Tests.Domain;

namespace Tests.Serialization;

public abstract class SourceSerializerTestBase
{
	protected static readonly Serializer _requestResponseSerializer;
	protected static readonly IElasticsearchClientSettings _settings;

	static SourceSerializerTestBase()
	{
		var settings = new ElasticsearchClientSettings();
		settings.DefaultMappingFor<Project>(m => m.IndexName("project"));

		var client = new ElasticClient(settings);
		
		_requestResponseSerializer = client.RequestResponseSerializer;
		_settings = client.ElasticsearchClientSettings;
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

	protected static async Task<string> SerializeAndGetJsonStringAsync<T>(T data)
	{
		var stream = new MemoryStream();
		await _requestResponseSerializer.SerializeAsync(data, stream);
		stream.Position = 0;
		var reader = new StreamReader(stream);
		return await reader.ReadToEndAsync();
	}
}
