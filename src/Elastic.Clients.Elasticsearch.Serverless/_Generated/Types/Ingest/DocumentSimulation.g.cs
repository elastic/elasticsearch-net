// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

#nullable restore

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.Ingest;

internal sealed partial class DocumentSimulationConverter : JsonConverter<DocumentSimulation>
{
	public override DocumentSimulation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.StartObject)
			throw new JsonException("Unexpected JSON detected.");
		string id = default;
		string index = default;
		Elastic.Clients.Elasticsearch.Serverless.Ingest.IngestInfo ingest = default;
		string? routing = default;
		IReadOnlyDictionary<string, object> source = default;
		long? version = default;
		Elastic.Clients.Elasticsearch.Serverless.VersionType? versionType = default;
		Dictionary<string, string> additionalProperties = null;
		while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
		{
			if (reader.TokenType == JsonTokenType.PropertyName)
			{
				var property = reader.GetString();
				if (property == "_id")
				{
					id = JsonSerializer.Deserialize<string>(ref reader, options);
					continue;
				}

				if (property == "_index")
				{
					index = JsonSerializer.Deserialize<string>(ref reader, options);
					continue;
				}

				if (property == "_ingest")
				{
					ingest = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Serverless.Ingest.IngestInfo>(ref reader, options);
					continue;
				}

				if (property == "_routing")
				{
					routing = JsonSerializer.Deserialize<string?>(ref reader, options);
					continue;
				}

				if (property == "_source")
				{
					source = JsonSerializer.Deserialize<IReadOnlyDictionary<string, object>>(ref reader, options);
					continue;
				}

				if (property == "_version")
				{
					version = JsonSerializer.Deserialize<long?>(ref reader, options);
					continue;
				}

				if (property == "_version_type")
				{
					versionType = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Serverless.VersionType?>(ref reader, options);
					continue;
				}

				additionalProperties ??= new Dictionary<string, string>();
				var additionalValue = JsonSerializer.Deserialize<string>(ref reader, options);
				additionalProperties.Add(property, additionalValue);
			}
		}

		return new DocumentSimulation { Id = id, Index = index, Ingest = ingest, Metadata = additionalProperties, Routing = routing, Source = source, Version = version, VersionType = versionType };
	}

	public override void Write(Utf8JsonWriter writer, DocumentSimulation value, JsonSerializerOptions options)
	{
		throw new NotImplementedException("'DocumentSimulation' is a readonly type, used only on responses and does not support being written to JSON.");
	}
}

/// <summary>
/// <para>
/// The simulated document, with optional metadata.
/// </para>
/// </summary>
[JsonConverter(typeof(DocumentSimulationConverter))]
public sealed partial class DocumentSimulation
{
	/// <summary>
	/// <para>
	/// Unique identifier for the document. This ID must be unique within the <c>_index</c>.
	/// </para>
	/// </summary>
	public string Id { get; init; }

	/// <summary>
	/// <para>
	/// Name of the index containing the document.
	/// </para>
	/// </summary>
	public string Index { get; init; }
	public Elastic.Clients.Elasticsearch.Serverless.Ingest.IngestInfo Ingest { get; init; }

	/// <summary>
	/// <para>
	/// Additional metadata
	/// </para>
	/// </summary>
	public IReadOnlyDictionary<string, string> Metadata { get; init; }

	/// <summary>
	/// <para>
	/// Value used to send the document to a specific primary shard.
	/// </para>
	/// </summary>
	public string? Routing { get; init; }

	/// <summary>
	/// <para>
	/// JSON body for the document.
	/// </para>
	/// </summary>
	public IReadOnlyDictionary<string, object> Source { get; init; }
	public long? Version { get; init; }
	public Elastic.Clients.Elasticsearch.Serverless.VersionType? VersionType { get; init; }
}