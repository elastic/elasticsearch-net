using System.Runtime.Serialization;

namespace Nest
{
	public class DeactivateWatchResponse : ResponseBase
	{
		[DataMember(Name ="status")]
		public ActivationStatus Status { get; internal set; }
	}
}
