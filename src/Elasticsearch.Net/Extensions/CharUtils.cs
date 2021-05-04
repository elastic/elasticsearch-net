// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Elasticsearch.Net.Extensions
{
	internal static class CharUtils
	{
		// https://referencesource.microsoft.com/#mscorlib/system/security/util/hex.cs,1bfe838f662feef3
		// converts number to hex digit. Does not do any range checks.
		internal static char HexDigit(int num) => (char)(num < 10 ? num + 48 : num + 55);
	}
}
