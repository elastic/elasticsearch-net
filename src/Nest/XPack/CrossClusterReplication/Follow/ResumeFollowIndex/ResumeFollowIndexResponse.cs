using System.Collections.Generic;

namespace Nest
{
	public interface IResumeFollowIndexResponse : IAcknowledgedResponse { }

	public class ResumeFollowIndexResponse : AcknowledgedResponseBase, IResumeFollowIndexResponse { }
}
