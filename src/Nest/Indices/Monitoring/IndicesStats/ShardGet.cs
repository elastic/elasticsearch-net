// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

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
