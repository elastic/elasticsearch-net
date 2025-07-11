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

namespace Elastic.Clients.Elasticsearch.QueryDsl;

internal sealed partial class LikeDocumentConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryDsl.LikeDocument>
{
	private static readonly System.Text.Json.JsonEncodedText PropDoc = System.Text.Json.JsonEncodedText.Encode("doc");
	private static readonly System.Text.Json.JsonEncodedText PropFields = System.Text.Json.JsonEncodedText.Encode("fields");
	private static readonly System.Text.Json.JsonEncodedText PropId = System.Text.Json.JsonEncodedText.Encode("_id");
	private static readonly System.Text.Json.JsonEncodedText PropIndex = System.Text.Json.JsonEncodedText.Encode("_index");
	private static readonly System.Text.Json.JsonEncodedText PropPerFieldAnalyzer = System.Text.Json.JsonEncodedText.Encode("per_field_analyzer");
	private static readonly System.Text.Json.JsonEncodedText PropRouting = System.Text.Json.JsonEncodedText.Encode("routing");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");
	private static readonly System.Text.Json.JsonEncodedText PropVersionType = System.Text.Json.JsonEncodedText.Encode("version_type");

	public override Elastic.Clients.Elasticsearch.QueryDsl.LikeDocument Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<object?> propDoc = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Fields?> propFields = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Id?> propId = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexName?> propIndex = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.Field, string>?> propPerFieldAnalyzer = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Routing?> propRouting = default;
		LocalJsonValue<long?> propVersion = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.VersionType?> propVersionType = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDoc.TryReadProperty(ref reader, options, PropDoc, null))
			{
				continue;
			}

			if (propFields.TryReadProperty(ref reader, options, PropFields, null))
			{
				continue;
			}

			if (propId.TryReadProperty(ref reader, options, PropId, null))
			{
				continue;
			}

			if (propIndex.TryReadProperty(ref reader, options, PropIndex, null))
			{
				continue;
			}

			if (propPerFieldAnalyzer.TryReadProperty(ref reader, options, PropPerFieldAnalyzer, static System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.Field, string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<Elastic.Clients.Elasticsearch.Field, string>(o, null, null)))
			{
				continue;
			}

			if (propRouting.TryReadProperty(ref reader, options, PropRouting, null))
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
		return new Elastic.Clients.Elasticsearch.QueryDsl.LikeDocument(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Doc = propDoc.Value,
			Fields = propFields.Value,
			Id = propId.Value,
			Index = propIndex.Value,
			PerFieldAnalyzer = propPerFieldAnalyzer.Value,
			Routing = propRouting.Value,
			Version = propVersion.Value,
			VersionType = propVersionType.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryDsl.LikeDocument value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDoc, value.Doc, null, null);
		writer.WriteProperty(options, PropFields, value.Fields, null, null);
		writer.WriteProperty(options, PropId, value.Id, null, null);
		writer.WriteProperty(options, PropIndex, value.Index, null, null);
		writer.WriteProperty(options, PropPerFieldAnalyzer, value.PerFieldAnalyzer, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.Field, string>? v) => w.WriteDictionaryValue<Elastic.Clients.Elasticsearch.Field, string>(o, v, null, null));
		writer.WriteProperty(options, PropRouting, value.Routing, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, long? v) => w.WriteNullableValue<long>(o, v));
		writer.WriteProperty(options, PropVersionType, value.VersionType, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.VersionType? v) => w.WriteNullableValue<Elastic.Clients.Elasticsearch.VersionType>(o, v));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentConverter))]
public sealed partial class LikeDocument
{
#if NET7_0_OR_GREATER
	public LikeDocument()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public LikeDocument()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal LikeDocument(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// A document not present in the index.
	/// </para>
	/// </summary>
	public object? Doc { get; set; }
	public Elastic.Clients.Elasticsearch.Fields? Fields { get; set; }

	/// <summary>
	/// <para>
	/// ID of a document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Id? Id { get; set; }

	/// <summary>
	/// <para>
	/// Index of a document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexName? Index { get; set; }

	/// <summary>
	/// <para>
	/// Overrides the default analyzer.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.Field, string>? PerFieldAnalyzer { get; set; }
	public Elastic.Clients.Elasticsearch.Routing? Routing { get; set; }
	public long? Version { get; set; }
	public Elastic.Clients.Elasticsearch.VersionType? VersionType { get; set; }
}

public readonly partial struct LikeDocumentDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.LikeDocument Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public LikeDocumentDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.LikeDocument instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public LikeDocumentDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.LikeDocument(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor<TDocument>(Elastic.Clients.Elasticsearch.QueryDsl.LikeDocument instance) => new Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.LikeDocument(Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A document not present in the index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor<TDocument> Doc(object? value)
	{
		Instance.Doc = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.Fields = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor<TDocument> Fields(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
	{
		Instance.Fields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// ID of a document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Id? value)
	{
		Instance.Id = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Index of a document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.IndexName? value)
	{
		Instance.Index = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Overrides the default analyzer.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor<TDocument> PerFieldAnalyzer(System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.Field, string>? value)
	{
		Instance.PerFieldAnalyzer = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Overrides the default analyzer.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor<TDocument> PerFieldAnalyzer()
	{
		Instance.PerFieldAnalyzer = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfFieldString<TDocument>.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Overrides the default analyzer.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor<TDocument> PerFieldAnalyzer(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfFieldString<TDocument>>? action)
	{
		Instance.PerFieldAnalyzer = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfFieldString<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor<TDocument> AddPerFieldAnalyzer(Elastic.Clients.Elasticsearch.Field key, string value)
	{
		Instance.PerFieldAnalyzer ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.Field, string>();
		Instance.PerFieldAnalyzer.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor<TDocument> AddPerFieldAnalyzer(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> key, string value)
	{
		Instance.PerFieldAnalyzer ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.Field, string>();
		Instance.PerFieldAnalyzer.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor<TDocument> Routing(Elastic.Clients.Elasticsearch.Routing? value)
	{
		Instance.Routing = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor<TDocument> Version(long? value)
	{
		Instance.Version = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor<TDocument> VersionType(Elastic.Clients.Elasticsearch.VersionType? value)
	{
		Instance.VersionType = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.LikeDocument Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.QueryDsl.LikeDocument(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.QueryDsl.LikeDocument(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct LikeDocumentDescriptor
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.LikeDocument Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public LikeDocumentDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.LikeDocument instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public LikeDocumentDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.LikeDocument(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.LikeDocument instance) => new Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.LikeDocument(Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A document not present in the index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor Doc(object? value)
	{
		Instance.Doc = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor Fields(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.Fields = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor Fields<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
	{
		Instance.Fields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// ID of a document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor Id(Elastic.Clients.Elasticsearch.Id? value)
	{
		Instance.Id = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Index of a document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor Index(Elastic.Clients.Elasticsearch.IndexName? value)
	{
		Instance.Index = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Overrides the default analyzer.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor PerFieldAnalyzer(System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.Field, string>? value)
	{
		Instance.PerFieldAnalyzer = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Overrides the default analyzer.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor PerFieldAnalyzer()
	{
		Instance.PerFieldAnalyzer = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfFieldString.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Overrides the default analyzer.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor PerFieldAnalyzer(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfFieldString>? action)
	{
		Instance.PerFieldAnalyzer = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfFieldString.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Overrides the default analyzer.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor PerFieldAnalyzer<T>(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfFieldString<T>>? action)
	{
		Instance.PerFieldAnalyzer = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfFieldString<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor AddPerFieldAnalyzer(Elastic.Clients.Elasticsearch.Field key, string value)
	{
		Instance.PerFieldAnalyzer ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.Field, string>();
		Instance.PerFieldAnalyzer.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor AddPerFieldAnalyzer<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> key, string value)
	{
		Instance.PerFieldAnalyzer ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.Field, string>();
		Instance.PerFieldAnalyzer.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor Routing(Elastic.Clients.Elasticsearch.Routing? value)
	{
		Instance.Routing = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor Version(long? value)
	{
		Instance.Version = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor VersionType(Elastic.Clients.Elasticsearch.VersionType? value)
	{
		Instance.VersionType = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.LikeDocument Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.QueryDsl.LikeDocument(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.LikeDocumentDescriptor(new Elastic.Clients.Elasticsearch.QueryDsl.LikeDocument(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}