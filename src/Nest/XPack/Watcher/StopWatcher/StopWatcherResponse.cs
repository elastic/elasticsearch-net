using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	public interface IStopWatcherResponse : IAcknowledgedResponse {}

	public class StopWatcherResponse : AcknowledgedResponseBase, IStopWatcherResponse {}
}
