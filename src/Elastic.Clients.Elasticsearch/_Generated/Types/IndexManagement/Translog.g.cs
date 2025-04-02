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

internal sealed partial class TranslogConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.Translog>
{
	private static readonly System.Text.Json.JsonEncodedText PropDurability = System.Text.Json.JsonEncodedText.Encode("durability");
	private static readonly System.Text.Json.JsonEncodedText PropFlushThresholdSize = System.Text.Json.JsonEncodedText.Encode("flush_threshold_size");
	private static readonly System.Text.Json.JsonEncodedText PropRetention = System.Text.Json.JsonEncodedText.Encode("retention");
	private static readonly System.Text.Json.JsonEncodedText PropSyncInterval = System.Text.Json.JsonEncodedText.Encode("sync_interval");

	public override Elastic.Clients.Elasticsearch.IndexManagement.Translog Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.TranslogDurability?> propDurability = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propFlushThresholdSize = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.TranslogRetention?> propRetention = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propSyncInterval = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDurability.TryReadProperty(ref reader, options, PropDurability, null))
			{
				continue;
			}

			if (propFlushThresholdSize.TryReadProperty(ref reader, options, PropFlushThresholdSize, null))
			{
				continue;
			}

			if (propRetention.TryReadProperty(ref reader, options, PropRetention, null))
			{
				continue;
			}

			if (propSyncInterval.TryReadProperty(ref reader, options, PropSyncInterval, null))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.Translog(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Durability = propDurability.Value,
			FlushThresholdSize = propFlushThresholdSize.Value,
			Retention = propRetention.Value,
			SyncInterval = propSyncInterval.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.Translog value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDurability, value.Durability, null, null);
		writer.WriteProperty(options, PropFlushThresholdSize, value.FlushThresholdSize, null, null);
		writer.WriteProperty(options, PropRetention, value.Retention, null, null);
		writer.WriteProperty(options, PropSyncInterval, value.SyncInterval, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.TranslogConverter))]
public sealed partial class Translog
{
#if NET7_0_OR_GREATER
	public Translog()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public Translog()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Translog(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Whether or not to <c>fsync</c> and commit the translog after every index, delete, update, or bulk request.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.TranslogDurability? Durability { get; set; }

	/// <summary>
	/// <para>
	/// The translog stores all operations that are not yet safely persisted in Lucene (i.e., are not
	/// part of a Lucene commit point). Although these operations are available for reads, they will need
	/// to be replayed if the shard was stopped and had to be recovered. This setting controls the
	/// maximum total size of these operations, to prevent recoveries from taking too long. Once the
	/// maximum size has been reached a flush will happen, generating a new Lucene commit point.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ByteSize? FlushThresholdSize { get; set; }
	public Elastic.Clients.Elasticsearch.IndexManagement.TranslogRetention? Retention { get; set; }

	/// <summary>
	/// <para>
	/// How often the translog is fsynced to disk and committed, regardless of write operations.
	/// Values less than 100ms are not allowed.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? SyncInterval { get; set; }
}

public readonly partial struct TranslogDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.Translog Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TranslogDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.Translog instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TranslogDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.Translog(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.TranslogDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.Translog instance) => new Elastic.Clients.Elasticsearch.IndexManagement.TranslogDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.Translog(Elastic.Clients.Elasticsearch.IndexManagement.TranslogDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Whether or not to <c>fsync</c> and commit the translog after every index, delete, update, or bulk request.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.TranslogDescriptor Durability(Elastic.Clients.Elasticsearch.IndexManagement.TranslogDurability? value)
	{
		Instance.Durability = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The translog stores all operations that are not yet safely persisted in Lucene (i.e., are not
	/// part of a Lucene commit point). Although these operations are available for reads, they will need
	/// to be replayed if the shard was stopped and had to be recovered. This setting controls the
	/// maximum total size of these operations, to prevent recoveries from taking too long. Once the
	/// maximum size has been reached a flush will happen, generating a new Lucene commit point.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.TranslogDescriptor FlushThresholdSize(Elastic.Clients.Elasticsearch.ByteSize? value)
	{
		Instance.FlushThresholdSize = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The translog stores all operations that are not yet safely persisted in Lucene (i.e., are not
	/// part of a Lucene commit point). Although these operations are available for reads, they will need
	/// to be replayed if the shard was stopped and had to be recovered. This setting controls the
	/// maximum total size of these operations, to prevent recoveries from taking too long. Once the
	/// maximum size has been reached a flush will happen, generating a new Lucene commit point.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.TranslogDescriptor FlushThresholdSize(System.Func<Elastic.Clients.Elasticsearch.ByteSizeBuilder, Elastic.Clients.Elasticsearch.ByteSize> action)
	{
		Instance.FlushThresholdSize = Elastic.Clients.Elasticsearch.ByteSizeBuilder.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.TranslogDescriptor Retention(Elastic.Clients.Elasticsearch.IndexManagement.TranslogRetention? value)
	{
		Instance.Retention = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.TranslogDescriptor Retention()
	{
		Instance.Retention = Elastic.Clients.Elasticsearch.IndexManagement.TranslogRetentionDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.TranslogDescriptor Retention(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.TranslogRetentionDescriptor>? action)
	{
		Instance.Retention = Elastic.Clients.Elasticsearch.IndexManagement.TranslogRetentionDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// How often the translog is fsynced to disk and committed, regardless of write operations.
	/// Values less than 100ms are not allowed.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.TranslogDescriptor SyncInterval(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.SyncInterval = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.Translog Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.TranslogDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.IndexManagement.Translog(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.TranslogDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.Translog(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}