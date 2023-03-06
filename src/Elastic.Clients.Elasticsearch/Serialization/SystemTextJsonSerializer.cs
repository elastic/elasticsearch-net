// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch.Serialization;

/// <summary>
/// An abstract implementation of the transport <see cref="Serializer"/> which serializes
/// using the Microsoft System.Text.Json library.
/// </summary>
public abstract class SystemTextJsonSerializer : Serializer
{
	private bool _initialized;

	public SystemTextJsonSerializer(IElasticsearchClientSettings settings)
	{
		if (settings is null)
			throw new ArgumentNullException(nameof(settings));

		Settings = settings;
	}

	internal JsonSerializerOptions? Options { get; private set; }

	internal JsonSerializerOptions? IndentedOptions { get; private set; }

	protected IElasticsearchClientSettings Settings { get; }

	protected abstract JsonSerializerOptions CreateJsonSerializerOptions();	

	/// <inheritdoc />
	public override T Deserialize<T>(Stream stream)
	{
		Initialize();

		if (TryReturnDefault(stream, out T deserialize))
			return deserialize;

		return JsonSerializer.Deserialize<T>(stream, Options);
	}

	/// <inheritdoc />
	public override object Deserialize(Type type, Stream stream)
	{
		Initialize();

		if (TryReturnDefault(stream, out object deserialize))
			return deserialize;

		return JsonSerializer.Deserialize(stream, type, Options);
	}

	/// <inheritdoc />
	public override ValueTask<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default)
	{
		Initialize();
		return JsonSerializer.DeserializeAsync<T>(stream, Options, cancellationToken);
	}

	/// <inheritdoc />
	public override ValueTask<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default)
	{
		Initialize();
		return JsonSerializer.DeserializeAsync(stream, type, Options, cancellationToken);
	}

	/// <inheritdoc />
	public override void Serialize<T>(T data, Stream writableStream,
		SerializationFormatting formatting = SerializationFormatting.None)
	{
		Initialize();
		using var writer = new Utf8JsonWriter(writableStream);
		JsonSerializer.Serialize(writer, data, typeof(T), formatting == SerializationFormatting.Indented && IndentedOptions is not null ? IndentedOptions : Options);
	}

	/// <inheritdoc />
	public override Task SerializeAsync<T>(T data, Stream stream,
		SerializationFormatting formatting = SerializationFormatting.None,
		CancellationToken cancellationToken = default)
	{
		Initialize();
		return JsonSerializer.SerializeAsync(stream, data, formatting == SerializationFormatting.Indented && IndentedOptions is not null ? IndentedOptions : Options, cancellationToken);
	}

	private static bool TryReturnDefault<T>(Stream stream, out T deserialize)
	{
		deserialize = default;
		return stream == null || stream == Stream.Null || (stream.CanSeek && stream.Length == 0);
	}

	protected void Initialize()
	{
		if (_initialized)
			return;

		var options = CreateJsonSerializerOptions();

		if (options is null)
		{
			Options = new JsonSerializerOptions();
			IndentedOptions = new JsonSerializerOptions { WriteIndented = true };
		}
		else
		{
			Options = options;
			IndentedOptions = new JsonSerializerOptions(options)
			{
				WriteIndented = true
			};
		}

		LinkOptionsAndSettings();

		_initialized = true;
	}

	private void LinkOptionsAndSettings()
	{
		if (!ElasticsearchClient.SettingsTable.TryGetValue(Options, out _))
		{
			ElasticsearchClient.SettingsTable.Add(Options, Settings);
		}

		if (!ElasticsearchClient.SettingsTable.TryGetValue(IndentedOptions, out _))
		{
			ElasticsearchClient.SettingsTable.Add(IndentedOptions, Settings);
		}
	}
}
