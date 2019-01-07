using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IPauseFollowIndexResponse : IAcknowledgedResponse { }

	public class PauseFollowIndexResponse : AcknowledgedResponseBase, IPauseFollowIndexResponse { }
}
