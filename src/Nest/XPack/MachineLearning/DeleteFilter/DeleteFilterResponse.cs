namespace Nest
{
	public interface IDeleteFilterResponse : IAcknowledgedResponse { }

	public class DeleteFilterResponse : AcknowledgedResponseBase, IDeleteFilterResponse { }
}
