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

internal sealed partial class RolloverActionConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverAction>
{
	private static readonly System.Text.Json.JsonEncodedText PropMaxAge = System.Text.Json.JsonEncodedText.Encode("max_age");
	private static readonly System.Text.Json.JsonEncodedText PropMaxDocs = System.Text.Json.JsonEncodedText.Encode("max_docs");
	private static readonly System.Text.Json.JsonEncodedText PropMaxPrimaryShardDocs = System.Text.Json.JsonEncodedText.Encode("max_primary_shard_docs");
	private static readonly System.Text.Json.JsonEncodedText PropMaxPrimaryShardSize = System.Text.Json.JsonEncodedText.Encode("max_primary_shard_size");
	private static readonly System.Text.Json.JsonEncodedText PropMaxSize = System.Text.Json.JsonEncodedText.Encode("max_size");
	private static readonly System.Text.Json.JsonEncodedText PropMinAge = System.Text.Json.JsonEncodedText.Encode("min_age");
	private static readonly System.Text.Json.JsonEncodedText PropMinDocs = System.Text.Json.JsonEncodedText.Encode("min_docs");
	private static readonly System.Text.Json.JsonEncodedText PropMinPrimaryShardDocs = System.Text.Json.JsonEncodedText.Encode("min_primary_shard_docs");
	private static readonly System.Text.Json.JsonEncodedText PropMinPrimaryShardSize = System.Text.Json.JsonEncodedText.Encode("min_primary_shard_size");
	private static readonly System.Text.Json.JsonEncodedText PropMinSize = System.Text.Json.JsonEncodedText.Encode("min_size");

	public override Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverAction Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propMaxAge = default;
		LocalJsonValue<long?> propMaxDocs = default;
		LocalJsonValue<long?> propMaxPrimaryShardDocs = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propMaxPrimaryShardSize = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propMaxSize = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propMinAge = default;
		LocalJsonValue<long?> propMinDocs = default;
		LocalJsonValue<long?> propMinPrimaryShardDocs = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propMinPrimaryShardSize = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propMinSize = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propMaxAge.TryReadProperty(ref reader, options, PropMaxAge, null))
			{
				continue;
			}

			if (propMaxDocs.TryReadProperty(ref reader, options, PropMaxDocs, null))
			{
				continue;
			}

			if (propMaxPrimaryShardDocs.TryReadProperty(ref reader, options, PropMaxPrimaryShardDocs, null))
			{
				continue;
			}

			if (propMaxPrimaryShardSize.TryReadProperty(ref reader, options, PropMaxPrimaryShardSize, null))
			{
				continue;
			}

			if (propMaxSize.TryReadProperty(ref reader, options, PropMaxSize, null))
			{
				continue;
			}

			if (propMinAge.TryReadProperty(ref reader, options, PropMinAge, null))
			{
				continue;
			}

			if (propMinDocs.TryReadProperty(ref reader, options, PropMinDocs, null))
			{
				continue;
			}

			if (propMinPrimaryShardDocs.TryReadProperty(ref reader, options, PropMinPrimaryShardDocs, null))
			{
				continue;
			}

			if (propMinPrimaryShardSize.TryReadProperty(ref reader, options, PropMinPrimaryShardSize, null))
			{
				continue;
			}

			if (propMinSize.TryReadProperty(ref reader, options, PropMinSize, null))
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
		return new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverAction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			MaxAge = propMaxAge.Value,
			MaxDocs = propMaxDocs.Value,
			MaxPrimaryShardDocs = propMaxPrimaryShardDocs.Value,
			MaxPrimaryShardSize = propMaxPrimaryShardSize.Value,
			MaxSize = propMaxSize.Value,
			MinAge = propMinAge.Value,
			MinDocs = propMinDocs.Value,
			MinPrimaryShardDocs = propMinPrimaryShardDocs.Value,
			MinPrimaryShardSize = propMinPrimaryShardSize.Value,
			MinSize = propMinSize.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverAction value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropMaxAge, value.MaxAge, null, null);
		writer.WriteProperty(options, PropMaxDocs, value.MaxDocs, null, null);
		writer.WriteProperty(options, PropMaxPrimaryShardDocs, value.MaxPrimaryShardDocs, null, null);
		writer.WriteProperty(options, PropMaxPrimaryShardSize, value.MaxPrimaryShardSize, null, null);
		writer.WriteProperty(options, PropMaxSize, value.MaxSize, null, null);
		writer.WriteProperty(options, PropMinAge, value.MinAge, null, null);
		writer.WriteProperty(options, PropMinDocs, value.MinDocs, null, null);
		writer.WriteProperty(options, PropMinPrimaryShardDocs, value.MinPrimaryShardDocs, null, null);
		writer.WriteProperty(options, PropMinPrimaryShardSize, value.MinPrimaryShardSize, null, null);
		writer.WriteProperty(options, PropMinSize, value.MinSize, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverActionConverter))]
public sealed partial class RolloverAction
{
#if NET7_0_OR_GREATER
	public RolloverAction()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public RolloverAction()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal RolloverAction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public Elastic.Clients.Elasticsearch.Duration? MaxAge { get; set; }
	public long? MaxDocs { get; set; }
	public long? MaxPrimaryShardDocs { get; set; }
	public Elastic.Clients.Elasticsearch.ByteSize? MaxPrimaryShardSize { get; set; }
	public Elastic.Clients.Elasticsearch.ByteSize? MaxSize { get; set; }
	public Elastic.Clients.Elasticsearch.Duration? MinAge { get; set; }
	public long? MinDocs { get; set; }
	public long? MinPrimaryShardDocs { get; set; }
	public Elastic.Clients.Elasticsearch.ByteSize? MinPrimaryShardSize { get; set; }
	public Elastic.Clients.Elasticsearch.ByteSize? MinSize { get; set; }
}

public readonly partial struct RolloverActionDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverAction Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RolloverActionDescriptor(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverAction instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RolloverActionDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverAction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverActionDescriptor(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverAction instance) => new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverActionDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverAction(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverActionDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverActionDescriptor MaxAge(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MaxAge = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverActionDescriptor MaxDocs(long? value)
	{
		Instance.MaxDocs = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverActionDescriptor MaxPrimaryShardDocs(long? value)
	{
		Instance.MaxPrimaryShardDocs = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverActionDescriptor MaxPrimaryShardSize(Elastic.Clients.Elasticsearch.ByteSize? value)
	{
		Instance.MaxPrimaryShardSize = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverActionDescriptor MaxPrimaryShardSize(System.Func<Elastic.Clients.Elasticsearch.ByteSizeBuilder, Elastic.Clients.Elasticsearch.ByteSize> action)
	{
		Instance.MaxPrimaryShardSize = Elastic.Clients.Elasticsearch.ByteSizeBuilder.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverActionDescriptor MaxSize(Elastic.Clients.Elasticsearch.ByteSize? value)
	{
		Instance.MaxSize = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverActionDescriptor MaxSize(System.Func<Elastic.Clients.Elasticsearch.ByteSizeBuilder, Elastic.Clients.Elasticsearch.ByteSize> action)
	{
		Instance.MaxSize = Elastic.Clients.Elasticsearch.ByteSizeBuilder.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverActionDescriptor MinAge(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MinAge = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverActionDescriptor MinDocs(long? value)
	{
		Instance.MinDocs = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverActionDescriptor MinPrimaryShardDocs(long? value)
	{
		Instance.MinPrimaryShardDocs = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverActionDescriptor MinPrimaryShardSize(Elastic.Clients.Elasticsearch.ByteSize? value)
	{
		Instance.MinPrimaryShardSize = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverActionDescriptor MinPrimaryShardSize(System.Func<Elastic.Clients.Elasticsearch.ByteSizeBuilder, Elastic.Clients.Elasticsearch.ByteSize> action)
	{
		Instance.MinPrimaryShardSize = Elastic.Clients.Elasticsearch.ByteSizeBuilder.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverActionDescriptor MinSize(Elastic.Clients.Elasticsearch.ByteSize? value)
	{
		Instance.MinSize = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverActionDescriptor MinSize(System.Func<Elastic.Clients.Elasticsearch.ByteSizeBuilder, Elastic.Clients.Elasticsearch.ByteSize> action)
	{
		Instance.MinSize = Elastic.Clients.Elasticsearch.ByteSizeBuilder.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverAction Build(System.Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverActionDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverAction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverActionDescriptor(new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.RolloverAction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}