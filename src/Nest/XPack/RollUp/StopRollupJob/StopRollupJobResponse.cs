using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface IStopRollupJobResponse : IAcknowledgedResponse {}

	public class StopRollupJobResponse : AcknowledgedResponseBase, IStopRollupJobResponse {}
}
