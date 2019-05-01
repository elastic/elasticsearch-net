using Newtonsoft.Json;

namespace Nest
{
	public interface IIlmMoveToStepResponse : IAcknowledgedResponse
	{
	}

	public class IlmMoveToStepResponse : AcknowledgedResponseBase, IIlmMoveToStepResponse
	{
	}
}
