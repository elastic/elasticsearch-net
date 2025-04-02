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

internal sealed partial class PercolateQueryConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryDsl.PercolateQuery>
{
	private static readonly System.Text.Json.JsonEncodedText PropBoost = System.Text.Json.JsonEncodedText.Encode("boost");
	private static readonly System.Text.Json.JsonEncodedText PropDocument = System.Text.Json.JsonEncodedText.Encode("document");
	private static readonly System.Text.Json.JsonEncodedText PropDocuments = System.Text.Json.JsonEncodedText.Encode("documents");
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropId = System.Text.Json.JsonEncodedText.Encode("id");
	private static readonly System.Text.Json.JsonEncodedText PropIndex = System.Text.Json.JsonEncodedText.Encode("index");
	private static readonly System.Text.Json.JsonEncodedText PropName = System.Text.Json.JsonEncodedText.Encode("name");
	private static readonly System.Text.Json.JsonEncodedText PropPreference = System.Text.Json.JsonEncodedText.Encode("preference");
	private static readonly System.Text.Json.JsonEncodedText PropQueryName = System.Text.Json.JsonEncodedText.Encode("_name");
	private static readonly System.Text.Json.JsonEncodedText PropRouting = System.Text.Json.JsonEncodedText.Encode("routing");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.QueryDsl.PercolateQuery Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<float?> propBoost = default;
		LocalJsonValue<object?> propDocument = default;
		LocalJsonValue<System.Collections.Generic.ICollection<object>?> propDocuments = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Id?> propId = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexName?> propIndex = default;
		LocalJsonValue<string?> propName = default;
		LocalJsonValue<string?> propPreference = default;
		LocalJsonValue<string?> propQueryName = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Routing?> propRouting = default;
		LocalJsonValue<long?> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBoost.TryReadProperty(ref reader, options, PropBoost, null))
			{
				continue;
			}

			if (propDocument.TryReadProperty(ref reader, options, PropDocument, null))
			{
				continue;
			}

			if (propDocuments.TryReadProperty(ref reader, options, PropDocuments, static System.Collections.Generic.ICollection<object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<object>(o, null)))
			{
				continue;
			}

			if (propField.TryReadProperty(ref reader, options, PropField, null))
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

			if (propName.TryReadProperty(ref reader, options, PropName, null))
			{
				continue;
			}

			if (propPreference.TryReadProperty(ref reader, options, PropPreference, null))
			{
				continue;
			}

			if (propQueryName.TryReadProperty(ref reader, options, PropQueryName, null))
			{
				continue;
			}

			if (propRouting.TryReadProperty(ref reader, options, PropRouting, null))
			{
				continue;
			}

			if (propVersion.TryReadProperty(ref reader, options, PropVersion, null))
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
		return new Elastic.Clients.Elasticsearch.QueryDsl.PercolateQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Boost = propBoost.Value,
			Document = propDocument.Value,
			Documents = propDocuments.Value,
			Field = propField.Value,
			Id = propId.Value,
			Index = propIndex.Value,
			Name = propName.Value,
			Preference = propPreference.Value,
			QueryName = propQueryName.Value,
			Routing = propRouting.Value,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryDsl.PercolateQuery value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBoost, value.Boost, null, null);
		writer.WriteProperty(options, PropDocument, value.Document, null, null);
		writer.WriteProperty(options, PropDocuments, value.Documents, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<object>? v) => w.WriteCollectionValue<object>(o, v, null));
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteProperty(options, PropId, value.Id, null, null);
		writer.WriteProperty(options, PropIndex, value.Index, null, null);
		writer.WriteProperty(options, PropName, value.Name, null, null);
		writer.WriteProperty(options, PropPreference, value.Preference, null, null);
		writer.WriteProperty(options, PropQueryName, value.QueryName, null, null);
		writer.WriteProperty(options, PropRouting, value.Routing, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryConverter))]
public sealed partial class PercolateQuery
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PercolateQuery(Elastic.Clients.Elasticsearch.Field field)
	{
		Field = field;
	}
#if NET7_0_OR_GREATER
	public PercolateQuery()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public PercolateQuery()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PercolateQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public float? Boost { get; set; }

	/// <summary>
	/// <para>
	/// The source of the document being percolated.
	/// </para>
	/// </summary>
	public object? Document { get; set; }

	/// <summary>
	/// <para>
	/// An array of sources of the documents being percolated.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<object>? Documents { get; set; }

	/// <summary>
	/// <para>
	/// Field that holds the indexed queries. The field must use the <c>percolator</c> mapping type.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// The ID of a stored document to percolate.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Id? Id { get; set; }

	/// <summary>
	/// <para>
	/// The index of a stored document to percolate.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexName? Index { get; set; }

	/// <summary>
	/// <para>
	/// The suffix used for the <c>_percolator_document_slot</c> field when multiple <c>percolate</c> queries are specified.
	/// </para>
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// <para>
	/// Preference used to fetch document to percolate.
	/// </para>
	/// </summary>
	public string? Preference { get; set; }
	public string? QueryName { get; set; }

	/// <summary>
	/// <para>
	/// Routing used to fetch document to percolate.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Routing? Routing { get; set; }

	/// <summary>
	/// <para>
	/// The expected version of a stored document to percolate.
	/// </para>
	/// </summary>
	public long? Version { get; set; }
}

public readonly partial struct PercolateQueryDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.PercolateQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PercolateQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.PercolateQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PercolateQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.PercolateQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor<TDocument>(Elastic.Clients.Elasticsearch.QueryDsl.PercolateQuery instance) => new Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.PercolateQuery(Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor<TDocument> Boost(float? value)
	{
		Instance.Boost = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The source of the document being percolated.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor<TDocument> Document(object? value)
	{
		Instance.Document = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// An array of sources of the documents being percolated.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor<TDocument> Documents(System.Collections.Generic.ICollection<object>? value)
	{
		Instance.Documents = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// An array of sources of the documents being percolated.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor<TDocument> Documents()
	{
		Instance.Documents = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfObject.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// An array of sources of the documents being percolated.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor<TDocument> Documents(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfObject>? action)
	{
		Instance.Documents = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfObject.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// An array of sources of the documents being percolated.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor<TDocument> Documents(params object[] values)
	{
		Instance.Documents = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Field that holds the indexed queries. The field must use the <c>percolator</c> mapping type.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Field that holds the indexed queries. The field must use the <c>percolator</c> mapping type.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The ID of a stored document to percolate.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Id? value)
	{
		Instance.Id = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The index of a stored document to percolate.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.IndexName? value)
	{
		Instance.Index = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The suffix used for the <c>_percolator_document_slot</c> field when multiple <c>percolate</c> queries are specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor<TDocument> Name(string? value)
	{
		Instance.Name = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Preference used to fetch document to percolate.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor<TDocument> Preference(string? value)
	{
		Instance.Preference = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor<TDocument> QueryName(string? value)
	{
		Instance.QueryName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Routing used to fetch document to percolate.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor<TDocument> Routing(Elastic.Clients.Elasticsearch.Routing? value)
	{
		Instance.Routing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The expected version of a stored document to percolate.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor<TDocument> Version(long? value)
	{
		Instance.Version = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.PercolateQuery Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.QueryDsl.PercolateQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct PercolateQueryDescriptor
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.PercolateQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PercolateQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.PercolateQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PercolateQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.PercolateQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.PercolateQuery instance) => new Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.PercolateQuery(Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor Boost(float? value)
	{
		Instance.Boost = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The source of the document being percolated.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor Document(object? value)
	{
		Instance.Document = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// An array of sources of the documents being percolated.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor Documents(System.Collections.Generic.ICollection<object>? value)
	{
		Instance.Documents = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// An array of sources of the documents being percolated.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor Documents()
	{
		Instance.Documents = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfObject.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// An array of sources of the documents being percolated.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor Documents(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfObject>? action)
	{
		Instance.Documents = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfObject.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// An array of sources of the documents being percolated.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor Documents(params object[] values)
	{
		Instance.Documents = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Field that holds the indexed queries. The field must use the <c>percolator</c> mapping type.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Field that holds the indexed queries. The field must use the <c>percolator</c> mapping type.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The ID of a stored document to percolate.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor Id(Elastic.Clients.Elasticsearch.Id? value)
	{
		Instance.Id = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The index of a stored document to percolate.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor Index(Elastic.Clients.Elasticsearch.IndexName? value)
	{
		Instance.Index = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The suffix used for the <c>_percolator_document_slot</c> field when multiple <c>percolate</c> queries are specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor Name(string? value)
	{
		Instance.Name = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Preference used to fetch document to percolate.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor Preference(string? value)
	{
		Instance.Preference = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor QueryName(string? value)
	{
		Instance.QueryName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Routing used to fetch document to percolate.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor Routing(Elastic.Clients.Elasticsearch.Routing? value)
	{
		Instance.Routing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The expected version of a stored document to percolate.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor Version(long? value)
	{
		Instance.Version = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.PercolateQuery Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.PercolateQueryDescriptor(new Elastic.Clients.Elasticsearch.QueryDsl.PercolateQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}