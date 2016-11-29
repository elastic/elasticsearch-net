using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface IStartWatcherResponse : IAcknowledgedResponse {}

	public class StartWatcherResponse : AcknowledgedResponseBase, IStartWatcherResponse {}
}
