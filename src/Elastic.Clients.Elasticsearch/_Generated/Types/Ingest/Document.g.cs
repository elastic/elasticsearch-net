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

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Ingest;

internal sealed partial class DocumentConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Ingest.Document>
{
	private static readonly System.Text.Json.JsonEncodedText PropId = System.Text.Json.JsonEncodedText.Encode("_id");
	private static readonly System.Text.Json.JsonEncodedText PropIndex = System.Text.Json.JsonEncodedText.Encode("_index");
	private static readonly System.Text.Json.JsonEncodedText PropSource = System.Text.Json.JsonEncodedText.Encode("_source");

	public override Elastic.Clients.Elasticsearch.Ingest.Document Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Id?> propId = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexName?> propIndex = default;
		LocalJsonValue<object> propSource = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propId.TryReadProperty(ref reader, options, PropId, null))
			{
				continue;
			}

			if (propIndex.TryReadProperty(ref reader, options, PropIndex, null))
			{
				continue;
			}

			if (propSource.TryReadProperty(ref reader, options, PropSource, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Ingest.Document(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Id = propId.Value,
			Index = propIndex.Value,
			Source = propSource.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Ingest.Document value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropId, value.Id, null, null);
		writer.WriteProperty(options, PropIndex, value.Index, null, null);
		writer.WriteProperty(options, PropSource, value.Source, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Ingest.DocumentConverter))]
public sealed partial class Document
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public Document(object source)
	{
		Source = source;
	}
#if NET7_0_OR_GREATER
	public Document()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public Document()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Document(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Unique identifier for the document.
	/// This ID must be unique within the <c>_index</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Id? Id { get; set; }

	/// <summary>
	/// <para>
	/// Name of the index containing the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexName? Index { get; set; }

	/// <summary>
	/// <para>
	/// JSON body for the document.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	object Source { get; set; }
}

public readonly partial struct DocumentDescriptor
{
	internal Elastic.Clients.Elasticsearch.Ingest.Document Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DocumentDescriptor(Elastic.Clients.Elasticsearch.Ingest.Document instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DocumentDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.Document(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Ingest.DocumentDescriptor(Elastic.Clients.Elasticsearch.Ingest.Document instance) => new Elastic.Clients.Elasticsearch.Ingest.DocumentDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.Document(Elastic.Clients.Elasticsearch.Ingest.DocumentDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Unique identifier for the document.
	/// This ID must be unique within the <c>_index</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DocumentDescriptor Id(Elastic.Clients.Elasticsearch.Id? value)
	{
		Instance.Id = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Name of the index containing the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DocumentDescriptor Index(Elastic.Clients.Elasticsearch.IndexName? value)
	{
		Instance.Index = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// JSON body for the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.DocumentDescriptor Source(object value)
	{
		Instance.Source = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Ingest.Document Build(System.Action<Elastic.Clients.Elasticsearch.Ingest.DocumentDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Ingest.DocumentDescriptor(new Elastic.Clients.Elasticsearch.Ingest.Document(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}