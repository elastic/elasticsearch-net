// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace Elastic.Clients.Elasticsearch;

public sealed class SortCollection : List<SortBase>
{
	public SortCollection() { }

	public SortCollection(IEnumerable<SortBase> sorts) => AddRange(sorts);

	public SortCollection(SortBase sort) => Add(sort);

	public SortCollection(SortBase sort1, SortBase sort2)
	{
		Add(sort1);
		Add(sort2);
	}
}

internal sealed class SortConverter : JsonConverter<SortCollection>
{
	private readonly IElasticsearchClientSettings _settings;

	public SortConverter(IElasticsearchClientSettings settings) => _settings = settings;

	public override SortCollection? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (typeToConvert is null)
			return null;

		if (reader.TokenType != JsonTokenType.StartArray)
			throw new JsonException("Unexpected JSON token. Expected start array.");

		var sort = new SortCollection();

		while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
		{
			var sortItem = ReadSortItem(ref reader, options);

			if (sortItem is not null)
				sort.Add(sortItem);
		}

		return sort;
	}

	private SortBase ReadSortItem(ref Utf8JsonReader reader, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.String)
		{
			var field = reader.GetString();
			return new FieldSort(field);
		}
		else if (reader.TokenType == JsonTokenType.StartObject)
		{
			var readAheadCopy = reader;

			readAheadCopy.Read();

			if (readAheadCopy.TokenType != JsonTokenType.PropertyName)
				throw new JsonException("Unexpected JSON token. Expected property name.");

			var value = readAheadCopy.GetString();

			if (!string.IsNullOrEmpty(value) && value == "_geo_distance")
			{
				// TODO
				throw new NotImplementedException("This feature is not complete.");
			}
			else if (!string.IsNullOrEmpty(value) && value == "_script")
			{
				// TODO
				throw new NotImplementedException("This feature is not complete.");
			}
			else
			{
				return ReadFieldSort(ref reader, options);
			}
		}

		return null;
	}

	private FieldSort ReadFieldSort(ref Utf8JsonReader reader, JsonSerializerOptions options)
	{
		reader.Read();

		if (reader.TokenType != JsonTokenType.PropertyName)
			throw new JsonException("Unexpected JSON token. Expected property name.");

		var field = reader.GetString();

		if (string.IsNullOrEmpty(field))
			throw new JsonException("Invalid field name.");

		reader.Read();

		if (reader.TokenType != JsonTokenType.StartObject)
			throw new JsonException("Unexpected JSON token. Expected start object.");

		var fieldSort = new FieldSort(field);

		while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
		{
			if (reader.TokenType == JsonTokenType.PropertyName)
			{
				if (reader.ValueTextEquals("order"))
				{
					var order = JsonSerializer.Deserialize<SortOrder>(ref reader, options);
					fieldSort.Order = order;
					continue;
				}

				if (reader.ValueTextEquals("missing"))
				{
					var missing = JsonSerializer.Deserialize<object>(ref reader, options);
					fieldSort.Missing = missing;
					continue;
				}

				if (reader.ValueTextEquals("format"))
				{
					var format = JsonSerializer.Deserialize<string>(ref reader, options);
					fieldSort.Format = format;
					continue;
				}

				if (reader.ValueTextEquals("mode"))
				{
					var sortMode = JsonSerializer.Deserialize<SortMode>(ref reader, options);
					fieldSort.Mode = sortMode;
					continue;
				}

				if (reader.ValueTextEquals("numeric_type"))
				{
					var numericType = JsonSerializer.Deserialize<FieldSortNumericType>(ref reader, options);
					fieldSort.NumericType = numericType;
					continue;
				}

				if (reader.ValueTextEquals("nested"))
				{
					var nested = JsonSerializer.Deserialize<NestedSort>(ref reader, options);
					fieldSort.Nested = nested;
					continue;
				}

				if (reader.ValueTextEquals("ignore_unmapped"))
				{
					var ignoreUnmapped = JsonSerializer.Deserialize<bool>(ref reader, options);
					fieldSort.IgnoreUnmapped = ignoreUnmapped;
					continue;
				}

				if (reader.ValueTextEquals("unmapped_type"))
				{
					var fieldType = JsonSerializer.Deserialize<FieldType>(ref reader, options);
					fieldSort.UnmappedType = fieldType;
					continue;
				}
			}
		}

		return fieldSort;
	}

	private GeoDistanceSort ReadGeoDistanceSort(ref Utf8JsonReader reader, JsonSerializerOptions options)
	{
		reader.Read();

		if (reader.TokenType != JsonTokenType.PropertyName)
			throw new JsonException("Unexpected JSON token. Expected property name.");

		var field = reader.GetString();

		if (field != "_geo_distance")
			throw new JsonException("Invalid geo distance sort object.");

		reader.Read();

		if (reader.TokenType != JsonTokenType.StartObject)
			throw new JsonException("Unexpected JSON token. Expected start object.");

		var geoDistanceSort = new GeoDistanceSort();

		while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
		{
			if (reader.TokenType == JsonTokenType.PropertyName)
			{
				if (reader.ValueTextEquals("order"))
				{
					var order = JsonSerializer.Deserialize<SortOrder>(ref reader, options);
					geoDistanceSort.Order = order;
					continue;
				}

				if (reader.ValueTextEquals("mode"))
				{
					var sortMode = JsonSerializer.Deserialize<SortMode>(ref reader, options);
					geoDistanceSort.Mode = sortMode;
					continue;
				}

				// TODO - Other properties
			}
		}

		return geoDistanceSort;
	}

	public override void Write(Utf8JsonWriter writer, SortCollection value, JsonSerializerOptions options)
	{
		writer.WriteStartArray();

		foreach (var sort in value)
		{
			if (sort is null)
				continue;

			if (sort is FieldSort fieldSort)
			{
				SortSerializationHelpers.WriteFieldSort(writer, fieldSort, options, _settings);
				continue;
			}

			// TODO - Other types
			throw new NotImplementedException("The sort type is not currently supported in this release.");
		}

		writer.WriteEndArray();
	}
}

internal static class SortSerializationHelpers
{
	public static void WriteFieldSort(Utf8JsonWriter writer, FieldSort fieldSort, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName(settings.Inferrer.Field(fieldSort.Field));
		writer.WriteStartObject();

		if (fieldSort.Order.HasValue)
		{
			writer.WritePropertyName("order");
			JsonSerializer.Serialize(writer, fieldSort.Order.Value, options);
		}

		if (fieldSort.Missing is not null)
		{
			writer.WritePropertyName("missing");
			JsonSerializer.Serialize(writer, fieldSort.Missing, options);
		}

		if (!string.IsNullOrEmpty(fieldSort.Format))
		{
			writer.WritePropertyName("format");
			writer.WriteStringValue(fieldSort.Format);
		}

		if (fieldSort.Mode.HasValue)
		{
			writer.WritePropertyName("mode");
			JsonSerializer.Serialize(writer, fieldSort.Mode.Value, options);
		}

		if (fieldSort.NumericType.HasValue)
		{
			writer.WritePropertyName("numeric_type");
			JsonSerializer.Serialize(writer, fieldSort.NumericType.Value, options);
		}

		if (fieldSort.Nested is not null)
		{
			writer.WritePropertyName("nested");
			JsonSerializer.Serialize(writer, fieldSort.Nested, options);
		}

		if (fieldSort.UnmappedType.HasValue)
		{
			writer.WritePropertyName("unmapped_type");
			JsonSerializer.Serialize(writer, fieldSort.UnmappedType.Value, options);
		}

		if (fieldSort.IgnoreUnmapped.HasValue)
		{
			writer.WritePropertyName("ignore_unmapped");
			JsonSerializer.Serialize(writer, fieldSort.IgnoreUnmapped.Value, options);
		}

		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}

public abstract class SortBase
{
	public SortMode? Mode { get; set; }

	public SortOrder? Order { get; set; }
}

public sealed class FieldSort : SortBase
{
	private const string ShardDoc = "_shard_doc";

	public static readonly IList<SortBase> ByDocumentOrder = new ReadOnlyCollection<SortBase>(new List<SortBase> { new FieldSort { Field = "_doc" } });

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

public sealed class FieldSortDescriptor<T> : SortDescriptorBase<FieldSortDescriptor<T>, T>
{
	private Field _field;
	
	private string _format;
	private bool? _ignoreUnmappedFields;
	private SortMode? _sortMode;
	private object _missing;
	private NestedSort _nestedSort;
	private NestedSortDescriptor<T> _nestedSortDescriptor;
	private Action<NestedSortDescriptor<T>> _nestedSortDescriptorAction;
	private SortOrder? _order;
	private FieldType? _unmappedType;

	/// <summary>
	/// Sorts by ascending sort order.
	/// </summary>
	public FieldSortDescriptor<T> Ascending() => Assign(SortOrder.Asc, (a, v) => Self._order = v);

	/// <summary>
	/// Sorts by descending sort order.
	/// </summary>
	public FieldSortDescriptor<T> Descending() => Assign(SortOrder.Desc, (a, v) => a._order = v);

	public FieldSortDescriptor<T> Field(Field field) => Assign(field, (a, v) => a._field = v);

	public FieldSortDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a._field = v);

	public FieldSortDescriptor<T> Format(string format) => Assign(format, (a, v) => a._format = v);

	public FieldSortDescriptor<T> IgnoreUnmappedFields(bool? ignore = true) => Assign(ignore, (a, v) => a._ignoreUnmappedFields = v);

	/// <summary>
	/// Specifies that documents which are missing the sort field should be ordered last
	/// </summary>
	public FieldSortDescriptor<T> MissingLast() => Assign("_last", (a, v) => a._missing = v);

	/// <summary>
	/// Specifies that documents which are missing the sort field should be ordered first
	/// </summary>
	public FieldSortDescriptor<T> MissingFirst() => Assign("_first", (a, v) => a._missing = v);

	/// <summary>
	/// Specifies how documents which are missing the sort field should
	/// be treated.
	/// </summary>
	public FieldSortDescriptor<T> Missing(object value) => Assign(value, (a, v) => a._missing = v);

	public FieldSortDescriptor<T> Mode(SortMode? mode) => Assign(mode, (a, v) => a._sortMode = v);

	public FieldSortDescriptor<T> Nested(NestedSort nestedSort) => Assign(nestedSort, (a, v) => a._nestedSort = v);

	public FieldSortDescriptor<T> Nested(NestedSortDescriptor<T> descriptor) =>
		Assign(descriptor, (a, v) => a._nestedSortDescriptor = v);

	public FieldSortDescriptor<T> Nested(Action<NestedSortDescriptor<T>> configure) =>
		Assign(configure, (a, v) => a._nestedSortDescriptorAction = v);

	public FieldSortDescriptor<T> Order(SortOrder? order) => Assign(order, (a, v) => a._order = v);

	public FieldSortDescriptor<T> UnmappedType(FieldType? fieldType) => Assign(fieldType, (a, v) => a._unmappedType = v);

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
			var descriptor = new NestedSortDescriptor<T>();
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

	//internal FieldSort ToFieldSort() => null;
}

public abstract class SortDescriptorBase<TDescriptor, T> : DescriptorBase<TDescriptor> where TDescriptor : DescriptorBase<TDescriptor>
{
	
}

public sealed class GeoDistanceSort : SortBase
{
	public GeoDistanceType? DistanceType { get; set; }

	public Field Field { get; set; }

	public GeoPoints GeoPoints { get; set; }

	public DistanceUnit? Unit { get; set; }

	public bool? IgnoreUnmapped { get; set; }
}

public sealed class NestedSort
{
	public QueryContainer Filter { get; set; }

	public NestedSort Nested { get; set; }

	public Field Path { get; set; }

	public int? MaxChildren { get; set; }
}

public sealed class NestedSortDescriptor<T> : DescriptorBase<NestedSortDescriptor<T>>
{
	private QueryContainer _filter;
	private QueryContainerDescriptor<T> _queryContainerDescriptor;
	private Action<QueryContainerDescriptor<T>> _queryContainerDescriptorAction;
	private Field _path;

	public NestedSortDescriptor<T> Path(Field path) => Assign(path, (a, v) => a._path = v);

	public NestedSortDescriptor<T> Path<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a._path = v);

	public NestedSortDescriptor<T> Filter(QueryContainer queryContainer) =>
		Assign(queryContainer, (a, v) => a._filter = v);

	public NestedSortDescriptor<T> Filter(QueryContainerDescriptor<T> descriptor) =>
		Assign(descriptor, (a, v) => a._queryContainerDescriptor = v);

	public NestedSortDescriptor<T> Filter(Action<QueryContainerDescriptor<T>> configure) =>
			Assign(configure, (a, v) => a._queryContainerDescriptorAction = v);

	// TODO - Complete this

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();

		if (_path is not null)
		{
			writer.WritePropertyName("path");
			JsonSerializer.Serialize(writer, _path, options);
		}

		if (_filter is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, _filter, options);
		}
		else if (_queryContainerDescriptor is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, _queryContainerDescriptor, options);
		}
		else if (_queryContainerDescriptorAction is not null)
		{
			writer.WritePropertyName("filter");
			var descriptor = new QueryContainerDescriptor<T>();
			_queryContainerDescriptorAction(descriptor);
			JsonSerializer.Serialize(writer, descriptor, options);
		}

		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(NumericTypeConverter))]
public enum FieldSortNumericType
{
	Long,
	Double,
	Date,
	DateNanos
}

internal sealed class NumericTypeConverter : JsonConverter<FieldSortNumericType>
{
	public override FieldSortNumericType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "long":
				return FieldSortNumericType.Long;
			case "double":
				return FieldSortNumericType.Double;
			case "date":
				return FieldSortNumericType.Date;
			case "date_nanos":
				return FieldSortNumericType.DateNanos;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, FieldSortNumericType value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case FieldSortNumericType.Long:
				writer.WriteStringValue("long");
				return;
			case FieldSortNumericType.Double:
				writer.WriteStringValue("double");
				return;
			case FieldSortNumericType.Date:
				writer.WriteStringValue("date");
				return;
			case FieldSortNumericType.DateNanos:
				writer.WriteStringValue("date_nanos");
				return;
		}

		writer.WriteNullValue();
	}
}

public enum SortSpecialField
{
	Score,
	DocumentIndexOrder,
	ShardDocumentOrder
}

public sealed class SortDescriptor<T> : DescriptorPromiseBase<SortDescriptor<T>, SortCollection>, ISelfSerializable
{
	public SortDescriptor() : base(new SortCollection()) { }

	public SortDescriptor(Action<SortDescriptor<T>> configure) : base(new SortCollection()) => configure(this);

	private List<Action<FieldSortDescriptor<T>>> _fieldSortDescriptorActions;

	public SortDescriptor<T> Ascending<TValue>(Expression<Func<T, TValue>> objectPath) =>
		Assign(objectPath, (a, v) => a.Add(new FieldSort(v) { Order = SortOrder.Asc }));

	public SortDescriptor<T> Descending<TValue>(Expression<Func<T, TValue>> objectPath) =>
		Assign(objectPath, (a, v) => a.Add(new FieldSort(v) { Order = SortOrder.Desc }));

	public SortDescriptor<T> Ascending(Field field) => Assign(field, (a, v) => a.Add(new FieldSort(v) { Order = SortOrder.Asc }));

	public SortDescriptor<T> Descending(Field field) => Assign(field, (a, v) => a.Add(new FieldSort(v) { Order = SortOrder.Desc }));

	public SortDescriptor<T> Ascending(SortSpecialField field) =>
		Assign(field == SortSpecialField.Score ? "_score" : field == SortSpecialField.DocumentIndexOrder ? "_doc" : "_shard_doc", (a, v) => a.Add(new FieldSort(v) { Order = SortOrder.Asc }));

	public SortDescriptor<T> Descending(SortSpecialField field) =>
		Assign(field == SortSpecialField.Score ? "_score" : field == SortSpecialField.DocumentIndexOrder ? "_doc" : "_shard_doc", (a, v) => a.Add(new FieldSort(v) { Order = SortOrder.Desc }));

	public SortDescriptor<T> Field(Action<FieldSortDescriptor<T>> sortSelector)
	{
		// TODO - Future idea: Store the actions and invoke at serialisation time

		//var descriptor = new FieldSortDescriptor<T>();
		//sortSelector?.Invoke(descriptor);

		if (_fieldSortDescriptorActions is null)
			_fieldSortDescriptorActions = new List<Action<FieldSortDescriptor<T>>>();

		_fieldSortDescriptorActions.Add(sortSelector);

		//return AddSort(descriptor.ToFieldSort());

		return this;
	}

	public SortDescriptor<T> Field(Field field, SortOrder order) => AddSort(new FieldSort(field) { Order = order });

	public SortDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field, SortOrder order) =>
		AddSort(new FieldSort(field) { Order = order });

	//public SortDescriptor<T> GeoDistance(Action<GeoDistanceSortDescriptor<T>> sortSelector) =>
	//	AddSort(sortSelector?.Invoke(new GeoDistanceSortDescriptor<T>()));

	//public SortDescriptor<T> Script(Func<ScriptSortDescriptor<T>, IScriptSort> sortSelector) =>
	//	AddSort(sortSelector?.Invoke(new ScriptSortDescriptor<T>()));

	private SortDescriptor<T> AddSort(SortBase sort) => sort == null ? this : Assign(sort, (a, v) => a.Add(v));

	public void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartArray();

		foreach (var sort in PromisedValue)
		{
			if (sort is null)
				continue;

			if (sort is FieldSort fieldSort)
			{
				SortSerializationHelpers.WriteFieldSort(writer, fieldSort, options, settings);
				continue;
			}

			// TODO - Other types
			throw new NotImplementedException("The sort type is not currently supported in this release.");
		}

		if (_fieldSortDescriptorActions is not null)
		{
			foreach (var action in _fieldSortDescriptorActions)
			{
				var descriptor = new FieldSortDescriptor<T>();
				action(descriptor);
				JsonSerializer.Serialize(writer, descriptor, options);
			}
		}

		writer.WriteEndArray();
	}
}
