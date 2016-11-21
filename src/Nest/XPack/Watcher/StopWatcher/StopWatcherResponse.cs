using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface IStopWatcherResponse : IAcknowledgedResponse {}

	public class StopWatcherResponse : AcknowledgedResponseBase, IStopWatcherResponse {}
}
