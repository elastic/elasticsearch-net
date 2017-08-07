namespace Nest
{
	public interface IDeleteJobResponse : IAcknowledgedResponse {}

	public class DeleteJobResponse : AcknowledgedResponseBase, IDeleteJobResponse {}
}
