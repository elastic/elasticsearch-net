using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class ActivateWatchResponse : ResponseBase
	{
		[DataMember(Name ="status")]
		public ActivationStatus Status { get; internal set; }
	}

	[DataContract]
	public class ActivationStatus
	{
		[DataMember(Name ="actions")]
		public IReadOnlyDictionary<string, ActionStatus> Actions { get; set; }

		[DataMember(Name ="state")]
		public ActivationState State { get; internal set; }
	}
}
