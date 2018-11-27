using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class NodeUsageInformation
	{
		[DataMember(Name ="rest_actions")]
		public IReadOnlyDictionary<string, int> RestActions { get; internal set; }

		[DataMember(Name ="since")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset Since { get; internal set; }

		[DataMember(Name ="timestamp")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset Timestamp { get; internal set; }
	}
}
