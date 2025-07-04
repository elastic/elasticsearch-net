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

namespace Elastic.Clients.Elasticsearch.Core.MGet;

internal sealed partial class MultiGetOperationConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation>
{
	private static readonly System.Text.Json.JsonEncodedText PropId = System.Text.Json.JsonEncodedText.Encode("_id");
	private static readonly System.Text.Json.JsonEncodedText PropIndex = System.Text.Json.JsonEncodedText.Encode("_index");
	private static readonly System.Text.Json.JsonEncodedText PropRouting = System.Text.Json.JsonEncodedText.Encode("routing");
	private static readonly System.Text.Json.JsonEncodedText PropSource = System.Text.Json.JsonEncodedText.Encode("_source");
	private static readonly System.Text.Json.JsonEncodedText PropStoredFields = System.Text.Json.JsonEncodedText.Encode("stored_fields");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");
	private static readonly System.Text.Json.JsonEncodedText PropVersionType = System.Text.Json.JsonEncodedText.Encode("version_type");

	public override Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Id> propId = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexName?> propIndex = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Routing?> propRouting = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Core.Search.SourceConfig?> propSource = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Fields?> propStoredFields = default;
		LocalJsonValue<long?> propVersion = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.VersionType?> propVersionType = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propId.TryReadProperty(ref reader, options, PropId, null))
			{
				continue;
			}

			if (propIndex.TryReadProperty(ref reader, options, PropIndex, null))
			{
				continue;
			}

			if (propRouting.TryReadProperty(ref reader, options, PropRouting, null))
			{
				continue;
			}

			if (propSource.TryReadProperty(ref reader, options, PropSource, null))
			{
				continue;
			}

			if (propStoredFields.TryReadProperty(ref reader, options, PropStoredFields, static Elastic.Clients.Elasticsearch.Fields? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<Elastic.Clients.Elasticsearch.Fields?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.SingleOrManyFieldsMarker))))
			{
				continue;
			}

			if (propVersion.TryReadProperty(ref reader, options, PropVersion, static long? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<long>(o)))
			{
				continue;
			}

			if (propVersionType.TryReadProperty(ref reader, options, PropVersionType, static Elastic.Clients.Elasticsearch.VersionType? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<Elastic.Clients.Elasticsearch.VersionType>(o)))
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
		return new Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Id = propId.Value,
			Index = propIndex.Value,
			Routing = propRouting.Value,
			Source = propSource.Value,
			StoredFields = propStoredFields.Value,
			Version = propVersion.Value,
			VersionType = propVersionType.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropId, value.Id, null, null);
		writer.WriteProperty(options, PropIndex, value.Index, null, null);
		writer.WriteProperty(options, PropRouting, value.Routing, null, null);
		writer.WriteProperty(options, PropSource, value.Source, null, null);
		writer.WriteProperty(options, PropStoredFields, value.StoredFields, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.Fields? v) => w.WriteValueEx<Elastic.Clients.Elasticsearch.Fields?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.SingleOrManyFieldsMarker)));
		writer.WriteProperty(options, PropVersion, value.Version, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, long? v) => w.WriteNullableValue<long>(o, v));
		writer.WriteProperty(options, PropVersionType, value.VersionType, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.VersionType? v) => w.WriteNullableValue<Elastic.Clients.Elasticsearch.VersionType>(o, v));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationConverter))]
public sealed partial class MultiGetOperation
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MultiGetOperation(Elastic.Clients.Elasticsearch.Id id)
	{
		Id = id;
	}
#if NET7_0_OR_GREATER
	public MultiGetOperation()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public MultiGetOperation()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal MultiGetOperation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The unique document ID.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id Id { get; set; }

	/// <summary>
	/// <para>
	/// The index that contains the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexName? Index { get; set; }

	/// <summary>
	/// <para>
	/// The key for the primary shard the document resides on. Required if routing is used during indexing.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Routing? Routing { get; set; }

	/// <summary>
	/// <para>
	/// If <c>false</c>, excludes all _source fields.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.SourceConfig? Source { get; set; }

	/// <summary>
	/// <para>
	/// The stored fields you want to retrieve.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? StoredFields { get; set; }
	public long? Version { get; set; }
	public Elastic.Clients.Elasticsearch.VersionType? VersionType { get; set; }
}

public readonly partial struct MultiGetOperationDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MultiGetOperationDescriptor(Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MultiGetOperationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation instance) => new Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation(Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The unique document ID.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.Id = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The index that contains the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.IndexName? value)
	{
		Instance.Index = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The key for the primary shard the document resides on. Required if routing is used during indexing.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor<TDocument> Routing(Elastic.Clients.Elasticsearch.Routing? value)
	{
		Instance.Routing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>false</c>, excludes all _source fields.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor<TDocument> Source(Elastic.Clients.Elasticsearch.Core.Search.SourceConfig? value)
	{
		Instance.Source = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>false</c>, excludes all _source fields.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor<TDocument> Source(System.Func<Elastic.Clients.Elasticsearch.Core.Search.SourceConfigFactory<TDocument>, Elastic.Clients.Elasticsearch.Core.Search.SourceConfig> action)
	{
		Instance.Source = Elastic.Clients.Elasticsearch.Core.Search.SourceConfigFactory<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The stored fields you want to retrieve.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor<TDocument> StoredFields(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.StoredFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The stored fields you want to retrieve.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor<TDocument> StoredFields(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
	{
		Instance.StoredFields = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor<TDocument> Version(long? value)
	{
		Instance.Version = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor<TDocument> VersionType(Elastic.Clients.Elasticsearch.VersionType? value)
	{
		Instance.VersionType = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation Build(System.Action<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct MultiGetOperationDescriptor
{
	internal Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MultiGetOperationDescriptor(Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MultiGetOperationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor(Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation instance) => new Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation(Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The unique document ID.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor Id(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.Id = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The index that contains the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor Index(Elastic.Clients.Elasticsearch.IndexName? value)
	{
		Instance.Index = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The key for the primary shard the document resides on. Required if routing is used during indexing.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor Routing(Elastic.Clients.Elasticsearch.Routing? value)
	{
		Instance.Routing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>false</c>, excludes all _source fields.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor Source(Elastic.Clients.Elasticsearch.Core.Search.SourceConfig? value)
	{
		Instance.Source = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>false</c>, excludes all _source fields.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor Source(System.Func<Elastic.Clients.Elasticsearch.Core.Search.SourceConfigFactory, Elastic.Clients.Elasticsearch.Core.Search.SourceConfig> action)
	{
		Instance.Source = Elastic.Clients.Elasticsearch.Core.Search.SourceConfigFactory.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>false</c>, excludes all _source fields.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor Source<T>(System.Func<Elastic.Clients.Elasticsearch.Core.Search.SourceConfigFactory<T>, Elastic.Clients.Elasticsearch.Core.Search.SourceConfig> action)
	{
		Instance.Source = Elastic.Clients.Elasticsearch.Core.Search.SourceConfigFactory<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The stored fields you want to retrieve.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor StoredFields(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.StoredFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The stored fields you want to retrieve.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor StoredFields<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
	{
		Instance.StoredFields = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor Version(long? value)
	{
		Instance.Version = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor VersionType(Elastic.Clients.Elasticsearch.VersionType? value)
	{
		Instance.VersionType = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation Build(System.Action<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor(new Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}