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

/// <summary>
/// <para>A reference to a field with formatting instructions on how to return the value</para>
/// </summary>
public sealed partial class FieldAndFormat
{
	[JsonInclude, JsonPropertyName("field")]
	public Elastic.Clients.Elasticsearch.Field Field { get; set; }
	[JsonInclude, JsonPropertyName("format")]
	public string? Format { get; set; }
	[JsonInclude, JsonPropertyName("include_unmapped")]
	public bool? IncludeUnmapped { get; set; }
}

public sealed partial class FieldAndFormatDescriptor<TDocument> : SerializableDescriptor<FieldAndFormatDescriptor<TDocument>>
{
	internal FieldAndFormatDescriptor(Action<FieldAndFormatDescriptor<TDocument>> configure) => configure.Invoke(this);

	public FieldAndFormatDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private string? FormatValue { get; set; }
	private bool? IncludeUnmappedValue { get; set; }

	public FieldAndFormatDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public FieldAndFormatDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public FieldAndFormatDescriptor<TDocument> Format(string? format)
	{
		FormatValue = format;
		return Self;
	}

	public FieldAndFormatDescriptor<TDocument> IncludeUnmapped(bool? includeUnmapped = true)
	{
		IncludeUnmappedValue = includeUnmapped;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		if (!string.IsNullOrEmpty(FormatValue))
		{
			writer.WritePropertyName("format");
			writer.WriteStringValue(FormatValue);
		}

		if (IncludeUnmappedValue.HasValue)
		{
			writer.WritePropertyName("include_unmapped");
			writer.WriteBooleanValue(IncludeUnmappedValue.Value);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class FieldAndFormatDescriptor : SerializableDescriptor<FieldAndFormatDescriptor>
{
	internal FieldAndFormatDescriptor(Action<FieldAndFormatDescriptor> configure) => configure.Invoke(this);

	public FieldAndFormatDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private string? FormatValue { get; set; }
	private bool? IncludeUnmappedValue { get; set; }

	public FieldAndFormatDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public FieldAndFormatDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public FieldAndFormatDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	public FieldAndFormatDescriptor Format(string? format)
	{
		FormatValue = format;
		return Self;
	}

	public FieldAndFormatDescriptor IncludeUnmapped(bool? includeUnmapped = true)
	{
		IncludeUnmappedValue = includeUnmapped;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		if (!string.IsNullOrEmpty(FormatValue))
		{
			writer.WritePropertyName("format");
			writer.WriteStringValue(FormatValue);
		}

		if (IncludeUnmappedValue.HasValue)
		{
			writer.WritePropertyName("include_unmapped");
			writer.WriteBooleanValue(IncludeUnmappedValue.Value);
		}

		writer.WriteEndObject();
	}
}