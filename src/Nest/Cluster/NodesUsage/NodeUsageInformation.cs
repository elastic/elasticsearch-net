using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	public class NodeUsageInformation
	{
		[DataMember(Name ="rest_actions")]
		public IReadOnlyDictionary<string, int> RestActions { get; internal set; }

		[DataMember(Name ="since")]
		[JsonFormatter(typeof(EpochMillisecondsDateTimeOffsetFormatter))]
		public DateTimeOffset Since { get; internal set; }

		[DataMember(Name ="timestamp")]
		[JsonFormatter(typeof(EpochMillisecondsDateTimeOffsetFormatter))]
		public DateTimeOffset Timestamp { get; internal set; }
	}
}
