namespace Nest
{
	public interface IDeleteForecastResponse : IAcknowledgedResponse { }

	public class DeleteForecastResponse : AcknowledgedResponseBase, IDeleteForecastResponse { }
}
