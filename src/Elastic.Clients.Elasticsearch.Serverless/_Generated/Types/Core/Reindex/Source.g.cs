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

namespace Elastic.Clients.Elasticsearch.Serverless.Core.Reindex;

public sealed partial class Source
{
	/// <summary>
	/// <para>If `true` reindexes all source fields.<br/>Set to a list to reindex select fields.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("_source")]
	public Elastic.Clients.Elasticsearch.Serverless.Fields? SourceFields { get; set; }

	/// <summary>
	/// <para>The name of the data stream, index, or alias you are copying from.<br/>Accepts a comma-separated list to reindex from multiple sources.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("index")]
	public Elastic.Clients.Elasticsearch.Serverless.Indices Index { get; set; }

	/// <summary>
	/// <para>Specifies the documents to reindex using the Query DSL.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("query")]
	public Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query? Query { get; set; }

	/// <summary>
	/// <para>A remote instance of Elasticsearch that you want to index from.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("remote")]
	public Elastic.Clients.Elasticsearch.Serverless.Core.Reindex.RemoteSource? Remote { get; set; }
	[JsonInclude, JsonPropertyName("runtime_mappings")]
	public IDictionary<Elastic.Clients.Elasticsearch.Serverless.Field, Elastic.Clients.Elasticsearch.Serverless.Mapping.RuntimeField>? RuntimeMappings { get; set; }

	/// <summary>
	/// <para>The number of documents to index per batch.<br/>Use when indexing from remote to ensure that the batches fit within the on-heap buffer, which defaults to a maximum size of 100 MB.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("size")]
	public int? Size { get; set; }

	/// <summary>
	/// <para>Slice the reindex request manually using the provided slice ID and total number of slices.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("slice")]
	public Elastic.Clients.Elasticsearch.Serverless.SlicedScroll? Slice { get; set; }
	[JsonInclude, JsonPropertyName("sort"), SingleOrManyCollectionConverter(typeof(Elastic.Clients.Elasticsearch.Serverless.SortOptions))]
	public ICollection<Elastic.Clients.Elasticsearch.Serverless.SortOptions>? Sort { get; set; }
}

public sealed partial class SourceDescriptor<TDocument> : SerializableDescriptor<SourceDescriptor<TDocument>>
{
	internal SourceDescriptor(Action<SourceDescriptor<TDocument>> configure) => configure.Invoke(this);

	public SourceDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query? QueryValue { get; set; }
	private QueryDsl.QueryDescriptor<TDocument> QueryDescriptor { get; set; }
	private Action<QueryDsl.QueryDescriptor<TDocument>> QueryDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.SlicedScroll? SliceValue { get; set; }
	private SlicedScrollDescriptor<TDocument> SliceDescriptor { get; set; }
	private Action<SlicedScrollDescriptor<TDocument>> SliceDescriptorAction { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Serverless.SortOptions>? SortValue { get; set; }
	private SortOptionsDescriptor<TDocument> SortDescriptor { get; set; }
	private Action<SortOptionsDescriptor<TDocument>> SortDescriptorAction { get; set; }
	private Action<SortOptionsDescriptor<TDocument>>[] SortDescriptorActions { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Fields? SourceFieldsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Indices IndexValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Core.Reindex.RemoteSource? RemoteValue { get; set; }
	private RemoteSourceDescriptor RemoteDescriptor { get; set; }
	private Action<RemoteSourceDescriptor> RemoteDescriptorAction { get; set; }
	private IDictionary<Elastic.Clients.Elasticsearch.Serverless.Field, Elastic.Clients.Elasticsearch.Serverless.Mapping.RuntimeField>? RuntimeMappingsValue { get; set; }
	private int? SizeValue { get; set; }

	/// <summary>
	/// <para>Specifies the documents to reindex using the Query DSL.</para>
	/// </summary>
	public SourceDescriptor<TDocument> Query(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query? query)
	{
		QueryDescriptor = null;
		QueryDescriptorAction = null;
		QueryValue = query;
		return Self;
	}

	public SourceDescriptor<TDocument> Query(QueryDsl.QueryDescriptor<TDocument> descriptor)
	{
		QueryValue = null;
		QueryDescriptorAction = null;
		QueryDescriptor = descriptor;
		return Self;
	}

	public SourceDescriptor<TDocument> Query(Action<QueryDsl.QueryDescriptor<TDocument>> configure)
	{
		QueryValue = null;
		QueryDescriptor = null;
		QueryDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>Slice the reindex request manually using the provided slice ID and total number of slices.</para>
	/// </summary>
	public SourceDescriptor<TDocument> Slice(Elastic.Clients.Elasticsearch.Serverless.SlicedScroll? slice)
	{
		SliceDescriptor = null;
		SliceDescriptorAction = null;
		SliceValue = slice;
		return Self;
	}

	public SourceDescriptor<TDocument> Slice(SlicedScrollDescriptor<TDocument> descriptor)
	{
		SliceValue = null;
		SliceDescriptorAction = null;
		SliceDescriptor = descriptor;
		return Self;
	}

	public SourceDescriptor<TDocument> Slice(Action<SlicedScrollDescriptor<TDocument>> configure)
	{
		SliceValue = null;
		SliceDescriptor = null;
		SliceDescriptorAction = configure;
		return Self;
	}

	public SourceDescriptor<TDocument> Sort(ICollection<Elastic.Clients.Elasticsearch.Serverless.SortOptions>? sort)
	{
		SortDescriptor = null;
		SortDescriptorAction = null;
		SortDescriptorActions = null;
		SortValue = sort;
		return Self;
	}

	public SourceDescriptor<TDocument> Sort(SortOptionsDescriptor<TDocument> descriptor)
	{
		SortValue = null;
		SortDescriptorAction = null;
		SortDescriptorActions = null;
		SortDescriptor = descriptor;
		return Self;
	}

	public SourceDescriptor<TDocument> Sort(Action<SortOptionsDescriptor<TDocument>> configure)
	{
		SortValue = null;
		SortDescriptor = null;
		SortDescriptorActions = null;
		SortDescriptorAction = configure;
		return Self;
	}

	public SourceDescriptor<TDocument> Sort(params Action<SortOptionsDescriptor<TDocument>>[] configure)
	{
		SortValue = null;
		SortDescriptor = null;
		SortDescriptorAction = null;
		SortDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>If `true` reindexes all source fields.<br/>Set to a list to reindex select fields.</para>
	/// </summary>
	public SourceDescriptor<TDocument> SourceFields(Elastic.Clients.Elasticsearch.Serverless.Fields? sourceFields)
	{
		SourceFieldsValue = sourceFields;
		return Self;
	}

	/// <summary>
	/// <para>The name of the data stream, index, or alias you are copying from.<br/>Accepts a comma-separated list to reindex from multiple sources.</para>
	/// </summary>
	public SourceDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.Serverless.Indices index)
	{
		IndexValue = index;
		return Self;
	}

	/// <summary>
	/// <para>A remote instance of Elasticsearch that you want to index from.</para>
	/// </summary>
	public SourceDescriptor<TDocument> Remote(Elastic.Clients.Elasticsearch.Serverless.Core.Reindex.RemoteSource? remote)
	{
		RemoteDescriptor = null;
		RemoteDescriptorAction = null;
		RemoteValue = remote;
		return Self;
	}

	public SourceDescriptor<TDocument> Remote(RemoteSourceDescriptor descriptor)
	{
		RemoteValue = null;
		RemoteDescriptorAction = null;
		RemoteDescriptor = descriptor;
		return Self;
	}

	public SourceDescriptor<TDocument> Remote(Action<RemoteSourceDescriptor> configure)
	{
		RemoteValue = null;
		RemoteDescriptor = null;
		RemoteDescriptorAction = configure;
		return Self;
	}

	public SourceDescriptor<TDocument> RuntimeMappings(Func<FluentDictionary<Elastic.Clients.Elasticsearch.Serverless.Field, Elastic.Clients.Elasticsearch.Serverless.Mapping.RuntimeField>, FluentDictionary<Elastic.Clients.Elasticsearch.Serverless.Field, Elastic.Clients.Elasticsearch.Serverless.Mapping.RuntimeField>> selector)
	{
		RuntimeMappingsValue = selector?.Invoke(new FluentDictionary<Elastic.Clients.Elasticsearch.Serverless.Field, Elastic.Clients.Elasticsearch.Serverless.Mapping.RuntimeField>());
		return Self;
	}

	/// <summary>
	/// <para>The number of documents to index per batch.<br/>Use when indexing from remote to ensure that the batches fit within the on-heap buffer, which defaults to a maximum size of 100 MB.</para>
	/// </summary>
	public SourceDescriptor<TDocument> Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (QueryDescriptor is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryDescriptor, options);
		}
		else if (QueryDescriptorAction is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, new QueryDsl.QueryDescriptor<TDocument>(QueryDescriptorAction), options);
		}
		else if (QueryValue is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryValue, options);
		}

		if (SliceDescriptor is not null)
		{
			writer.WritePropertyName("slice");
			JsonSerializer.Serialize(writer, SliceDescriptor, options);
		}
		else if (SliceDescriptorAction is not null)
		{
			writer.WritePropertyName("slice");
			JsonSerializer.Serialize(writer, new SlicedScrollDescriptor<TDocument>(SliceDescriptorAction), options);
		}
		else if (SliceValue is not null)
		{
			writer.WritePropertyName("slice");
			JsonSerializer.Serialize(writer, SliceValue, options);
		}

		if (SortDescriptor is not null)
		{
			writer.WritePropertyName("sort");
			JsonSerializer.Serialize(writer, SortDescriptor, options);
		}
		else if (SortDescriptorAction is not null)
		{
			writer.WritePropertyName("sort");
			JsonSerializer.Serialize(writer, new SortOptionsDescriptor<TDocument>(SortDescriptorAction), options);
		}
		else if (SortDescriptorActions is not null)
		{
			writer.WritePropertyName("sort");
			if (SortDescriptorActions.Length > 1)
				writer.WriteStartArray();
			foreach (var action in SortDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new SortOptionsDescriptor<TDocument>(action), options);
			}

			if (SortDescriptorActions.Length > 1)
				writer.WriteEndArray();
		}
		else if (SortValue is not null)
		{
			writer.WritePropertyName("sort");
			SingleOrManySerializationHelper.Serialize<Elastic.Clients.Elasticsearch.Serverless.SortOptions>(SortValue, writer, options);
		}

		if (SourceFieldsValue is not null)
		{
			writer.WritePropertyName("_source");
			JsonSerializer.Serialize(writer, SourceFieldsValue, options);
		}

		writer.WritePropertyName("index");
		JsonSerializer.Serialize(writer, IndexValue, options);
		if (RemoteDescriptor is not null)
		{
			writer.WritePropertyName("remote");
			JsonSerializer.Serialize(writer, RemoteDescriptor, options);
		}
		else if (RemoteDescriptorAction is not null)
		{
			writer.WritePropertyName("remote");
			JsonSerializer.Serialize(writer, new RemoteSourceDescriptor(RemoteDescriptorAction), options);
		}
		else if (RemoteValue is not null)
		{
			writer.WritePropertyName("remote");
			JsonSerializer.Serialize(writer, RemoteValue, options);
		}

		if (RuntimeMappingsValue is not null)
		{
			writer.WritePropertyName("runtime_mappings");
			JsonSerializer.Serialize(writer, RuntimeMappingsValue, options);
		}

		if (SizeValue.HasValue)
		{
			writer.WritePropertyName("size");
			writer.WriteNumberValue(SizeValue.Value);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class SourceDescriptor : SerializableDescriptor<SourceDescriptor>
{
	internal SourceDescriptor(Action<SourceDescriptor> configure) => configure.Invoke(this);

	public SourceDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query? QueryValue { get; set; }
	private QueryDsl.QueryDescriptor QueryDescriptor { get; set; }
	private Action<QueryDsl.QueryDescriptor> QueryDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.SlicedScroll? SliceValue { get; set; }
	private SlicedScrollDescriptor SliceDescriptor { get; set; }
	private Action<SlicedScrollDescriptor> SliceDescriptorAction { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Serverless.SortOptions>? SortValue { get; set; }
	private SortOptionsDescriptor SortDescriptor { get; set; }
	private Action<SortOptionsDescriptor> SortDescriptorAction { get; set; }
	private Action<SortOptionsDescriptor>[] SortDescriptorActions { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Fields? SourceFieldsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Indices IndexValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Core.Reindex.RemoteSource? RemoteValue { get; set; }
	private RemoteSourceDescriptor RemoteDescriptor { get; set; }
	private Action<RemoteSourceDescriptor> RemoteDescriptorAction { get; set; }
	private IDictionary<Elastic.Clients.Elasticsearch.Serverless.Field, Elastic.Clients.Elasticsearch.Serverless.Mapping.RuntimeField>? RuntimeMappingsValue { get; set; }
	private int? SizeValue { get; set; }

	/// <summary>
	/// <para>Specifies the documents to reindex using the Query DSL.</para>
	/// </summary>
	public SourceDescriptor Query(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query? query)
	{
		QueryDescriptor = null;
		QueryDescriptorAction = null;
		QueryValue = query;
		return Self;
	}

	public SourceDescriptor Query(QueryDsl.QueryDescriptor descriptor)
	{
		QueryValue = null;
		QueryDescriptorAction = null;
		QueryDescriptor = descriptor;
		return Self;
	}

	public SourceDescriptor Query(Action<QueryDsl.QueryDescriptor> configure)
	{
		QueryValue = null;
		QueryDescriptor = null;
		QueryDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>Slice the reindex request manually using the provided slice ID and total number of slices.</para>
	/// </summary>
	public SourceDescriptor Slice(Elastic.Clients.Elasticsearch.Serverless.SlicedScroll? slice)
	{
		SliceDescriptor = null;
		SliceDescriptorAction = null;
		SliceValue = slice;
		return Self;
	}

	public SourceDescriptor Slice(SlicedScrollDescriptor descriptor)
	{
		SliceValue = null;
		SliceDescriptorAction = null;
		SliceDescriptor = descriptor;
		return Self;
	}

	public SourceDescriptor Slice(Action<SlicedScrollDescriptor> configure)
	{
		SliceValue = null;
		SliceDescriptor = null;
		SliceDescriptorAction = configure;
		return Self;
	}

	public SourceDescriptor Sort(ICollection<Elastic.Clients.Elasticsearch.Serverless.SortOptions>? sort)
	{
		SortDescriptor = null;
		SortDescriptorAction = null;
		SortDescriptorActions = null;
		SortValue = sort;
		return Self;
	}

	public SourceDescriptor Sort(SortOptionsDescriptor descriptor)
	{
		SortValue = null;
		SortDescriptorAction = null;
		SortDescriptorActions = null;
		SortDescriptor = descriptor;
		return Self;
	}

	public SourceDescriptor Sort(Action<SortOptionsDescriptor> configure)
	{
		SortValue = null;
		SortDescriptor = null;
		SortDescriptorActions = null;
		SortDescriptorAction = configure;
		return Self;
	}

	public SourceDescriptor Sort(params Action<SortOptionsDescriptor>[] configure)
	{
		SortValue = null;
		SortDescriptor = null;
		SortDescriptorAction = null;
		SortDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>If `true` reindexes all source fields.<br/>Set to a list to reindex select fields.</para>
	/// </summary>
	public SourceDescriptor SourceFields(Elastic.Clients.Elasticsearch.Serverless.Fields? sourceFields)
	{
		SourceFieldsValue = sourceFields;
		return Self;
	}

	/// <summary>
	/// <para>The name of the data stream, index, or alias you are copying from.<br/>Accepts a comma-separated list to reindex from multiple sources.</para>
	/// </summary>
	public SourceDescriptor Index(Elastic.Clients.Elasticsearch.Serverless.Indices index)
	{
		IndexValue = index;
		return Self;
	}

	/// <summary>
	/// <para>A remote instance of Elasticsearch that you want to index from.</para>
	/// </summary>
	public SourceDescriptor Remote(Elastic.Clients.Elasticsearch.Serverless.Core.Reindex.RemoteSource? remote)
	{
		RemoteDescriptor = null;
		RemoteDescriptorAction = null;
		RemoteValue = remote;
		return Self;
	}

	public SourceDescriptor Remote(RemoteSourceDescriptor descriptor)
	{
		RemoteValue = null;
		RemoteDescriptorAction = null;
		RemoteDescriptor = descriptor;
		return Self;
	}

	public SourceDescriptor Remote(Action<RemoteSourceDescriptor> configure)
	{
		RemoteValue = null;
		RemoteDescriptor = null;
		RemoteDescriptorAction = configure;
		return Self;
	}

	public SourceDescriptor RuntimeMappings(Func<FluentDictionary<Elastic.Clients.Elasticsearch.Serverless.Field, Elastic.Clients.Elasticsearch.Serverless.Mapping.RuntimeField>, FluentDictionary<Elastic.Clients.Elasticsearch.Serverless.Field, Elastic.Clients.Elasticsearch.Serverless.Mapping.RuntimeField>> selector)
	{
		RuntimeMappingsValue = selector?.Invoke(new FluentDictionary<Elastic.Clients.Elasticsearch.Serverless.Field, Elastic.Clients.Elasticsearch.Serverless.Mapping.RuntimeField>());
		return Self;
	}

	/// <summary>
	/// <para>The number of documents to index per batch.<br/>Use when indexing from remote to ensure that the batches fit within the on-heap buffer, which defaults to a maximum size of 100 MB.</para>
	/// </summary>
	public SourceDescriptor Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (QueryDescriptor is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryDescriptor, options);
		}
		else if (QueryDescriptorAction is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, new QueryDsl.QueryDescriptor(QueryDescriptorAction), options);
		}
		else if (QueryValue is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryValue, options);
		}

		if (SliceDescriptor is not null)
		{
			writer.WritePropertyName("slice");
			JsonSerializer.Serialize(writer, SliceDescriptor, options);
		}
		else if (SliceDescriptorAction is not null)
		{
			writer.WritePropertyName("slice");
			JsonSerializer.Serialize(writer, new SlicedScrollDescriptor(SliceDescriptorAction), options);
		}
		else if (SliceValue is not null)
		{
			writer.WritePropertyName("slice");
			JsonSerializer.Serialize(writer, SliceValue, options);
		}

		if (SortDescriptor is not null)
		{
			writer.WritePropertyName("sort");
			JsonSerializer.Serialize(writer, SortDescriptor, options);
		}
		else if (SortDescriptorAction is not null)
		{
			writer.WritePropertyName("sort");
			JsonSerializer.Serialize(writer, new SortOptionsDescriptor(SortDescriptorAction), options);
		}
		else if (SortDescriptorActions is not null)
		{
			writer.WritePropertyName("sort");
			if (SortDescriptorActions.Length > 1)
				writer.WriteStartArray();
			foreach (var action in SortDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new SortOptionsDescriptor(action), options);
			}

			if (SortDescriptorActions.Length > 1)
				writer.WriteEndArray();
		}
		else if (SortValue is not null)
		{
			writer.WritePropertyName("sort");
			SingleOrManySerializationHelper.Serialize<Elastic.Clients.Elasticsearch.Serverless.SortOptions>(SortValue, writer, options);
		}

		if (SourceFieldsValue is not null)
		{
			writer.WritePropertyName("_source");
			JsonSerializer.Serialize(writer, SourceFieldsValue, options);
		}

		writer.WritePropertyName("index");
		JsonSerializer.Serialize(writer, IndexValue, options);
		if (RemoteDescriptor is not null)
		{
			writer.WritePropertyName("remote");
			JsonSerializer.Serialize(writer, RemoteDescriptor, options);
		}
		else if (RemoteDescriptorAction is not null)
		{
			writer.WritePropertyName("remote");
			JsonSerializer.Serialize(writer, new RemoteSourceDescriptor(RemoteDescriptorAction), options);
		}
		else if (RemoteValue is not null)
		{
			writer.WritePropertyName("remote");
			JsonSerializer.Serialize(writer, RemoteValue, options);
		}

		if (RuntimeMappingsValue is not null)
		{
			writer.WritePropertyName("runtime_mappings");
			JsonSerializer.Serialize(writer, RuntimeMappingsValue, options);
		}

		if (SizeValue.HasValue)
		{
			writer.WritePropertyName("size");
			writer.WriteNumberValue(SizeValue.Value);
		}

		writer.WriteEndObject();
	}
}