namespace Nest
{
	public interface IDeleteCalendarResponse : IAcknowledgedResponse { }

	public class DeleteCalendarResponse : AcknowledgedResponseBase, IDeleteCalendarResponse { }
}
