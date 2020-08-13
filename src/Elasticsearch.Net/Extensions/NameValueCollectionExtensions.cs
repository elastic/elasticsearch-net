// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net.Extensions
{
	internal static class NameValueCollectionExtensions
	{
		private const int MaxCharsOnStack = 256; // 512 bytes

		internal static string ToQueryString(this NameValueCollection nv)
		{
			if (nv == null || nv.AllKeys.Length == 0) return string.Empty;

			var maxLength = 1 + nv.AllKeys.Length - 1; // account for '?', and any required '&' chars
			foreach (var key in nv.AllKeys)
			{
				var bytes = Encoding.UTF8.GetByteCount(key) + Encoding.UTF8.GetByteCount(nv[key] ?? string.Empty);
				var maxEncodedSize = bytes * 3; // worst case, assume all bytes are URL escaped to 3 chars
				maxLength += 1 + maxEncodedSize; // '=' + encoded chars
			}

			// prefer stack allocated array for short lengths
			// note: renting for larger lengths is slightly more efficient since no zeroing occurs
			char[] rentedFromPool = null;
			var buffer = maxLength > MaxCharsOnStack
				? rentedFromPool = ArrayPool<char>.Shared.Rent(maxLength)
				: stackalloc char[maxLength];

			try
			{
				var position = 0;
				buffer[position++] = '?';

				foreach (var key in nv.AllKeys)
				{
					if (position != 1)
						buffer[position++] = '&';

					var escapedKey = Uri.EscapeDataString(key);
					escapedKey.AsSpan().CopyTo(buffer.Slice(position));
					position += escapedKey.Length;

					var value = nv[key];

					if (value.IsNullOrEmpty()) continue;

					buffer[position++] = '=';
					var escapedValue = Uri.EscapeDataString(value);
					escapedValue.AsSpan().CopyTo(buffer.Slice(position));
					position += escapedValue.Length;
				}

				return buffer.Slice(0, position).ToString();
			}
			finally
			{
				if (rentedFromPool is object)
					ArrayPool<char>.Shared.Return(rentedFromPool, clearArray: false);
			}
		}

		internal static void UpdateFromDictionary(this NameValueCollection queryString, Dictionary<string, object> queryStringUpdates,
			ElasticsearchUrlFormatter provider
		)
		{
			if (queryString == null || queryString.Count < 0) return;
			if (queryStringUpdates == null || queryStringUpdates.Count < 0) return;

			foreach (var kv in queryStringUpdates.Where(kv => !kv.Key.IsNullOrEmpty()))
			{
				if (kv.Value == null)
				{
					queryString.Remove(kv.Key);
					continue;
				}
				var resolved = provider.CreateString(kv.Value);
				if (resolved != null)
					queryString[kv.Key] = resolved;
				else
					queryString.Remove(kv.Key);
			}
		}
	}
}
