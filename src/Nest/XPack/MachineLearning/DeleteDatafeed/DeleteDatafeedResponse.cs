namespace Nest
{
	public interface IDeleteDatafeedResponse : IAcknowledgedResponse {}

	public class DeleteDatafeedResponse : AcknowledgedResponseBase, IDeleteDatafeedResponse {}
}
