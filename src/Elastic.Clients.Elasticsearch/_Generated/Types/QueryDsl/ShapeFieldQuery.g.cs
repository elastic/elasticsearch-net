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

namespace Elastic.Clients.Elasticsearch.QueryDsl;

public sealed partial class ShapeFieldQuery
{
	/// <summary>
	/// <para>
	/// Queries using a pre-indexed shape.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("indexed_shape")]
	public Elastic.Clients.Elasticsearch.QueryDsl.FieldLookup? IndexedShape { get; set; }

	/// <summary>
	/// <para>
	/// Spatial relation between the query shape and the document shape.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("relation")]
	public Elastic.Clients.Elasticsearch.GeoShapeRelation? Relation { get; set; }

	/// <summary>
	/// <para>
	/// Queries using an inline shape definition in GeoJSON or Well Known Text (WKT) format.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("shape")]
	public object? Shape { get; set; }
}

public sealed partial class ShapeFieldQueryDescriptor<TDocument> : SerializableDescriptor<ShapeFieldQueryDescriptor<TDocument>>
{
	internal ShapeFieldQueryDescriptor(Action<ShapeFieldQueryDescriptor<TDocument>> configure) => configure.Invoke(this);

	public ShapeFieldQueryDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.QueryDsl.FieldLookup? IndexedShapeValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.FieldLookupDescriptor<TDocument> IndexedShapeDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryDsl.FieldLookupDescriptor<TDocument>> IndexedShapeDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.GeoShapeRelation? RelationValue { get; set; }
	private object? ShapeValue { get; set; }

	/// <summary>
	/// <para>
	/// Queries using a pre-indexed shape.
	/// </para>
	/// </summary>
	public ShapeFieldQueryDescriptor<TDocument> IndexedShape(Elastic.Clients.Elasticsearch.QueryDsl.FieldLookup? indexedShape)
	{
		IndexedShapeDescriptor = null;
		IndexedShapeDescriptorAction = null;
		IndexedShapeValue = indexedShape;
		return Self;
	}

	public ShapeFieldQueryDescriptor<TDocument> IndexedShape(Elastic.Clients.Elasticsearch.QueryDsl.FieldLookupDescriptor<TDocument> descriptor)
	{
		IndexedShapeValue = null;
		IndexedShapeDescriptorAction = null;
		IndexedShapeDescriptor = descriptor;
		return Self;
	}

	public ShapeFieldQueryDescriptor<TDocument> IndexedShape(Action<Elastic.Clients.Elasticsearch.QueryDsl.FieldLookupDescriptor<TDocument>> configure)
	{
		IndexedShapeValue = null;
		IndexedShapeDescriptor = null;
		IndexedShapeDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Spatial relation between the query shape and the document shape.
	/// </para>
	/// </summary>
	public ShapeFieldQueryDescriptor<TDocument> Relation(Elastic.Clients.Elasticsearch.GeoShapeRelation? relation)
	{
		RelationValue = relation;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Queries using an inline shape definition in GeoJSON or Well Known Text (WKT) format.
	/// </para>
	/// </summary>
	public ShapeFieldQueryDescriptor<TDocument> Shape(object? shape)
	{
		ShapeValue = shape;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (IndexedShapeDescriptor is not null)
		{
			writer.WritePropertyName("indexed_shape");
			JsonSerializer.Serialize(writer, IndexedShapeDescriptor, options);
		}
		else if (IndexedShapeDescriptorAction is not null)
		{
			writer.WritePropertyName("indexed_shape");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryDsl.FieldLookupDescriptor<TDocument>(IndexedShapeDescriptorAction), options);
		}
		else if (IndexedShapeValue is not null)
		{
			writer.WritePropertyName("indexed_shape");
			JsonSerializer.Serialize(writer, IndexedShapeValue, options);
		}

		if (RelationValue is not null)
		{
			writer.WritePropertyName("relation");
			JsonSerializer.Serialize(writer, RelationValue, options);
		}

		if (ShapeValue is not null)
		{
			writer.WritePropertyName("shape");
			JsonSerializer.Serialize(writer, ShapeValue, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class ShapeFieldQueryDescriptor : SerializableDescriptor<ShapeFieldQueryDescriptor>
{
	internal ShapeFieldQueryDescriptor(Action<ShapeFieldQueryDescriptor> configure) => configure.Invoke(this);

	public ShapeFieldQueryDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.QueryDsl.FieldLookup? IndexedShapeValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.FieldLookupDescriptor IndexedShapeDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryDsl.FieldLookupDescriptor> IndexedShapeDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.GeoShapeRelation? RelationValue { get; set; }
	private object? ShapeValue { get; set; }

	/// <summary>
	/// <para>
	/// Queries using a pre-indexed shape.
	/// </para>
	/// </summary>
	public ShapeFieldQueryDescriptor IndexedShape(Elastic.Clients.Elasticsearch.QueryDsl.FieldLookup? indexedShape)
	{
		IndexedShapeDescriptor = null;
		IndexedShapeDescriptorAction = null;
		IndexedShapeValue = indexedShape;
		return Self;
	}

	public ShapeFieldQueryDescriptor IndexedShape(Elastic.Clients.Elasticsearch.QueryDsl.FieldLookupDescriptor descriptor)
	{
		IndexedShapeValue = null;
		IndexedShapeDescriptorAction = null;
		IndexedShapeDescriptor = descriptor;
		return Self;
	}

	public ShapeFieldQueryDescriptor IndexedShape(Action<Elastic.Clients.Elasticsearch.QueryDsl.FieldLookupDescriptor> configure)
	{
		IndexedShapeValue = null;
		IndexedShapeDescriptor = null;
		IndexedShapeDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Spatial relation between the query shape and the document shape.
	/// </para>
	/// </summary>
	public ShapeFieldQueryDescriptor Relation(Elastic.Clients.Elasticsearch.GeoShapeRelation? relation)
	{
		RelationValue = relation;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Queries using an inline shape definition in GeoJSON or Well Known Text (WKT) format.
	/// </para>
	/// </summary>
	public ShapeFieldQueryDescriptor Shape(object? shape)
	{
		ShapeValue = shape;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (IndexedShapeDescriptor is not null)
		{
			writer.WritePropertyName("indexed_shape");
			JsonSerializer.Serialize(writer, IndexedShapeDescriptor, options);
		}
		else if (IndexedShapeDescriptorAction is not null)
		{
			writer.WritePropertyName("indexed_shape");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryDsl.FieldLookupDescriptor(IndexedShapeDescriptorAction), options);
		}
		else if (IndexedShapeValue is not null)
		{
			writer.WritePropertyName("indexed_shape");
			JsonSerializer.Serialize(writer, IndexedShapeValue, options);
		}

		if (RelationValue is not null)
		{
			writer.WritePropertyName("relation");
			JsonSerializer.Serialize(writer, RelationValue, options);
		}

		if (ShapeValue is not null)
		{
			writer.WritePropertyName("shape");
			JsonSerializer.Serialize(writer, ShapeValue, options);
		}

		writer.WriteEndObject();
	}
}