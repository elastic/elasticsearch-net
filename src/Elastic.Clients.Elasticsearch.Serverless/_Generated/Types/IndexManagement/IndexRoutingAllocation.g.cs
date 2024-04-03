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

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.IndexManagement;

public sealed partial class IndexRoutingAllocation
{
	[JsonInclude, JsonPropertyName("disk")]
	public Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexRoutingAllocationDisk? Disk { get; set; }
	[JsonInclude, JsonPropertyName("enable")]
	public Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexRoutingAllocationOptions? Enable { get; set; }
	[JsonInclude, JsonPropertyName("include")]
	public Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexRoutingAllocationInclude? Include { get; set; }
	[JsonInclude, JsonPropertyName("initial_recovery")]
	public Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexRoutingAllocationInitialRecovery? InitialRecovery { get; set; }
}

public sealed partial class IndexRoutingAllocationDescriptor : SerializableDescriptor<IndexRoutingAllocationDescriptor>
{
	internal IndexRoutingAllocationDescriptor(Action<IndexRoutingAllocationDescriptor> configure) => configure.Invoke(this);

	public IndexRoutingAllocationDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexRoutingAllocationDisk? DiskValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexRoutingAllocationDiskDescriptor DiskDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexRoutingAllocationDiskDescriptor> DiskDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexRoutingAllocationOptions? EnableValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexRoutingAllocationInclude? IncludeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexRoutingAllocationIncludeDescriptor IncludeDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexRoutingAllocationIncludeDescriptor> IncludeDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexRoutingAllocationInitialRecovery? InitialRecoveryValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexRoutingAllocationInitialRecoveryDescriptor InitialRecoveryDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexRoutingAllocationInitialRecoveryDescriptor> InitialRecoveryDescriptorAction { get; set; }

	public IndexRoutingAllocationDescriptor Disk(Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexRoutingAllocationDisk? disk)
	{
		DiskDescriptor = null;
		DiskDescriptorAction = null;
		DiskValue = disk;
		return Self;
	}

	public IndexRoutingAllocationDescriptor Disk(Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexRoutingAllocationDiskDescriptor descriptor)
	{
		DiskValue = null;
		DiskDescriptorAction = null;
		DiskDescriptor = descriptor;
		return Self;
	}

	public IndexRoutingAllocationDescriptor Disk(Action<Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexRoutingAllocationDiskDescriptor> configure)
	{
		DiskValue = null;
		DiskDescriptor = null;
		DiskDescriptorAction = configure;
		return Self;
	}

	public IndexRoutingAllocationDescriptor Enable(Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexRoutingAllocationOptions? enable)
	{
		EnableValue = enable;
		return Self;
	}

	public IndexRoutingAllocationDescriptor Include(Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexRoutingAllocationInclude? include)
	{
		IncludeDescriptor = null;
		IncludeDescriptorAction = null;
		IncludeValue = include;
		return Self;
	}

	public IndexRoutingAllocationDescriptor Include(Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexRoutingAllocationIncludeDescriptor descriptor)
	{
		IncludeValue = null;
		IncludeDescriptorAction = null;
		IncludeDescriptor = descriptor;
		return Self;
	}

	public IndexRoutingAllocationDescriptor Include(Action<Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexRoutingAllocationIncludeDescriptor> configure)
	{
		IncludeValue = null;
		IncludeDescriptor = null;
		IncludeDescriptorAction = configure;
		return Self;
	}

	public IndexRoutingAllocationDescriptor InitialRecovery(Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexRoutingAllocationInitialRecovery? initialRecovery)
	{
		InitialRecoveryDescriptor = null;
		InitialRecoveryDescriptorAction = null;
		InitialRecoveryValue = initialRecovery;
		return Self;
	}

	public IndexRoutingAllocationDescriptor InitialRecovery(Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexRoutingAllocationInitialRecoveryDescriptor descriptor)
	{
		InitialRecoveryValue = null;
		InitialRecoveryDescriptorAction = null;
		InitialRecoveryDescriptor = descriptor;
		return Self;
	}

	public IndexRoutingAllocationDescriptor InitialRecovery(Action<Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexRoutingAllocationInitialRecoveryDescriptor> configure)
	{
		InitialRecoveryValue = null;
		InitialRecoveryDescriptor = null;
		InitialRecoveryDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (DiskDescriptor is not null)
		{
			writer.WritePropertyName("disk");
			JsonSerializer.Serialize(writer, DiskDescriptor, options);
		}
		else if (DiskDescriptorAction is not null)
		{
			writer.WritePropertyName("disk");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexRoutingAllocationDiskDescriptor(DiskDescriptorAction), options);
		}
		else if (DiskValue is not null)
		{
			writer.WritePropertyName("disk");
			JsonSerializer.Serialize(writer, DiskValue, options);
		}

		if (EnableValue is not null)
		{
			writer.WritePropertyName("enable");
			JsonSerializer.Serialize(writer, EnableValue, options);
		}

		if (IncludeDescriptor is not null)
		{
			writer.WritePropertyName("include");
			JsonSerializer.Serialize(writer, IncludeDescriptor, options);
		}
		else if (IncludeDescriptorAction is not null)
		{
			writer.WritePropertyName("include");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexRoutingAllocationIncludeDescriptor(IncludeDescriptorAction), options);
		}
		else if (IncludeValue is not null)
		{
			writer.WritePropertyName("include");
			JsonSerializer.Serialize(writer, IncludeValue, options);
		}

		if (InitialRecoveryDescriptor is not null)
		{
			writer.WritePropertyName("initial_recovery");
			JsonSerializer.Serialize(writer, InitialRecoveryDescriptor, options);
		}
		else if (InitialRecoveryDescriptorAction is not null)
		{
			writer.WritePropertyName("initial_recovery");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexRoutingAllocationInitialRecoveryDescriptor(InitialRecoveryDescriptorAction), options);
		}
		else if (InitialRecoveryValue is not null)
		{
			writer.WritePropertyName("initial_recovery");
			JsonSerializer.Serialize(writer, InitialRecoveryValue, options);
		}

		writer.WriteEndObject();
	}
}