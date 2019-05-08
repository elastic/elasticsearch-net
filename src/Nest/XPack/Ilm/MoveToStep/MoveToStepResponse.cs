namespace Nest
{
	public interface IMoveToStepResponse : IAcknowledgedResponse { }

	public class MoveToStepResponse : AcknowledgedResponseBase, IMoveToStepResponse { }
}
