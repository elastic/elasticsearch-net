using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	public interface IStartWatcherResponse : IAcknowledgedResponse {}

	public class StartWatcherResponse : AcknowledgedResponseBase, IStartWatcherResponse {}
}
