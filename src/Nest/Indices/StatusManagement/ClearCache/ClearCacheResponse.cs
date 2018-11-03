namespace Nest
{
	public interface IClearCacheResponse : IShardsOperationResponse { }

	public class ClearCacheResponse : ShardsOperationResponseBase, IClearCacheResponse { }
}
