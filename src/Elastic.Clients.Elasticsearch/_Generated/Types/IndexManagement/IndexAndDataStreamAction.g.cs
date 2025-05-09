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

namespace Elastic.Clients.Elasticsearch.IndexManagement;

internal sealed partial class IndexAndDataStreamActionConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.IndexAndDataStreamAction>
{
	private static readonly System.Text.Json.JsonEncodedText PropDataStream = System.Text.Json.JsonEncodedText.Encode("data_stream");
	private static readonly System.Text.Json.JsonEncodedText PropIndex = System.Text.Json.JsonEncodedText.Encode("index");

	public override Elastic.Clients.Elasticsearch.IndexManagement.IndexAndDataStreamAction Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.DataStreamName> propDataStream = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexName> propIndex = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDataStream.TryReadProperty(ref reader, options, PropDataStream, null))
			{
				continue;
			}

			if (propIndex.TryReadProperty(ref reader, options, PropIndex, null))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.IndexAndDataStreamAction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			DataStream = propDataStream.Value,
			Index = propIndex.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.IndexAndDataStreamAction value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDataStream, value.DataStream, null, null);
		writer.WriteProperty(options, PropIndex, value.Index, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.IndexAndDataStreamActionConverter))]
public sealed partial class IndexAndDataStreamAction
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IndexAndDataStreamAction(Elastic.Clients.Elasticsearch.DataStreamName dataStream, Elastic.Clients.Elasticsearch.IndexName index)
	{
		DataStream = dataStream;
		Index = index;
	}
#if NET7_0_OR_GREATER
	public IndexAndDataStreamAction()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public IndexAndDataStreamAction()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal IndexAndDataStreamAction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Data stream targeted by the action.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.DataStreamName DataStream { get; set; }

	/// <summary>
	/// <para>
	/// Index for the action.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.IndexName Index { get; set; }
}

public readonly partial struct IndexAndDataStreamActionDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.IndexAndDataStreamAction Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IndexAndDataStreamActionDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.IndexAndDataStreamAction instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IndexAndDataStreamActionDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.IndexAndDataStreamAction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.IndexAndDataStreamActionDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.IndexAndDataStreamAction instance) => new Elastic.Clients.Elasticsearch.IndexManagement.IndexAndDataStreamActionDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.IndexAndDataStreamAction(Elastic.Clients.Elasticsearch.IndexManagement.IndexAndDataStreamActionDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Data stream targeted by the action.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.IndexAndDataStreamActionDescriptor DataStream(Elastic.Clients.Elasticsearch.DataStreamName value)
	{
		Instance.DataStream = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Index for the action.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.IndexAndDataStreamActionDescriptor Index(Elastic.Clients.Elasticsearch.IndexName value)
	{
		Instance.Index = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.IndexAndDataStreamAction Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.IndexAndDataStreamActionDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.IndexAndDataStreamActionDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.IndexAndDataStreamAction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}