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

namespace Elastic.Clients.Elasticsearch.Rollup;

public sealed partial class TermsGrouping
{
	/// <summary>
	/// <para>
	/// The set of fields that you wish to collect terms for.
	/// This array can contain fields that are both keyword and numerics.
	/// Order does not matter.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("fields")]
	[JsonConverter(typeof(SingleOrManyFieldsConverter))]
	public Elastic.Clients.Elasticsearch.Fields Fields { get; set; }
}

public sealed partial class TermsGroupingDescriptor<TDocument> : SerializableDescriptor<TermsGroupingDescriptor<TDocument>>
{
	internal TermsGroupingDescriptor(Action<TermsGroupingDescriptor<TDocument>> configure) => configure.Invoke(this);

	public TermsGroupingDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Fields FieldsValue { get; set; }

	/// <summary>
	/// <para>
	/// The set of fields that you wish to collect terms for.
	/// This array can contain fields that are both keyword and numerics.
	/// Order does not matter.
	/// </para>
	/// </summary>
	public TermsGroupingDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Fields fields)
	{
		FieldsValue = fields;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("fields");
		JsonSerializer.Serialize(writer, FieldsValue, options);
		writer.WriteEndObject();
	}
}

public sealed partial class TermsGroupingDescriptor : SerializableDescriptor<TermsGroupingDescriptor>
{
	internal TermsGroupingDescriptor(Action<TermsGroupingDescriptor> configure) => configure.Invoke(this);

	public TermsGroupingDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Fields FieldsValue { get; set; }

	/// <summary>
	/// <para>
	/// The set of fields that you wish to collect terms for.
	/// This array can contain fields that are both keyword and numerics.
	/// Order does not matter.
	/// </para>
	/// </summary>
	public TermsGroupingDescriptor Fields(Elastic.Clients.Elasticsearch.Fields fields)
	{
		FieldsValue = fields;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("fields");
		JsonSerializer.Serialize(writer, FieldsValue, options);
		writer.WriteEndObject();
	}
}