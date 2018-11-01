namespace Nest
{
	public interface IPutMappingResponse : IIndicesResponse { }

	public class PutMappingResponse : IndicesResponseBase, IPutMappingResponse { }
}
