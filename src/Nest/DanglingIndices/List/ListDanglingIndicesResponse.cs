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
using Elasticsearch.Net;

namespace Nest
{
	[DataContract]
	public class ListDanglingIndicesResponse : ResponseBase
	{
		[DataMember(Name = "dangling_indices")]
		public IReadOnlyCollection<AggregatedDanglingIndexInfo> DanglingIndices { get; internal set; } =
			EmptyReadOnly<AggregatedDanglingIndexInfo>.Collection;
	}

	public class AggregatedDanglingIndexInfo
	{
		private DateTimeOffset? _creationDate;

		[DataMember(Name = "index_name")]
		public string IndexName { get; internal set; }

		[DataMember(Name = "index_uuid")]
		public string IndexUUID { get; internal set; }

		[DataMember(Name = "creation_date_millis")]
		public long CreationDateInMilliseconds { get; internal set; }

		[DataMember(Name = "creation_date")]
		public DateTimeOffset CreationDate
		{
			get
			{
				_creationDate ??= DateTimeOffset.FromUnixTimeMilliseconds(CreationDateInMilliseconds);
				return _creationDate.Value;
			}
			internal set => _creationDate = value;
		}

		[DataMember(Name = "node_ids")]
		public IReadOnlyCollection<string> NodeIds { get; internal set; }
	}
}
