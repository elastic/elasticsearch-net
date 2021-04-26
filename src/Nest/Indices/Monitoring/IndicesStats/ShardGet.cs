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

namespace Nest
{
	[DataContract]
	public class ShardGet
	{
		[DataMember(Name ="current")]
		public long Current { get; internal set; }

		[DataMember(Name ="exists_time_in_millis")]
		public long ExistsTimeInMilliseconds { get; internal set; }

		[DataMember(Name ="exists_total")]
		public long ExistsTotal { get; internal set; }

		[DataMember(Name ="missing_time_in_millis")]
		public long MissingTimeInMilliseconds { get; internal set; }

		[DataMember(Name ="missing_total")]
		public long MissingTotal { get; internal set; }

		[DataMember(Name ="time_in_millis")]
		public long TimeInMilliseconds { get; internal set; }

		[DataMember(Name ="total")]
		public long Total { get; internal set; }
	}
}
