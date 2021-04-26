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

using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class CloseIndexResponse : AcknowledgedResponseBase
	{
		/// <summary>
		/// Individual index responses
		/// <para />
		/// Valid only for Elasticsearch 7.3.0+
		/// </summary>
		[DataMember(Name = "indices")]
		public IReadOnlyDictionary<string, CloseIndexResult> Indices { get; internal set; } = EmptyReadOnly<string, CloseIndexResult>.Dictionary;

		/// <summary>
		/// Acknowledgement from shards
		/// <para />
		/// Valid only for Elasticsearch 7.2.0+
		/// </summary>
		[DataMember(Name = "shards_acknowledged")]
		public bool ShardsAcknowledged { get; internal set; }
	}

	[DataContract]
	public class CloseIndexResult
	{
		[DataMember(Name = "closed")]
		public bool Closed { get; internal set; }

		[DataMember(Name = "shards")]
		public IReadOnlyDictionary<string, CloseShardResult> Shards { get; internal set; } = EmptyReadOnly<string, CloseShardResult>.Dictionary;
	}

	[DataContract]
	public class CloseShardResult
	{
		[DataMember(Name = "failures")]
		public IReadOnlyCollection<ShardFailure> Failures { get; internal set; } = EmptyReadOnly<ShardFailure>.Collection;
	}
}
