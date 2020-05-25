// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.CompilerServices;
using Elasticsearch.Net.Utf8Json;


namespace Nest
{
	internal static class Utf8JsonReaderExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double? ReadNullableDouble(this ref JsonReader reader)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.Null)
				return reader.ReadDouble();

			reader.ReadNext();
			return null;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long? ReadNullableLong(this ref JsonReader reader)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.Null)
				return reader.ReadInt64();

			reader.ReadNext();
			return null;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool? ReadNullableBoolean(this ref JsonReader reader)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.Null)
				return reader.ReadBoolean();

			reader.ReadNext();
			return null;
		}
	}
}
