using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	public interface IRestartWatcherResponse : IAcknowledgedResponse {}

	public class RestartWatcherResponse : AcknowledgedResponseBase, IRestartWatcherResponse {}
}
