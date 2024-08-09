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

namespace Elastic.Clients.Elasticsearch.QueryDsl;

public sealed partial class PinnedDoc
{
	/// <summary>
	/// <para>
	/// The unique document ID.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("_id")]
	public Elastic.Clients.Elasticsearch.Id Id { get; set; }

	/// <summary>
	/// <para>
	/// The index that contains the document.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("_index")]
	public Elastic.Clients.Elasticsearch.IndexName Index { get; set; }
}

public sealed partial class PinnedDocDescriptor : SerializableDescriptor<PinnedDocDescriptor>
{
	internal PinnedDocDescriptor(Action<PinnedDocDescriptor> configure) => configure.Invoke(this);

	public PinnedDocDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Id IdValue { get; set; }
	private Elastic.Clients.Elasticsearch.IndexName IndexValue { get; set; }

	/// <summary>
	/// <para>
	/// The unique document ID.
	/// </para>
	/// </summary>
	public PinnedDocDescriptor Id(Elastic.Clients.Elasticsearch.Id id)
	{
		IdValue = id;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The index that contains the document.
	/// </para>
	/// </summary>
	public PinnedDocDescriptor Index(Elastic.Clients.Elasticsearch.IndexName index)
	{
		IndexValue = index;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("_id");
		JsonSerializer.Serialize(writer, IdValue, options);
		writer.WritePropertyName("_index");
		JsonSerializer.Serialize(writer, IndexValue, options);
		writer.WriteEndObject();
	}
}