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



internal sealed class ScriptBaseConverter : JsonConverter<ScriptBase>
{
	public override ScriptBase? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

	public override void Write(Utf8JsonWriter writer, ScriptBase value, JsonSerializerOptions options)
	{
		if (value is InlineScript scriptSort)
			JsonSerializer.Serialize<InlineScript>(writer, scriptSort, options);

		else if (value is StoredScriptId storedScript)
			JsonSerializer.Serialize<StoredScriptId>(writer, storedScript, options);

		else
			throw new JsonException("Unsupported script implementation");
	}
}

internal static class ScriptSerializationHelpers
{
	public static ScriptBase ReadScriptSort(ref Utf8JsonReader reader, JsonSerializerOptions options)
	{
		var readAheadCopy = reader;

		readAheadCopy.Read();
		readAheadCopy.Read(); // {

		if (readAheadCopy.TokenType != JsonTokenType.PropertyName)
			throw new JsonException("Unexpected token type");

		if (readAheadCopy.ValueTextEquals("id"))
			return JsonSerializer.Deserialize<StoredScriptId>(ref reader, options);

		return JsonSerializer.Deserialize<InlineScript>(ref reader, options);
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

	public static void WriteScriptSort(Utf8JsonWriter writer, ScriptSort scriptSort, JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("_script");
		writer.WriteStartObject();

		if (scriptSort.Order.HasValue)
		{
			writer.WritePropertyName("order");
			JsonSerializer.Serialize(writer, scriptSort.Order.Value, options);
		}

		if (scriptSort.Script is not null)
		{
			writer.WritePropertyName("script");
			var type = scriptSort.Script.GetType();
			JsonSerializer.Serialize(writer, scriptSort.Script, type, options);
		}

		if (scriptSort.Type.HasValue)
		{
			writer.WritePropertyName("type");
			JsonSerializer.Serialize(writer, scriptSort.Type.Value, options);
		}

		if (scriptSort.Mode.HasValue)
		{
			writer.WritePropertyName("mode");
			JsonSerializer.Serialize(writer, scriptSort.Mode.Value, options);
		}

		if (scriptSort.Nested is not null)
		{
			writer.WritePropertyName("nested");
			JsonSerializer.Serialize(writer, scriptSort.Nested, options);
		}

		writer.WriteEndObject();
		writer.WriteEndObject();
	}

	public static void WriteGeoDistanceSort(Utf8JsonWriter writer, GeoDistanceSort geoDistanceSort, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("_geo_distance");
		writer.WriteStartObject();

		if (geoDistanceSort.Order.HasValue)
		{
			writer.WritePropertyName("order");
			JsonSerializer.Serialize(writer, geoDistanceSort.Order.Value, options);
		}

		if (geoDistanceSort.DistanceType.HasValue)
		{
			writer.WritePropertyName("distance_type");
			JsonSerializer.Serialize(writer, geoDistanceSort.DistanceType.Value, options);
		}

		if (geoDistanceSort.Mode.HasValue)
		{
			writer.WritePropertyName("mode");
			JsonSerializer.Serialize(writer, geoDistanceSort.Mode.Value, options);
		}

		if (geoDistanceSort.Unit.HasValue)
		{
			writer.WritePropertyName("unit");
			JsonSerializer.Serialize(writer, geoDistanceSort.Unit.Value, options);
		}

		if (geoDistanceSort.IgnoreUnmapped.HasValue)
		{
			writer.WritePropertyName("ignore_unmapped");
			JsonSerializer.Serialize(writer, geoDistanceSort.IgnoreUnmapped.Value, options);
		}

		if (geoDistanceSort.Field is not null && geoDistanceSort.GeoPoints is not null)
		{
			writer.WritePropertyName(settings.Inferrer.Field(geoDistanceSort.Field));
			JsonSerializer.Serialize(writer, geoDistanceSort.GeoPoints, options);
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

public sealed class ScriptSort : SortBase
{
	[JsonConverter(typeof(ScriptBaseConverter))]
	public ScriptBase Script { get; set; }

	public ScriptSortType? Type { get; set; }

	public NestedSort? Nested { get; set; }
}

public sealed class ScriptSortDescriptor<T> : SortDescriptorBase<ScriptSortDescriptor<T>, T>
{
	private SortMode? _sortMode;
	private NestedSort _nestedSort;
	private NestedSortDescriptor<T> _nestedSortDescriptor;
	private Action<NestedSortDescriptor<T>> _nestedSortDescriptorAction;
	private SortOrder? _order;
	private ScriptSortType? _scriptSortType;
	private ScriptBase _script;
	private InlineScriptDescriptor _inlineScriptDescriptor;
	private Action<InlineScriptDescriptor> _inlineScriptDescriptorAction;

	// TODO - Stored Script - Is that supported??

	/// <summary>
	/// Sorts by ascending sort order.
	/// </summary>
	public ScriptSortDescriptor<T> Ascending() => Assign(SortOrder.Asc, (a, v) => Self._order = v);

	/// <summary>
	/// Sorts by descending sort order.
	/// </summary>
	public ScriptSortDescriptor<T> Descending() => Assign(SortOrder.Desc, (a, v) => a._order = v);

	public ScriptSortDescriptor<T> Mode(SortMode? mode) => Assign(mode, (a, v) => a._sortMode = v);

	public ScriptSortDescriptor<T> Nested(NestedSort nestedSort) => Assign(nestedSort, (a, v) => a._nestedSort = v);

	public ScriptSortDescriptor<T> Nested(NestedSortDescriptor<T> descriptor) =>
		Assign(descriptor, (a, v) => a._nestedSortDescriptor = v);

	public ScriptSortDescriptor<T> Nested(Action<NestedSortDescriptor<T>> configure) =>
		Assign(configure, (a, v) => a._nestedSortDescriptorAction = v);

	public ScriptSortDescriptor<T> Order(SortOrder? order) => Assign(order, (a, v) => a._order = v);

	public ScriptSortDescriptor<T> Script(ScriptBase script) => Assign(script, (a, v) => a._script = v);

	public ScriptSortDescriptor<T> Script(InlineScriptDescriptor descriptor) =>
		Assign(descriptor, (a, v) => a._inlineScriptDescriptor = v);

	public ScriptSortDescriptor<T> Script(Action<InlineScriptDescriptor> configure) =>
		Assign(configure, (a, v) => a._inlineScriptDescriptorAction = v);

	public ScriptSortDescriptor<T> Type(ScriptSortType? type) => Assign(type, (a, v) => a._scriptSortType = v);

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("_script");
		writer.WriteStartObject();

		if (_order.HasValue)
		{
			writer.WritePropertyName("order");
			JsonSerializer.Serialize(writer, _order.Value, options);
		}

		if (_sortMode.HasValue)
		{
			writer.WritePropertyName("mode");
			JsonSerializer.Serialize(writer, _sortMode.Value, options);
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

		if (_script is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, _script, options);
		}
		else if (_inlineScriptDescriptor is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, _inlineScriptDescriptor, options);
		}
		else if (_inlineScriptDescriptorAction is not null)
		{
			writer.WritePropertyName("script");
			var descriptor = new InlineScriptDescriptor();
			_inlineScriptDescriptorAction(descriptor);
			JsonSerializer.Serialize(writer, descriptor, options);
		}

		if (_scriptSortType.HasValue)
		{
			writer.WritePropertyName("type");
			JsonSerializer.Serialize(writer, _scriptSortType.Value, options);
		}

		writer.WriteEndObject();
		writer.WriteEndObject();
	}

	//internal FieldSort ToFieldSort() => null;
}

[JsonConverter(typeof(ScriptSortTypeConverter))]
public enum ScriptSortType
{
	String,
	Number
}

internal sealed class ScriptSortTypeConverter : JsonConverter<ScriptSortType>
{
	public override ScriptSortType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "string":
				return ScriptSortType.String;
			case "number":
				return ScriptSortType.Number;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, ScriptSortType value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case ScriptSortType.String:
				writer.WriteStringValue("string");
				return;
			case ScriptSortType.Number:
				writer.WriteStringValue("number");
				return;
		}

		writer.WriteNullValue();
	}
}

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

public sealed class GeoDistanceSortDescriptor<T> : DescriptorBase<GeoDistanceSortDescriptor<T>>
{
	private Field _field;
	private GeoDistanceType? _geoDistanceType;
	private SortMode? _sortMode;
	private SortOrder? _order;
	private DistanceUnit? _distanceUnit;
	private GeoPoints _points;
	private bool? _ignoreUnmappedFields;

	public GeoDistanceSortDescriptor<T> DistanceType(GeoDistanceType distanceType) => Assign(distanceType, (a, v) => a._geoDistanceType = v);

	public GeoDistanceSortDescriptor<T> Field(Field field) => Assign(field, (a, v) => a._field = v);

	public GeoDistanceSortDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a._field = v);

	public GeoDistanceSortDescriptor<T> GeoPoints(params GeoPoint[] geoPoints) => Assign(geoPoints, (a, v) => a._points = v);

	public GeoDistanceSortDescriptor<T> GeoPoints(IEnumerable<GeoPoint> geoPoints) => Assign(geoPoints, (a, v) => a._points = new GeoPoints(v));

	public GeoDistanceSortDescriptor<T> IgnoreUnmappedFields(bool? ignore = true) => Assign(ignore, (a, v) => a._ignoreUnmappedFields = v);

	public GeoDistanceSortDescriptor<T> Mode(SortMode mode) => Assign(mode, (a, v) => a._sortMode = v);

	public GeoDistanceSortDescriptor<T> Order(SortOrder order) => Assign(order, (a, v) => a._order = v);

	public GeoDistanceSortDescriptor<T> Unit(DistanceUnit mode) => Assign(mode, (a, v) => a._distanceUnit = v);

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("_geo_distance");
		writer.WriteStartObject();

		if (_geoDistanceType.HasValue)
		{
			writer.WritePropertyName("distance_type");
			JsonSerializer.Serialize(writer, _geoDistanceType.Value, options);
		}

		if (_distanceUnit.HasValue)
		{
			writer.WritePropertyName("unit");
			JsonSerializer.Serialize(writer, _distanceUnit.Value, options);
		}

		if (_ignoreUnmappedFields.HasValue)
		{
			writer.WritePropertyName("ignore_unmapped");
			JsonSerializer.Serialize(writer, _ignoreUnmappedFields.Value, options);
		}

		if (_order.HasValue)
		{
			writer.WritePropertyName("order");
			JsonSerializer.Serialize(writer, _order.Value, options);
		}

		if (_sortMode.HasValue)
		{
			writer.WritePropertyName("mode");
			JsonSerializer.Serialize(writer, _sortMode.Value, options);
		}

		if (_field is not null && _points is not null)
		{
			writer.WritePropertyName(settings.Inferrer.Field(_field));
			JsonSerializer.Serialize(writer, _points, options);
		}

		writer.WriteEndObject();
		writer.WriteEndObject();
	}
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
	private List<Action<ScriptSortDescriptor<T>>> _scriptSortDescriptorActions;
	private List<Action<GeoDistanceSortDescriptor<T>>> _geoDistanceSortDescriptorActions;
	private readonly List<byte> _serializationOrderTracker = new();

	public SortDescriptor<T> Ascending<TValue>(Expression<Func<T, TValue>> objectPath) =>
		Assign(objectPath, (a, v) => a.Add(new FieldSort(v) { Order = SortOrder.Asc }), a => a._serializationOrderTracker.Add(0));

	public SortDescriptor<T> Descending<TValue>(Expression<Func<T, TValue>> objectPath) =>
		Assign(objectPath, (a, v) => a.Add(new FieldSort(v) { Order = SortOrder.Desc }), a => a._serializationOrderTracker.Add(0));

	public SortDescriptor<T> Ascending(Field field) => Assign(field, (a, v) => {  a.Add(new FieldSort(v) { Order = SortOrder.Asc }); }, a => a._serializationOrderTracker.Add(0));

	public SortDescriptor<T> Descending(Field field) => Assign(field, (a, v) => a.Add(new FieldSort(v) { Order = SortOrder.Desc }), a => a._serializationOrderTracker.Add(0));

	public SortDescriptor<T> Ascending(SortSpecialField field) =>
		Assign(field == SortSpecialField.Score ? "_score" : field == SortSpecialField.DocumentIndexOrder ? "_doc" : "_shard_doc", (a, v) => a.Add(new FieldSort(v) { Order = SortOrder.Asc }), a => a._serializationOrderTracker.Add(0));

	public SortDescriptor<T> Descending(SortSpecialField field) =>
		Assign(field == SortSpecialField.Score ? "_score" : field == SortSpecialField.DocumentIndexOrder ? "_doc" : "_shard_doc", (a, v) => a.Add(new FieldSort(v) { Order = SortOrder.Desc }), a => a._serializationOrderTracker.Add(0));

	public SortDescriptor<T> Field(Action<FieldSortDescriptor<T>> configure)
	{
		// TODO - Future idea: Store the actions and invoke at serialisation time

		//var descriptor = new FieldSortDescriptor<T>();
		//configure?.Invoke(descriptor);

		if (_fieldSortDescriptorActions is null)
			_fieldSortDescriptorActions = new List<Action<FieldSortDescriptor<T>>>();

		_fieldSortDescriptorActions.Add(configure);

		_serializationOrderTracker.Add(1);

		//return AddSort(descriptor.ToFieldSort());

		return this;
	}

	public SortDescriptor<T> Field(Field field, SortOrder order) => AddSort(new FieldSort(field) { Order = order });

	public SortDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field, SortOrder order) =>
		AddSort(new FieldSort(field) { Order = order });

	public SortDescriptor<T> GeoDistance(Action<GeoDistanceSortDescriptor<T>> configure)
	{
		if (_geoDistanceSortDescriptorActions is null)
			_geoDistanceSortDescriptorActions = new List<Action<GeoDistanceSortDescriptor<T>>>();

		_geoDistanceSortDescriptorActions.Add(configure);

		_serializationOrderTracker.Add(2);

		return this;
	}

	public SortDescriptor<T> Script(Action<ScriptSortDescriptor<T>> configure)
	{
		if (_scriptSortDescriptorActions is null)
			_scriptSortDescriptorActions = new List<Action<ScriptSortDescriptor<T>>>();

		_scriptSortDescriptorActions.Add(configure);

		_serializationOrderTracker.Add(3);

		return this;
	}

	private SortDescriptor<T> AddSort(SortBase sort) => sort == null ? this : Assign(sort, (a, v) => a.Add(v), a => a._serializationOrderTracker.Add(0));

	public void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartArray();

		var sortsIndex = 0;
		var fieldsDescriptorIndex = 0;
		var scriptsDescriptorIndex = 0;
		var geoDescriptorIndex = 0;

		foreach (var item in _serializationOrderTracker)
		{
			if (item == 0)
			{
				var sort = PromisedValue[sortsIndex++];

				if (sort is null)
					continue;

				if (sort is FieldSort fieldSort)
				{
					SortSerializationHelpers.WriteFieldSort(writer, fieldSort, options, settings);
					continue;
				}

				if (sort is ScriptSort scriptSort)
				{
					SortSerializationHelpers.WriteScriptSort(writer, scriptSort, options);
					continue;
				}

				if (sort is GeoDistanceSort geoDistanceSort)
				{
					SortSerializationHelpers.WriteGeoDistanceSort(writer, geoDistanceSort, options, settings);
					continue;
				}

				// TODO - Other types
				throw new NotImplementedException("The sort type is not currently supported in this release.");
			}
			else if (item == 1)
			{
				var action = _fieldSortDescriptorActions[fieldsDescriptorIndex++];
				var descriptor = new FieldSortDescriptor<T>();
				action(descriptor);
				JsonSerializer.Serialize(writer, descriptor, options);
			}
			else if (item == 2)
			{
				var action = _geoDistanceSortDescriptorActions[geoDescriptorIndex++];
				var descriptor = new GeoDistanceSortDescriptor<T>();
				action(descriptor);
				JsonSerializer.Serialize(writer, descriptor, options);
			}
			else if (item == 3)
			{
				var action = _scriptSortDescriptorActions[scriptsDescriptorIndex++];
				var descriptor = new ScriptSortDescriptor<T>();
				action(descriptor);
				JsonSerializer.Serialize(writer, descriptor, options);
			}
		}

		//foreach (var sort in PromisedValue)
		//{
		//	if (sort is null)
		//		continue;

		//	if (sort is FieldSort fieldSort)
		//	{
		//		SortSerializationHelpers.WriteFieldSort(writer, fieldSort, options, settings);
		//		continue;
		//	}

		//	if (sort is ScriptSort scriptSort)
		//	{
		//		SortSerializationHelpers.WriteScriptSort(writer, scriptSort, options);
		//		continue;
		//	}

		//	if (sort is GeoDistanceSort geoDistanceSort)
		//	{
		//		SortSerializationHelpers.WriteGeoDistanceSort(writer, geoDistanceSort, options, settings);
		//		continue;
		//	}

		//	// TODO - Other types
		//	throw new NotImplementedException("The sort type is not currently supported in this release.");
		//}

		//if (_fieldSortDescriptorActions is not null)
		//{
		//	foreach (var action in _fieldSortDescriptorActions)
		//	{
		//		var descriptor = new FieldSortDescriptor<T>();
		//		action(descriptor);
		//		JsonSerializer.Serialize(writer, descriptor, options);
		//	}
		//}

		//if (_geoDistanceSortDescriptorActions is not null)
		//{
		//	foreach (var action in _geoDistanceSortDescriptorActions)
		//	{
		//		var descriptor = new GeoDistanceSortDescriptor<T>();
		//		action(descriptor);
		//		JsonSerializer.Serialize(writer, descriptor, options);
		//	}
		//}

		//if (_scriptSortDescriptorActions is not null)
		//{
		//	foreach (var action in _scriptSortDescriptorActions)
		//	{
		//		var descriptor = new ScriptSortDescriptor<T>();
		//		action(descriptor);
		//		JsonSerializer.Serialize(writer, descriptor, options);
		//	}
		//}

		writer.WriteEndArray();
	}
}
