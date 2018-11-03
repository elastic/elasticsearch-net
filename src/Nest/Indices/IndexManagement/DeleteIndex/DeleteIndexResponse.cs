namespace Nest
{
	public interface IDeleteIndexResponse : IIndicesResponse { }

	public class DeleteIndexResponse : IndicesResponseBase, IDeleteIndexResponse { }
}
