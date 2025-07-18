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

namespace Elastic.Clients.Elasticsearch.Core.Reindex;

internal sealed partial class DestinationConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.Reindex.Destination>
{
	private static readonly System.Text.Json.JsonEncodedText PropIndex = System.Text.Json.JsonEncodedText.Encode("index");
	private static readonly System.Text.Json.JsonEncodedText PropOpType = System.Text.Json.JsonEncodedText.Encode("op_type");
	private static readonly System.Text.Json.JsonEncodedText PropPipeline = System.Text.Json.JsonEncodedText.Encode("pipeline");
	private static readonly System.Text.Json.JsonEncodedText PropRouting = System.Text.Json.JsonEncodedText.Encode("routing");
	private static readonly System.Text.Json.JsonEncodedText PropVersionType = System.Text.Json.JsonEncodedText.Encode("version_type");

	public override Elastic.Clients.Elasticsearch.Core.Reindex.Destination Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexName> propIndex = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.OpType?> propOpType = default;
		LocalJsonValue<string?> propPipeline = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Routing?> propRouting = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.VersionType?> propVersionType = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propIndex.TryReadProperty(ref reader, options, PropIndex, null))
			{
				continue;
			}

			if (propOpType.TryReadProperty(ref reader, options, PropOpType, static Elastic.Clients.Elasticsearch.OpType? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<Elastic.Clients.Elasticsearch.OpType>(o)))
			{
				continue;
			}

			if (propPipeline.TryReadProperty(ref reader, options, PropPipeline, null))
			{
				continue;
			}

			if (propRouting.TryReadProperty(ref reader, options, PropRouting, null))
			{
				continue;
			}

			if (propVersionType.TryReadProperty(ref reader, options, PropVersionType, static Elastic.Clients.Elasticsearch.VersionType? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<Elastic.Clients.Elasticsearch.VersionType>(o)))
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
		return new Elastic.Clients.Elasticsearch.Core.Reindex.Destination(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Index = propIndex.Value,
			OpType = propOpType.Value,
			Pipeline = propPipeline.Value,
			Routing = propRouting.Value,
			VersionType = propVersionType.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.Reindex.Destination value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropIndex, value.Index, null, null);
		writer.WriteProperty(options, PropOpType, value.OpType, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.OpType? v) => w.WriteNullableValue<Elastic.Clients.Elasticsearch.OpType>(o, v));
		writer.WriteProperty(options, PropPipeline, value.Pipeline, null, null);
		writer.WriteProperty(options, PropRouting, value.Routing, null, null);
		writer.WriteProperty(options, PropVersionType, value.VersionType, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.VersionType? v) => w.WriteNullableValue<Elastic.Clients.Elasticsearch.VersionType>(o, v));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.Reindex.DestinationConverter))]
public sealed partial class Destination
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public Destination(Elastic.Clients.Elasticsearch.IndexName index)
	{
		Index = index;
	}
#if NET7_0_OR_GREATER
	public Destination()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public Destination()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Destination(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The name of the data stream, index, or index alias you are copying to.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.IndexName Index { get; set; }

	/// <summary>
	/// <para>
	/// If it is <c>create</c>, the operation will only index documents that do not already exist (also known as "put if absent").
	/// </para>
	/// <para>
	/// IMPORTANT: To reindex to a data stream destination, this argument must be <c>create</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.OpType? OpType { get; set; }

	/// <summary>
	/// <para>
	/// The name of the pipeline to use.
	/// </para>
	/// </summary>
	public string? Pipeline { get; set; }

	/// <summary>
	/// <para>
	/// By default, a document's routing is preserved unless it's changed by the script.
	/// If it is <c>keep</c>, the routing on the bulk request sent for each match is set to the routing on the match.
	/// If it is <c>discard</c>, the routing on the bulk request sent for each match is set to <c>null</c>.
	/// If it is <c>=value</c>, the routing on the bulk request sent for each match is set to all value specified after the equals sign (<c>=</c>).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Routing? Routing { get; set; }

	/// <summary>
	/// <para>
	/// The versioning to use for the indexing operation.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.VersionType? VersionType { get; set; }
}

public readonly partial struct DestinationDescriptor
{
	internal Elastic.Clients.Elasticsearch.Core.Reindex.Destination Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DestinationDescriptor(Elastic.Clients.Elasticsearch.Core.Reindex.Destination instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DestinationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Core.Reindex.Destination(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Core.Reindex.DestinationDescriptor(Elastic.Clients.Elasticsearch.Core.Reindex.Destination instance) => new Elastic.Clients.Elasticsearch.Core.Reindex.DestinationDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Core.Reindex.Destination(Elastic.Clients.Elasticsearch.Core.Reindex.DestinationDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The name of the data stream, index, or index alias you are copying to.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.DestinationDescriptor Index(Elastic.Clients.Elasticsearch.IndexName value)
	{
		Instance.Index = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If it is <c>create</c>, the operation will only index documents that do not already exist (also known as "put if absent").
	/// </para>
	/// <para>
	/// IMPORTANT: To reindex to a data stream destination, this argument must be <c>create</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.DestinationDescriptor OpType(Elastic.Clients.Elasticsearch.OpType? value)
	{
		Instance.OpType = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the pipeline to use.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.DestinationDescriptor Pipeline(string? value)
	{
		Instance.Pipeline = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// By default, a document's routing is preserved unless it's changed by the script.
	/// If it is <c>keep</c>, the routing on the bulk request sent for each match is set to the routing on the match.
	/// If it is <c>discard</c>, the routing on the bulk request sent for each match is set to <c>null</c>.
	/// If it is <c>=value</c>, the routing on the bulk request sent for each match is set to all value specified after the equals sign (<c>=</c>).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.DestinationDescriptor Routing(Elastic.Clients.Elasticsearch.Routing? value)
	{
		Instance.Routing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The versioning to use for the indexing operation.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.DestinationDescriptor VersionType(Elastic.Clients.Elasticsearch.VersionType? value)
	{
		Instance.VersionType = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.Reindex.Destination Build(System.Action<Elastic.Clients.Elasticsearch.Core.Reindex.DestinationDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Core.Reindex.DestinationDescriptor(new Elastic.Clients.Elasticsearch.Core.Reindex.Destination(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}