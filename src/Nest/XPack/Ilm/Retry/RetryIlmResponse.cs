namespace Nest
{
	public interface IRetryIlmResponse : IAcknowledgedResponse { }

	public class RetryIlmResponse : AcknowledgedResponseBase, IRetryIlmResponse { }
}
