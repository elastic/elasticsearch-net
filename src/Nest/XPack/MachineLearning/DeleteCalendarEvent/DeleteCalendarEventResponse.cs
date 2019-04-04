namespace Nest
{
	public interface IDeleteCalendarEventResponse : IAcknowledgedResponse { }

	public class DeleteCalendarEventResponse : AcknowledgedResponseBase, IDeleteCalendarEventResponse { }
}
