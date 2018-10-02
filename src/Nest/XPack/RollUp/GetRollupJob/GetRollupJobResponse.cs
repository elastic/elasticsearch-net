using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface IGetRollupJobResponse : IAcknowledgedResponse {}

	public class GetRollupJobResponse : AcknowledgedResponseBase, IGetRollupJobResponse {}
}
