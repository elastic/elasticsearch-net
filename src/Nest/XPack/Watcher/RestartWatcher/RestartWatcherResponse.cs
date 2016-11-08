using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface IRestartWatcherResponse : IAcknowledgedResponse {}

	public class RestartWatcherResponse : AcknowledgedResponseBase, IRestartWatcherResponse {}
}
