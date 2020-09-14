// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;

namespace Elasticsearch.Net.Extensions
{
	internal static class DateTimeUtil
	{
		public static readonly DateTimeOffset UnixEpoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);
	}
}
