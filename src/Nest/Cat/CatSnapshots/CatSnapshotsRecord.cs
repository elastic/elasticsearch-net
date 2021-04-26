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

using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[DataContract]
	public class CatSnapshotsRecord : ICatRecord
	{
		[DataMember(Name ="duration")]
		public Time Duration { get; set; }

		[DataMember(Name ="end_epoch")]
		[JsonFormatter(typeof(StringLongFormatter))]
		public long EndEpoch { get; set; }

		[DataMember(Name ="end_time")]
		public string EndTime { get; set; }

		[DataMember(Name ="failed_shards")]
		[JsonFormatter(typeof(StringLongFormatter))]
		public long FailedShards { get; set; }

		// duration indices successful_shards failed_shards total_shards
		[DataMember(Name ="id")]
		public string Id { get; set; }

		[DataMember(Name ="indices")]
		[JsonFormatter(typeof(StringLongFormatter))]
		public long Indices { get; set; }

		[DataMember(Name ="start_epoch")]
		[JsonFormatter(typeof(StringLongFormatter))]
		public long StartEpoch { get; set; }

		[DataMember(Name ="start_time")]
		public string StartTime { get; set; }

		[DataMember(Name ="status")]
		public string Status { get; set; }

		[DataMember(Name ="successful_shards")]
		[JsonFormatter(typeof(StringLongFormatter))]
		public long SuccessfulShards { get; set; }

		[DataMember(Name ="total_shards")]
		[JsonFormatter(typeof(StringLongFormatter))]
		public long TotalShards { get; set; }
	}
}
