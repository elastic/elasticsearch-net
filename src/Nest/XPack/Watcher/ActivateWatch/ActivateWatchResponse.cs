using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IActivateWatchResponse : IResponse
	{
		ActivationStatus Status { get; }
	}

	public class ActivateWatchResponse : ResponseBase, IActivateWatchResponse
	{
		[JsonProperty("_status")]
		public ActivationStatus Status { get; internal set; }
	}

	[JsonObject]
	public class ActivationStatus
	{
		[JsonProperty("state")]
		public ActivationState State { get; internal set; }

		[JsonProperty("actions")]
		public Dictionary<string, ActionStatus> Actions { get; set; }
	}
}
