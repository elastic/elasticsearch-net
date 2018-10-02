using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface IDeleteRollupJobResponse : IAcknowledgedResponse {}

	public class DeleteRollupJobResponse : AcknowledgedResponseBase, IDeleteJobResponse {}
}
