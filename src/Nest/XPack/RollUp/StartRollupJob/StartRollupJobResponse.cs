using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface IStartRollupJobResponse : IAcknowledgedResponse {}

	public class StartRollupJobResponse : AcknowledgedResponseBase, IStartRollupJobResponse {}
}
