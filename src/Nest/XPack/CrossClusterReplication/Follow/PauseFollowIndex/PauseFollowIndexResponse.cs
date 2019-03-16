using System.Collections.Generic;

namespace Nest
{
	public interface IPauseFollowIndexResponse : IAcknowledgedResponse { }

	public class PauseFollowIndexResponse : AcknowledgedResponseBase, IPauseFollowIndexResponse { }
}
