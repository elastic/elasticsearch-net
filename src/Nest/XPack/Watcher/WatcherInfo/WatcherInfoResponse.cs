using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface IWatcherInfoResponse : IResponse
	{
		[JsonProperty("version")]
		WatcherVersion Version { get; }

		[JsonProperty("tagline")]
		string Tagline { get; }
	}

	public class WatcherInfoResponse : ResponseBase, IWatcherInfoResponse
	{
		public WatcherVersion Version { get; internal set; }

		public string Tagline { get; internal set; }
	}

	[JsonObject]
	public class WatcherVersion
	{
		[JsonProperty("number")]
		public string Number { get; internal set; }

		[JsonProperty("build_hash")]
		public string BuildHash { get; internal set; }

		[JsonProperty("build_timestamp")]
		public DateTimeOffset BuildTimestamp { get; internal set; }

		[JsonProperty("build_snapshot")]
		public bool BuildSnapshot { get; internal set; }
	}
}
