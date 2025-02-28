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

namespace Elastic.Clients.Elasticsearch.Ingest;

[JsonConverter(typeof(ConvertTypeConverter))]
public enum ConvertType
{
	[EnumMember(Value = "string")]
	String,
	[EnumMember(Value = "long")]
	Long,
	[EnumMember(Value = "ip")]
	Ip,
	[EnumMember(Value = "integer")]
	Integer,
	[EnumMember(Value = "float")]
	Float,
	[EnumMember(Value = "double")]
	Double,
	[EnumMember(Value = "boolean")]
	Boolean,
	[EnumMember(Value = "auto")]
	Auto
}

internal sealed partial class ConvertTypeConverter : System.Text.Json.Serialization.JsonConverter<ConvertType>
{
	private static readonly System.Text.Json.JsonEncodedText MemberString = System.Text.Json.JsonEncodedText.Encode("string");
	private static readonly System.Text.Json.JsonEncodedText MemberLong = System.Text.Json.JsonEncodedText.Encode("long");
	private static readonly System.Text.Json.JsonEncodedText MemberIp = System.Text.Json.JsonEncodedText.Encode("ip");
	private static readonly System.Text.Json.JsonEncodedText MemberInteger = System.Text.Json.JsonEncodedText.Encode("integer");
	private static readonly System.Text.Json.JsonEncodedText MemberFloat = System.Text.Json.JsonEncodedText.Encode("float");
	private static readonly System.Text.Json.JsonEncodedText MemberDouble = System.Text.Json.JsonEncodedText.Encode("double");
	private static readonly System.Text.Json.JsonEncodedText MemberBoolean = System.Text.Json.JsonEncodedText.Encode("boolean");
	private static readonly System.Text.Json.JsonEncodedText MemberAuto = System.Text.Json.JsonEncodedText.Encode("auto");

	public override ConvertType Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		if (reader.ValueTextEquals(MemberString))
		{
			return ConvertType.String;
		}

		if (reader.ValueTextEquals(MemberLong))
		{
			return ConvertType.Long;
		}

		if (reader.ValueTextEquals(MemberIp))
		{
			return ConvertType.Ip;
		}

		if (reader.ValueTextEquals(MemberInteger))
		{
			return ConvertType.Integer;
		}

		if (reader.ValueTextEquals(MemberFloat))
		{
			return ConvertType.Float;
		}

		if (reader.ValueTextEquals(MemberDouble))
		{
			return ConvertType.Double;
		}

		if (reader.ValueTextEquals(MemberBoolean))
		{
			return ConvertType.Boolean;
		}

		if (reader.ValueTextEquals(MemberAuto))
		{
			return ConvertType.Auto;
		}

		var value = reader.GetString()!;
		if (string.Equals(value, MemberString.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return ConvertType.String;
		}

		if (string.Equals(value, MemberLong.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return ConvertType.Long;
		}

		if (string.Equals(value, MemberIp.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return ConvertType.Ip;
		}

		if (string.Equals(value, MemberInteger.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return ConvertType.Integer;
		}

		if (string.Equals(value, MemberFloat.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return ConvertType.Float;
		}

		if (string.Equals(value, MemberDouble.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return ConvertType.Double;
		}

		if (string.Equals(value, MemberBoolean.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return ConvertType.Boolean;
		}

		if (string.Equals(value, MemberAuto.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return ConvertType.Auto;
		}

		throw new System.Text.Json.JsonException($"Unknown member '{value}' for enum '{nameof(ConvertType)}'.");
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, ConvertType value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value)
		{
			case ConvertType.String:
				writer.WriteStringValue(MemberString);
				break;
			case ConvertType.Long:
				writer.WriteStringValue(MemberLong);
				break;
			case ConvertType.Ip:
				writer.WriteStringValue(MemberIp);
				break;
			case ConvertType.Integer:
				writer.WriteStringValue(MemberInteger);
				break;
			case ConvertType.Float:
				writer.WriteStringValue(MemberFloat);
				break;
			case ConvertType.Double:
				writer.WriteStringValue(MemberDouble);
				break;
			case ConvertType.Boolean:
				writer.WriteStringValue(MemberBoolean);
				break;
			case ConvertType.Auto:
				writer.WriteStringValue(MemberAuto);
				break;
			default:
				throw new System.Text.Json.JsonException($"Invalid value '{value}' for enum '{nameof(ConvertType)}'.");
		}
	}

	public override ConvertType ReadAsPropertyName(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return Read(ref reader, typeToConvert, options);
	}

	public override void WriteAsPropertyName(System.Text.Json.Utf8JsonWriter writer, ConvertType value, System.Text.Json.JsonSerializerOptions options)
	{
		Write(writer, value, options);
	}
}

[JsonConverter(typeof(FingerprintDigestConverter))]
public enum FingerprintDigest
{
	[EnumMember(Value = "SHA-512")]
	Sha512,
	[EnumMember(Value = "SHA-256")]
	Sha256,
	[EnumMember(Value = "SHA-1")]
	Sha1,
	[EnumMember(Value = "MurmurHash3")]
	Murmurhash3,
	[EnumMember(Value = "MD5")]
	Md5
}

internal sealed partial class FingerprintDigestConverter : System.Text.Json.Serialization.JsonConverter<FingerprintDigest>
{
	private static readonly System.Text.Json.JsonEncodedText MemberSha512 = System.Text.Json.JsonEncodedText.Encode("SHA-512");
	private static readonly System.Text.Json.JsonEncodedText MemberSha256 = System.Text.Json.JsonEncodedText.Encode("SHA-256");
	private static readonly System.Text.Json.JsonEncodedText MemberSha1 = System.Text.Json.JsonEncodedText.Encode("SHA-1");
	private static readonly System.Text.Json.JsonEncodedText MemberMurmurhash3 = System.Text.Json.JsonEncodedText.Encode("MurmurHash3");
	private static readonly System.Text.Json.JsonEncodedText MemberMd5 = System.Text.Json.JsonEncodedText.Encode("MD5");

	public override FingerprintDigest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		if (reader.ValueTextEquals(MemberSha512))
		{
			return FingerprintDigest.Sha512;
		}

		if (reader.ValueTextEquals(MemberSha256))
		{
			return FingerprintDigest.Sha256;
		}

		if (reader.ValueTextEquals(MemberSha1))
		{
			return FingerprintDigest.Sha1;
		}

		if (reader.ValueTextEquals(MemberMurmurhash3))
		{
			return FingerprintDigest.Murmurhash3;
		}

		if (reader.ValueTextEquals(MemberMd5))
		{
			return FingerprintDigest.Md5;
		}

		var value = reader.GetString()!;
		if (string.Equals(value, MemberSha512.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return FingerprintDigest.Sha512;
		}

		if (string.Equals(value, MemberSha256.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return FingerprintDigest.Sha256;
		}

		if (string.Equals(value, MemberSha1.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return FingerprintDigest.Sha1;
		}

		if (string.Equals(value, MemberMurmurhash3.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return FingerprintDigest.Murmurhash3;
		}

		if (string.Equals(value, MemberMd5.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return FingerprintDigest.Md5;
		}

		throw new System.Text.Json.JsonException($"Unknown member '{value}' for enum '{nameof(FingerprintDigest)}'.");
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, FingerprintDigest value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value)
		{
			case FingerprintDigest.Sha512:
				writer.WriteStringValue(MemberSha512);
				break;
			case FingerprintDigest.Sha256:
				writer.WriteStringValue(MemberSha256);
				break;
			case FingerprintDigest.Sha1:
				writer.WriteStringValue(MemberSha1);
				break;
			case FingerprintDigest.Murmurhash3:
				writer.WriteStringValue(MemberMurmurhash3);
				break;
			case FingerprintDigest.Md5:
				writer.WriteStringValue(MemberMd5);
				break;
			default:
				throw new System.Text.Json.JsonException($"Invalid value '{value}' for enum '{nameof(FingerprintDigest)}'.");
		}
	}

	public override FingerprintDigest ReadAsPropertyName(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return Read(ref reader, typeToConvert, options);
	}

	public override void WriteAsPropertyName(System.Text.Json.Utf8JsonWriter writer, FingerprintDigest value, System.Text.Json.JsonSerializerOptions options)
	{
		Write(writer, value, options);
	}
}

[JsonConverter(typeof(GeoGridTargetFormatConverter))]
public enum GeoGridTargetFormat
{
	[EnumMember(Value = "wkt")]
	Wkt,
	[EnumMember(Value = "geojson")]
	Geojson
}

internal sealed partial class GeoGridTargetFormatConverter : System.Text.Json.Serialization.JsonConverter<GeoGridTargetFormat>
{
	private static readonly System.Text.Json.JsonEncodedText MemberWkt = System.Text.Json.JsonEncodedText.Encode("wkt");
	private static readonly System.Text.Json.JsonEncodedText MemberGeojson = System.Text.Json.JsonEncodedText.Encode("geojson");

	public override GeoGridTargetFormat Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		if (reader.ValueTextEquals(MemberWkt))
		{
			return GeoGridTargetFormat.Wkt;
		}

		if (reader.ValueTextEquals(MemberGeojson))
		{
			return GeoGridTargetFormat.Geojson;
		}

		var value = reader.GetString()!;
		if (string.Equals(value, MemberWkt.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return GeoGridTargetFormat.Wkt;
		}

		if (string.Equals(value, MemberGeojson.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return GeoGridTargetFormat.Geojson;
		}

		throw new System.Text.Json.JsonException($"Unknown member '{value}' for enum '{nameof(GeoGridTargetFormat)}'.");
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, GeoGridTargetFormat value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value)
		{
			case GeoGridTargetFormat.Wkt:
				writer.WriteStringValue(MemberWkt);
				break;
			case GeoGridTargetFormat.Geojson:
				writer.WriteStringValue(MemberGeojson);
				break;
			default:
				throw new System.Text.Json.JsonException($"Invalid value '{value}' for enum '{nameof(GeoGridTargetFormat)}'.");
		}
	}

	public override GeoGridTargetFormat ReadAsPropertyName(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return Read(ref reader, typeToConvert, options);
	}

	public override void WriteAsPropertyName(System.Text.Json.Utf8JsonWriter writer, GeoGridTargetFormat value, System.Text.Json.JsonSerializerOptions options)
	{
		Write(writer, value, options);
	}
}

[JsonConverter(typeof(GeoGridTileTypeConverter))]
public enum GeoGridTileType
{
	[EnumMember(Value = "geotile")]
	Geotile,
	[EnumMember(Value = "geohex")]
	Geohex,
	[EnumMember(Value = "geohash")]
	Geohash
}

internal sealed partial class GeoGridTileTypeConverter : System.Text.Json.Serialization.JsonConverter<GeoGridTileType>
{
	private static readonly System.Text.Json.JsonEncodedText MemberGeotile = System.Text.Json.JsonEncodedText.Encode("geotile");
	private static readonly System.Text.Json.JsonEncodedText MemberGeohex = System.Text.Json.JsonEncodedText.Encode("geohex");
	private static readonly System.Text.Json.JsonEncodedText MemberGeohash = System.Text.Json.JsonEncodedText.Encode("geohash");

	public override GeoGridTileType Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		if (reader.ValueTextEquals(MemberGeotile))
		{
			return GeoGridTileType.Geotile;
		}

		if (reader.ValueTextEquals(MemberGeohex))
		{
			return GeoGridTileType.Geohex;
		}

		if (reader.ValueTextEquals(MemberGeohash))
		{
			return GeoGridTileType.Geohash;
		}

		var value = reader.GetString()!;
		if (string.Equals(value, MemberGeotile.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return GeoGridTileType.Geotile;
		}

		if (string.Equals(value, MemberGeohex.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return GeoGridTileType.Geohex;
		}

		if (string.Equals(value, MemberGeohash.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return GeoGridTileType.Geohash;
		}

		throw new System.Text.Json.JsonException($"Unknown member '{value}' for enum '{nameof(GeoGridTileType)}'.");
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, GeoGridTileType value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value)
		{
			case GeoGridTileType.Geotile:
				writer.WriteStringValue(MemberGeotile);
				break;
			case GeoGridTileType.Geohex:
				writer.WriteStringValue(MemberGeohex);
				break;
			case GeoGridTileType.Geohash:
				writer.WriteStringValue(MemberGeohash);
				break;
			default:
				throw new System.Text.Json.JsonException($"Invalid value '{value}' for enum '{nameof(GeoGridTileType)}'.");
		}
	}

	public override GeoGridTileType ReadAsPropertyName(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return Read(ref reader, typeToConvert, options);
	}

	public override void WriteAsPropertyName(System.Text.Json.Utf8JsonWriter writer, GeoGridTileType value, System.Text.Json.JsonSerializerOptions options)
	{
		Write(writer, value, options);
	}
}

[JsonConverter(typeof(JsonProcessorConflictStrategyConverter))]
public enum JsonProcessorConflictStrategy
{
	/// <summary>
	/// <para>
	/// Root fields that conflict with fields from the parsed JSON will be overridden.
	/// </para>
	/// </summary>
	[EnumMember(Value = "replace")]
	Replace,
	/// <summary>
	/// <para>
	/// Conflicting fields will be merged.
	/// </para>
	/// </summary>
	[EnumMember(Value = "merge")]
	Merge
}

internal sealed partial class JsonProcessorConflictStrategyConverter : System.Text.Json.Serialization.JsonConverter<JsonProcessorConflictStrategy>
{
	private static readonly System.Text.Json.JsonEncodedText MemberReplace = System.Text.Json.JsonEncodedText.Encode("replace");
	private static readonly System.Text.Json.JsonEncodedText MemberMerge = System.Text.Json.JsonEncodedText.Encode("merge");

	public override JsonProcessorConflictStrategy Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		if (reader.ValueTextEquals(MemberReplace))
		{
			return JsonProcessorConflictStrategy.Replace;
		}

		if (reader.ValueTextEquals(MemberMerge))
		{
			return JsonProcessorConflictStrategy.Merge;
		}

		var value = reader.GetString()!;
		if (string.Equals(value, MemberReplace.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return JsonProcessorConflictStrategy.Replace;
		}

		if (string.Equals(value, MemberMerge.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return JsonProcessorConflictStrategy.Merge;
		}

		throw new System.Text.Json.JsonException($"Unknown member '{value}' for enum '{nameof(JsonProcessorConflictStrategy)}'.");
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, JsonProcessorConflictStrategy value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value)
		{
			case JsonProcessorConflictStrategy.Replace:
				writer.WriteStringValue(MemberReplace);
				break;
			case JsonProcessorConflictStrategy.Merge:
				writer.WriteStringValue(MemberMerge);
				break;
			default:
				throw new System.Text.Json.JsonException($"Invalid value '{value}' for enum '{nameof(JsonProcessorConflictStrategy)}'.");
		}
	}

	public override JsonProcessorConflictStrategy ReadAsPropertyName(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return Read(ref reader, typeToConvert, options);
	}

	public override void WriteAsPropertyName(System.Text.Json.Utf8JsonWriter writer, JsonProcessorConflictStrategy value, System.Text.Json.JsonSerializerOptions options)
	{
		Write(writer, value, options);
	}
}

[JsonConverter(typeof(ShapeTypeConverter))]
public enum ShapeType
{
	[EnumMember(Value = "shape")]
	Shape,
	[EnumMember(Value = "geo_shape")]
	GeoShape
}

internal sealed partial class ShapeTypeConverter : System.Text.Json.Serialization.JsonConverter<ShapeType>
{
	private static readonly System.Text.Json.JsonEncodedText MemberShape = System.Text.Json.JsonEncodedText.Encode("shape");
	private static readonly System.Text.Json.JsonEncodedText MemberGeoShape = System.Text.Json.JsonEncodedText.Encode("geo_shape");

	public override ShapeType Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		if (reader.ValueTextEquals(MemberShape))
		{
			return ShapeType.Shape;
		}

		if (reader.ValueTextEquals(MemberGeoShape))
		{
			return ShapeType.GeoShape;
		}

		var value = reader.GetString()!;
		if (string.Equals(value, MemberShape.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return ShapeType.Shape;
		}

		if (string.Equals(value, MemberGeoShape.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return ShapeType.GeoShape;
		}

		throw new System.Text.Json.JsonException($"Unknown member '{value}' for enum '{nameof(ShapeType)}'.");
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, ShapeType value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value)
		{
			case ShapeType.Shape:
				writer.WriteStringValue(MemberShape);
				break;
			case ShapeType.GeoShape:
				writer.WriteStringValue(MemberGeoShape);
				break;
			default:
				throw new System.Text.Json.JsonException($"Invalid value '{value}' for enum '{nameof(ShapeType)}'.");
		}
	}

	public override ShapeType ReadAsPropertyName(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return Read(ref reader, typeToConvert, options);
	}

	public override void WriteAsPropertyName(System.Text.Json.Utf8JsonWriter writer, ShapeType value, System.Text.Json.JsonSerializerOptions options)
	{
		Write(writer, value, options);
	}
}

[JsonConverter(typeof(UserAgentPropertyConverter))]
public enum UserAgentProperty
{
	[EnumMember(Value = "version")]
	Version,
	[EnumMember(Value = "os")]
	Os,
	[EnumMember(Value = "original")]
	Original,
	[EnumMember(Value = "name")]
	Name,
	[EnumMember(Value = "device")]
	Device
}

internal sealed partial class UserAgentPropertyConverter : System.Text.Json.Serialization.JsonConverter<UserAgentProperty>
{
	private static readonly System.Text.Json.JsonEncodedText MemberVersion = System.Text.Json.JsonEncodedText.Encode("version");
	private static readonly System.Text.Json.JsonEncodedText MemberOs = System.Text.Json.JsonEncodedText.Encode("os");
	private static readonly System.Text.Json.JsonEncodedText MemberOriginal = System.Text.Json.JsonEncodedText.Encode("original");
	private static readonly System.Text.Json.JsonEncodedText MemberName = System.Text.Json.JsonEncodedText.Encode("name");
	private static readonly System.Text.Json.JsonEncodedText MemberDevice = System.Text.Json.JsonEncodedText.Encode("device");

	public override UserAgentProperty Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		if (reader.ValueTextEquals(MemberVersion))
		{
			return UserAgentProperty.Version;
		}

		if (reader.ValueTextEquals(MemberOs))
		{
			return UserAgentProperty.Os;
		}

		if (reader.ValueTextEquals(MemberOriginal))
		{
			return UserAgentProperty.Original;
		}

		if (reader.ValueTextEquals(MemberName))
		{
			return UserAgentProperty.Name;
		}

		if (reader.ValueTextEquals(MemberDevice))
		{
			return UserAgentProperty.Device;
		}

		var value = reader.GetString()!;
		if (string.Equals(value, MemberVersion.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return UserAgentProperty.Version;
		}

		if (string.Equals(value, MemberOs.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return UserAgentProperty.Os;
		}

		if (string.Equals(value, MemberOriginal.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return UserAgentProperty.Original;
		}

		if (string.Equals(value, MemberName.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return UserAgentProperty.Name;
		}

		if (string.Equals(value, MemberDevice.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return UserAgentProperty.Device;
		}

		throw new System.Text.Json.JsonException($"Unknown member '{value}' for enum '{nameof(UserAgentProperty)}'.");
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, UserAgentProperty value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value)
		{
			case UserAgentProperty.Version:
				writer.WriteStringValue(MemberVersion);
				break;
			case UserAgentProperty.Os:
				writer.WriteStringValue(MemberOs);
				break;
			case UserAgentProperty.Original:
				writer.WriteStringValue(MemberOriginal);
				break;
			case UserAgentProperty.Name:
				writer.WriteStringValue(MemberName);
				break;
			case UserAgentProperty.Device:
				writer.WriteStringValue(MemberDevice);
				break;
			default:
				throw new System.Text.Json.JsonException($"Invalid value '{value}' for enum '{nameof(UserAgentProperty)}'.");
		}
	}

	public override UserAgentProperty ReadAsPropertyName(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return Read(ref reader, typeToConvert, options);
	}

	public override void WriteAsPropertyName(System.Text.Json.Utf8JsonWriter writer, UserAgentProperty value, System.Text.Json.JsonSerializerOptions options)
	{
		Write(writer, value, options);
	}
}