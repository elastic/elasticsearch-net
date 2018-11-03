namespace Nest
{
	public interface IForceMergeResponse : IShardsOperationResponse { }

	public class ForceMergeResponse : ShardsOperationResponseBase, IForceMergeResponse { }
}
