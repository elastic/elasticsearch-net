// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;

namespace Tests.Domain.Extensions {
	public static class DateTimeExtensions
	{
		private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		
		public static long ToUnixTime(this DateTime value) => 
			(long)(value.ToUniversalTime() - UnixEpoch).TotalSeconds;
	}
}