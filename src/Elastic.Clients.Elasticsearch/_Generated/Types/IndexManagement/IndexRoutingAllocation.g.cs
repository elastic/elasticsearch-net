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

internal sealed partial class IndexRoutingAllocationConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocation>
{
	private static readonly System.Text.Json.JsonEncodedText PropDisk = System.Text.Json.JsonEncodedText.Encode("disk");
	private static readonly System.Text.Json.JsonEncodedText PropEnable = System.Text.Json.JsonEncodedText.Encode("enable");
	private static readonly System.Text.Json.JsonEncodedText PropInclude = System.Text.Json.JsonEncodedText.Encode("include");
	private static readonly System.Text.Json.JsonEncodedText PropInitialRecovery = System.Text.Json.JsonEncodedText.Encode("initial_recovery");

	public override Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocation Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationDisk?> propDisk = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationOptions?> propEnable = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationInclude?> propInclude = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationInitialRecovery?> propInitialRecovery = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDisk.TryReadProperty(ref reader, options, PropDisk, null))
			{
				continue;
			}

			if (propEnable.TryReadProperty(ref reader, options, PropEnable, null))
			{
				continue;
			}

			if (propInclude.TryReadProperty(ref reader, options, PropInclude, null))
			{
				continue;
			}

			if (propInitialRecovery.TryReadProperty(ref reader, options, PropInitialRecovery, null))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Disk = propDisk.Value,
			Enable = propEnable.Value,
			Include = propInclude.Value,
			InitialRecovery = propInitialRecovery.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocation value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDisk, value.Disk, null, null);
		writer.WriteProperty(options, PropEnable, value.Enable, null, null);
		writer.WriteProperty(options, PropInclude, value.Include, null, null);
		writer.WriteProperty(options, PropInitialRecovery, value.InitialRecovery, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationConverter))]
public sealed partial class IndexRoutingAllocation
{
#if NET7_0_OR_GREATER
	public IndexRoutingAllocation()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public IndexRoutingAllocation()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal IndexRoutingAllocation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationDisk? Disk { get; set; }
	public Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationOptions? Enable { get; set; }
	public Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationInclude? Include { get; set; }
	public Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationInitialRecovery? InitialRecovery { get; set; }
}

public readonly partial struct IndexRoutingAllocationDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IndexRoutingAllocationDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IndexRoutingAllocationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocation instance) => new Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocation(Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationDescriptor Disk(Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationDisk? value)
	{
		Instance.Disk = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationDescriptor Disk()
	{
		Instance.Disk = Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationDiskDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationDescriptor Disk(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationDiskDescriptor>? action)
	{
		Instance.Disk = Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationDiskDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationDescriptor Enable(Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationOptions? value)
	{
		Instance.Enable = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationDescriptor Include(Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationInclude? value)
	{
		Instance.Include = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationDescriptor Include()
	{
		Instance.Include = Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationIncludeDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationDescriptor Include(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationIncludeDescriptor>? action)
	{
		Instance.Include = Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationIncludeDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationDescriptor InitialRecovery(Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationInitialRecovery? value)
	{
		Instance.InitialRecovery = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationDescriptor InitialRecovery()
	{
		Instance.InitialRecovery = Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationInitialRecoveryDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationDescriptor InitialRecovery(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationInitialRecoveryDescriptor>? action)
	{
		Instance.InitialRecovery = Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationInitialRecoveryDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocation Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocationDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.IndexRoutingAllocation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}