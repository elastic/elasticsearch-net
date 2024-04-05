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

using Elastic.Clients.Elasticsearch.Core;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using System;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Core.SearchMvt;

[JsonConverter(typeof(GridAggregationTypeConverter))]
public enum GridAggregationType
{
	[EnumMember(Value = "geotile")]
	Geotile,
	[EnumMember(Value = "geohex")]
	Geohex
}

internal sealed class GridAggregationTypeConverter : JsonConverter<GridAggregationType>
{
	public override GridAggregationType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "geotile":
				return GridAggregationType.Geotile;
			case "geohex":
				return GridAggregationType.Geohex;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, GridAggregationType value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case GridAggregationType.Geotile:
				writer.WriteStringValue("geotile");
				return;
			case GridAggregationType.Geohex:
				writer.WriteStringValue("geohex");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(GridTypeConverter))]
public enum GridType
{
	[EnumMember(Value = "point")]
	Point,
	[EnumMember(Value = "grid")]
	Grid,
	[EnumMember(Value = "centroid")]
	Centroid
}

internal sealed class GridTypeConverter : JsonConverter<GridType>
{
	public override GridType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "point":
				return GridType.Point;
			case "grid":
				return GridType.Grid;
			case "centroid":
				return GridType.Centroid;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, GridType value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case GridType.Point:
				writer.WriteStringValue("point");
				return;
			case GridType.Grid:
				writer.WriteStringValue("grid");
				return;
			case GridType.Centroid:
				writer.WriteStringValue("centroid");
				return;
		}

		writer.WriteNullValue();
	}
}