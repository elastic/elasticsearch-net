using Newtonsoft.Json;

namespace Nest
{
	public interface IUpdateModelSnapshotResponse : IAcknowledgedResponse
	{
		[JsonProperty("model")]
		ModelSnapshot Model { get; }
	}

	public class UpdateModelSnapshotResponse : AcknowledgedResponseBase, IUpdateModelSnapshotResponse
	{
		public ModelSnapshot Model { get; internal set; }
	}
}
