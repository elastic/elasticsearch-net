namespace Nest
{
	public interface IRefreshResponse : IShardsOperationResponse { }

	public class RefreshResponse : ShardsOperationResponseBase, IRefreshResponse { }
}
