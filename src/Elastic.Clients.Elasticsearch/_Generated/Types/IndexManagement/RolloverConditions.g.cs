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

internal sealed partial class RolloverConditionsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditions>
{
	private static readonly System.Text.Json.JsonEncodedText PropMaxAge = System.Text.Json.JsonEncodedText.Encode("max_age");
	private static readonly System.Text.Json.JsonEncodedText PropMaxAgeMillis = System.Text.Json.JsonEncodedText.Encode("max_age_millis");
	private static readonly System.Text.Json.JsonEncodedText PropMaxDocs = System.Text.Json.JsonEncodedText.Encode("max_docs");
	private static readonly System.Text.Json.JsonEncodedText PropMaxPrimaryShardDocs = System.Text.Json.JsonEncodedText.Encode("max_primary_shard_docs");
	private static readonly System.Text.Json.JsonEncodedText PropMaxPrimaryShardSize = System.Text.Json.JsonEncodedText.Encode("max_primary_shard_size");
	private static readonly System.Text.Json.JsonEncodedText PropMaxPrimaryShardSizeBytes = System.Text.Json.JsonEncodedText.Encode("max_primary_shard_size_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropMaxSize = System.Text.Json.JsonEncodedText.Encode("max_size");
	private static readonly System.Text.Json.JsonEncodedText PropMaxSizeBytes = System.Text.Json.JsonEncodedText.Encode("max_size_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropMinAge = System.Text.Json.JsonEncodedText.Encode("min_age");
	private static readonly System.Text.Json.JsonEncodedText PropMinDocs = System.Text.Json.JsonEncodedText.Encode("min_docs");
	private static readonly System.Text.Json.JsonEncodedText PropMinPrimaryShardDocs = System.Text.Json.JsonEncodedText.Encode("min_primary_shard_docs");
	private static readonly System.Text.Json.JsonEncodedText PropMinPrimaryShardSize = System.Text.Json.JsonEncodedText.Encode("min_primary_shard_size");
	private static readonly System.Text.Json.JsonEncodedText PropMinPrimaryShardSizeBytes = System.Text.Json.JsonEncodedText.Encode("min_primary_shard_size_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropMinSize = System.Text.Json.JsonEncodedText.Encode("min_size");
	private static readonly System.Text.Json.JsonEncodedText PropMinSizeBytes = System.Text.Json.JsonEncodedText.Encode("min_size_bytes");

	public override Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditions Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propMaxAge = default;
		LocalJsonValue<System.TimeSpan?> propMaxAgeMillis = default;
		LocalJsonValue<long?> propMaxDocs = default;
		LocalJsonValue<long?> propMaxPrimaryShardDocs = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propMaxPrimaryShardSize = default;
		LocalJsonValue<long?> propMaxPrimaryShardSizeBytes = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propMaxSize = default;
		LocalJsonValue<long?> propMaxSizeBytes = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propMinAge = default;
		LocalJsonValue<long?> propMinDocs = default;
		LocalJsonValue<long?> propMinPrimaryShardDocs = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propMinPrimaryShardSize = default;
		LocalJsonValue<long?> propMinPrimaryShardSizeBytes = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propMinSize = default;
		LocalJsonValue<long?> propMinSizeBytes = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propMaxAge.TryReadProperty(ref reader, options, PropMaxAge, null))
			{
				continue;
			}

			if (propMaxAgeMillis.TryReadProperty(ref reader, options, PropMaxAgeMillis, static System.TimeSpan? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
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

			if (propMaxPrimaryShardSizeBytes.TryReadProperty(ref reader, options, PropMaxPrimaryShardSizeBytes, null))
			{
				continue;
			}

			if (propMaxSize.TryReadProperty(ref reader, options, PropMaxSize, null))
			{
				continue;
			}

			if (propMaxSizeBytes.TryReadProperty(ref reader, options, PropMaxSizeBytes, null))
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

			if (propMinPrimaryShardSizeBytes.TryReadProperty(ref reader, options, PropMinPrimaryShardSizeBytes, null))
			{
				continue;
			}

			if (propMinSize.TryReadProperty(ref reader, options, PropMinSize, null))
			{
				continue;
			}

			if (propMinSizeBytes.TryReadProperty(ref reader, options, PropMinSizeBytes, null))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			MaxAge = propMaxAge.Value,
			MaxAgeMillis = propMaxAgeMillis.Value,
			MaxDocs = propMaxDocs.Value,
			MaxPrimaryShardDocs = propMaxPrimaryShardDocs.Value,
			MaxPrimaryShardSize = propMaxPrimaryShardSize.Value,
			MaxPrimaryShardSizeBytes = propMaxPrimaryShardSizeBytes.Value,
			MaxSize = propMaxSize.Value,
			MaxSizeBytes = propMaxSizeBytes.Value,
			MinAge = propMinAge.Value,
			MinDocs = propMinDocs.Value,
			MinPrimaryShardDocs = propMinPrimaryShardDocs.Value,
			MinPrimaryShardSize = propMinPrimaryShardSize.Value,
			MinPrimaryShardSizeBytes = propMinPrimaryShardSizeBytes.Value,
			MinSize = propMinSize.Value,
			MinSizeBytes = propMinSizeBytes.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditions value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropMaxAge, value.MaxAge, null, null);
		writer.WriteProperty(options, PropMaxAgeMillis, value.MaxAgeMillis, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan? v) => w.WriteValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteProperty(options, PropMaxDocs, value.MaxDocs, null, null);
		writer.WriteProperty(options, PropMaxPrimaryShardDocs, value.MaxPrimaryShardDocs, null, null);
		writer.WriteProperty(options, PropMaxPrimaryShardSize, value.MaxPrimaryShardSize, null, null);
		writer.WriteProperty(options, PropMaxPrimaryShardSizeBytes, value.MaxPrimaryShardSizeBytes, null, null);
		writer.WriteProperty(options, PropMaxSize, value.MaxSize, null, null);
		writer.WriteProperty(options, PropMaxSizeBytes, value.MaxSizeBytes, null, null);
		writer.WriteProperty(options, PropMinAge, value.MinAge, null, null);
		writer.WriteProperty(options, PropMinDocs, value.MinDocs, null, null);
		writer.WriteProperty(options, PropMinPrimaryShardDocs, value.MinPrimaryShardDocs, null, null);
		writer.WriteProperty(options, PropMinPrimaryShardSize, value.MinPrimaryShardSize, null, null);
		writer.WriteProperty(options, PropMinPrimaryShardSizeBytes, value.MinPrimaryShardSizeBytes, null, null);
		writer.WriteProperty(options, PropMinSize, value.MinSize, null, null);
		writer.WriteProperty(options, PropMinSizeBytes, value.MinSizeBytes, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsConverter))]
public sealed partial class RolloverConditions
{
#if NET7_0_OR_GREATER
	public RolloverConditions()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public RolloverConditions()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal RolloverConditions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public Elastic.Clients.Elasticsearch.Duration? MaxAge { get; set; }
	public System.TimeSpan? MaxAgeMillis { get; set; }
	public long? MaxDocs { get; set; }
	public long? MaxPrimaryShardDocs { get; set; }
	public Elastic.Clients.Elasticsearch.ByteSize? MaxPrimaryShardSize { get; set; }
	public long? MaxPrimaryShardSizeBytes { get; set; }
	public Elastic.Clients.Elasticsearch.ByteSize? MaxSize { get; set; }
	public long? MaxSizeBytes { get; set; }
	public Elastic.Clients.Elasticsearch.Duration? MinAge { get; set; }
	public long? MinDocs { get; set; }
	public long? MinPrimaryShardDocs { get; set; }
	public Elastic.Clients.Elasticsearch.ByteSize? MinPrimaryShardSize { get; set; }
	public long? MinPrimaryShardSizeBytes { get; set; }
	public Elastic.Clients.Elasticsearch.ByteSize? MinSize { get; set; }
	public long? MinSizeBytes { get; set; }
}

public readonly partial struct RolloverConditionsDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditions Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RolloverConditionsDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditions instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RolloverConditionsDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditions instance) => new Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditions(Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor MaxAge(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MaxAge = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor MaxAgeMillis(System.TimeSpan? value)
	{
		Instance.MaxAgeMillis = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor MaxDocs(long? value)
	{
		Instance.MaxDocs = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor MaxPrimaryShardDocs(long? value)
	{
		Instance.MaxPrimaryShardDocs = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor MaxPrimaryShardSize(Elastic.Clients.Elasticsearch.ByteSize? value)
	{
		Instance.MaxPrimaryShardSize = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor MaxPrimaryShardSize(System.Func<Elastic.Clients.Elasticsearch.ByteSizeFactory, Elastic.Clients.Elasticsearch.ByteSize> action)
	{
		Instance.MaxPrimaryShardSize = Elastic.Clients.Elasticsearch.ByteSizeFactory.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor MaxPrimaryShardSizeBytes(long? value)
	{
		Instance.MaxPrimaryShardSizeBytes = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor MaxSize(Elastic.Clients.Elasticsearch.ByteSize? value)
	{
		Instance.MaxSize = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor MaxSize(System.Func<Elastic.Clients.Elasticsearch.ByteSizeFactory, Elastic.Clients.Elasticsearch.ByteSize> action)
	{
		Instance.MaxSize = Elastic.Clients.Elasticsearch.ByteSizeFactory.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor MaxSizeBytes(long? value)
	{
		Instance.MaxSizeBytes = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor MinAge(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MinAge = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor MinDocs(long? value)
	{
		Instance.MinDocs = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor MinPrimaryShardDocs(long? value)
	{
		Instance.MinPrimaryShardDocs = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor MinPrimaryShardSize(Elastic.Clients.Elasticsearch.ByteSize? value)
	{
		Instance.MinPrimaryShardSize = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor MinPrimaryShardSize(System.Func<Elastic.Clients.Elasticsearch.ByteSizeFactory, Elastic.Clients.Elasticsearch.ByteSize> action)
	{
		Instance.MinPrimaryShardSize = Elastic.Clients.Elasticsearch.ByteSizeFactory.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor MinPrimaryShardSizeBytes(long? value)
	{
		Instance.MinPrimaryShardSizeBytes = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor MinSize(Elastic.Clients.Elasticsearch.ByteSize? value)
	{
		Instance.MinSize = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor MinSize(System.Func<Elastic.Clients.Elasticsearch.ByteSizeFactory, Elastic.Clients.Elasticsearch.ByteSize> action)
	{
		Instance.MinSize = Elastic.Clients.Elasticsearch.ByteSizeFactory.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor MinSizeBytes(long? value)
	{
		Instance.MinSizeBytes = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditions Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}