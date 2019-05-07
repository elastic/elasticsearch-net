namespace Nest
{
	public interface IStopIlmResponse : IAcknowledgedResponse { }

	public class StopIlmResponse : AcknowledgedResponseBase, IStopIlmResponse { }
}
