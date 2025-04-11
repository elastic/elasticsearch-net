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

internal sealed partial class GeoShapeFieldQueryConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQuery>
{
	private static readonly System.Text.Json.JsonEncodedText PropIndexedShape = System.Text.Json.JsonEncodedText.Encode("indexed_shape");
	private static readonly System.Text.Json.JsonEncodedText PropRelation = System.Text.Json.JsonEncodedText.Encode("relation");
	private static readonly System.Text.Json.JsonEncodedText PropShape = System.Text.Json.JsonEncodedText.Encode("shape");

	public override Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQuery Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.FieldLookup?> propIndexedShape = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.GeoShapeRelation?> propRelation = default;
		LocalJsonValue<object?> propShape = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propIndexedShape.TryReadProperty(ref reader, options, PropIndexedShape, null))
			{
				continue;
			}

			if (propRelation.TryReadProperty(ref reader, options, PropRelation, null))
			{
				continue;
			}

			if (propShape.TryReadProperty(ref reader, options, PropShape, null))
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
		return new Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			IndexedShape = propIndexedShape.Value,
			Relation = propRelation.Value,
			Shape = propShape.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQuery value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropIndexedShape, value.IndexedShape, null, null);
		writer.WriteProperty(options, PropRelation, value.Relation, null, null);
		writer.WriteProperty(options, PropShape, value.Shape, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQueryConverter))]
public sealed partial class GeoShapeFieldQuery
{
#if NET7_0_OR_GREATER
	public GeoShapeFieldQuery()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public GeoShapeFieldQuery()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GeoShapeFieldQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Query using an indexed shape retrieved from the the specified document and path.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.FieldLookup? IndexedShape { get; set; }

	/// <summary>
	/// <para>
	/// Spatial relation operator used to search a geo field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.GeoShapeRelation? Relation { get; set; }
	public object? Shape { get; set; }
}

public readonly partial struct GeoShapeFieldQueryDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoShapeFieldQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoShapeFieldQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQueryDescriptor<TDocument>(Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQuery instance) => new Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQueryDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQuery(Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQueryDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Query using an indexed shape retrieved from the the specified document and path.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQueryDescriptor<TDocument> IndexedShape(Elastic.Clients.Elasticsearch.QueryDsl.FieldLookup? value)
	{
		Instance.IndexedShape = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Query using an indexed shape retrieved from the the specified document and path.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQueryDescriptor<TDocument> IndexedShape(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.FieldLookupDescriptor<TDocument>> action)
	{
		Instance.IndexedShape = Elastic.Clients.Elasticsearch.QueryDsl.FieldLookupDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Spatial relation operator used to search a geo field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQueryDescriptor<TDocument> Relation(Elastic.Clients.Elasticsearch.GeoShapeRelation? value)
	{
		Instance.Relation = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQueryDescriptor<TDocument> Shape(object? value)
	{
		Instance.Shape = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQuery Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQueryDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQueryDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct GeoShapeFieldQueryDescriptor
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoShapeFieldQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoShapeFieldQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQuery instance) => new Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQueryDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQuery(Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQueryDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Query using an indexed shape retrieved from the the specified document and path.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQueryDescriptor IndexedShape(Elastic.Clients.Elasticsearch.QueryDsl.FieldLookup? value)
	{
		Instance.IndexedShape = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Query using an indexed shape retrieved from the the specified document and path.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQueryDescriptor IndexedShape(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.FieldLookupDescriptor> action)
	{
		Instance.IndexedShape = Elastic.Clients.Elasticsearch.QueryDsl.FieldLookupDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Query using an indexed shape retrieved from the the specified document and path.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQueryDescriptor IndexedShape<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.FieldLookupDescriptor<T>> action)
	{
		Instance.IndexedShape = Elastic.Clients.Elasticsearch.QueryDsl.FieldLookupDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Spatial relation operator used to search a geo field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQueryDescriptor Relation(Elastic.Clients.Elasticsearch.GeoShapeRelation? value)
	{
		Instance.Relation = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQueryDescriptor Shape(object? value)
	{
		Instance.Shape = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQuery Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQueryDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQueryDescriptor(new Elastic.Clients.Elasticsearch.QueryDsl.GeoShapeFieldQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}