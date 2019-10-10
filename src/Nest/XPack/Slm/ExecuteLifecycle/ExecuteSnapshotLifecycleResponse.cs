using System.Runtime.Serialization;

namespace Nest
{
	public class ExecuteSnapshotLifecycleResponse : ResponseBase
	{
		/// <summary>
		/// The generated snapshot name
		/// </summary>
		[DataMember(Name = "snapshot_name")]
		public string SnapshotName { get; internal set; }
	}
}
