namespace Nest
{
	public interface IBulkAliasResponse : IAcknowledgedResponse { }

	public class BulkAliasResponse : AcknowledgedResponseBase, IBulkAliasResponse { }
}
