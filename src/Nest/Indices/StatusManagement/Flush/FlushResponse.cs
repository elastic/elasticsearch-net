namespace Nest
{
	public interface IFlushResponse : IShardsOperationResponse { }

	public class FlushResponse : ShardsOperationResponseBase, IFlushResponse { }
}
