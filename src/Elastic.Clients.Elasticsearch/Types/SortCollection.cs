// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

/// <summary>
/// Represents a collection of sort conditions to apply to a query.
/// </summary>
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

internal sealed class SortCollectionConverter : JsonConverter<SortCollection>
{
	private readonly IElasticsearchClientSettings _settings;

	public SortCollectionConverter(IElasticsearchClientSettings settings) => _settings = settings;

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
				return ReadGeoDistanceSort(ref reader, options);
			}
			else if (!string.IsNullOrEmpty(value) && value == "_script")
			{
				return ReadScriptSort(ref reader, options);
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

				if (reader.ValueTextEquals("unit"))
				{
					var unit = JsonSerializer.Deserialize<DistanceUnit>(ref reader, options);
					geoDistanceSort.Unit = unit;
					continue;
				}

				if (reader.ValueTextEquals("distance_type"))
				{
					var geoDistanceType = JsonSerializer.Deserialize<GeoDistanceType>(ref reader, options);
					geoDistanceSort.DistanceType = geoDistanceType;
					continue;
				}

				if (reader.ValueTextEquals("ignore_unmapped"))
				{
					var ignoreUnmapped = JsonSerializer.Deserialize<bool>(ref reader, options);
					geoDistanceSort.IgnoreUnmapped = ignoreUnmapped;
					continue;
				}

				// If we get this far, we must be on the field name for the sort

				geoDistanceSort.Field = reader.GetString();
				reader.Read();
				geoDistanceSort.GeoPoints = JsonSerializer.Deserialize<GeoPoints>(ref reader, options);
			}
		}

		return geoDistanceSort;
	}

	private ScriptSort ReadScriptSort(ref Utf8JsonReader reader, JsonSerializerOptions options)
	{
		reader.Read();

		if (reader.TokenType != JsonTokenType.PropertyName)
			throw new JsonException("Unexpected JSON token. Expected property name.");

		var field = reader.GetString();

		if (field != "_script")
			throw new JsonException("Invalid script sort object.");

		reader.Read();

		if (reader.TokenType != JsonTokenType.StartObject)
			throw new JsonException("Unexpected JSON token. Expected start object.");

		var scriptSort = new ScriptSort();

		while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
		{
			if (reader.TokenType == JsonTokenType.PropertyName)
			{
				if (reader.ValueTextEquals("order"))
				{
					var order = JsonSerializer.Deserialize<SortOrder>(ref reader, options);
					scriptSort.Order = order;
					continue;
				}

				if (reader.ValueTextEquals("script"))
				{
					var script = ScriptSerializationHelpers.ReadScriptSort(ref reader, options);
					scriptSort.Script = script;
					continue;
				}

				if (reader.ValueTextEquals("type"))
				{
					var scriptSortType = JsonSerializer.Deserialize<ScriptSortType>(ref reader, options);
					scriptSort.Type = scriptSortType;
					continue;
				}

				if (reader.ValueTextEquals("mode"))
				{
					var sortMode = JsonSerializer.Deserialize<SortMode>(ref reader, options);
					scriptSort.Mode = sortMode;
					continue;
				}

				if (reader.ValueTextEquals("nested"))
				{
					var nested = JsonSerializer.Deserialize<NestedSort>(ref reader, options);
					scriptSort.Nested = nested;
					continue;
				}
			}
		}

		return scriptSort;
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

			if (sort is ScriptSort scriptSort)
			{
				SortSerializationHelpers.WriteScriptSort(writer, scriptSort, options);
				continue;
			}

			if (sort is GeoDistanceSort geoDistanceSort)
			{
				SortSerializationHelpers.WriteGeoDistanceSort(writer, geoDistanceSort, options, _settings);
				continue;
			}

			throw new NotImplementedException("The sort type is not currently supported in this release.");
		}

		writer.WriteEndArray();
	}
}
