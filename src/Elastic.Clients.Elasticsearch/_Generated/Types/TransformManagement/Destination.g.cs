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

namespace Elastic.Clients.Elasticsearch.TransformManagement;

internal sealed partial class DestinationConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.TransformManagement.Destination>
{
	private static readonly System.Text.Json.JsonEncodedText PropIndex = System.Text.Json.JsonEncodedText.Encode("index");
	private static readonly System.Text.Json.JsonEncodedText PropPipeline = System.Text.Json.JsonEncodedText.Encode("pipeline");

	public override Elastic.Clients.Elasticsearch.TransformManagement.Destination Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexName?> propIndex = default;
		LocalJsonValue<string?> propPipeline = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propIndex.TryReadProperty(ref reader, options, PropIndex, null))
			{
				continue;
			}

			if (propPipeline.TryReadProperty(ref reader, options, PropPipeline, null))
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
		return new Elastic.Clients.Elasticsearch.TransformManagement.Destination(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Index = propIndex.Value,
			Pipeline = propPipeline.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.TransformManagement.Destination value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropIndex, value.Index, null, null);
		writer.WriteProperty(options, PropPipeline, value.Pipeline, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.TransformManagement.DestinationConverter))]
public sealed partial class Destination
{
#if NET7_0_OR_GREATER
	public Destination()
	{
	}
#endif
#if !NET7_0_OR_GREATER
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
	/// The destination index for the transform. The mappings of the destination index are deduced based on the source
	/// fields when possible. If alternate mappings are required, use the create index API prior to starting the
	/// transform.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexName? Index { get; set; }

	/// <summary>
	/// <para>
	/// The unique identifier for an ingest pipeline.
	/// </para>
	/// </summary>
	public string? Pipeline { get; set; }
}

public readonly partial struct DestinationDescriptor
{
	internal Elastic.Clients.Elasticsearch.TransformManagement.Destination Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DestinationDescriptor(Elastic.Clients.Elasticsearch.TransformManagement.Destination instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DestinationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.TransformManagement.Destination(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.TransformManagement.DestinationDescriptor(Elastic.Clients.Elasticsearch.TransformManagement.Destination instance) => new Elastic.Clients.Elasticsearch.TransformManagement.DestinationDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.TransformManagement.Destination(Elastic.Clients.Elasticsearch.TransformManagement.DestinationDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The destination index for the transform. The mappings of the destination index are deduced based on the source
	/// fields when possible. If alternate mappings are required, use the create index API prior to starting the
	/// transform.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.DestinationDescriptor Index(Elastic.Clients.Elasticsearch.IndexName? value)
	{
		Instance.Index = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The unique identifier for an ingest pipeline.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.DestinationDescriptor Pipeline(string? value)
	{
		Instance.Pipeline = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.TransformManagement.Destination Build(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.DestinationDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.TransformManagement.Destination(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.TransformManagement.DestinationDescriptor(new Elastic.Clients.Elasticsearch.TransformManagement.Destination(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}