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
namespace Elastic.Clients.Elasticsearch.QueryDsl;
public sealed partial class ParentIdQuery : Query
{
	[JsonInclude]
	[JsonPropertyName("_name")]
	public string? QueryName { get; set; }

	[JsonInclude]
	[JsonPropertyName("boost")]
	public float? Boost { get; set; }

	[JsonInclude]
	[JsonPropertyName("id")]
	public Elastic.Clients.Elasticsearch.Id? Id { get; set; }

	[JsonInclude]
	[JsonPropertyName("ignore_unmapped")]
	public bool? IgnoreUnmapped { get; set; }

	[JsonInclude]
	[JsonPropertyName("type")]
	public string? Type { get; set; }

	public static implicit operator QueryContainer(ParentIdQuery parentIdQuery) => QueryContainer.ParentId(parentIdQuery);
}

public sealed partial class ParentIdQueryDescriptor : SerializableDescriptor<ParentIdQueryDescriptor>
{
	internal ParentIdQueryDescriptor(Action<ParentIdQueryDescriptor> configure) => configure.Invoke(this);
	public ParentIdQueryDescriptor() : base()
	{
	}

	private string? QueryNameValue { get; set; }

	private float? BoostValue { get; set; }

	private Elastic.Clients.Elasticsearch.Id? IdValue { get; set; }

	private bool? IgnoreUnmappedValue { get; set; }

	private string? TypeValue { get; set; }

	public ParentIdQueryDescriptor QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	public ParentIdQueryDescriptor Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	public ParentIdQueryDescriptor Id(Elastic.Clients.Elasticsearch.Id? id)
	{
		IdValue = id;
		return Self;
	}

	public ParentIdQueryDescriptor IgnoreUnmapped(bool? ignoreUnmapped = true)
	{
		IgnoreUnmappedValue = ignoreUnmapped;
		return Self;
	}

	public ParentIdQueryDescriptor Type(string? type)
	{
		TypeValue = type;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		if (BoostValue.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(BoostValue.Value);
		}

		if (IdValue is not null)
		{
			writer.WritePropertyName("id");
			JsonSerializer.Serialize(writer, IdValue, options);
		}

		if (IgnoreUnmappedValue.HasValue)
		{
			writer.WritePropertyName("ignore_unmapped");
			writer.WriteBooleanValue(IgnoreUnmappedValue.Value);
		}

		if (TypeValue is not null)
		{
			writer.WritePropertyName("type");
			JsonSerializer.Serialize(writer, TypeValue, options);
		}

		writer.WriteEndObject();
	}
}