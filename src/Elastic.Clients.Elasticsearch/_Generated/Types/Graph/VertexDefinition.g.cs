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

namespace Elastic.Clients.Elasticsearch.Graph;

public sealed partial class VertexDefinition
{
	/// <summary>
	/// <para>
	/// Prevents the specified terms from being included in the results.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("exclude")]
	public ICollection<string>? Exclude { get; set; }

	/// <summary>
	/// <para>
	/// Identifies a field in the documents of interest.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("field")]
	public Elastic.Clients.Elasticsearch.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// Identifies the terms of interest that form the starting points from which you want to spider out.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("include")]
	public ICollection<Elastic.Clients.Elasticsearch.Graph.VertexInclude>? Include { get; set; }

	/// <summary>
	/// <para>
	/// Specifies how many documents must contain a pair of terms before it is considered to be a useful connection.
	/// This setting acts as a certainty threshold.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("min_doc_count")]
	public long? MinDocCount { get; set; }

	/// <summary>
	/// <para>
	/// Controls how many documents on a particular shard have to contain a pair of terms before the connection is returned for global consideration.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("shard_min_doc_count")]
	public long? ShardMinDocCount { get; set; }

	/// <summary>
	/// <para>
	/// Specifies the maximum number of vertex terms returned for each field.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("size")]
	public int? Size { get; set; }
}

public sealed partial class VertexDefinitionDescriptor<TDocument> : SerializableDescriptor<VertexDefinitionDescriptor<TDocument>>
{
	internal VertexDefinitionDescriptor(Action<VertexDefinitionDescriptor<TDocument>> configure) => configure.Invoke(this);

	public VertexDefinitionDescriptor() : base()
	{
	}

	private ICollection<string>? ExcludeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Graph.VertexInclude>? IncludeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Graph.VertexIncludeDescriptor IncludeDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Graph.VertexIncludeDescriptor> IncludeDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Graph.VertexIncludeDescriptor>[] IncludeDescriptorActions { get; set; }
	private long? MinDocCountValue { get; set; }
	private long? ShardMinDocCountValue { get; set; }
	private int? SizeValue { get; set; }

	/// <summary>
	/// <para>
	/// Prevents the specified terms from being included in the results.
	/// </para>
	/// </summary>
	public VertexDefinitionDescriptor<TDocument> Exclude(ICollection<string>? exclude)
	{
		ExcludeValue = exclude;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Identifies a field in the documents of interest.
	/// </para>
	/// </summary>
	public VertexDefinitionDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Identifies a field in the documents of interest.
	/// </para>
	/// </summary>
	public VertexDefinitionDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Identifies a field in the documents of interest.
	/// </para>
	/// </summary>
	public VertexDefinitionDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Identifies the terms of interest that form the starting points from which you want to spider out.
	/// </para>
	/// </summary>
	public VertexDefinitionDescriptor<TDocument> Include(ICollection<Elastic.Clients.Elasticsearch.Graph.VertexInclude>? include)
	{
		IncludeDescriptor = null;
		IncludeDescriptorAction = null;
		IncludeDescriptorActions = null;
		IncludeValue = include;
		return Self;
	}

	public VertexDefinitionDescriptor<TDocument> Include(Elastic.Clients.Elasticsearch.Graph.VertexIncludeDescriptor descriptor)
	{
		IncludeValue = null;
		IncludeDescriptorAction = null;
		IncludeDescriptorActions = null;
		IncludeDescriptor = descriptor;
		return Self;
	}

	public VertexDefinitionDescriptor<TDocument> Include(Action<Elastic.Clients.Elasticsearch.Graph.VertexIncludeDescriptor> configure)
	{
		IncludeValue = null;
		IncludeDescriptor = null;
		IncludeDescriptorActions = null;
		IncludeDescriptorAction = configure;
		return Self;
	}

	public VertexDefinitionDescriptor<TDocument> Include(params Action<Elastic.Clients.Elasticsearch.Graph.VertexIncludeDescriptor>[] configure)
	{
		IncludeValue = null;
		IncludeDescriptor = null;
		IncludeDescriptorAction = null;
		IncludeDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Specifies how many documents must contain a pair of terms before it is considered to be a useful connection.
	/// This setting acts as a certainty threshold.
	/// </para>
	/// </summary>
	public VertexDefinitionDescriptor<TDocument> MinDocCount(long? minDocCount)
	{
		MinDocCountValue = minDocCount;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Controls how many documents on a particular shard have to contain a pair of terms before the connection is returned for global consideration.
	/// </para>
	/// </summary>
	public VertexDefinitionDescriptor<TDocument> ShardMinDocCount(long? shardMinDocCount)
	{
		ShardMinDocCountValue = shardMinDocCount;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Specifies the maximum number of vertex terms returned for each field.
	/// </para>
	/// </summary>
	public VertexDefinitionDescriptor<TDocument> Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (ExcludeValue is not null)
		{
			writer.WritePropertyName("exclude");
			JsonSerializer.Serialize(writer, ExcludeValue, options);
		}

		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		if (IncludeDescriptor is not null)
		{
			writer.WritePropertyName("include");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, IncludeDescriptor, options);
			writer.WriteEndArray();
		}
		else if (IncludeDescriptorAction is not null)
		{
			writer.WritePropertyName("include");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Graph.VertexIncludeDescriptor(IncludeDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (IncludeDescriptorActions is not null)
		{
			writer.WritePropertyName("include");
			writer.WriteStartArray();
			foreach (var action in IncludeDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Graph.VertexIncludeDescriptor(action), options);
			}

			writer.WriteEndArray();
		}
		else if (IncludeValue is not null)
		{
			writer.WritePropertyName("include");
			JsonSerializer.Serialize(writer, IncludeValue, options);
		}

		if (MinDocCountValue.HasValue)
		{
			writer.WritePropertyName("min_doc_count");
			writer.WriteNumberValue(MinDocCountValue.Value);
		}

		if (ShardMinDocCountValue.HasValue)
		{
			writer.WritePropertyName("shard_min_doc_count");
			writer.WriteNumberValue(ShardMinDocCountValue.Value);
		}

		if (SizeValue.HasValue)
		{
			writer.WritePropertyName("size");
			writer.WriteNumberValue(SizeValue.Value);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class VertexDefinitionDescriptor : SerializableDescriptor<VertexDefinitionDescriptor>
{
	internal VertexDefinitionDescriptor(Action<VertexDefinitionDescriptor> configure) => configure.Invoke(this);

	public VertexDefinitionDescriptor() : base()
	{
	}

	private ICollection<string>? ExcludeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Graph.VertexInclude>? IncludeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Graph.VertexIncludeDescriptor IncludeDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Graph.VertexIncludeDescriptor> IncludeDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Graph.VertexIncludeDescriptor>[] IncludeDescriptorActions { get; set; }
	private long? MinDocCountValue { get; set; }
	private long? ShardMinDocCountValue { get; set; }
	private int? SizeValue { get; set; }

	/// <summary>
	/// <para>
	/// Prevents the specified terms from being included in the results.
	/// </para>
	/// </summary>
	public VertexDefinitionDescriptor Exclude(ICollection<string>? exclude)
	{
		ExcludeValue = exclude;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Identifies a field in the documents of interest.
	/// </para>
	/// </summary>
	public VertexDefinitionDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Identifies a field in the documents of interest.
	/// </para>
	/// </summary>
	public VertexDefinitionDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Identifies a field in the documents of interest.
	/// </para>
	/// </summary>
	public VertexDefinitionDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Identifies the terms of interest that form the starting points from which you want to spider out.
	/// </para>
	/// </summary>
	public VertexDefinitionDescriptor Include(ICollection<Elastic.Clients.Elasticsearch.Graph.VertexInclude>? include)
	{
		IncludeDescriptor = null;
		IncludeDescriptorAction = null;
		IncludeDescriptorActions = null;
		IncludeValue = include;
		return Self;
	}

	public VertexDefinitionDescriptor Include(Elastic.Clients.Elasticsearch.Graph.VertexIncludeDescriptor descriptor)
	{
		IncludeValue = null;
		IncludeDescriptorAction = null;
		IncludeDescriptorActions = null;
		IncludeDescriptor = descriptor;
		return Self;
	}

	public VertexDefinitionDescriptor Include(Action<Elastic.Clients.Elasticsearch.Graph.VertexIncludeDescriptor> configure)
	{
		IncludeValue = null;
		IncludeDescriptor = null;
		IncludeDescriptorActions = null;
		IncludeDescriptorAction = configure;
		return Self;
	}

	public VertexDefinitionDescriptor Include(params Action<Elastic.Clients.Elasticsearch.Graph.VertexIncludeDescriptor>[] configure)
	{
		IncludeValue = null;
		IncludeDescriptor = null;
		IncludeDescriptorAction = null;
		IncludeDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Specifies how many documents must contain a pair of terms before it is considered to be a useful connection.
	/// This setting acts as a certainty threshold.
	/// </para>
	/// </summary>
	public VertexDefinitionDescriptor MinDocCount(long? minDocCount)
	{
		MinDocCountValue = minDocCount;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Controls how many documents on a particular shard have to contain a pair of terms before the connection is returned for global consideration.
	/// </para>
	/// </summary>
	public VertexDefinitionDescriptor ShardMinDocCount(long? shardMinDocCount)
	{
		ShardMinDocCountValue = shardMinDocCount;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Specifies the maximum number of vertex terms returned for each field.
	/// </para>
	/// </summary>
	public VertexDefinitionDescriptor Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (ExcludeValue is not null)
		{
			writer.WritePropertyName("exclude");
			JsonSerializer.Serialize(writer, ExcludeValue, options);
		}

		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		if (IncludeDescriptor is not null)
		{
			writer.WritePropertyName("include");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, IncludeDescriptor, options);
			writer.WriteEndArray();
		}
		else if (IncludeDescriptorAction is not null)
		{
			writer.WritePropertyName("include");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Graph.VertexIncludeDescriptor(IncludeDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (IncludeDescriptorActions is not null)
		{
			writer.WritePropertyName("include");
			writer.WriteStartArray();
			foreach (var action in IncludeDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Graph.VertexIncludeDescriptor(action), options);
			}

			writer.WriteEndArray();
		}
		else if (IncludeValue is not null)
		{
			writer.WritePropertyName("include");
			JsonSerializer.Serialize(writer, IncludeValue, options);
		}

		if (MinDocCountValue.HasValue)
		{
			writer.WritePropertyName("min_doc_count");
			writer.WriteNumberValue(MinDocCountValue.Value);
		}

		if (ShardMinDocCountValue.HasValue)
		{
			writer.WritePropertyName("shard_min_doc_count");
			writer.WriteNumberValue(ShardMinDocCountValue.Value);
		}

		if (SizeValue.HasValue)
		{
			writer.WritePropertyName("size");
			writer.WriteNumberValue(SizeValue.Value);
		}

		writer.WriteEndObject();
	}
}