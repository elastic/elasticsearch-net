namespace Nest
{
	public interface IPutLifecycleResponse : IAcknowledgedResponse { }

	public class PutLifecycleResponse : AcknowledgedResponseBase, IPutLifecycleResponse { }
}
