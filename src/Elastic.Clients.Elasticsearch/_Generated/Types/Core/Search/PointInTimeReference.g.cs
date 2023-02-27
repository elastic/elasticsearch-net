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
namespace Elastic.Clients.Elasticsearch.Core.Search;
public sealed partial class PointInTimeReference
{
	[JsonInclude, JsonPropertyName("id")]
	public string Id { get; set; }

	[JsonInclude, JsonPropertyName("keep_alive")]
	public Elastic.Clients.Elasticsearch.Duration? KeepAlive { get; set; }
}

public sealed partial class PointInTimeReferenceDescriptor : SerializableDescriptor<PointInTimeReferenceDescriptor>
{
	internal PointInTimeReferenceDescriptor(Action<PointInTimeReferenceDescriptor> configure) => configure.Invoke(this);
	public PointInTimeReferenceDescriptor() : base()
	{
	}

	private string IdValue { get; set; }

	private Elastic.Clients.Elasticsearch.Duration? KeepAliveValue { get; set; }

	public PointInTimeReferenceDescriptor Id(string id)
	{
		IdValue = id;
		return Self;
	}

	public PointInTimeReferenceDescriptor KeepAlive(Elastic.Clients.Elasticsearch.Duration? keepAlive)
	{
		KeepAliveValue = keepAlive;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("id");
		JsonSerializer.Serialize(writer, IdValue, options);
		if (KeepAliveValue is not null)
		{
			writer.WritePropertyName("keep_alive");
			JsonSerializer.Serialize(writer, KeepAliveValue, options);
		}

		writer.WriteEndObject();
	}
}