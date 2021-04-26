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
	[DataContract]
	public class ClusterPendingTasksResponse : ResponseBase
	{
		[DataMember(Name ="tasks")]
		public IReadOnlyCollection<PendingTask> Tasks { get; internal set; } = EmptyReadOnly<PendingTask>.Collection;
	}

	[DataContract]
	public class PendingTask
	{
		[DataMember(Name ="insert_order")]
		public int InsertOrder { get; internal set; }

		[DataMember(Name ="priority")]
		public string Priority { get; internal set; }

		[DataMember(Name ="source")]
		public string Source { get; internal set; }

		[DataMember(Name ="time_in_queue")]
		public string TimeInQueue { get; internal set; }

		[DataMember(Name ="time_in_queue_millis")]
		public int TimeInQueueMilliseconds { get; internal set; }
	}
}
