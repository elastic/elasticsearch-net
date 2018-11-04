namespace Nest
{
	public interface ICreateRepositoryResponse : IAcknowledgedResponse { }

	public class CreateRepositoryResponse : AcknowledgedResponseBase, ICreateRepositoryResponse { }
}
