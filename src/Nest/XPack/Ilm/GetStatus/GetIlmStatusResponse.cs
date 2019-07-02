using System.Runtime.Serialization;

namespace Nest
{
	public class GetIlmStatusResponse : ResponseBase
	{
		[DataMember(Name = "operation_mode")]
		public LifecycleOperationMode OperationMode { get; internal set; }
	}
}
