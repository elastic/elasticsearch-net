namespace Nest
{
	public interface IIlmStartResponse : IAcknowledgedResponse
	{
	}

	public class IlmStartResponse : AcknowledgedResponseBase, IIlmStartResponse
	{
	}
}
