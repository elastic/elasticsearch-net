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

namespace Elastic.Clients.Elasticsearch.IndexLifecycleManagement;

internal sealed partial class ForceMergeActionConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ForceMergeAction>
{
	private static readonly System.Text.Json.JsonEncodedText PropIndexCodec = System.Text.Json.JsonEncodedText.Encode("index_codec");
	private static readonly System.Text.Json.JsonEncodedText PropMaxNumSegments = System.Text.Json.JsonEncodedText.Encode("max_num_segments");

	public override Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ForceMergeAction Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propIndexCodec = default;
		LocalJsonValue<int> propMaxNumSegments = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propIndexCodec.TryReadProperty(ref reader, options, PropIndexCodec, null))
			{
				continue;
			}

			if (propMaxNumSegments.TryReadProperty(ref reader, options, PropMaxNumSegments, null))
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
		return new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ForceMergeAction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			IndexCodec = propIndexCodec.Value,
			MaxNumSegments = propMaxNumSegments.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ForceMergeAction value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropIndexCodec, value.IndexCodec, null, null);
		writer.WriteProperty(options, PropMaxNumSegments, value.MaxNumSegments, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ForceMergeActionConverter))]
public sealed partial class ForceMergeAction
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ForceMergeAction(int maxNumSegments)
	{
		MaxNumSegments = maxNumSegments;
	}
#if NET7_0_OR_GREATER
	public ForceMergeAction()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ForceMergeAction()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ForceMergeAction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public string? IndexCodec { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int MaxNumSegments { get; set; }
}

public readonly partial struct ForceMergeActionDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ForceMergeAction Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ForceMergeActionDescriptor(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ForceMergeAction instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ForceMergeActionDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ForceMergeAction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ForceMergeActionDescriptor(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ForceMergeAction instance) => new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ForceMergeActionDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ForceMergeAction(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ForceMergeActionDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ForceMergeActionDescriptor IndexCodec(string? value)
	{
		Instance.IndexCodec = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ForceMergeActionDescriptor MaxNumSegments(int value)
	{
		Instance.MaxNumSegments = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ForceMergeAction Build(System.Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ForceMergeActionDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ForceMergeActionDescriptor(new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ForceMergeAction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}