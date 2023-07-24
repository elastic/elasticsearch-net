// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;

#if NETCOREAPP

using System.Buffers;

#endif

namespace Elastic.Clients.Elasticsearch.Serialization;

internal static class JsonConstants
{
	public static ReadOnlySpan<byte> LiteralNaN => "NaN"u8;
	public static ReadOnlySpan<byte> LiteralPositiveInfinity => "Infinity"u8;
	public static ReadOnlySpan<byte> LiteralNegativeInfinity => "-Infinity"u8;
	public static JsonEncodedText EncodedNaN => JsonEncodedText.Encode(LiteralNaN);
	public static JsonEncodedText EncodedPositiveInfinity => JsonEncodedText.Encode(LiteralPositiveInfinity);
	public static JsonEncodedText EncodedNegativeInfinity => JsonEncodedText.Encode(LiteralNegativeInfinity);

#if NETCOREAPP
	public static ReadOnlySpan<byte> NonIntegerChars => "E."u8;
#else
	public static char[] NonIntegerChars => new[] { 'E', '.' };
#endif

	public const string DoubleFormatString = "G17"; // 'R' does not roundtrip correctly in some cases prior to .NET Core 3
	public const string SingleFormatString = "G9";  // https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings#round-trip-format-specifier-r

#if NETCOREAPP
	public static readonly StandardFormat DoubleStandardFormat = StandardFormat.Parse(DoubleFormatString);
	public static readonly StandardFormat SingleStandardFormat = StandardFormat.Parse(SingleFormatString);

	public const int MaximumFormatDoubleLength = 128; // https://github.com/dotnet/runtime/blob/main/src/libraries/System.Text.Json/src/System/Text/Json/JsonConstants.cs#L78
	public const int MaximumFormatSingleLength = 128; // https://github.com/dotnet/runtime/blob/main/src/libraries/System.Text.Json/src/System/Text/Json/JsonConstants.cs#L79
#endif
}
