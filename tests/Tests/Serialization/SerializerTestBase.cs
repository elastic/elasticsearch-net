// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.Threading.Tasks;
using Tests.Domain.Extensions;
using VerifyXunit;
using Xunit.Abstractions;

namespace Tests.Serialization;

public abstract class VerifySerializerTestBase : VerifyBase
{
	protected static readonly SerializerBase _requestResponseSerializer;
	protected static readonly IElasticsearchClientSettings _settings;

	static VerifySerializerTestBase() 
	{
		var settings = new ElasticsearchClientSettings();
		settings.ApplyDomainSettings();

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

	/// <summary>
	/// Serialises the <paramref name="data"/> using the sync and async request/response serializer methods, comparing the results.
	/// </summary>
	/// <returns>
	/// The JSON as a string for further comparisons and assertions.
	/// </returns>
	protected static async Task<string> SerializeAndGetJsonStringAsync<T>(T data)
	{
		var stream = new MemoryStream();
		await _requestResponseSerializer.SerializeAsync(data, stream);
		stream.Position = 0;
		var reader = new StreamReader(stream);
		var asyncJsonString = await reader.ReadToEndAsync();

		stream.SetLength(0);
		_requestResponseSerializer.Serialize(data, stream);
		stream.Position = 0;
		reader = new StreamReader(stream);
		var syncJsonString = await reader.ReadToEndAsync();

		syncJsonString.Should().Be(asyncJsonString);

		return asyncJsonString;
	}
}

public abstract class SerializerTestBase
{
	protected static readonly SerializerBase _requestResponseSerializer;
	protected static readonly IElasticsearchClientSettings _settings;

	static SerializerTestBase()
	{
		var settings = new ElasticsearchClientSettings();
		settings.ApplyDomainSettings();

		var client = new ElasticClient(settings);
		
		_requestResponseSerializer = client.RequestResponseSerializer;
		_settings = client.ElasticsearchClientSettings;
	}

	protected static Inferrer Inferrer => _settings.Inferrer;

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

	/// <summary>
	/// Serialises the <paramref name="data"/> using the sync and async request/response serializer methods, comparing the results.
	/// </summary>
	/// <returns>
	/// The JSON as a string for further comparisons and assertions.
	/// </returns>
	protected static async Task<string> SerializeAndGetJsonStringAsync<T>(T data)
	{
		var stream = new MemoryStream();
		await _requestResponseSerializer.SerializeAsync(data, stream);
		stream.Position = 0;
		var reader = new StreamReader(stream);
		var asyncJsonString = await reader.ReadToEndAsync();

		stream.SetLength(0);
		_requestResponseSerializer.Serialize(data, stream);
		stream.Position = 0;
		reader = new StreamReader(stream);
		var syncJsonString = await reader.ReadToEndAsync();

		syncJsonString.Should().Be(asyncJsonString);

		return asyncJsonString;
	}
}
