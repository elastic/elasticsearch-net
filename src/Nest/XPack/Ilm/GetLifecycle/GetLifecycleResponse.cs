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
using Nest.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(DictionaryResponseFormatter<GetLifecycleResponse, string, LifecyclePolicy>))]
	public class GetLifecycleResponse : DictionaryResponseBase<string, LifecyclePolicy>
	{
		public IReadOnlyDictionary<string, LifecyclePolicy> Policies => Self.BackingDictionary;
	}

	public class LifecyclePolicy
	{
		[DataMember(Name = "modified_date")]
		[JsonFormatter(typeof(DateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset ModifiedDate { get; internal set; }

		[DataMember(Name = "policy")]
		public Policy Policy { get; internal set; }

		[DataMember(Name = "version")]
		public int Version { get; internal set; }
	}
}
