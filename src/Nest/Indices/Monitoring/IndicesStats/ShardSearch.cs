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
	public class ShardSearch
	{
		[DataMember(Name ="fetch_current")]
		public long FetchCurrent { get; internal set; }

		[DataMember(Name ="fetch_time_in_millis")]
		public long FetchTimeInMilliseconds { get; internal set; }

		[DataMember(Name ="fetch_total")]
		public long FetchTotal { get; internal set; }

		[DataMember(Name ="open_contexts")]
		public long OpenContexts { get; internal set; }

		[DataMember(Name ="query_current")]
		public long QueryCurrent { get; internal set; }

		[DataMember(Name ="query_time_in_millis")]
		public long QueryTimeInMilliseconds { get; internal set; }

		[DataMember(Name ="query_total")]
		public long QueryTotal { get; internal set; }

		[DataMember(Name ="scroll_current")]
		public long ScrollCurrent { get; internal set; }

		[DataMember(Name ="scroll_time_in_millis")]
		public long ScrollTimeInMilliseconds { get; internal set; }

		[DataMember(Name ="scroll_total")]
		public long ScrollTotal { get; internal set; }

		[DataMember(Name ="suggest_current")]
		public long SuggestCurrent { get; internal set; }

		[DataMember(Name ="suggest_time_in_millis")]
		public long SuggestTimeInMilliseconds { get; internal set; }

		[DataMember(Name ="suggest_total")]
		public long SuggestTotal { get; internal set; }
	}
}
