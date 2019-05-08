namespace Nest
{
	public interface IDeleteLifecycleResponse : IAcknowledgedResponse
	{
	}

	public class DeleteLifecycleResponse : AcknowledgedResponseBase, IDeleteLifecycleResponse
	{
	}
}
