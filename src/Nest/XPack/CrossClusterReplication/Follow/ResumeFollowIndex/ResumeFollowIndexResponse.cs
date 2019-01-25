using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IResumeFollowIndexResponse : IAcknowledgedResponse { }

	public class ResumeFollowIndexResponse : AcknowledgedResponseBase, IResumeFollowIndexResponse { }
}
