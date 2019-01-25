namespace Nest
{
	public interface IUnfollowIndexResponse : IAcknowledgedResponse { }

	public class UnfollowIndexResponse : AcknowledgedResponseBase, IUnfollowIndexResponse { }
}
