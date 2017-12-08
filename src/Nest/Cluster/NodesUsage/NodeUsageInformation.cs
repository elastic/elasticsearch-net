using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class NodeUsageInformation
	{
		[JsonProperty("timestamp")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset Timestamp { get; internal set; }

		[JsonProperty("since")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset Since { get;  internal set; }

		[JsonProperty("rest_actions")]
		public IReadOnlyDictionary<string, int> RestActions { get;  internal set; }
	}
}