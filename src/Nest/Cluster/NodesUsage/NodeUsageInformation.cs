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

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

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
