// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless;
#else
namespace Elastic.Clients.Elasticsearch;
#endif

public static class TimeUnitExtensions
{
	public static string GetStringValue(this TimeUnit value)
	{
		switch (value)
		{
			case TimeUnit.Nanoseconds:
				return "nanos";
			case TimeUnit.Microseconds:
				return "micros";
			case TimeUnit.Milliseconds:
				return "ms";
			case TimeUnit.Seconds:
				return "s";
			case TimeUnit.Minutes:
				return "m";
			case TimeUnit.Hours:
				return "h";
			case TimeUnit.Days:
				return "d";
			default:
				throw new ArgumentOutOfRangeException(nameof(value), value, null);
		}
	}
}
