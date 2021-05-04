// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	public class NodeUsageInformation
	{
		/// <summary>
		/// Aggregation usage.
		/// <para />
		/// Available in Elasticsearch 7.8.0+
		/// </summary>
		[DataMember(Name ="aggregations")]
		public IReadOnlyDictionary<string, IReadOnlyDictionary<string, long>> Aggregations { get; internal set; }

		[DataMember(Name ="rest_actions")]
		public IReadOnlyDictionary<string, int> RestActions { get; internal set; }

		[DataMember(Name ="since")]
		[JsonFormatter(typeof(DateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset Since { get; internal set; }

		[DataMember(Name ="timestamp")]
		[JsonFormatter(typeof(DateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset Timestamp { get; internal set; }
	}
}
