using Newtonsoft.Json;

namespace Nest
{
	public interface IRevertModelSnapshotResponse : IAcknowledgedResponse
	{
		[JsonProperty("model")]
		ModelSnapshot Model { get; }
	}

	public class RevertModelSnapshotResponse : AcknowledgedResponseBase, IRevertModelSnapshotResponse
	{
		public ModelSnapshot Model { get; internal set; }
	}
}
