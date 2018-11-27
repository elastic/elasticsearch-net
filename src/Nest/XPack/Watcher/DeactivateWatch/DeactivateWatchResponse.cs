using System.Runtime.Serialization;

namespace Nest
{
	public interface IDeactivateWatchResponse : IResponse
	{
		[DataMember(Name ="status")]
		ActivationStatus Status { get; }
	}

	public class DeactivateWatchResponse : ResponseBase, IDeactivateWatchResponse
	{
		[DataMember(Name ="status")]
		public ActivationStatus Status { get; internal set; }
	}
}
