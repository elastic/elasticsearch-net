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

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Core.Reindex;

internal sealed partial class SourceConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.Reindex.Source>
{
	private static readonly System.Text.Json.JsonEncodedText PropIndices = System.Text.Json.JsonEncodedText.Encode("index");
	private static readonly System.Text.Json.JsonEncodedText PropQuery = System.Text.Json.JsonEncodedText.Encode("query");
	private static readonly System.Text.Json.JsonEncodedText PropRemote = System.Text.Json.JsonEncodedText.Encode("remote");
	private static readonly System.Text.Json.JsonEncodedText PropRuntimeMappings = System.Text.Json.JsonEncodedText.Encode("runtime_mappings");
	private static readonly System.Text.Json.JsonEncodedText PropSize = System.Text.Json.JsonEncodedText.Encode("size");
	private static readonly System.Text.Json.JsonEncodedText PropSlice = System.Text.Json.JsonEncodedText.Encode("slice");
	private static readonly System.Text.Json.JsonEncodedText PropSourceFields = System.Text.Json.JsonEncodedText.Encode("_source");

	public override Elastic.Clients.Elasticsearch.Core.Reindex.Source Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Indices> propIndices = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.Query?> propQuery = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSource?> propRemote = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>?> propRuntimeMappings = default;
		LocalJsonValue<int?> propSize = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.SlicedScroll?> propSlice = default;
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

			if (propRuntimeMappings.TryReadProperty(ref reader, options, PropRuntimeMappings, static System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>(o, null, null)))
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

			if (propSourceFields.TryReadProperty(ref reader, options, PropSourceFields, static Elastic.Clients.Elasticsearch.Fields? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<Elastic.Clients.Elasticsearch.Fields?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.SingleOrManyFieldsMarker))))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Core.Reindex.Source(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Indices = propIndices.Value,
			Query = propQuery.Value,
			Remote = propRemote.Value,
			RuntimeMappings = propRuntimeMappings.Value,
			Size = propSize.Value,
			Slice = propSlice.Value,
			SourceFields = propSourceFields.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.Reindex.Source value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropIndices, value.Indices, null, null);
		writer.WriteProperty(options, PropQuery, value.Query, null, null);
		writer.WriteProperty(options, PropRemote, value.Remote, null, null);
		writer.WriteProperty(options, PropRuntimeMappings, value.RuntimeMappings, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>? v) => w.WriteDictionaryValue<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>(o, v, null, null));
		writer.WriteProperty(options, PropSize, value.Size, null, null);
		writer.WriteProperty(options, PropSlice, value.Slice, null, null);
		writer.WriteProperty(options, PropSourceFields, value.SourceFields, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.Fields? v) => w.WriteValueEx<Elastic.Clients.Elasticsearch.Fields?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.SingleOrManyFieldsMarker)));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.Reindex.SourceConverter))]
public sealed partial class Source
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public Source(Elastic.Clients.Elasticsearch.Indices indices)
	{
		Indices = indices;
	}
#if NET7_0_OR_GREATER
	public Source()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public Source()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Source(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The name of the data stream, index, or alias you are copying from.
	/// It accepts a comma-separated list to reindex from multiple sources.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Indices Indices { get; set; }

	/// <summary>
	/// <para>
	/// The documents to reindex, which is defined with Query DSL.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.Query? Query { get; set; }

	/// <summary>
	/// <para>
	/// A remote instance of Elasticsearch that you want to index from.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSource? Remote { get; set; }
	public System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>? RuntimeMappings { get; set; }

	/// <summary>
	/// <para>
	/// The number of documents to index per batch.
	/// Use it when you are indexing from remote to ensure that the batches fit within the on-heap buffer, which defaults to a maximum size of 100 MB.
	/// </para>
	/// </summary>
	public int? Size { get; set; }

	/// <summary>
	/// <para>
	/// Slice the reindex request manually using the provided slice ID and total number of slices.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.SlicedScroll? Slice { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c>, reindex all source fields.
	/// Set it to a list to reindex select fields.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? SourceFields { get; set; }
}

public readonly partial struct SourceDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Core.Reindex.Source Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SourceDescriptor(Elastic.Clients.Elasticsearch.Core.Reindex.Source instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SourceDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Core.Reindex.Source(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Core.Reindex.Source instance) => new Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Core.Reindex.Source(Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The name of the data stream, index, or alias you are copying from.
	/// It accepts a comma-separated list to reindex from multiple sources.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor<TDocument> Indices(Elastic.Clients.Elasticsearch.Indices value)
	{
		Instance.Indices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The documents to reindex, which is defined with Query DSL.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor<TDocument> Query(Elastic.Clients.Elasticsearch.QueryDsl.Query? value)
	{
		Instance.Query = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The documents to reindex, which is defined with Query DSL.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor<TDocument> Query(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>> action)
	{
		Instance.Query = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A remote instance of Elasticsearch that you want to index from.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor<TDocument> Remote(Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSource? value)
	{
		Instance.Remote = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A remote instance of Elasticsearch that you want to index from.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor<TDocument> Remote(System.Action<Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor> action)
	{
		Instance.Remote = Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor<TDocument> RuntimeMappings(System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>? value)
	{
		Instance.RuntimeMappings = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor<TDocument> RuntimeMappings()
	{
		Instance.RuntimeMappings = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfFieldRuntimeField<TDocument>.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor<TDocument> RuntimeMappings(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfFieldRuntimeField<TDocument>>? action)
	{
		Instance.RuntimeMappings = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfFieldRuntimeField<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor<TDocument> AddRuntimeMapping(Elastic.Clients.Elasticsearch.Field key, Elastic.Clients.Elasticsearch.Mapping.RuntimeField value)
	{
		Instance.RuntimeMappings ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>();
		Instance.RuntimeMappings.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor<TDocument> AddRuntimeMapping(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> key, Elastic.Clients.Elasticsearch.Mapping.RuntimeField value)
	{
		Instance.RuntimeMappings ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>();
		Instance.RuntimeMappings.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor<TDocument> AddRuntimeMapping(Elastic.Clients.Elasticsearch.Field key, System.Action<Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor<TDocument>> action)
	{
		Instance.RuntimeMappings ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>();
		Instance.RuntimeMappings.Add(key, Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor<TDocument>.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor<TDocument> AddRuntimeMapping(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> key, System.Action<Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor<TDocument>> action)
	{
		Instance.RuntimeMappings ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>();
		Instance.RuntimeMappings.Add(key, Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor<TDocument>.Build(action));
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of documents to index per batch.
	/// Use it when you are indexing from remote to ensure that the batches fit within the on-heap buffer, which defaults to a maximum size of 100 MB.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor<TDocument> Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Slice the reindex request manually using the provided slice ID and total number of slices.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor<TDocument> Slice(Elastic.Clients.Elasticsearch.SlicedScroll? value)
	{
		Instance.Slice = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Slice the reindex request manually using the provided slice ID and total number of slices.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor<TDocument> Slice(System.Action<Elastic.Clients.Elasticsearch.SlicedScrollDescriptor<TDocument>> action)
	{
		Instance.Slice = Elastic.Clients.Elasticsearch.SlicedScrollDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, reindex all source fields.
	/// Set it to a list to reindex select fields.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor<TDocument> SourceFields(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.SourceFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, reindex all source fields.
	/// Set it to a list to reindex select fields.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor<TDocument> SourceFields(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
	{
		Instance.SourceFields = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.Reindex.Source Build(System.Action<Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Core.Reindex.Source(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct SourceDescriptor
{
	internal Elastic.Clients.Elasticsearch.Core.Reindex.Source Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SourceDescriptor(Elastic.Clients.Elasticsearch.Core.Reindex.Source instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SourceDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Core.Reindex.Source(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor(Elastic.Clients.Elasticsearch.Core.Reindex.Source instance) => new Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Core.Reindex.Source(Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The name of the data stream, index, or alias you are copying from.
	/// It accepts a comma-separated list to reindex from multiple sources.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor Indices(Elastic.Clients.Elasticsearch.Indices value)
	{
		Instance.Indices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The documents to reindex, which is defined with Query DSL.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor Query(Elastic.Clients.Elasticsearch.QueryDsl.Query? value)
	{
		Instance.Query = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The documents to reindex, which is defined with Query DSL.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor Query(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor> action)
	{
		Instance.Query = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The documents to reindex, which is defined with Query DSL.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor Query<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>> action)
	{
		Instance.Query = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A remote instance of Elasticsearch that you want to index from.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor Remote(Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSource? value)
	{
		Instance.Remote = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A remote instance of Elasticsearch that you want to index from.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor Remote(System.Action<Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor> action)
	{
		Instance.Remote = Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor RuntimeMappings(System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>? value)
	{
		Instance.RuntimeMappings = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor RuntimeMappings()
	{
		Instance.RuntimeMappings = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfFieldRuntimeField.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor RuntimeMappings(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfFieldRuntimeField>? action)
	{
		Instance.RuntimeMappings = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfFieldRuntimeField.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor RuntimeMappings<T>(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfFieldRuntimeField<T>>? action)
	{
		Instance.RuntimeMappings = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfFieldRuntimeField<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor AddRuntimeMapping(Elastic.Clients.Elasticsearch.Field key, Elastic.Clients.Elasticsearch.Mapping.RuntimeField value)
	{
		Instance.RuntimeMappings ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>();
		Instance.RuntimeMappings.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor AddRuntimeMapping<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> key, Elastic.Clients.Elasticsearch.Mapping.RuntimeField value)
	{
		Instance.RuntimeMappings ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>();
		Instance.RuntimeMappings.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor AddRuntimeMapping(Elastic.Clients.Elasticsearch.Field key, System.Action<Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor> action)
	{
		Instance.RuntimeMappings ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>();
		Instance.RuntimeMappings.Add(key, Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor AddRuntimeMapping<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> key, System.Action<Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor> action)
	{
		Instance.RuntimeMappings ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>();
		Instance.RuntimeMappings.Add(key, Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor AddRuntimeMapping<T>(Elastic.Clients.Elasticsearch.Field key, System.Action<Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor<T>> action)
	{
		Instance.RuntimeMappings ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>();
		Instance.RuntimeMappings.Add(key, Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor<T>.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor AddRuntimeMapping<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> key, System.Action<Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor<T>> action)
	{
		Instance.RuntimeMappings ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Mapping.RuntimeField>();
		Instance.RuntimeMappings.Add(key, Elastic.Clients.Elasticsearch.Mapping.RuntimeFieldDescriptor<T>.Build(action));
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of documents to index per batch.
	/// Use it when you are indexing from remote to ensure that the batches fit within the on-heap buffer, which defaults to a maximum size of 100 MB.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Slice the reindex request manually using the provided slice ID and total number of slices.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor Slice(Elastic.Clients.Elasticsearch.SlicedScroll? value)
	{
		Instance.Slice = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Slice the reindex request manually using the provided slice ID and total number of slices.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor Slice(System.Action<Elastic.Clients.Elasticsearch.SlicedScrollDescriptor> action)
	{
		Instance.Slice = Elastic.Clients.Elasticsearch.SlicedScrollDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Slice the reindex request manually using the provided slice ID and total number of slices.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor Slice<T>(System.Action<Elastic.Clients.Elasticsearch.SlicedScrollDescriptor<T>> action)
	{
		Instance.Slice = Elastic.Clients.Elasticsearch.SlicedScrollDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, reindex all source fields.
	/// Set it to a list to reindex select fields.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor SourceFields(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.SourceFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, reindex all source fields.
	/// Set it to a list to reindex select fields.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor SourceFields<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
	{
		Instance.SourceFields = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.Reindex.Source Build(System.Action<Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Core.Reindex.SourceDescriptor(new Elastic.Clients.Elasticsearch.Core.Reindex.Source(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}