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
	public override Sort? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

	public override void Write(Utf8JsonWriter writer, Sort value, JsonSerializerOptions options)
	{
		writer.WriteStartArray();

		foreach (var sort in value)
		{
			var type = sort.GetType();
			JsonSerializer.Serialize(writer, sort, type, options);
		}

		writer.WriteEndArray();
	}
}

public abstract class SortBase
{
	public string Format { get; set; }

	public object Missing { get; set; }

	public SortMode? Mode { get; set; }

	public NumericType? NumericType { get; set; }

	public NestedSort Nested { get; set; }

	public SortOrder? Order { get; set; }

	protected abstract Field SortKey { get; }
}

public sealed class FieldSort : SortBase
{
	private const string ShardDoc = "_shard_doc";

	public static readonly IList<SortBase> ByDocumentOrder = new ReadOnlyCollection<SortBase>(new List<SortBase> { new FieldSort { Field = "_doc" } });

	public static readonly IList<SortBase> ByShardDocumentOrder = new ReadOnlyCollection<SortBase>(new List<SortBase> { new FieldSort { Field = ShardDoc } });

	public static readonly FieldSort ShardDocumentOrderAscending = new() { Field = ShardDoc, Order = SortOrder.Asc };

	public static readonly FieldSort ShardDocumentOrderDescending = new() { Field = ShardDoc, Order = SortOrder.Desc };

	public Field Field { get; set; }
	public bool? IgnoreUnmappedFields { get; set; }
	public FieldType? UnmappedType { get; set; }
	protected override Field SortKey => Field;
}

internal sealed class FieldSortConverter : JsonConverter<FieldSort>
{
	public override FieldSort? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
	public override void Write(Utf8JsonWriter writer, FieldSort value, JsonSerializerOptions options) => throw new NotImplementedException();
}

public sealed class GeoDistanceSort : SortBase
{
	public GeoDistanceType? DistanceType { get; set; }

	public Field Field { get; set; }

	public DistanceUnit? Unit { get; set; }

	public bool? IgnoreUnmapped { get; set; }

	protected override Field SortKey => "_geo_distance";
}

public sealed class NestedSort
{
	public QueryContainer Filter { get; set; }

	public NestedSort Nested { get; set; }

	public Field Path { get; set; }

	public int? MaxChildren { get; set; }
}

[JsonConverter(typeof(NumericTypeConverter))]
public enum NumericType
{
	Long,
	Double,
	Date,
	DateNanos
}

public class NumericTypeConverter : JsonConverter<NumericType>
{
	public override NumericType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "long":
				return NumericType.Long;
			case "double":
				return NumericType.Double;
			case "date":
				return NumericType.Date;
			case "date_nanos":
				return NumericType.DateNanos;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, NumericType value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case NumericType.Long:
				writer.WriteStringValue("long");
				return;
			case NumericType.Double:
				writer.WriteStringValue("double");
				return;
			case NumericType.Date:
				writer.WriteStringValue("date");
				return;
			case NumericType.DateNanos:
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
		Assign(objectPath, (a, v) => a.Add(new FieldSort { Field = v, Order = SortOrder.Asc }));

	public SortDescriptor<T> Descending<TValue>(Expression<Func<T, TValue>> objectPath) =>
		Assign(objectPath, (a, v) => a.Add(new FieldSort { Field = v, Order = SortOrder.Desc }));

	public SortDescriptor<T> Ascending(Field field) => Assign(field, (a, v) => a.Add(new FieldSort { Field = v, Order = SortOrder.Asc }));

	public SortDescriptor<T> Descending(Field field) => Assign(field, (a, v) => a.Add(new FieldSort { Field = v, Order = SortOrder.Desc }));

	public SortDescriptor<T> Ascending(SortSpecialField field) =>
		Assign(field == SortSpecialField.Score ? "_score" : field == SortSpecialField.DocumentIndexOrder ? "_doc" : "_shard_doc", (a, v) => a.Add(new FieldSort { Field = v, Order = SortOrder.Asc }));

	public SortDescriptor<T> Descending(SortSpecialField field) =>
		Assign(field == SortSpecialField.Score ? "_score" : field == SortSpecialField.DocumentIndexOrder ? "_doc" : "_shard_doc", (a, v) => a.Add(new FieldSort { Field = v, Order = SortOrder.Desc }));

	//public SortDescriptor<T> Field(Action<FieldSortDescriptor<T>> sortSelector) =>
	//	AddSort(sortSelector?.Invoke(new FieldSortDescriptor<T>()));

	public SortDescriptor<T> Field(Field field, SortOrder order) => AddSort(new FieldSort { Field = field, Order = order });

	public SortDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field, SortOrder order) =>
		AddSort(new FieldSort { Field = field, Order = order });

	//public SortDescriptor<T> GeoDistance(Action<GeoDistanceSortDescriptor<T>> sortSelector) =>
	//	AddSort(sortSelector?.Invoke(new GeoDistanceSortDescriptor<T>()));

	//public SortDescriptor<T> Script(Func<ScriptSortDescriptor<T>, IScriptSort> sortSelector) =>
	//	AddSort(sortSelector?.Invoke(new ScriptSortDescriptor<T>()));

	private SortDescriptor<T> AddSort(SortBase sort) => sort == null ? this : Assign(sort, (a, v) => a.Add(v));
}
