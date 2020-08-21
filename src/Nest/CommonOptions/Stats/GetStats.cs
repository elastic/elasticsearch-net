// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class GetStats
	{
		[DataMember(Name ="current")]
		public long Current { get; set; }

		[DataMember(Name ="exists_time")]
		public string ExistsTime { get; set; }

		[DataMember(Name ="exists_time_in_millis")]
		public long ExistsTimeInMilliseconds { get; set; }

		[DataMember(Name ="exists_total")]
		public long ExistsTotal { get; set; }

		[DataMember(Name ="missing_time")]
		public string MissingTime { get; set; }

		[DataMember(Name ="missing_time_in_millis")]
		public long MissingTimeInMilliseconds { get; set; }

		[DataMember(Name ="missing_total")]
		public long MissingTotal { get; set; }

		[DataMember(Name ="time")]
		public string Time { get; set; }

		[DataMember(Name ="time_in_millis")]
		public long TimeInMilliseconds { get; set; }

		[DataMember(Name ="total")]
		public long Total { get; set; }
	}
}
