using System.Runtime.Serialization;

namespace Nest
{
	public class GetSnapshotLifecycleManagementStatusResponse : ResponseBase
	{
		[DataMember(Name = "operation_mode")]
		public LifecycleOperationMode OperationMode { get; internal set; }
	}
}
