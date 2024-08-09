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

public sealed partial class Storage
{
	/// <summary>
	/// <para>
	/// You can restrict the use of the mmapfs and the related hybridfs store type via the setting node.store.allow_mmap.
	/// This is a boolean setting indicating whether or not memory-mapping is allowed. The default is to allow it. This
	/// setting is useful, for example, if you are in an environment where you can not control the ability to create a lot
	/// of memory maps so you need disable the ability to use memory-mapping.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("allow_mmap")]
	public bool? AllowMmap { get; set; }
	[JsonInclude, JsonPropertyName("type")]
	public Elastic.Clients.Elasticsearch.Serverless.IndexManagement.StorageType Type { get; set; }
}

public sealed partial class StorageDescriptor : SerializableDescriptor<StorageDescriptor>
{
	internal StorageDescriptor(Action<StorageDescriptor> configure) => configure.Invoke(this);

	public StorageDescriptor() : base()
	{
	}

	private bool? AllowMmapValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.IndexManagement.StorageType TypeValue { get; set; }

	/// <summary>
	/// <para>
	/// You can restrict the use of the mmapfs and the related hybridfs store type via the setting node.store.allow_mmap.
	/// This is a boolean setting indicating whether or not memory-mapping is allowed. The default is to allow it. This
	/// setting is useful, for example, if you are in an environment where you can not control the ability to create a lot
	/// of memory maps so you need disable the ability to use memory-mapping.
	/// </para>
	/// </summary>
	public StorageDescriptor AllowMmap(bool? allowMmap = true)
	{
		AllowMmapValue = allowMmap;
		return Self;
	}

	public StorageDescriptor Type(Elastic.Clients.Elasticsearch.Serverless.IndexManagement.StorageType type)
	{
		TypeValue = type;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (AllowMmapValue.HasValue)
		{
			writer.WritePropertyName("allow_mmap");
			writer.WriteBooleanValue(AllowMmapValue.Value);
		}

		writer.WritePropertyName("type");
		JsonSerializer.Serialize(writer, TypeValue, options);
		writer.WriteEndObject();
	}
}