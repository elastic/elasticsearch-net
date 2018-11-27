using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IActivateWatchResponse : IResponse
	{
		[DataMember(Name ="status")]
		ActivationStatus Status { get; }
	}

	public class ActivateWatchResponse : ResponseBase, IActivateWatchResponse
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
