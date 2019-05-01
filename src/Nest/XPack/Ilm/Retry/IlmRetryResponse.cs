namespace Nest
{
	public interface IIlmRetryResponse : IAcknowledgedResponse
	{
	}

	public class IlmRetryResponse : AcknowledgedResponseBase, IIlmRetryResponse
	{
	}
}
