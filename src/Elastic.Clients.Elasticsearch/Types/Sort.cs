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
	public override ScriptBase? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var readAheadCopy = reader;

		if (readAheadCopy.TokenType == JsonTokenType.String)
		{
			var source = reader.GetString();
			return new InlineScript(source);
		}

		readAheadCopy.Read(); // {

		if (readAheadCopy.TokenType != JsonTokenType.PropertyName)
			throw new JsonException("Unexpected token type");

		if (readAheadCopy.ValueTextEquals("params"))
		{
			while (readAheadCopy.Read() && readAheadCopy.TokenType != JsonTokenType.EndObject)
			{
			}

			readAheadCopy.Read();
		}

		if (readAheadCopy.ValueTextEquals("id"))
			return JsonSerializer.Deserialize<StoredScriptId>(ref reader, options);

		return JsonSerializer.Deserialize<InlineScript>(ref reader, options);
	}

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



// TODO - We can have an option that marks a type as having a custom converter
// so that this attribute is added
[JsonConverter(typeof(ScriptBaseConverter))]
public abstract partial class ScriptBase
{
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

//[JsonConverter(typeof(ScriptSortTypeConverter))]
//public enum ScriptSortType
//{
//	String,
//	Number
//}

//internal sealed class ScriptSortTypeConverter : JsonConverter<ScriptSortType>
//{
//	public override ScriptSortType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//	{
//		var enumString = reader.GetString();
//		switch (enumString)
//		{
//			case "string":
//				return ScriptSortType.String;
//			case "number":
//				return ScriptSortType.Number;
//		}

//		ThrowHelper.ThrowJsonException();
//		return default;
//	}

//	public override void Write(Utf8JsonWriter writer, ScriptSortType value, JsonSerializerOptions options)
//	{
//		switch (value)
//		{
//			case ScriptSortType.String:
//				writer.WriteStringValue("string");
//				return;
//			case ScriptSortType.Number:
//				writer.WriteStringValue("number");
//				return;
//		}

//		writer.WriteNullValue();
//	}
//}



public interface ISort { }

public abstract class SortDescriptorBase<TDescriptor> : SerializableDescriptorBase<TDescriptor> where TDescriptor : SerializableDescriptorBase<TDescriptor>
{
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
