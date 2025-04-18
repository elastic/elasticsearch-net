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

internal sealed partial class TranslogRetentionConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.TranslogRetention>
{
	private static readonly System.Text.Json.JsonEncodedText PropAge = System.Text.Json.JsonEncodedText.Encode("age");
	private static readonly System.Text.Json.JsonEncodedText PropSize = System.Text.Json.JsonEncodedText.Encode("size");

	public override Elastic.Clients.Elasticsearch.IndexManagement.TranslogRetention Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propAge = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propSize = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAge.TryReadProperty(ref reader, options, PropAge, null))
			{
				continue;
			}

			if (propSize.TryReadProperty(ref reader, options, PropSize, null))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.TranslogRetention(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Age = propAge.Value,
			Size = propSize.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.TranslogRetention value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAge, value.Age, null, null);
		writer.WriteProperty(options, PropSize, value.Size, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.TranslogRetentionConverter))]
public sealed partial class TranslogRetention
{
#if NET7_0_OR_GREATER
	public TranslogRetention()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public TranslogRetention()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal TranslogRetention(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// This controls the maximum duration for which translog files are kept by each shard. Keeping more
	/// translog files increases the chance of performing an operation based sync when recovering replicas. If
	/// the translog files are not sufficient, replica recovery will fall back to a file based sync. This setting
	/// is ignored, and should not be set, if soft deletes are enabled. Soft deletes are enabled by default in
	/// indices created in Elasticsearch versions 7.0.0 and later.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Age { get; set; }

	/// <summary>
	/// <para>
	/// This controls the total size of translog files to keep for each shard. Keeping more translog files increases
	/// the chance of performing an operation based sync when recovering a replica. If the translog files are not
	/// sufficient, replica recovery will fall back to a file based sync. This setting is ignored, and should not be
	/// set, if soft deletes are enabled. Soft deletes are enabled by default in indices created in Elasticsearch
	/// versions 7.0.0 and later.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ByteSize? Size { get; set; }
}

public readonly partial struct TranslogRetentionDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.TranslogRetention Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TranslogRetentionDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.TranslogRetention instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TranslogRetentionDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.TranslogRetention(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.TranslogRetentionDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.TranslogRetention instance) => new Elastic.Clients.Elasticsearch.IndexManagement.TranslogRetentionDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.TranslogRetention(Elastic.Clients.Elasticsearch.IndexManagement.TranslogRetentionDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// This controls the maximum duration for which translog files are kept by each shard. Keeping more
	/// translog files increases the chance of performing an operation based sync when recovering replicas. If
	/// the translog files are not sufficient, replica recovery will fall back to a file based sync. This setting
	/// is ignored, and should not be set, if soft deletes are enabled. Soft deletes are enabled by default in
	/// indices created in Elasticsearch versions 7.0.0 and later.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.TranslogRetentionDescriptor Age(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Age = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// This controls the total size of translog files to keep for each shard. Keeping more translog files increases
	/// the chance of performing an operation based sync when recovering a replica. If the translog files are not
	/// sufficient, replica recovery will fall back to a file based sync. This setting is ignored, and should not be
	/// set, if soft deletes are enabled. Soft deletes are enabled by default in indices created in Elasticsearch
	/// versions 7.0.0 and later.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.TranslogRetentionDescriptor Size(Elastic.Clients.Elasticsearch.ByteSize? value)
	{
		Instance.Size = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// This controls the total size of translog files to keep for each shard. Keeping more translog files increases
	/// the chance of performing an operation based sync when recovering a replica. If the translog files are not
	/// sufficient, replica recovery will fall back to a file based sync. This setting is ignored, and should not be
	/// set, if soft deletes are enabled. Soft deletes are enabled by default in indices created in Elasticsearch
	/// versions 7.0.0 and later.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.TranslogRetentionDescriptor Size(System.Func<Elastic.Clients.Elasticsearch.ByteSizeFactory, Elastic.Clients.Elasticsearch.ByteSize> action)
	{
		Instance.Size = Elastic.Clients.Elasticsearch.ByteSizeFactory.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.TranslogRetention Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.TranslogRetentionDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.IndexManagement.TranslogRetention(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.TranslogRetentionDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.TranslogRetention(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}