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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.IndexManagement;
public sealed partial class SoftDeletes
{
	[JsonInclude]
	[JsonPropertyName("enabled")]
	public bool? Enabled { get; set; }

	[JsonInclude]
	[JsonPropertyName("retention_lease")]
	public Elastic.Clients.Elasticsearch.IndexManagement.RetentionLease? RetentionLease { get; set; }
}

public sealed partial class SoftDeletesDescriptor : SerializableDescriptor<SoftDeletesDescriptor>
{
	internal SoftDeletesDescriptor(Action<SoftDeletesDescriptor> configure) => configure.Invoke(this);
	public SoftDeletesDescriptor() : base()
	{
	}

	private bool? EnabledValue { get; set; }

	private Elastic.Clients.Elasticsearch.IndexManagement.RetentionLease? RetentionLeaseValue { get; set; }

	private RetentionLeaseDescriptor RetentionLeaseDescriptor { get; set; }

	private Action<RetentionLeaseDescriptor> RetentionLeaseDescriptorAction { get; set; }

	public SoftDeletesDescriptor Enabled(bool? enabled = true)
	{
		EnabledValue = enabled;
		return Self;
	}

	public SoftDeletesDescriptor RetentionLease(Elastic.Clients.Elasticsearch.IndexManagement.RetentionLease? retentionLease)
	{
		RetentionLeaseDescriptor = null;
		RetentionLeaseDescriptorAction = null;
		RetentionLeaseValue = retentionLease;
		return Self;
	}

	public SoftDeletesDescriptor RetentionLease(RetentionLeaseDescriptor descriptor)
	{
		RetentionLeaseValue = null;
		RetentionLeaseDescriptorAction = null;
		RetentionLeaseDescriptor = descriptor;
		return Self;
	}

	public SoftDeletesDescriptor RetentionLease(Action<RetentionLeaseDescriptor> configure)
	{
		RetentionLeaseValue = null;
		RetentionLeaseDescriptor = null;
		RetentionLeaseDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (EnabledValue.HasValue)
		{
			writer.WritePropertyName("enabled");
			writer.WriteBooleanValue(EnabledValue.Value);
		}

		if (RetentionLeaseDescriptor is not null)
		{
			writer.WritePropertyName("retention_lease");
			JsonSerializer.Serialize(writer, RetentionLeaseDescriptor, options);
		}
		else if (RetentionLeaseDescriptorAction is not null)
		{
			writer.WritePropertyName("retention_lease");
			JsonSerializer.Serialize(writer, new RetentionLeaseDescriptor(RetentionLeaseDescriptorAction), options);
		}
		else if (RetentionLeaseValue is not null)
		{
			writer.WritePropertyName("retention_lease");
			JsonSerializer.Serialize(writer, RetentionLeaseValue, options);
		}

		writer.WriteEndObject();
	}
}