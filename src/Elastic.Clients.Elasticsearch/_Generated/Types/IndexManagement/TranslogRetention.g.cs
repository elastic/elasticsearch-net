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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.IndexManagement;

public sealed partial class TranslogRetention
{
	/// <summary>
	/// <para>
	/// This controls the maximum duration for which translog files are kept by each shard. Keeping more
	/// translog files increases the chance of performing an operation based sync when recovering replicas. If
	/// the translog files are not sufficient, replica recovery will fall back to a file based sync. This setting
	/// is ignored, and should not be set, if soft deletes are enabled. Soft deletes are enabled by default in
	/// indices created in Elasticsearch versions 7.0.0 and later.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("age")]
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
	[JsonInclude, JsonPropertyName("size")]
	public Elastic.Clients.Elasticsearch.ByteSize? Size { get; set; }
}

public sealed partial class TranslogRetentionDescriptor : SerializableDescriptor<TranslogRetentionDescriptor>
{
	internal TranslogRetentionDescriptor(Action<TranslogRetentionDescriptor> configure) => configure.Invoke(this);

	public TranslogRetentionDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Duration? AgeValue { get; set; }
	private Elastic.Clients.Elasticsearch.ByteSize? SizeValue { get; set; }

	/// <summary>
	/// <para>
	/// This controls the maximum duration for which translog files are kept by each shard. Keeping more
	/// translog files increases the chance of performing an operation based sync when recovering replicas. If
	/// the translog files are not sufficient, replica recovery will fall back to a file based sync. This setting
	/// is ignored, and should not be set, if soft deletes are enabled. Soft deletes are enabled by default in
	/// indices created in Elasticsearch versions 7.0.0 and later.
	/// </para>
	/// </summary>
	public TranslogRetentionDescriptor Age(Elastic.Clients.Elasticsearch.Duration? age)
	{
		AgeValue = age;
		return Self;
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
	public TranslogRetentionDescriptor Size(Elastic.Clients.Elasticsearch.ByteSize? size)
	{
		SizeValue = size;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (AgeValue is not null)
		{
			writer.WritePropertyName("age");
			JsonSerializer.Serialize(writer, AgeValue, options);
		}

		if (SizeValue is not null)
		{
			writer.WritePropertyName("size");
			JsonSerializer.Serialize(writer, SizeValue, options);
		}

		writer.WriteEndObject();
	}
}