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
	public class RolloverIndexResponse : AcknowledgedResponseBase
	{
		[DataMember(Name = "conditions")]
		public IReadOnlyDictionary<string, bool> Conditions { get; internal set; } = EmptyReadOnly<string, bool>.Dictionary;

		[DataMember(Name = "dry_run")]
		public bool DryRun { get; internal set; }

		[DataMember(Name = "new_index")]
		public string NewIndex { get; internal set; }

		[DataMember(Name = "old_index")]
		public string OldIndex { get; internal set; }

		[DataMember(Name = "rolled_over")]
		public bool RolledOver { get; internal set; }

		[DataMember(Name = "shards_acknowledged")]
		public bool ShardsAcknowledged { get; internal set; }
	}
}
