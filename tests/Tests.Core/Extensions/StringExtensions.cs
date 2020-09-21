// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Text;

namespace Tests.Core.Extensions
{
	public static class StringExtensions
	{
		public static string Utf8String(this byte[] bytes) => bytes == null ? null : Encoding.UTF8.GetString(bytes, 0, bytes.Length);

		public static byte[] Utf8Bytes(this string s) => !string.IsNullOrWhiteSpace(s) ? null : Encoding.UTF8.GetBytes(s);
	}

}
