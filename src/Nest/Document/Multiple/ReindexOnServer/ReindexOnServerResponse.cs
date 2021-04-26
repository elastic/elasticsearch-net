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

namespace Nest
{
	public class ReindexOnServerResponse : ResponseBase
	{
		public override bool IsValid => base.IsValid && !Failures.HasAny();

		[DataMember(Name ="batches")]
		public long Batches { get; internal set; }

		[DataMember(Name ="created")]
		public long Created { get; internal set; }

		[DataMember(Name ="failures")]
		public IReadOnlyCollection<BulkIndexByScrollFailure> Failures { get; internal set; }
			= EmptyReadOnly<BulkIndexByScrollFailure>.Collection;

		[DataMember(Name ="noops")]
		public long Noops { get; internal set; }

		[DataMember(Name ="retries")]
		public Retries Retries { get; internal set; }

		[DataMember(Name ="slice_id")]
		public int? SliceId { get; internal set; }

		/// <summary>
		/// Only has a value if WaitForCompletion is set to false on the request
		/// </summary>
		[DataMember(Name ="task")]
		public TaskId Task { get; internal set; }

		[DataMember(Name ="timed_out")]
		public bool TimedOut { get; internal set; }

		//https://github.com/elastic/elasticsearch/commit/11f90bffda50f0acc8dc1409f3f33005e1249234
		// 2.3 released this writing the time value's to string e.g 123.4ms instead of long as all the others took responses
		[DataMember(Name ="took")]
		public Time Took { get; internal set; }

		[DataMember(Name ="total")]
		public long Total { get; internal set; }

		[DataMember(Name ="updated")]
		public long Updated { get; internal set; }

		[DataMember(Name ="version_conflicts")]
		public long VersionConflicts { get; internal set; }
	}
}
