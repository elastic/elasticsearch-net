// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Text.Json;

namespace Elastic.Clients.Elasticsearch;

public sealed class FieldSort : SortBase
{
	private const string Doc = "_doc";
	private const string ShardDoc = "_shard_doc";

	public static readonly IList<SortBase> ByDocumentOrder = new ReadOnlyCollection<SortBase>(new List<SortBase> { new FieldSort { Field = Doc } });

	public static readonly IList<SortBase> ByShardDocumentOrder = new ReadOnlyCollection<SortBase>(new List<SortBase> { new FieldSort { Field = ShardDoc } });

	public static readonly FieldSort ShardDocumentOrderAscending = new() { Field = ShardDoc, Order = SortOrder.Asc };

	public static readonly FieldSort ShardDocumentOrderDescending = new() { Field = ShardDoc, Order = SortOrder.Desc };

	public FieldSort() { }

	public FieldSort(Field field) => Field = field;

	public Field Field { get; init; }

	public string? Format { get; set; }

	public object? Missing { get; set; } // TODO - Decide on final type for this

	public FieldSortNumericType? NumericType { get; set; }

	public NestedSort? Nested { get; set; }

	public bool? IgnoreUnmapped { get; set; }

	public FieldType? UnmappedType { get; set; }
}

public sealed class FieldSortDescriptor : SortDescriptorBase<FieldSortDescriptor>
{
	private Field _field;	
	private string _format;
	private bool? _ignoreUnmappedFields;
	private SortMode? _sortMode;
	private object _missing;
	private NestedSort _nestedSort;
	private NestedSortDescriptor _nestedSortDescriptor;
	private Action<NestedSortDescriptor> _nestedSortDescriptorAction;
	private SortOrder? _order;
	private FieldType? _unmappedType;

	/// <summary>
	/// Sorts by ascending sort order.
	/// </summary>
	public FieldSortDescriptor Ascending() => Assign(SortOrder.Asc, (a, v) => Self._order = v);

	/// <summary>
	/// Sorts by descending sort order.
	/// </summary>
	public FieldSortDescriptor Descending() => Assign(SortOrder.Desc, (a, v) => a._order = v);

	public FieldSortDescriptor Field(Field field) => Assign(field, (a, v) => a._field = v);

	public FieldSortDescriptor Field<T, TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a._field = v);

	public FieldSortDescriptor Field<T>(Expression<Func<T, object>> objectPath) => Assign(objectPath, (a, v) => a._field = v);

	public FieldSortDescriptor Format(string format) => Assign(format, (a, v) => a._format = v);

	public FieldSortDescriptor IgnoreUnmappedFields(bool? ignore = true) => Assign(ignore, (a, v) => a._ignoreUnmappedFields = v);

	/// <summary>
	/// Specifies that documents which are missing the sort field should be ordered last
	/// </summary>
	public FieldSortDescriptor MissingLast() => Assign("_last", (a, v) => a._missing = v);

	/// <summary>
	/// Specifies that documents which are missing the sort field should be ordered first
	/// </summary>
	public FieldSortDescriptor MissingFirst() => Assign("_first", (a, v) => a._missing = v);

	/// <summary>
	/// Specifies how documents which are missing the sort field should
	/// be treated.
	/// </summary>
	public FieldSortDescriptor Missing(object value) => Assign(value, (a, v) => a._missing = v);

	public FieldSortDescriptor Mode(SortMode? mode) => Assign(mode, (a, v) => a._sortMode = v);

	public FieldSortDescriptor Nested(NestedSort nestedSort) => Assign(nestedSort, (a, v) => a._nestedSort = v);

	public FieldSortDescriptor Nested(NestedSortDescriptor descriptor) =>
		Assign(descriptor, (a, v) => a._nestedSortDescriptor = v);

	public FieldSortDescriptor Nested(Action<NestedSortDescriptor> configure) =>
		Assign(configure, (a, v) => a._nestedSortDescriptorAction = v);

	public FieldSortDescriptor Order(SortOrder? order) => Assign(order, (a, v) => a._order = v);

	public FieldSortDescriptor UnmappedType(FieldType? fieldType) => Assign(fieldType, (a, v) => a._unmappedType = v);

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName(settings.Inferrer.Field(_field));
		writer.WriteStartObject();

		if (_order.HasValue)
		{
			writer.WritePropertyName("order");
			JsonSerializer.Serialize(writer, _order.Value, options);
		}

		if (_missing is not null)
		{
			writer.WritePropertyName("missing");
			JsonSerializer.Serialize(writer, _missing, options);
		}

		if (_sortMode is not null)
		{
			writer.WritePropertyName("mode");
			JsonSerializer.Serialize(writer, _sortMode, options);
		}

		if (!string.IsNullOrEmpty(_format))
		{
			writer.WritePropertyName("format");
			writer.WriteStringValue(_format);
		}

		if (_ignoreUnmappedFields.HasValue)
		{
			writer.WritePropertyName("ignore_unmapped");
			JsonSerializer.Serialize(writer, _ignoreUnmappedFields.Value, options);
		}

		if (_nestedSort is not null)
		{
			writer.WritePropertyName("nested");
			JsonSerializer.Serialize(writer, _nestedSort, options);
		}
		else if (_nestedSortDescriptor is not null)
		{
			writer.WritePropertyName("nested");
			JsonSerializer.Serialize(writer, _nestedSortDescriptor, options);
		}
		else if (_nestedSortDescriptorAction is not null)
		{
			writer.WritePropertyName("nested");
			var descriptor = new NestedSortDescriptor();
			_nestedSortDescriptorAction(descriptor);
			JsonSerializer.Serialize(writer, descriptor, options);
		}

		if (_unmappedType.HasValue)
		{
			writer.WritePropertyName("unmapped_type");
			JsonSerializer.Serialize(writer, _unmappedType.Value, options);
		}

		writer.WriteEndObject();
		writer.WriteEndObject();
	}

	internal FieldSort ToFieldSort()
	{
		var fieldSort = new FieldSort(_field)
		{
			// Nested = // HARD
			IgnoreUnmapped = _ignoreUnmappedFields,
			UnmappedType = _unmappedType
		};

		return fieldSort;
	}
}

public sealed class FieldSortDescriptor<TDocument> : SortDescriptorBase<FieldSortDescriptor<TDocument>>
{
	private Field _field;
	private string _format;
	private bool? _ignoreUnmappedFields;
	private SortMode? _sortMode;
	private object _missing;
	private NestedSort _nestedSort;
	private NestedSortDescriptor<TDocument> _nestedSortDescriptor;
	private Action<NestedSortDescriptor<TDocument>> _nestedSortDescriptorAction;
	private SortOrder? _order;
	private FieldType? _unmappedType;

	/// <summary>
	/// Sorts by ascending sort order.
	/// </summary>
	public FieldSortDescriptor<TDocument> Ascending() => Assign(SortOrder.Asc, (a, v) => Self._order = v);

	/// <summary>
	/// Sorts by descending sort order.
	/// </summary>
	public FieldSortDescriptor<TDocument> Descending() => Assign(SortOrder.Desc, (a, v) => a._order = v);

	public FieldSortDescriptor<TDocument> Field(Field field) => Assign(field, (a, v) => a._field = v);

	public FieldSortDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> objectPath) => Assign(objectPath, (a, v) => a._field = v);

	public FieldSortDescriptor<TDocument> Field(Expression<Func<TDocument, object>> objectPath) => Assign(objectPath, (a, v) => a._field = v);

	public FieldSortDescriptor<TDocument> Format(string format) => Assign(format, (a, v) => a._format = v);

	public FieldSortDescriptor<TDocument> IgnoreUnmappedFields(bool? ignore = true) => Assign(ignore, (a, v) => a._ignoreUnmappedFields = v);

	/// <summary>
	/// Specifies that documents which are missing the sort field should be ordered last
	/// </summary>
	public FieldSortDescriptor<TDocument> MissingLast() => Assign("_last", (a, v) => a._missing = v);

	/// <summary>
	/// Specifies that documents which are missing the sort field should be ordered first
	/// </summary>
	public FieldSortDescriptor<TDocument> MissingFirst() => Assign("_first", (a, v) => a._missing = v);

	/// <summary>
	/// Specifies how documents which are missing the sort field should
	/// be treated.
	/// </summary>
	public FieldSortDescriptor<TDocument> Missing(object value) => Assign(value, (a, v) => a._missing = v);

	public FieldSortDescriptor<TDocument> Mode(SortMode? mode) => Assign(mode, (a, v) => a._sortMode = v);

	public FieldSortDescriptor<TDocument> Nested(NestedSort nestedSort) => Assign(nestedSort, (a, v) => a._nestedSort = v);

	public FieldSortDescriptor<TDocument> Nested(NestedSortDescriptor<TDocument> descriptor) =>
		Assign(descriptor, (a, v) => a._nestedSortDescriptor = v);

	public FieldSortDescriptor<TDocument> Nested(Action<NestedSortDescriptor<TDocument>> configure) =>
		Assign(configure, (a, v) => a._nestedSortDescriptorAction = v);

	public FieldSortDescriptor<TDocument> Order(SortOrder? order) => Assign(order, (a, v) => a._order = v);

	public FieldSortDescriptor<TDocument> UnmappedType(FieldType? fieldType) => Assign(fieldType, (a, v) => a._unmappedType = v);

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName(settings.Inferrer.Field(_field));
		writer.WriteStartObject();

		if (_order.HasValue)
		{
			writer.WritePropertyName("order");
			JsonSerializer.Serialize(writer, _order.Value, options);
		}

		if (_missing is not null)
		{
			writer.WritePropertyName("missing");
			JsonSerializer.Serialize(writer, _missing, options);
		}

		if (_sortMode is not null)
		{
			writer.WritePropertyName("mode");
			JsonSerializer.Serialize(writer, _sortMode, options);
		}

		if (!string.IsNullOrEmpty(_format))
		{
			writer.WritePropertyName("format");
			writer.WriteStringValue(_format);
		}

		if (_ignoreUnmappedFields.HasValue)
		{
			writer.WritePropertyName("ignore_unmapped");
			JsonSerializer.Serialize(writer, _ignoreUnmappedFields.Value, options);
		}

		if (_nestedSort is not null)
		{
			writer.WritePropertyName("nested");
			JsonSerializer.Serialize(writer, _nestedSort, options);
		}
		else if (_nestedSortDescriptor is not null)
		{
			writer.WritePropertyName("nested");
			JsonSerializer.Serialize(writer, _nestedSortDescriptor, options);
		}
		else if (_nestedSortDescriptorAction is not null)
		{
			writer.WritePropertyName("nested");
			var descriptor = new NestedSortDescriptor<TDocument>();
			_nestedSortDescriptorAction(descriptor);
			JsonSerializer.Serialize(writer, descriptor, options);
		}

		if (_unmappedType.HasValue)
		{
			writer.WritePropertyName("unmapped_type");
			JsonSerializer.Serialize(writer, _unmappedType.Value, options);
		}

		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}
