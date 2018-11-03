using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface ICreateRollupJobResponse : IAcknowledgedResponse { }

	public class CreateRollupJobResponse : AcknowledgedResponseBase, ICreateRollupJobResponse { }
}
