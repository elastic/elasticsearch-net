namespace Nest
{
	public interface IDeleteRepositoryResponse : IAcknowledgedResponse { }

	public class DeleteRepositoryResponse : AcknowledgedResponseBase, IDeleteRepositoryResponse { }
}
