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

namespace Elastic.Clients.Elasticsearch.Core.Reindex;

internal sealed partial class SourceConverter : System.Text.Json.Serialization.JsonConverter<Source>
{
	private static readonly System.Text.Json.JsonEncodedText PropIndices = System.Text.Json.JsonEncodedText.Encode("index");
	private static readonly System.Text.Json.JsonEncodedText PropQuery = System.Text.Json.JsonEncodedText.Encode("query");
	private static readonly System.Text.Json.JsonEncodedText PropRemote = System.Text.Json.JsonEncodedText.Encode("remote");
	private static readonly System.Text.Json.JsonEncodedText PropRuntimeMappings = System.Text.Json.JsonEncodedText.Encode("runtime_mappings");
	private static readonly System.Text.Json.JsonEncodedText PropSize = System.Text.Json.JsonEncodedText.Encode("size");
	private static readonly System.Text.Json.JsonEncodedText PropSlice = System.Text.Json.JsonEncodedText.Encode("slice");
	private static readonly System.Text.Json.JsonEncodedText PropSort = System.Text.Json.JsonEncodedText.Encode("sort");
	private static readonly System.Text.Json.JsonEncodedText PropSourceFields = System.Text.Json.JsonEncodedText.Encode("_source");

	public override Source Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Indices> propIndices = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.Query?> propQuery = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSource?> propRemote = default;
		LocalJsonValue<IDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>?> propRuntimeMappings = default;
		LocalJsonValue<int?> propSize = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.SlicedScroll?> propSlice = default;
		LocalJsonValue<ICollection<Elastic.Clients.Elasticsearch.SortOptions>?> propSort = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Fields?> propSourceFields = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propIndices.TryReadProperty(ref reader, options, PropIndices, null))
			{
				continue;
			}

			if (propQuery.TryReadProperty(ref reader, options, PropQuery, null))
			{
				continue;
			}

			if (propRemote.TryReadProperty(ref reader, options, PropRemote, null))
			{
				continue;
			}

			if (propRuntimeMappings.TryReadProperty(ref reader, options, PropRuntimeMappings, static IDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>(o, null, null)))
			{
				continue;
			}

			if (propSize.TryReadProperty(ref reader, options, PropSize, null))
			{
				continue;
			}

			if (propSlice.TryReadProperty(ref reader, options, PropSlice, null))
			{
				continue;
			}

			if (propSort.TryReadProperty(ref reader, options, PropSort, static ICollection<Elastic.Clients.Elasticsearch.SortOptions>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.SortOptions>(o, null)))
			{
				continue;
			}

			if (propSourceFields.TryReadProperty(ref reader, options, PropSourceFields, static Elastic.Clients.Elasticsearch.Fields? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<Elastic.Clients.Elasticsearch.Fields?>(o, typeof(SingleOrManyFieldsMarker))))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Source
		{
			Indices = propIndices.Value
,
			Query = propQuery.Value
,
			Remote = propRemote.Value
,
			RuntimeMappings = propRuntimeMappings.Value
,
			Size = propSize.Value
,
			Slice = propSlice.Value
,
			Sort = propSort.Value
,
			SourceFields = propSourceFields.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Source value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropIndices, value.Indices, null, null);
		writer.WriteProperty(options, PropQuery, value.Query, null, null);
		writer.WriteProperty(options, PropRemote, value.Remote, null, null);
		writer.WriteProperty(options, PropRuntimeMappings, value.RuntimeMappings, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, IDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>? v) => w.WriteDictionaryValue<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>(o, v, null, null));
		writer.WriteProperty(options, PropSize, value.Size, null, null);
		writer.WriteProperty(options, PropSlice, value.Slice, null, null);
		writer.WriteProperty(options, PropSort, value.Sort, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, ICollection<Elastic.Clients.Elasticsearch.SortOptions>? v) => w.WriteSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.SortOptions>(o, v, null));
		writer.WriteProperty(options, PropSourceFields, value.SourceFields, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.Fields? v) => w.WriteValueEx<Elastic.Clients.Elasticsearch.Fields?>(o, v, typeof(SingleOrManyFieldsMarker)));
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(SourceConverter))]
public sealed partial class Source
{
	/// <summary>
	/// <para>
	/// The name of the data stream, index, or alias you are copying from.
	/// Accepts a comma-separated list to reindex from multiple sources.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Indices Indices { get; set; }

	/// <summary>
	/// <para>
	/// Specifies the documents to reindex using the Query DSL.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.Query? Query { get; set; }

	/// <summary>
	/// <para>
	/// A remote instance of Elasticsearch that you want to index from.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSource? Remote { get; set; }
	public IDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>? RuntimeMappings { get; set; }

	/// <summary>
	/// <para>
	/// The number of documents to index per batch.
	/// Use when indexing from remote to ensure that the batches fit within the on-heap buffer, which defaults to a maximum size of 100 MB.
	/// </para>
	/// </summary>
	public int? Size { get; set; }

	/// <summary>
	/// <para>
	/// Slice the reindex request manually using the provided slice ID and total number of slices.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.SlicedScroll? Slice { get; set; }
	public ICollection<Elastic.Clients.Elasticsearch.SortOptions>? Sort { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c> reindexes all source fields.
	/// Set to a list to reindex select fields.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? SourceFields { get; set; }
}

public sealed partial class SourceDescriptor<TDocument> : SerializableDescriptor<SourceDescriptor<TDocument>>
{
	internal SourceDescriptor(Action<SourceDescriptor<TDocument>> configure) => configure.Invoke(this);

	public SourceDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Indices IndicesValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.Query? QueryValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument> QueryDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>> QueryDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSource? RemoteValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor RemoteDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor> RemoteDescriptorAction { get; set; }
	private IDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor<TDocument>> RuntimeMappingsValue { get; set; }
	private int? SizeValue { get; set; }
	private Elastic.Clients.Elasticsearch.SlicedScroll? SliceValue { get; set; }
	private Elastic.Clients.Elasticsearch.SlicedScrollDescriptor<TDocument> SliceDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.SlicedScrollDescriptor<TDocument>> SliceDescriptorAction { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.SortOptions>? SortValue { get; set; }
	private Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument> SortDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument>> SortDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument>>[] SortDescriptorActions { get; set; }
	private Elastic.Clients.Elasticsearch.Fields? SourceFieldsValue { get; set; }

	/// <summary>
	/// <para>
	/// The name of the data stream, index, or alias you are copying from.
	/// Accepts a comma-separated list to reindex from multiple sources.
	/// </para>
	/// </summary>
	public SourceDescriptor<TDocument> Indices(Elastic.Clients.Elasticsearch.Indices indices)
	{
		IndicesValue = indices;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Specifies the documents to reindex using the Query DSL.
	/// </para>
	/// </summary>
	public SourceDescriptor<TDocument> Query(Elastic.Clients.Elasticsearch.QueryDsl.Query? query)
	{
		QueryDescriptor = null;
		QueryDescriptorAction = null;
		QueryValue = query;
		return Self;
	}

	public SourceDescriptor<TDocument> Query(Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument> descriptor)
	{
		QueryValue = null;
		QueryDescriptorAction = null;
		QueryDescriptor = descriptor;
		return Self;
	}

	public SourceDescriptor<TDocument> Query(Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>> configure)
	{
		QueryValue = null;
		QueryDescriptor = null;
		QueryDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// A remote instance of Elasticsearch that you want to index from.
	/// </para>
	/// </summary>
	public SourceDescriptor<TDocument> Remote(Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSource? remote)
	{
		RemoteDescriptor = null;
		RemoteDescriptorAction = null;
		RemoteValue = remote;
		return Self;
	}

	public SourceDescriptor<TDocument> Remote(Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor descriptor)
	{
		RemoteValue = null;
		RemoteDescriptorAction = null;
		RemoteDescriptor = descriptor;
		return Self;
	}

	public SourceDescriptor<TDocument> Remote(Action<Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor> configure)
	{
		RemoteValue = null;
		RemoteDescriptor = null;
		RemoteDescriptorAction = configure;
		return Self;
	}

	public SourceDescriptor<TDocument> RuntimeMappings(Func<FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor<TDocument>>, FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor<TDocument>>> selector)
	{
		RuntimeMappingsValue = selector?.Invoke(new FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor<TDocument>>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// The number of documents to index per batch.
	/// Use when indexing from remote to ensure that the batches fit within the on-heap buffer, which defaults to a maximum size of 100 MB.
	/// </para>
	/// </summary>
	public SourceDescriptor<TDocument> Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Slice the reindex request manually using the provided slice ID and total number of slices.
	/// </para>
	/// </summary>
	public SourceDescriptor<TDocument> Slice(Elastic.Clients.Elasticsearch.SlicedScroll? slice)
	{
		SliceDescriptor = null;
		SliceDescriptorAction = null;
		SliceValue = slice;
		return Self;
	}

	public SourceDescriptor<TDocument> Slice(Elastic.Clients.Elasticsearch.SlicedScrollDescriptor<TDocument> descriptor)
	{
		SliceValue = null;
		SliceDescriptorAction = null;
		SliceDescriptor = descriptor;
		return Self;
	}

	public SourceDescriptor<TDocument> Slice(Action<Elastic.Clients.Elasticsearch.SlicedScrollDescriptor<TDocument>> configure)
	{
		SliceValue = null;
		SliceDescriptor = null;
		SliceDescriptorAction = configure;
		return Self;
	}

	public SourceDescriptor<TDocument> Sort(ICollection<Elastic.Clients.Elasticsearch.SortOptions>? sort)
	{
		SortDescriptor = null;
		SortDescriptorAction = null;
		SortDescriptorActions = null;
		SortValue = sort;
		return Self;
	}

	public SourceDescriptor<TDocument> Sort(Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument> descriptor)
	{
		SortValue = null;
		SortDescriptorAction = null;
		SortDescriptorActions = null;
		SortDescriptor = descriptor;
		return Self;
	}

	public SourceDescriptor<TDocument> Sort(Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument>> configure)
	{
		SortValue = null;
		SortDescriptor = null;
		SortDescriptorActions = null;
		SortDescriptorAction = configure;
		return Self;
	}

	public SourceDescriptor<TDocument> Sort(params Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument>>[] configure)
	{
		SortValue = null;
		SortDescriptor = null;
		SortDescriptorAction = null;
		SortDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> reindexes all source fields.
	/// Set to a list to reindex select fields.
	/// </para>
	/// </summary>
	public SourceDescriptor<TDocument> SourceFields(Elastic.Clients.Elasticsearch.Fields? sourceFields)
	{
		SourceFieldsValue = sourceFields;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("index");
		JsonSerializer.Serialize(writer, IndicesValue, options);
		if (QueryDescriptor is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryDescriptor, options);
		}
		else if (QueryDescriptorAction is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>(QueryDescriptorAction), options);
		}
		else if (QueryValue is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryValue, options);
		}

		if (RemoteDescriptor is not null)
		{
			writer.WritePropertyName("remote");
			JsonSerializer.Serialize(writer, RemoteDescriptor, options);
		}
		else if (RemoteDescriptorAction is not null)
		{
			writer.WritePropertyName("remote");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor(RemoteDescriptorAction), options);
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

		if (SliceDescriptor is not null)
		{
			writer.WritePropertyName("slice");
			JsonSerializer.Serialize(writer, SliceDescriptor, options);
		}
		else if (SliceDescriptorAction is not null)
		{
			writer.WritePropertyName("slice");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.SlicedScrollDescriptor<TDocument>(SliceDescriptorAction), options);
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument>(SortDescriptorAction), options);
		}
		else if (SortDescriptorActions is not null)
		{
			writer.WritePropertyName("sort");
			if (SortDescriptorActions.Length != 1)
				writer.WriteStartArray();
			foreach (var action in SortDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument>(action), options);
			}

			if (SortDescriptorActions.Length != 1)
				writer.WriteEndArray();
		}
		else if (SortValue is not null)
		{
			writer.WritePropertyName("sort");
			SingleOrManySerializationHelper.Serialize<Elastic.Clients.Elasticsearch.SortOptions>(SortValue, writer, options);
		}

		if (SourceFieldsValue is not null)
		{
			writer.WritePropertyName("_source");
			JsonSerializer.Serialize(writer, SourceFieldsValue, options);
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

	private Elastic.Clients.Elasticsearch.Indices IndicesValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.Query? QueryValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor QueryDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor> QueryDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSource? RemoteValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor RemoteDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor> RemoteDescriptorAction { get; set; }
	private IDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor> RuntimeMappingsValue { get; set; }
	private int? SizeValue { get; set; }
	private Elastic.Clients.Elasticsearch.SlicedScroll? SliceValue { get; set; }
	private Elastic.Clients.Elasticsearch.SlicedScrollDescriptor SliceDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.SlicedScrollDescriptor> SliceDescriptorAction { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.SortOptions>? SortValue { get; set; }
	private Elastic.Clients.Elasticsearch.SortOptionsDescriptor SortDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor> SortDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor>[] SortDescriptorActions { get; set; }
	private Elastic.Clients.Elasticsearch.Fields? SourceFieldsValue { get; set; }

	/// <summary>
	/// <para>
	/// The name of the data stream, index, or alias you are copying from.
	/// Accepts a comma-separated list to reindex from multiple sources.
	/// </para>
	/// </summary>
	public SourceDescriptor Indices(Elastic.Clients.Elasticsearch.Indices indices)
	{
		IndicesValue = indices;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Specifies the documents to reindex using the Query DSL.
	/// </para>
	/// </summary>
	public SourceDescriptor Query(Elastic.Clients.Elasticsearch.QueryDsl.Query? query)
	{
		QueryDescriptor = null;
		QueryDescriptorAction = null;
		QueryValue = query;
		return Self;
	}

	public SourceDescriptor Query(Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor descriptor)
	{
		QueryValue = null;
		QueryDescriptorAction = null;
		QueryDescriptor = descriptor;
		return Self;
	}

	public SourceDescriptor Query(Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor> configure)
	{
		QueryValue = null;
		QueryDescriptor = null;
		QueryDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// A remote instance of Elasticsearch that you want to index from.
	/// </para>
	/// </summary>
	public SourceDescriptor Remote(Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSource? remote)
	{
		RemoteDescriptor = null;
		RemoteDescriptorAction = null;
		RemoteValue = remote;
		return Self;
	}

	public SourceDescriptor Remote(Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor descriptor)
	{
		RemoteValue = null;
		RemoteDescriptorAction = null;
		RemoteDescriptor = descriptor;
		return Self;
	}

	public SourceDescriptor Remote(Action<Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor> configure)
	{
		RemoteValue = null;
		RemoteDescriptor = null;
		RemoteDescriptorAction = configure;
		return Self;
	}

	public SourceDescriptor RuntimeMappings(Func<FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor>, FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor>> selector)
	{
		RuntimeMappingsValue = selector?.Invoke(new FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// The number of documents to index per batch.
	/// Use when indexing from remote to ensure that the batches fit within the on-heap buffer, which defaults to a maximum size of 100 MB.
	/// </para>
	/// </summary>
	public SourceDescriptor Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Slice the reindex request manually using the provided slice ID and total number of slices.
	/// </para>
	/// </summary>
	public SourceDescriptor Slice(Elastic.Clients.Elasticsearch.SlicedScroll? slice)
	{
		SliceDescriptor = null;
		SliceDescriptorAction = null;
		SliceValue = slice;
		return Self;
	}

	public SourceDescriptor Slice(Elastic.Clients.Elasticsearch.SlicedScrollDescriptor descriptor)
	{
		SliceValue = null;
		SliceDescriptorAction = null;
		SliceDescriptor = descriptor;
		return Self;
	}

	public SourceDescriptor Slice(Action<Elastic.Clients.Elasticsearch.SlicedScrollDescriptor> configure)
	{
		SliceValue = null;
		SliceDescriptor = null;
		SliceDescriptorAction = configure;
		return Self;
	}

	public SourceDescriptor Sort(ICollection<Elastic.Clients.Elasticsearch.SortOptions>? sort)
	{
		SortDescriptor = null;
		SortDescriptorAction = null;
		SortDescriptorActions = null;
		SortValue = sort;
		return Self;
	}

	public SourceDescriptor Sort(Elastic.Clients.Elasticsearch.SortOptionsDescriptor descriptor)
	{
		SortValue = null;
		SortDescriptorAction = null;
		SortDescriptorActions = null;
		SortDescriptor = descriptor;
		return Self;
	}

	public SourceDescriptor Sort(Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor> configure)
	{
		SortValue = null;
		SortDescriptor = null;
		SortDescriptorActions = null;
		SortDescriptorAction = configure;
		return Self;
	}

	public SourceDescriptor Sort(params Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor>[] configure)
	{
		SortValue = null;
		SortDescriptor = null;
		SortDescriptorAction = null;
		SortDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> reindexes all source fields.
	/// Set to a list to reindex select fields.
	/// </para>
	/// </summary>
	public SourceDescriptor SourceFields(Elastic.Clients.Elasticsearch.Fields? sourceFields)
	{
		SourceFieldsValue = sourceFields;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("index");
		JsonSerializer.Serialize(writer, IndicesValue, options);
		if (QueryDescriptor is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryDescriptor, options);
		}
		else if (QueryDescriptorAction is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor(QueryDescriptorAction), options);
		}
		else if (QueryValue is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryValue, options);
		}

		if (RemoteDescriptor is not null)
		{
			writer.WritePropertyName("remote");
			JsonSerializer.Serialize(writer, RemoteDescriptor, options);
		}
		else if (RemoteDescriptorAction is not null)
		{
			writer.WritePropertyName("remote");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor(RemoteDescriptorAction), options);
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

		if (SliceDescriptor is not null)
		{
			writer.WritePropertyName("slice");
			JsonSerializer.Serialize(writer, SliceDescriptor, options);
		}
		else if (SliceDescriptorAction is not null)
		{
			writer.WritePropertyName("slice");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.SlicedScrollDescriptor(SliceDescriptorAction), options);
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.SortOptionsDescriptor(SortDescriptorAction), options);
		}
		else if (SortDescriptorActions is not null)
		{
			writer.WritePropertyName("sort");
			if (SortDescriptorActions.Length != 1)
				writer.WriteStartArray();
			foreach (var action in SortDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.SortOptionsDescriptor(action), options);
			}

			if (SortDescriptorActions.Length != 1)
				writer.WriteEndArray();
		}
		else if (SortValue is not null)
		{
			writer.WritePropertyName("sort");
			SingleOrManySerializationHelper.Serialize<Elastic.Clients.Elasticsearch.SortOptions>(SortValue, writer, options);
		}

		if (SourceFieldsValue is not null)
		{
			writer.WritePropertyName("_source");
			JsonSerializer.Serialize(writer, SourceFieldsValue, options);
		}

		writer.WriteEndObject();
	}
}