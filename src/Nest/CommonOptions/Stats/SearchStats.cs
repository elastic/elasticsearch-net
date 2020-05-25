// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class SearchStats
	{
		[DataMember(Name ="fetch_current")]
		public long FetchCurrent { get; set; }

		[DataMember(Name ="fetch_time_in_millis")]
		public long FetchTimeInMilliseconds { get; set; }

		[DataMember(Name ="fetch_total")]
		public long FetchTotal { get; set; }

		[DataMember(Name ="open_contexts")]
		public long OpenContexts { get; set; }

		[DataMember(Name ="query_current")]
		public long QueryCurrent { get; set; }

		[DataMember(Name ="query_time_in_millis")]
		public long QueryTimeInMilliseconds { get; set; }

		[DataMember(Name ="query_total")]
		public long QueryTotal { get; set; }

		[DataMember(Name ="scroll_current")]
		public long ScrollCurrent { get; set; }

		[DataMember(Name ="scroll_time_in_millis")]
		public long ScrollTimeInMilliseconds { get; set; }

		[DataMember(Name ="scroll_total")]
		public long ScrollTotal { get; set; }

		[DataMember(Name ="suggest_current")]
		public long SuggestCurrent { get; set; }

		[DataMember(Name ="suggest_time_in_millis")]
		public long SuggestTimeInMilliseconds { get; set; }

		[DataMember(Name ="suggest_total")]
		public long SuggestTotal { get; set; }
	}
}
