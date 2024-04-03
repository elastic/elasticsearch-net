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

namespace Elastic.Clients.Elasticsearch.Serverless.Mapping;

public sealed partial class DenseVectorIndexOptions
{
	[JsonInclude, JsonPropertyName("ef_construction")]
	public int EfConstruction { get; set; }
	[JsonInclude, JsonPropertyName("m")]
	public int m { get; set; }
	[JsonInclude, JsonPropertyName("type")]
	public string Type { get; set; }
}

public sealed partial class DenseVectorIndexOptionsDescriptor : SerializableDescriptor<DenseVectorIndexOptionsDescriptor>
{
	internal DenseVectorIndexOptionsDescriptor(Action<DenseVectorIndexOptionsDescriptor> configure) => configure.Invoke(this);

	public DenseVectorIndexOptionsDescriptor() : base()
	{
	}

	private int EfConstructionValue { get; set; }
	private int mValue { get; set; }
	private string TypeValue { get; set; }

	public DenseVectorIndexOptionsDescriptor EfConstruction(int efConstruction)
	{
		EfConstructionValue = efConstruction;
		return Self;
	}

	public DenseVectorIndexOptionsDescriptor m(int m)
	{
		mValue = m;
		return Self;
	}

	public DenseVectorIndexOptionsDescriptor Type(string type)
	{
		TypeValue = type;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("ef_construction");
		writer.WriteNumberValue(EfConstructionValue);
		writer.WritePropertyName("m");
		writer.WriteNumberValue(mValue);
		writer.WritePropertyName("type");
		writer.WriteStringValue(TypeValue);
		writer.WriteEndObject();
	}
}