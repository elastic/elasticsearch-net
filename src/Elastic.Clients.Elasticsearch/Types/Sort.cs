// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace Elastic.Clients.Elasticsearch;

public sealed class Sort : List<SortBase>
{
	public Sort() { }

	public Sort(IEnumerable<SortBase> sorts) => AddRange(sorts);

	public Sort(SortBase sort) => Add(sort);

	public Sort(SortBase sort1, SortBase sort2)
	{
		Add(sort1);
		Add(sort2);
	}
}

internal sealed class SortConverter : JsonConverter<Sort>
{
	private readonly IElasticsearchClientSettings _settings;

	public SortConverter(IElasticsearchClientSettings settings) => _settings = settings;

	public override Sort? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (typeToConvert is null)
			return null;

		if (reader.TokenType != JsonTokenType.StartArray)
			throw new JsonException("Unexpected JSON token. Expected start array.");

		var sort = new Sort();

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
			}
		}

		return geoDistanceSort;
	}

	public override void Write(Utf8JsonWriter writer, Sort value, JsonSerializerOptions options)
	{
		writer.WriteStartArray();

		foreach (var sort in value)
		{
			if (sort is null)
				continue;

			if (sort is FieldSort fieldSort)
				WriteFieldSort(writer, fieldSort, options);

			// TODO - Other types
		}

		writer.WriteEndArray();
	}

	private void WriteFieldSort(Utf8JsonWriter writer, FieldSort fieldSort, JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WritePropertyName(_settings.Inferrer.Field(fieldSort.Field));
		writer.WriteStartObject();

		if (fieldSort.Order is not null)
		{
			writer.WritePropertyName("order");
			JsonSerializer.Serialize(writer, fieldSort.Order, options);
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

		if (fieldSort.Mode is not null)
		{
			writer.WritePropertyName("mode");
			JsonSerializer.Serialize(writer, fieldSort.Mode, options);
		}

		if (fieldSort.NumericType is not null)
		{
			writer.WritePropertyName("numeric_type");
			JsonSerializer.Serialize(writer, fieldSort.NumericType, options);
		}

		if (fieldSort.Nested is not null)
		{
			writer.WritePropertyName("nested");
			JsonSerializer.Serialize(writer, fieldSort.Nested, options);
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

	private FieldSort() { }

	public FieldSort(Field field) => Field = field;

	public Field Field { get; private set; }

	public string? Format { get; set; }

	public object? Missing { get; set; } // TODO - Decide on final type for this

	public FieldSortNumericType? NumericType { get; set; }

	public NestedSort? Nested { get; set; }

	public bool? IgnoreUnmapped { get; set; }

	public FieldType? UnmappedType { get; set; }
}

public sealed class GeoDistanceSort : SortBase
{
	// TODO - Support geopoint against a field name.

	public GeoDistanceType? DistanceType { get; set; }

	public Field Field { get; set; }

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

public sealed class SortDescriptor<T> : DescriptorPromiseBase<SortDescriptor<T>, IList<SortBase>>
{
	public SortDescriptor() : base(new List<SortBase>()) { }

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

	//public SortDescriptor<T> Field(Action<FieldSortDescriptor<T>> sortSelector) =>
	//	AddSort(sortSelector?.Invoke(new FieldSortDescriptor<T>()));

	public SortDescriptor<T> Field(Field field, SortOrder order) => AddSort(new FieldSort(field) { Order = order });

	public SortDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field, SortOrder order) =>
		AddSort(new FieldSort(field) { Order = order });

	//public SortDescriptor<T> GeoDistance(Action<GeoDistanceSortDescriptor<T>> sortSelector) =>
	//	AddSort(sortSelector?.Invoke(new GeoDistanceSortDescriptor<T>()));

	//public SortDescriptor<T> Script(Func<ScriptSortDescriptor<T>, IScriptSort> sortSelector) =>
	//	AddSort(sortSelector?.Invoke(new ScriptSortDescriptor<T>()));

	private SortDescriptor<T> AddSort(SortBase sort) => sort == null ? this : Assign(sort, (a, v) => a.Add(v));
}
