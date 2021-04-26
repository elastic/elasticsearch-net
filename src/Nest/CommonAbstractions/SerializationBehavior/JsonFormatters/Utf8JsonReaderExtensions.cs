/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Runtime.CompilerServices;
using Nest.Utf8Json;


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
